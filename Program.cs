using crud_in_terminal_csharp.models;
using crud_in_terminal_csharp.views;
using System;

class Program {
    static void Main() {
        // abrir bases / carregar dados
        ClienteDAO.Abrir();
        CategoriaDAO.Abrir();
        ProdutoDAO.Abrir();

        // garante que admin exista
        View.CriarAdmin();

        // inicia menu principal da aplicação
        View.RodarMenuInicial();
    }
}