using crud_in_terminal_csharp.models;
using crud_in_terminal_csharp.views;
using System;

Console.WriteLine("=== MODELS + DAOs QUICK TEST ===\n");

try {
    // load
    ClienteDAO.Abrir();
    CategoriaDAO.Abrir();
    ProdutoDAO.Abrir();

    // Cliente test
    Console.WriteLine($"Clientes antes: {ClienteDAO.Listar().Count}");
    View.ClienteInserir("Teste", "teste@ex.com", "0000", "123");
    ClienteDAO.Salvar();
    Console.WriteLine($"Clientes depois: {ClienteDAO.Listar().Count}");
    foreach (var c in ClienteDAO.Listar()) Console.WriteLine($"  {c}");

    // Categoria test
    Console.WriteLine($"\nCategorias antes: {CategoriaDAO.Listar().Count}");
    View.CategoriaInserir("CatTeste");
    CategoriaDAO.Salvar();
    Console.WriteLine($"Categorias depois: {CategoriaDAO.Listar().Count}");
    foreach (var cat in CategoriaDAO.Listar()) Console.WriteLine($"  {cat}");

    // Produto test
    Console.WriteLine($"\nProdutos antes: {ProdutoDAO.Listar().Count}");
    int catId = (CategoriaDAO.Listar().Count > 0) ? CategoriaDAO.Listar()[0].Id : 0;
    View.ProdutoInserir("ProdTeste", 9.9, 5, catId);
    ProdutoDAO.Salvar();
    Console.WriteLine($"Produtos depois: {ProdutoDAO.Listar().Count}");
    foreach (var p in ProdutoDAO.Listar()) Console.WriteLine($"  {p}");

    // Re-open to verify persistence
    ClienteDAO.Abrir(); CategoriaDAO.Abrir(); ProdutoDAO.Abrir();
    Console.WriteLine($"\nAfter reopen - Clientes: {ClienteDAO.Listar().Count}, Categorias: {CategoriaDAO.Listar().Count}, Produtos: {ProdutoDAO.Listar().Count}");
}
catch (Exception ex) {
    Console.WriteLine($"ERROR: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}