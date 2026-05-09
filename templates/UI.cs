using crud_in_terminal_csharp.models;
using crud_in_terminal_csharp.views;
using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.templates {
    internal class UI {
        public static string MenuBoasVindas() {
            Console.WriteLine("=======TELA INICIAL========");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Criar Conta");
            Console.WriteLine("3. Sair");
            View.CriarAdmin();
            return LerDados();
        }

        public static Cliente MenuCriarConta() {
            Console.WriteLine("======REGISTRO DE CLIENTES======");
            Console.WriteLine("Digite seu nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.WriteLine("Digite seu telefone: ");
            string telefone = Console.ReadLine() ?? "";
            Console.WriteLine("Digite um email para o cadastro: ");
            string email = Console.ReadLine() ?? "";
            Console.WriteLine("Digite um senha para o cadastro: ");
            string senha = Console.ReadLine() ?? "";
            Cliente cliente = View.CriarNovoCliente(nome, email, telefone, senha);
            return cliente;
        }       
        public static (string email, string senha) MenuLogin() {
            Console.WriteLine("Digite seu email cadastrado para entrar: ");
            string email = Console.ReadLine() ?? "";
            Console.WriteLine("Digite sua senha cadastrada para entrar: ");
            string senha = Console.ReadLine() ?? "";
            return (email, senha);
        }

        public static void ExibirMensagem(String mensagem) {
            Console.WriteLine($"{mensagem}");
        }

        public static string LerDados() {
            return Console.ReadLine() ?? "";
        }

        public static string MenuPrincipalCliente() {
            Console.WriteLine("=========MENU PRINCIPAL=========");
            Console.WriteLine("1. Listar produtos");
            Console.WriteLine("2. Inserir produtos no Carrinho");
            Console.WriteLine("3. Ver Carrinho");
            Console.WriteLine("4. Finalizar Comprar");
            Console.WriteLine("5. Meus Pedidos");
            Console.WriteLine("6. Sair");
            return LerDados();
        }

        public static string MenuPrincipalAdmin() {
            Console.WriteLine("1. Adicionar um novo Cliente");
            Console.WriteLine("2. Listar Clientes");
            Console.WriteLine("3. Atualizar dados de um Cliente");
            Console.WriteLine("4. Excluir um Cliente\n");

            Console.WriteLine("5. Adicionar um novo Produto");
            Console.WriteLine("6. Listar Produtos");
            Console.WriteLine("7. Atualizar um Produto");
            Console.WriteLine("8. Excluir um Produto\n");

            Console.WriteLine("9. Adicionar uma nova Categoria");
            Console.WriteLine("10. Listar Categorias");
            Console.WriteLine("11. Atualizar uma Categoria");
            Console.WriteLine("12. Excluir uma Categoria\n");

            Console.WriteLine("13. Listar Vendas");
            Console.WriteLine("14. Aplicar Descontos");
            Console.WriteLine("15. Reajustar Preços");
            Console.WriteLine("16. Sair\n");
            return LerDados();
        }

        public static List<string> ObterDadosCliente() {
            List<string> objetos = new List<string>();
            string nome = LerDados();
            string email = LerDados();
            string telefone = LerDados();
            string senha = LerDados();
            objetos.Add(nome);
            objetos.Add(email);
            objetos.Add(telefone);
            objetos.Add(senha);
            return objetos;
        }

        public static List<string> ObterDadosCategoria() {
            List<string> objetos = new List<string>();
            string descricao = LerDados();
            objetos.Add(descricao);
            return objetos;
        }

        public static List<string> ObterDadosProduto() {
            List<string> objetos = new List<string>();
            string descricao = LerDados();
            string preco = LerDados();
            string estoque = LerDados();
            string idCategoria = LerDados();
            objetos.Add(descricao);
            objetos.Add(preco);
            objetos.Add(estoque);
            objetos.Add(idCategoria);
            return objetos;
        }
    }
}
