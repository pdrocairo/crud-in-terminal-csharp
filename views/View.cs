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
                        else {
                            UI.ExibirMensagem("Erro ao localizar cliente.");
                            Pausar();
                        }
                    }
                    else {
                        UI.ExibirMensagem("Usuário ou senha inválidos.");
                        Pausar();
                    }
                }
                else if (opcao == "2") {
                    UI.MenuCriarConta();
                    UI.ExibirMensagem("Conta criada com sucesso.");
                    Pausar();
                }
                else if (opcao == "3") {
                    UI.ExibirMensagem("Saindo...");
                }
                else {
                    UI.ExibirMensagem("Opção inválida.");
                    Pausar();
                }
            }
        }


        private static void RodarMenuCliente(int clienteId) {
            string opcao = "";
            while (opcao != "6") {
                opcao = UI.MenuPrincipalCliente();

                if (opcao == "1") {

                    var produtos = ProdutoDAO.Listar();
                    foreach (var p in produtos) {
                        UI.ExibirMensagem(p.ToString());
                    }
                    Pausar();
                }
                else if (opcao == "2") {

                    UI.ExibirMensagem("Produtos disponíveis:");
                    var produtos = ProdutoDAO.Listar();
                    foreach (var p in produtos) {
                        UI.ExibirMensagem(p.ToString());
                    }
                    Pausar();

                    UI.ExibirMensagem("Digite o ID do produto que quer adicionar: ");
                    int idProduto = ObterId();
                    Produto? produto = ProdutoDAO.Listar_Id(idProduto);
                    if (produto == null) { UI.ExibirMensagem("Produto não encontrado."); Pausar(); continue; }

                    UI.ExibirMensagem("Digite a quantidade: ");
                    int quantidade = 0;
                    if (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0) {
                        UI.ExibirMensagem("Quantidade inválida.");
                        Pausar();
                        continue;
                    }

                    if (produto.Estoque < quantidade) {
                        UI.ExibirMensagem($"Estoque insuficiente. Disponível: {produto.Estoque}");
                        Pausar();
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
                    Pausar();
                }
                else if (opcao == "3") {

                    Venda? carrinho = null;
                    foreach (var v in VendaDAO.Listar()) {
                        if (v.IdCliente == clienteId && v.Carrinho) { carrinho = v; break; }
                    }
                    if (carrinho == null) { UI.ExibirMensagem("Carrinho vazio."); Pausar(); continue; }

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
                    Pausar();
                }
                else if (opcao == "4") {

                    Venda? carrinho = null;
                    foreach (var v in VendaDAO.Listar()) {
                        if (v.IdCliente == clienteId && v.Carrinho) { carrinho = v; break; }
                    }
                    if (carrinho == null) { UI.ExibirMensagem("Carrinho vazio."); Pausar(); continue; }

                    double totalCalc = 0;
                    bool temItens = false;
                    foreach (var item in VendaItemDAO.Listar()) {
                        if (item.IdVenda != carrinho.Id) continue;
                        temItens = true;
                        totalCalc += item.Preco * item.Quantidade;
                    }
                    if (!temItens) { UI.ExibirMensagem("Carrinho sem itens."); Pausar(); continue; }


                    carrinho.Total = totalCalc;
                    carrinho.Carrinho = false;
                    VendaDAO.Atualizar(carrinho);
                    UI.ExibirMensagem($"Compra finalizada. Total: {totalCalc:F2}");
                    Pausar();
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
                    Pausar();
                }
                else if (opcao == "6") {
                    UI.ExibirMensagem("Voltando...");
                }
                else {
                    UI.ExibirMensagem("Opção inválida.");
                    Pausar();
                }
            }
        }

        public static int ObterId() {
            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static void RodarMenuAdmin() {
            string opcao = "";
            while (opcao != "16") {
                opcao = UI.MenuPrincipalAdmin();

                if (opcao == "1") {
                    UI.ExibirMensagem("Digite o nome, email, telefone e senha (cada um em uma linha): ");
                    var dados = UI.ObterDadosCliente();
                    View.ClienteInserir(dados[0], dados[1], dados[2], dados[3]);
                    UI.ExibirMensagem("Cliente criado com sucesso.");
                    Pausar();
                }
                else if (opcao == "2") {
                    var clientes = ClienteDAO.Listar();
                    foreach (var c in clientes) {
                        UI.ExibirMensagem(c.ToString());
                    }
                    Pausar();
                }
                else if (opcao == "3") {
                    UI.ExibirMensagem("Digite o ID do cliente que você quer atualizar: ");
                    int id = View.ObterId();
                    Cliente cliente = ClienteDAO.Listar_Id(id);
                    if (cliente != null) {
                        UI.ExibirMensagem("Digite novo nome, email, telefone e senha (cada um em uma linha): ");
                        var dados = UI.ObterDadosCliente();
                        View.ClienteAtualizar(id, dados[0], dados[1], dados[2], dados[3]);
                        UI.ExibirMensagem("Cliente atualizado com sucesso.");
                        Pausar();
                    }
                    else {
                        UI.ExibirMensagem("Cliente não encontrado.");
                        Pausar();
                    }
                }
                else if (opcao == "4") {
                    UI.ExibirMensagem("Digite o ID do cliente que você quer excluir: ");
                    int id = View.ObterId();
                    Cliente cliente = ClienteDAO.Listar_Id(id);
                    if (cliente != null) {
                        View.ClienteExcluir(id);
                        UI.ExibirMensagem("Cliente excluído com sucesso.");
                        Pausar();
                    }
                    else {
                        UI.ExibirMensagem("Cliente não encontrado.");
                        Pausar();
                    }
                }
                else if (opcao == "5") {
                    UI.ExibirMensagem("Digite descricao, preco, estoque e idCategoria (cada um em uma linha): ");
                    var dados = UI.ObterDadosProduto();
                    View.ProdutoInserir(dados[0], double.Parse(dados[1]), int.Parse(dados[2]), int.Parse(dados[3]));
                    UI.ExibirMensagem("Produto criado com sucesso.");
                    Pausar();
                }
                else if (opcao == "6") {
                    var produtos = ProdutoDAO.Listar();
                    foreach (var p in produtos) {
                        UI.ExibirMensagem(p.ToString());
                    }
                    Pausar();
                }
                else if (opcao == "7") {
                    UI.ExibirMensagem("Digite o ID do produto que você quer atualizar: ");
                    int id = View.ObterId();
                    Produto produto = ProdutoDAO.Listar_Id(id);
                    if (produto != null) {
                        UI.ExibirMensagem("Digite descricao, preco, estoque e idCategoria (cada um em uma linha): ");
                        var dados = UI.ObterDadosProduto();
                        View.ProdutoAtualizar(id, dados[0], double.Parse(dados[1]), int.Parse(dados[2]), int.Parse(dados[3]));
                        UI.ExibirMensagem("Produto atualizado com sucesso.");
                        Pausar();
                    }
                    else {
                        UI.ExibirMensagem("Produto não encontrado.");
                        Pausar();
                    }
                }
                else if (opcao == "8") {
                    UI.ExibirMensagem("Digite o ID do produto que você quer excluir: ");
                    int id = View.ObterId();
                    Produto produto = ProdutoDAO.Listar_Id(id);
                    if (produto != null) {
                        View.ProdutoExcluir(id);
                        UI.ExibirMensagem("Produto excluído com sucesso.");
                        Pausar();
                    }
                    else {
                        UI.ExibirMensagem("Produto não encontrado.");
                        Pausar();
                    }
                }
                else if (opcao == "9") {
                    UI.ExibirMensagem("Digite a descricao da Categoria: ");
                    var dados = UI.ObterDadosCategoria();
                    View.CategoriaInserir(dados[0]);
                    UI.ExibirMensagem("Categoria criada com sucesso.");
                    Pausar();
                }
                else if (opcao == "10") {
                    var categorias = CategoriaDAO.Listar();
                    foreach (var c in categorias) UI.ExibirMensagem(c.ToString());
                    Pausar();
                }
                else if (opcao == "11") {
                    UI.ExibirMensagem("Digite o ID da Categoria que você quer atualizar: ");
                    int id = View.ObterId();
                    Categoria categoria = CategoriaDAO.Listar_Id(id);
                    if (categoria != null) {
                        UI.ExibirMensagem("Digite a nova descrição: ");
                        var dados = UI.ObterDadosCategoria();
                        View.CategoriaAtualizar(id, dados[0]);
                        UI.ExibirMensagem("Categoria atualizada com sucesso.");
                        Pausar();
                    }
                    else {
                        UI.ExibirMensagem("Categoria não encontrada.");
                        Pausar();
                    }
                }
                else if (opcao == "12") {
                    UI.ExibirMensagem("Digite o ID da categoria que você quer excluir: ");
                    int id = View.ObterId();
                    Categoria categoria = CategoriaDAO.Listar_Id(id);
                    if (categoria != null) {
                        View.CategoriaExcluir(id);
                        UI.ExibirMensagem("Categoria excluída com sucesso.");
                        Pausar();
                    }
                    else {
                        UI.ExibirMensagem("Categoria não encontrada.");
                        Pausar();
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
                    Pausar();
                }
                else if (opcao == "14") {
                    var produtos = ProdutoDAO.Listar();
                    foreach (var p in produtos) {
                        UI.ExibirMensagem(p.ToString());
                    }
                    Pausar();
                    UI.ExibirMensagem("Digite o ID do produto que você quer aplicar um desconto: ");
                    int id = View.ObterId();
                    UI.ExibirMensagem("Digite a porcentagem do desconto desejado (0 a 100): ");
                    double porcentagem = double.Parse(UI.LerDados());
                    double novoPreco = View.AplicarDesconto(id, porcentagem);
                    UI.ExibirMensagem($"Desconto Aplicado com sucesso! Novo Preço: {novoPreco:F2}");
                    Pausar();

                }
                else if (opcao == "15") {
                    var produtos = ProdutoDAO.Listar();
                    foreach (var p in produtos) {
                        UI.ExibirMensagem(p.ToString());
                    }
                    Pausar();
                    UI.ExibirMensagem("Digite o ID do produto que você quer reajustar o valor: ");
                    int id = View.ObterId();
                    UI.ExibirMensagem("Digite a porcentagem do desconto desejado (0 a 100): ");
                    double porcentagem = double.Parse(UI.LerDados());
                    UI.ExibirMensagem("Se você deseja subir o valor digite 0, caso seja o contrario digite 1: ");
                    int alt = int.Parse(UI.LerDados());
                    double novoPreco = View.ReajustarPreco(id, porcentagem, alt);
                    UI.ExibirMensagem($"Reajuste Aplicado com sucesso! Novo Preço: {novoPreco:F2}");
                    Pausar();
                }
                else if (opcao == "16") {
                    UI.ExibirMensagem("Saindo..");

                }
                else {
                    UI.ExibirMensagem("Opção inválida.");
                    Pausar();
                }
            }
        }
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

        public static void Pausar() {
            Console.WriteLine("\nPressione ENTER para continuar");
            Console.ReadLine();
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
                produto.Preco = novoPreco;
            }
            else {
                novoPreco = produto.Preco - (produto.Preco * (porcentagem / 100));
                produto.Preco = novoPreco;
            }


            ProdutoDAO.Atualizar(produto);
            return novoPreco;


        }
    }
}