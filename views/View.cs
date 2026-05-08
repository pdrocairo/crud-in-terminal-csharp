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
            return c;
        }

        public static void ClienteInserir(string nome, string email, string fone, string senha) {
            Cliente c = new Cliente(nome, email, fone, senha);
            ClienteDAO.Inserir(c);
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

        public static void AutenticarLogin(string email, string senha) {
            if (email == "admin" && senha == "admin") {
                UI.MenuPrincipalAdmin();
                return;
            }

            foreach (var obj in ClienteDAO.objetos) {
                if (obj.Email == email && obj.Senha == senha) {
                    UI.MenuPrincipalCliente();
                    return;
                }
            }
            UI.ExibirMensagem("Usuário ou senha inválidos.");
        }
        private static void RodarMenuCliente() {
            string opcao = "";
            while (opcao != "6") {
                UI.MenuPrincipalCliente();
                opcao = UI.LerDados();

                if (opcao == "1") {
                    ProdutoDAO.Listar();
                }
                else if (opcao == "2") {
                    return;
                }
                else if (opcao == "3") {

                }
            }

        }

        public static int ObterId() {
            return int.Parse(Console.ReadLine());
        }

        private static void RodarMenuAdmin() {
            string opcao = "";
            while (opcao != "14") {
                UI.MenuPrincipalAdmin();
                opcao = UI.LerDados();

                if (opcao == "1") {
                    UI.ExibirMensagem("Digite o nome do cliente: ");
                    string nome = UI.ObterDadosCliente()[0];
                    UI.ExibirMensagem("Digite o email do cliente: ");
                    string email = UI.ObterDadosCliente()[1];
                    UI.ExibirMensagem("Digite o telefone do cliente: ");
                    string telefone = UI.ObterDadosCliente()[2];
                    UI.ExibirMensagem("Digite a senha do cliente: ");
                    string senha = UI.ObterDadosCliente()[3];
                    View.ClienteInserir(nome, email, telefone, senha);
                }
                else if (opcao == "2") {
                    View.ClienteListar();
                }
                else if (opcao == "3") {
                    UI.ExibirMensagem("Digite o ID do cliente que você quer atualizar: ");
                    int id = View.ObterId();
                    Cliente cliente = ClienteDAO.Listar_Id(id);
                    if (cliente != null) {
                        UI.ExibirMensagem("Digite um novo nome: ");
                        string nome = UI.ObterDadosCliente()[0];
                        UI.ExibirMensagem("Digite um novo email: ");
                        string email = UI.ObterDadosCliente()[1];
                        UI.ExibirMensagem("Digite um novo telefone: ");
                        string telefone = UI.ObterDadosCliente()[2];
                        UI.ExibirMensagem("Digite um novo senha: ");
                        string senha = UI.ObterDadosCliente()[3];
                        View.ClienteAtualizar(id, nome, email, telefone, senha);
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
                    UI.ExibirMensagem("Digite a descricao do produto: ");
                    string descricao = UI.ObterDadosProduto()[0];
                    UI.ExibirMensagem("Digite o preco do produto: ");
                    double preco = double.Parse(UI.ObterDadosProduto()[1]);
                    UI.ExibirMensagem("Digite o estoque do cliente: ");
                    int estoque = int.Parse(UI.ObterDadosProduto()[2]);
                    UI.ExibirMensagem("Digite o id da Categoria do produto: ");
                    int idCategoria = int.Parse(UI.ObterDadosProduto()[3]);
                    View.ProdutoInserir(descricao, preco, estoque, idCategoria);
                }
                else if (opcao == "6") {
                    View.ProdutoListar();
                }
                else if (opcao == "7") {
                    UI.ExibirMensagem("Digite o ID do produto que você quer atualizar: ");
                    int id = View.ObterId();
                    Produto produto = ProdutoDAO.Listar_Id(id);
                    if (produto != null) {
                        UI.ExibirMensagem("Digite um novo nome: ");
                        string descricao = UI.ObterDadosProduto()[0];
                        UI.ExibirMensagem("Digite um novo email: ");
                        double preco = int.Parse(UI.ObterDadosProduto()[1]);
                        UI.ExibirMensagem("Digite um novo telefone: ");
                        int estoque = int.Parse(UI.ObterDadosProduto()[2]);
                        UI.ExibirMensagem("Digite um novo senha: ");
                        int idCategoria = int.Parse(UI.ObterDadosProduto()[3]);
                        View.ProdutoAtualizar(id, descricao, preco, estoque, idCategoria);
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
                    string descricao = UI.ObterDadosCategoria()[0];
                    View.CategoriaInserir(descricao);
                }
                else if (opcao == "10") {
                    View.CategoriaListar();
                }
                else if (opcao == "11") {
                    UI.ExibirMensagem("Digite o ID da Categoria que você quer atualizar: ");
                    int id = View.ObterId();
                    Categoria categoria = CategoriaDAO.Listar_Id(id);
                    if (categoria != null) {
                        UI.ExibirMensagem("Digite uma nova descrição: ");
                        string descricao = UI.ObterDadosProduto()[0];
                        View.CategoriaAtualizar(id, descricao);
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
                    foreach (var item in VendaDAO.Listar()) {
                        UI.ExibirMensagem(item.ToString());
                    }
                }


            }

        }
    }
}
