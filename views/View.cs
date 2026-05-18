using crud_in_terminal_csharp.models;
using crud_in_terminal_csharp.templates;
using System;
using System.Collections.Generic;

namespace crud_in_terminal_csharp.views {
    internal class View {

        // ==================== ADMIN ====================

        public static void CriarAdmin() {
            foreach (var obj in View.ClienteListar()) {
                if (obj.Email == "admin") {
                    return;
                }
            }
            View.ClienteInserir("admin", "admin", "admin", "admin");
        }

        // ==================== CLIENTE ====================

        public static Cliente CriarNovoCliente(string nome, string email, string fone, string senha) {
            Cliente c = new Cliente(nome, email, fone, senha);
            ClienteDAO.Inserir(c);
            ClienteDAO.Salvar();
            return c;
        }

        public static void ClienteInserir(string nome, string email, string fone, string senha) {
            Cliente c = new Cliente(nome, email, fone, senha);
            ClienteDAO.Inserir(c);
            ClienteDAO.Salvar();
        }

        public static List<Cliente> ClienteListar() {
            return ClienteDAO.Listar();
        }

        public static void ClienteAtualizar(int id, string nome, string email, string fone, string senha) {
            Cliente? cliente = ClienteDAO.Listar_Id(id);
            if (cliente != null) {
                cliente.Nome = nome;
                cliente.Email = email;
                cliente.Fone = fone;
                cliente.Senha = senha;
                ClienteDAO.Atualizar(cliente);
            }
        }

        public static void ClienteExcluir(int id) {
            Cliente? cliente = ClienteDAO.Listar_Id(id);
            if (cliente != null) {
                ClienteDAO.Excluir(cliente);
            }
        }

        // ==================== CATEGORIA ====================

        public static void CategoriaInserir(string descricao) {
            Categoria c = new Categoria(descricao);
            CategoriaDAO.Inserir(c);
            CategoriaDAO.Salvar();
        }

        public static List<Categoria> CategoriaListar() {
            return CategoriaDAO.Listar();
        }

        public static void CategoriaAtualizar(int id, string descricao) {
            Categoria? categoria = CategoriaDAO.Listar_Id(id);
            if (categoria != null) {
                categoria.Descricao = descricao;
                CategoriaDAO.Atualizar(categoria);
            }
        }

        public static void CategoriaExcluir(int id) {
            Categoria? categoria = CategoriaDAO.Listar_Id(id);
            if (categoria != null) {
                CategoriaDAO.Excluir(categoria);
            }
        }

        // ==================== PRODUTO ====================

        public static void ProdutoInserir(string descricao, double preco, int estoque, int idCategoria) {
            Produto p = new Produto(descricao, preco, estoque, idCategoria);
            ProdutoDAO.Inserir(p);
            ProdutoDAO.Salvar();
        }

        public static List<Produto> ProdutoListar() {
            return ProdutoDAO.Listar();
        }

        public static void ProdutoAtualizar(int id, string descricao, double preco, int estoque, int idCategoria) {
            Produto? produto = ProdutoDAO.Listar_Id(id);
            if (produto != null) {
                produto.Descricao = descricao;
                produto.Preco = preco;
                produto.Estoque = estoque;
                produto.IdCategoria = idCategoria;
                ProdutoDAO.Atualizar(produto);
            }
        }

        public static void ProdutoExcluir(int id) {
            Produto? produto = ProdutoDAO.Listar_Id(id);
            if (produto != null) {
                ProdutoDAO.Excluir(produto);
            }
        }

        // ==================== LOGIN ====================

        public static string? AutenticarLogin(string email, string senha) {
            if (email == "admin" && senha == "admin") {
                return "admin";
            }
            foreach (var obj in ClienteDAO.Listar()) {
                if (obj.Email == email && obj.Senha == senha) {
                    return "cliente";
                }
            }
            return null;
        }

        // ==================== PREÇOS ====================

        public static double AplicarDesconto(int id, double porcentagem) {
            if (porcentagem < 0 || porcentagem > 100) {
                throw new ArgumentOutOfRangeException(nameof(porcentagem), "Porcentagem deve ser entre 0 e 100.");
            }
            var produto = ProdutoDAO.Listar_Id(id);
            if (produto == null) {
                throw new InvalidOperationException($"Produto com ID {id} não encontrado.");
            }
            double novoPreco = produto.Preco - (produto.Preco * (porcentagem / 100));
            produto.Preco = novoPreco;
            ProdutoDAO.Atualizar(produto);
            return novoPreco;
        }

        public static double ReajustarPreco(int id, double porcentagem, int alt) {
            if (porcentagem < 0 || porcentagem > 100) {
                throw new ArgumentOutOfRangeException(nameof(porcentagem), "Porcentagem deve ser entre 0 e 100.");
            }
            if (alt < 0 || alt > 1) {
                throw new ArgumentOutOfRangeException(nameof(alt), "Valor deve ser 0 ou 1.");
            }
            var produto = ProdutoDAO.Listar_Id(id);
            if (produto == null) {
                throw new InvalidOperationException($"Produto com ID {id} não encontrado.");
            }
            double novoPreco = 0;
            if (alt == 0) {
                novoPreco = produto.Preco + (produto.Preco * (porcentagem / 100));
            } else {
                novoPreco = produto.Preco - (produto.Preco * (porcentagem / 100));
            }
            produto.Preco = novoPreco;
            ProdutoDAO.Atualizar(produto);
            return novoPreco;
        }

        // ==================== CARRINHO ====================

        // Busca o carrinho aberto do cliente, ou cria um novo
        public static Venda ObterOuCriarCarrinho(int idCliente) {
            foreach (var venda in VendaDAO.Listar()) {
                if (venda.IdCliente == idCliente && venda.Carrinho == true) {
                    return venda;
                }
            }
            // Não existe carrinho aberto, cria um novo
            Venda novoCarrinho = new Venda(DateTime.Now, true, 0, idCliente);
            VendaDAO.Inserir(novoCarrinho);
            return novoCarrinho;
        }

        // Adiciona produto ao carrinho. Se já existe, soma a quantidade.
        public static string AdicionarAoCarrinho(int idCliente, int idProduto, int quantidade) {
            Produto? produto = ProdutoDAO.Listar_Id(idProduto);
            if (produto == null) {
                return "Produto não encontrado.";
            }
            if (produto.Estoque < quantidade) {
                return $"Estoque insuficiente. Disponível: {produto.Estoque}";
            }

            Venda carrinho = ObterOuCriarCarrinho(idCliente);

            // Verifica se o produto já está no carrinho
            foreach (var item in VendaItemDAO.Listar()) {
                if (item.IdVenda == carrinho.Id && item.IdProduto == idProduto) {
                    // Produto já existe no carrinho, soma a quantidade
                    item.Quantidade += quantidade;
                    VendaItemDAO.Atualizar(item);
                    return "Quantidade atualizada no carrinho.";
                }
            }

            // Produto não está no carrinho ainda, insere novo item
            VendaItem novoItem = new VendaItem(quantidade, produto.Preco, carrinho.Id, idProduto);
            VendaItemDAO.Inserir(novoItem);
            return "Produto adicionado ao carrinho.";
        }

        // Retorna os itens do carrinho aberto do cliente
        public static List<VendaItem> VerCarrinho(int idCliente) {
            Venda carrinho = ObterOuCriarCarrinho(idCliente);
            return VendaItemDAO.ObterPorVenda(carrinho.Id);
        }

        // Finaliza a compra do carrinho
        public static string FinalizarCompra(int idCliente) {
            Venda? carrinho = null;
            foreach (var venda in VendaDAO.Listar()) {
                if (venda.IdCliente == idCliente && venda.Carrinho == true) {
                    carrinho = venda;
                    break;
                }
            }

            if (carrinho == null) {
                return "Seu carrinho está vazio.";
            }

            List<VendaItem> itens = VendaItemDAO.ObterPorVenda(carrinho.Id);
            if (itens.Count == 0) {
                return "Seu carrinho está vazio.";
            }

            // Desconta do estoque e calcula total
            double total = 0;
            foreach (var item in itens) {
                Produto? produto = ProdutoDAO.Listar_Id(item.IdProduto);
                if (produto != null) {
                    produto.Estoque -= item.Quantidade;
                    ProdutoDAO.Atualizar(produto);
                    total += item.Preco * item.Quantidade;
                }
            }

            VendaDAO.FinalizarVenda(carrinho.Id);
            return $"Compra finalizada com sucesso! Total: R$ {total:F2}";
        }

        // Lista todas as compras finalizadas do cliente
        public static List<Venda> ListarMinhasCompras(int idCliente) {
            List<Venda> compras = new List<Venda>();
            foreach (var venda in VendaDAO.Listar()) {
                if (venda.IdCliente == idCliente && venda.Carrinho == false) {
                    compras.Add(venda);
                }
            }
            return compras;
        }

        // Lista todas as vendas finalizadas (admin)
        public static List<Venda> ListarTodasVendas() {
            List<Venda> vendas = new List<Venda>();
            foreach (var venda in VendaDAO.Listar()) {
                if (venda.Carrinho == false) {
                    vendas.Add(venda);
                }
            }
            return vendas;
        }
    }
}