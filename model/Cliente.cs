

namespace crud_in_terminal_csharp.model {
    class Cliente {

        private int id;
        private string nome;
        private string email;
        private string fone;

        public Cliente(int id, string nome, string email, string fone) {
            
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Fone = fone;
        }

        public int Id { get => id; set => id = value; }

        public string Nome { get => nome; set => nome = value; }

        public string Email { get => email; set => email = value; }

        public string Fone { get => fone; set => fone = value; }

        public override string ToString() {
            return $"ID: {Id} | Nome: {Nome} | E-mail: {Email}";
        }
    }
}
