using crud_in_terminal_csharp.models;
using crud_in_terminal_csharp.templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.views {
    internal class View {
        public static void CriarAdmin() {
            foreach (var obj in View.ClienteListar()) {
                if (obj.Email == "admin") {
                    return;
                }
            }
            View.ClienteInserir("admin", "admin", "admin", "admin");
        }

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

        public static void RodarMenuInicial() {
            string opcao = "";
            while (opcao != "3") {
                opcao = UI.MenuBoasVindas();
                if (opcao == "1") {
                    var login = UI.MenuLogin();
                    string? tipoDeUsuario = AutenticarLogin(login.email, login.senha);
                    if (tipoDeUsuario == "admin") RodarMenuAdmin();
                    else if (tipoDeUsuario == "cliente") {
                       
                        Cliente? cli = null;
                        foreach (var c in ClienteDAO.Listar()) {
                            if (c.Email == login.email) { cli = c; break; }
                        }
                        if (cli != null) RodarMenuCliente(cli.Id);
                        else UI.ExibirMensagem("Erro ao localizar cliente.");
                    }
                    else UI.ExibirMensagem("Usuário ou senha inválidos.");
                }
                else if (opcao == "2") {
                    UI.MenuCriarConta();
                    UI.ExibirMensagem("Conta criada com sucesso.");
                }
                else if (opcao == "3") {
                    UI.ExibirMensagem("Saindo...");
                }
                else {
                    UI.ExibirMensagem("Opção inválida.");
                }
            }
        }

       
        private static void RodarMenuCliente(int clienteId) {
            string opcao = "";
            while (opcao != "6") {
                opcao = UI.MenuPrincipalCliente();

                if (opcao == "1") {
                    
                    foreach (var p in ProdutoDAO.Listar()) UI.ExibirMensagem(p.ToString());
                }
                else if (opcao == "2") {
                    
                    UI.ExibirMensagem("Produtos disponíveis:");
                    foreach (var p in ProdutoDAO.Listar()) UI.ExibirMensagem(p.ToString());

                    UI.ExibirMensagem("Digite o ID do produto que quer adicionar: ");
                    int idProduto = ObterId();
                    Produto? produto = ProdutoDAO.Listar_Id(idProduto);
                    if (produto == null) { UI.ExibirMensagem("Produto não encontrado."); continue; }

                    UI.ExibirMensagem("Digite a quantidade: ");
                    int quantidade = 0;
                    if (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0) {
                        UI.ExibirMensagem("Quantidade inválida.");
                        continue;
                    }

                    if (produto.Estoque < quantidade) {
                        UI.ExibirMensagem($"Estoque insuficiente. Disponível: {produto.Estoque}");
                        continue;
                    }

                    
                    Venda? carrinho = null;
                    foreach (var v in VendaDAO.Listar()) {
                        if (v.IdCliente == clienteId && v.Carrinho) { carrinho = v; break; }
                    }
                    if (carrinho == null) {
                        carrinho = new Venda(DateTime.Now, true, 0, clienteId);
                        VendaDAO.Inserir(carrinho);
                        VendaDAO.Salvar();
                    }

                   
                    VendaItem item = new VendaItem(quantidade, produto.Preco, carrinho.Id, produto.Id);
                    VendaItemDAO.Inserir(item);
                    VendaItemDAO.Salvar();

                    produto.Estoque -= quantidade;
                    ProdutoDAO.Atualizar(produto);

                    UI.ExibirMensagem("Produto adicionado ao carrinho.");
                }
                else if (opcao == "3") {
                    
                    Venda? carrinho = null;
                    foreach (var v in VendaDAO.Listar()) {
                        if (v.IdCliente == clienteId && v.Carrinho) { carrinho = v; break; }
                    }
                    if (carrinho == null) { UI.ExibirMensagem("Carrinho vazio."); continue; }

                    double totalCalc = 0;
                    bool temItens = false;
                    UI.ExibirMensagem($"Carrinho ID: {carrinho.Id} | Data: {carrinho.Data}");
                    foreach (var item in VendaItemDAO.Listar()) {
                        if (item.IdVenda != carrinho.Id) continue;
                        temItens = true;
                        Produto? prod = ProdutoDAO.Listar_Id(item.IdProduto);
                        string desc = prod != null ? prod.Descricao : $"#{item.IdProduto}";
                        double subtotal = item.Preco * item.Quantidade;
                        totalCalc += subtotal;
                        UI.ExibirMensagem($"  Item ID:{item.Id} | Produto: {desc} | Qtd: {item.Quantidade} | Preço: {item.Preco:F2} | Subtotal: {subtotal:F2}");
                    }
                    if (!temItens) UI.ExibirMensagem("  (sem itens)");
                    UI.ExibirMensagem($"Total atual: {totalCalc:F2}");
                }
                else if (opcao == "4") {
                    
                    Venda? carrinho = null;
                    foreach (var v in VendaDAO.Listar()) {
                        if (v.IdCliente == clienteId && v.Carrinho) { carrinho = v; break; }
                    }
                    if (carrinho == null) { UI.ExibirMensagem("Carrinho vazio."); continue; }

                    double totalCalc = 0;
                    bool temItens = false;
                    foreach (var item in VendaItemDAO.Listar()) {
                        if (item.IdVenda != carrinho.Id) continue;
                        temItens = true;
                        totalCalc += item.Preco * item.Quantidade;
                    }
                    if (!temItens) { UI.ExibirMensagem("Carrinho sem itens."); continue; }

                    
                    carrinho.Total = totalCalc;
                    carrinho.Carrinho = false;
                    VendaDAO.Atualizar(carrinho);
                    UI.ExibirMensagem($"Compra finalizada. Total: {totalCalc:F2}");
                }
                else if (opcao == "5") {
                    
                    var vendas = VendaDAO.Listar();
                    bool tem = false;
                    foreach (var venda in vendas) {
                        if (venda.IdCliente != clienteId) continue;
                        if (venda.Carrinho) continue;
                        tem = true;
                        UI.ExibirMensagem(venda.ToString());
                        double totalCalc = 0;
                        bool temItens = false;
                        foreach (var item in VendaItemDAO.Listar()) {
                            if (item.IdVenda != venda.Id) continue;
                            temItens = true;
                            var prod = ProdutoDAO.Listar_Id(item.IdProduto);
                            string desc = prod != null ? prod.Descricao : $"#{item.IdProduto}";
                            double subtotal = item.Preco * item.Quantidade;
                            totalCalc += subtotal;
                            UI.ExibirMensagem($"  Item ID:{item.Id} | Produto: {desc} | Qtd: {item.Quantidade} | Preço: {item.Preco:F2} | Subtotal: {subtotal:F2}");
                        }
                        if (!temItens) UI.ExibirMensagem("  (sem itens)");
                        UI.ExibirMensagem($"  Total: {totalCalc:F2}");
                        UI.ExibirMensagem("");
                    }
                    if (!tem) UI.ExibirMensagem("Nenhuma venda encontrada.");
                }
                else if (opcao == "6") {
                    UI.ExibirMensagem("Voltando...");
                }
                else {
                    UI.ExibirMensagem("Opção inválida.");
                }
            }
        }

        public static int ObterId() {
            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static void RodarMenuAdmin() {
            string opcao = "";
            while (opcao != "14") {
                opcao = UI.MenuPrincipalAdmin();

                if (opcao == "1") {
                    UI.ExibirMensagem("Digite o nome, email, telefone e senha (cada um em uma linha): ");
                    var dados = UI.ObterDadosCliente();
                    View.ClienteInserir(dados[0], dados[1], dados[2], dados[3]);
                }
                else if (opcao == "2") {
                    foreach (var c in ClienteDAO.Listar()) UI.ExibirMensagem(c.ToString());
                }
                else if (opcao == "3") {
                    UI.ExibirMensagem("Digite o ID do cliente que você quer atualizar: ");
                    int id = View.ObterId();
                    Cliente cliente = ClienteDAO.Listar_Id(id);
                    if (cliente != null) {
                        UI.ExibirMensagem("Digite novo nome, email, telefone e senha (cada um em uma linha): ");
                        var dados = UI.ObterDadosCliente();
                        View.ClienteAtualizar(id, dados[0], dados[1], dados[2], dados[3]);
                    }
                }
                else if (opcao == "4") {
                    UI.ExibirMensagem("Digite o ID do cliente que você quer excluir: ");
                    int id = View.ObterId();
                    Cliente cliente = ClienteDAO.Listar_Id(id);
                    if (cliente != null) {
                        View.ClienteExcluir(id);
                    }
                }
                else if (opcao == "5") {
                    UI.ExibirMensagem("Digite descricao, preco, estoque e idCategoria (cada um em uma linha): ");
                    var dados = UI.ObterDadosProduto();
                    View.ProdutoInserir(dados[0], double.Parse(dados[1]), int.Parse(dados[2]), int.Parse(dados[3]));
                }
                else if (opcao == "6") {
                    foreach (var p in ProdutoDAO.Listar()) UI.ExibirMensagem(p.ToString());
                }
                else if (opcao == "7") {
                    UI.ExibirMensagem("Digite o ID do produto que você quer atualizar: ");
                    int id = View.ObterId();
                    Produto produto = ProdutoDAO.Listar_Id(id);
                    if (produto != null) {
                        UI.ExibirMensagem("Digite descricao, preco, estoque e idCategoria (cada um em uma linha): ");
                        var dados = UI.ObterDadosProduto();
                        View.ProdutoAtualizar(id, dados[0], double.Parse(dados[1]), int.Parse(dados[2]), int.Parse(dados[3]));
                    }
                }
                else if (opcao == "8") {
                    UI.ExibirMensagem("Digite o ID do produto que você quer excluir: ");
                    int id = View.ObterId();
                    Produto produto = ProdutoDAO.Listar_Id(id);
                    if (produto != null) {
                        View.ProdutoExcluir(id);
                    }
                }
                else if (opcao == "9") {
                    UI.ExibirMensagem("Digite a descricao da Categoria: ");
                    var dados = UI.ObterDadosCategoria();
                    View.CategoriaInserir(dados[0]);
                }
                else if (opcao == "10") {
                    foreach (var c in CategoriaDAO.Listar()) UI.ExibirMensagem(c.ToString());
                }
                else if (opcao == "11") {
                    UI.ExibirMensagem("Digite o ID da Categoria que você quer atualizar: ");
                    int id = View.ObterId();
                    Categoria categoria = CategoriaDAO.Listar_Id(id);
                    if (categoria != null) {
                        UI.ExibirMensagem("Digite a nova descrição: ");
                        var dados = UI.ObterDadosCategoria();
                        View.CategoriaAtualizar(id, dados[0]);
                    }
                }
                else if (opcao == "12") {
                    UI.ExibirMensagem("Digite o ID da categoria que você quer excluir: ");
                    int id = View.ObterId();
                    Categoria categoria = CategoriaDAO.Listar_Id(id);
                    if (categoria != null) {
                        View.CategoriaExcluir(id);
                    }
                }
                else if (opcao == "13") {
                    var vendas = VendaDAO.Listar();
                    foreach (var venda in vendas) {
                        if (venda.Carrinho) continue;

                        var cliente = ClienteDAO.Listar_Id(venda.IdCliente);
                        UI.ExibirMensagem(venda.ToString() + (cliente != null ? $" | Cliente: {cliente.Nome}" : ""));

                        double totalCalc = 0;
                        bool temItens = false;

                        foreach (var item in VendaItemDAO.Listar()) {
                            if (item.IdVenda != venda.Id) continue;
                            temItens = true;
                            var produto = ProdutoDAO.Listar_Id(item.IdProduto);
                            string prodDescricao = produto != null ? produto.Descricao : $"#{item.IdProduto}";
                                double subtotal = item.Preco * item.Quantidade;
                            totalCalc += subtotal;
                            UI.ExibirMensagem($"  Item ID:{item.Id} | Produto: {prodDescricao} | Qtd: {item.Quantidade} | Preço: {item.Preco:F2} | Subtotal: {subtotal:F2}");
                        }

                        if (!temItens) UI.ExibirMensagem("  (sem itens)");
                        UI.ExibirMensagem($"  Total calculado: {totalCalc:F2}");
                        UI.ExibirMensagem("");
                    }
                }
                else if (opcao == "14") {
                    UI.ExibirMensagem("Saindo..");
                }
                else {
                    UI.ExibirMensagem("Opção inválida.");
                }
            }
        }
    }
}
