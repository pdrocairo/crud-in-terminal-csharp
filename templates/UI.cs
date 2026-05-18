using crud_in_terminal_csharp.models;
using crud_in_terminal_csharp.views;
using System;
using System.Collections.Generic;

namespace crud_in_terminal_csharp.templates {
    internal class UI {

        // ===================== MENUS (só exibem e leem opção) =====================

        public static string MenuBoasVindas() {
            Console.WriteLine("=======TELA INICIAL========");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Criar Conta");
            Console.WriteLine("3. Sair");
            View.CriarAdmin();
            return Console.ReadLine();
        }

        public static (string email, string senha) MenuLogin() {
            Console.WriteLine("Digite seu email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Digite sua senha: ");
            string senha = Console.ReadLine();
            return (email, senha);
        }

        public static Cliente MenuCriarConta() {
            Console.WriteLine("======REGISTRO DE CLIENTES======");
            Console.WriteLine("Digite seu nome: ");
            string nome = Console.ReadLine() ;
            Console.WriteLine("Digite seu telefone: ");
            string telefone = Console.ReadLine() ;
            Console.WriteLine("Digite um email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Digite uma senha: ");
            string senha = Console.ReadLine();
            return View.CriarNovoCliente(nome, email, telefone, senha);
        }

        public static string MenuPrincipalCliente() {
            Console.WriteLine("=========MENU CLIENTE=========");
            Console.WriteLine("1. Listar Produtos");
            Console.WriteLine("2. Inserir Produto no Carrinho");
            Console.WriteLine("3. Ver Carrinho");
            Console.WriteLine("4. Finalizar Compra");
            Console.WriteLine("5. Meus Pedidos");
            Console.WriteLine("6. Sair");
            return Console.ReadLine();
        }

        public static string MenuPrincipalAdmin() {
            Console.WriteLine("=========MENU ADMIN=========");
            Console.WriteLine("----- Clientes -----");
            Console.WriteLine("1. Inserir  2. Listar  3. Atualizar  4. Excluir");
            Console.WriteLine("----- Categorias -----");
            Console.WriteLine("5. Inserir  6. Listar  7. Atualizar  8. Excluir");
            Console.WriteLine("----- Produtos -----");
            Console.WriteLine("9. Inserir  10. Listar  11. Atualizar  12. Excluir");
            Console.WriteLine("13. Listar Vendas  14. Aplicar Desconto  15. Reajustar Preco  16. Sair");
            return Console.ReadLine();
        }

        // ===================== HELPERS =====================

        public static void ExibirMensagem(string mensagem) {
            Console.WriteLine(mensagem);
        }

        public static void Pausar() {
            Console.WriteLine("\nPressione ENTER para continuar");
            Console.ReadLine();
        }

        public static int LerInt(string label) {
            Console.WriteLine(label);
            return int.TryParse(Console.ReadLine(), out int val) ? val : 0;
        }

        public static double LerDouble(string label) {
            Console.WriteLine(label);
            return double.TryParse(Console.ReadLine(), out double val) ? val : 0;
        }

        public static string LerTexto(string label) {
            Console.WriteLine(label);
            return Console.ReadLine();
        }

        // ===================== LOOP INICIAL =====================

        public static void RodarMenuInicial() {
            while (true) {
                string opcao = MenuBoasVindas();

                if (opcao == "1") {
                    var login = MenuLogin();
                    string? tipo = View.AutenticarLogin(login.email, login.senha);

                    if (tipo == "admin") {
                        RodarMenuAdmin();
                    } else if (tipo == "cliente") {
                        Cliente? cliente = ClienteDAO.Listar().Find(c => c.Email == login.email);
                        if (cliente != null) {
                            RodarMenuCliente(cliente.Id);
                        } else {
                            ExibirMensagem("Erro ao localizar cliente.");
                            Pausar();
                        }
                    } else {
                        ExibirMensagem("Email ou senha incorretos.");
                        Pausar();
                    }
                } else if (opcao == "2") {
                    MenuCriarConta();
                    ExibirMensagem("Conta criada com sucesso!");
                    Pausar();
                } else if (opcao == "3") {
                    ExibirMensagem("Saindo...");
                    return;
                } else {
                    ExibirMensagem("Opção inválida.");
                    Pausar();
                }
            }
        }

        // ===================== LOOP ADMIN =====================

        public static void RodarMenuAdmin() {
            while (true) {
                string opcao = MenuPrincipalAdmin();

                if (opcao == "1") {
                    // Inserir Cliente
                    string nome = LerTexto("Nome:");
                    string email = LerTexto("Email:");
                    string fone = LerTexto("Telefone:");
                    string senha = LerTexto("Senha:");
                    View.ClienteInserir(nome, email, fone, senha);
                    ExibirMensagem("Cliente inserido!");

                } else if (opcao == "2") {
                    // Listar Clientes
                    ExibirMensagem("=== Clientes ===");
                    foreach (var c in View.ClienteListar()) {
                        Console.WriteLine(c);
                    }

                } else if (opcao == "3") {
                    // Atualizar Cliente
                    int id = LerInt("ID do cliente:");
                    string nome = LerTexto("Novo nome:");
                    string email = LerTexto("Novo email:");
                    string fone = LerTexto("Novo telefone:");
                    string senha = LerTexto("Nova senha:");
                    View.ClienteAtualizar(id, nome, email, fone, senha);
                    ExibirMensagem("Cliente atualizado!");

                } else if (opcao == "4") {
                    // Excluir Cliente
                    int id = LerInt("ID do cliente a excluir:");
                    View.ClienteExcluir(id);
                    ExibirMensagem("Cliente excluído!");

                } else if (opcao == "5") {
                    // Inserir Categoria
                    string desc = LerTexto("Descrição da categoria:");
                    View.CategoriaInserir(desc);
                    ExibirMensagem("Categoria inserida!");

                } else if (opcao == "6") {
                    // Listar Categorias
                    ExibirMensagem("=== Categorias ===");
                    foreach (var c in View.CategoriaListar()) {
                        Console.WriteLine(c);
                    }

                } else if (opcao == "7") {
                    // Atualizar Categoria
                    int id = LerInt("ID da categoria:");
                    string desc = LerTexto("Nova descrição:");
                    View.CategoriaAtualizar(id, desc);
                    ExibirMensagem("Categoria atualizada!");

                } else if (opcao == "8") {
                    // Excluir Categoria
                    int id = LerInt("ID da categoria a excluir:");
                    View.CategoriaExcluir(id);
                    ExibirMensagem("Categoria excluída!");

                } else if (opcao == "9") {
                    // Inserir Produto
                    string desc = LerTexto("Descrição do produto:");
                    double preco = LerDouble("Preço:");
                    int estoque = LerInt("Estoque:");
                    int idCat = LerInt("ID da categoria:");
                    View.ProdutoInserir(desc, preco, estoque, idCat);
                    ExibirMensagem("Produto inserido!");

                } else if (opcao == "10") {
                    // Listar Produtos
                    ExibirMensagem("=== Produtos ===");
                    foreach (var p in View.ProdutoListar()) {
                        Console.WriteLine(p);
                    }

                } else if (opcao == "11") {
                    // Atualizar Produto
                    int id = LerInt("ID do produto:");
                    string desc = LerTexto("Nova descrição:");
                    double preco = LerDouble("Novo preço:");
                    int estoque = LerInt("Novo estoque:");
                    int idCat = LerInt("ID da categoria:");
                    View.ProdutoAtualizar(id, desc, preco, estoque, idCat);
                    ExibirMensagem("Produto atualizado!");

                } else if (opcao == "12") {
                    // Excluir Produto
                    int id = LerInt("ID do produto a excluir:");
                    View.ProdutoExcluir(id);
                    ExibirMensagem("Produto excluído!");

                } else if (opcao == "13") {
                    // Listar Vendas
                    ExibirMensagem("=== Todas as Vendas ===");
                    foreach (var v in View.ListarTodasVendas()) {
                        Console.WriteLine(v);
                        foreach (var item in VendaItemDAO.ObterPorVenda(v.Id)) {
                            Produto? p = ProdutoDAO.Listar_Id(item.IdProduto);
                            string nomeProd = p != null ? p.Descricao : "?";
                            Console.WriteLine($"  -> {nomeProd} | Qtd: {item.Quantidade} | Preço unit: R$ {item.Preco:F2} | Subtotal: R$ {item.Preco * item.Quantidade:F2}");
                        }
                    }

                } else if (opcao == "14") {
                    // Aplicar Desconto
                    int id          = LerInt("ID do produto:");
                    double percent  = LerDouble("Porcentagem de desconto (0-100):");
                    double novo     = View.AplicarDesconto(id, percent);
                    ExibirMensagem($"Novo preço: R$ {novo:F2}");

                } else if (opcao == "15") {
                    // Reajustar Preço
                    int id          = LerInt("ID do produto:");
                    double percent  = LerDouble("Porcentagem de reajuste (0-100):");
                    int alt         = LerInt("0 = aumentar  /  1 = diminuir:");
                    double novo     = View.ReajustarPreco(id, percent, alt);
                    ExibirMensagem($"Novo preço: R$ {novo:F2}");

                } else if (opcao == "16") {
                    ExibirMensagem("Saindo do menu admin...");
                    return;

                } else {
                    ExibirMensagem("Opção inválida.");
                }

                Pausar();
            }
        }

        // ===================== LOOP CLIENTE =====================

        public static void RodarMenuCliente(int idCliente) {
            while (true) {
                string opcao = MenuPrincipalCliente();

                if (opcao == "1") {
                    // Listar Produtos
                    ExibirMensagem("=== Produtos Disponíveis ===");
                    foreach (var p in View.ProdutoListar()) {
                        Console.WriteLine(p);
                    }

                } else if (opcao == "2") {
                    // Inserir produto no carrinho
                    ExibirMensagem("=== Produtos Disponíveis ===");
                    foreach (var p in View.ProdutoListar()) {
                        Console.WriteLine(p);
                    }
                    int idProduto   = LerInt("Digite o ID do produto:");
                    int quantidade  = LerInt("Digite a quantidade:");
                    string resultado = View.AdicionarAoCarrinho(idCliente, idProduto, quantidade);
                    ExibirMensagem(resultado);

                } else if (opcao == "3") {
                    // Ver Carrinho
                    ExibirMensagem("=== Seu Carrinho ===");
                    List<VendaItem> itens = View.VerCarrinho(idCliente);
                    if (itens.Count == 0) {
                        ExibirMensagem("Carrinho vazio.");
                    } else {
                        double totalCarrinho = 0;
                        foreach (var item in itens) {
                            Produto? p = ProdutoDAO.Listar_Id(item.IdProduto);
                            string nomeProd = p != null ? p.Descricao : "?";
                            double subtotal = item.Preco * item.Quantidade;
                            totalCarrinho += subtotal;
                            Console.WriteLine($"  {nomeProd} | Preço unit: R$ {item.Preco:F2} | Qtd: {item.Quantidade} | Subtotal: R$ {subtotal:F2}");
                        }
                        Console.WriteLine($"  TOTAL: R$ {totalCarrinho:F2}");
                    }

                } else if (opcao == "4") {
                    // Finalizar Compra
                    string resultado = View.FinalizarCompra(idCliente);
                    ExibirMensagem(resultado);

                } else if (opcao == "5") {
                    // Meus Pedidos
                    ExibirMensagem("=== Minhas Compras ===");
                    List<Venda> compras = View.ListarMinhasCompras(idCliente);
                    if (compras.Count == 0) {
                        ExibirMensagem("Nenhuma compra encontrada.");
                    } else {
                        foreach (var v in compras) {
                            Console.WriteLine($"Pedido ID: {v.Id} | Data: {v.Data:dd/MM/yyyy} | Total: R$ {v.Total:F2}");
                            foreach (var item in VendaItemDAO.ObterPorVenda(v.Id)) {
                                Produto? p = ProdutoDAO.Listar_Id(item.IdProduto);
                                string nomeProd = p != null ? p.Descricao : "?";
                                Console.WriteLine($"  -> {nomeProd} | Qtd: {item.Quantidade} | Preço unit: R$ {item.Preco:F2}");
                            }
                        }
                    }

                } else if (opcao == "6") {
                    ExibirMensagem("Voltando ao menu inicial...");
                    return;

                } else {
                    ExibirMensagem("Opção inválida.");
                }

                Pausar();
            }
        }
    }
}
