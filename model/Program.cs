

namespace crud_in_terminal_csharp.model {
    class Program {
        static void Main() {
            Cliente c1 = new Cliente(1,"Pedro","email.com","123-321");
            Categoria cat1 = new Categoria(1, 3);
            Console.WriteLine($"{c1}");
            Console.WriteLine($"{cat1}");
        }
    }
}
