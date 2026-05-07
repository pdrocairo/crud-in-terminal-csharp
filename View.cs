using System;
using System.Collections.Generic;
using System.Text;
using crud_in_terminal_csharp.daos;
using crud_in_terminal_csharp.model;

namespace crud_in_terminal_csharp {
    internal class View {

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
    }
}
