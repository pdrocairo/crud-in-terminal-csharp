namespace crud_in_terminal_csharp.model {
    class Cliente {

        private static int proximoId = 1;
        private int id;
        private string nome;
        private string email;
        private string fone;
        private string senha;

        
        public Cliente(string nome, string email, string fone, string senha) {
            this.id = proximoId++;
            this.Nome = nome;
            this.Email = email;
            this.Fone = fone;
            this.Senha = senha;
        }

        public int Id { get => id; }

        public string Nome { get => nome; set => nome = value; }

        public string Email { get => email; set => email = value; }

        public string Fone { get => fone; set => fone = value; }
        public string Senha { get => senha; set => senha = value; }

        public override string ToString() {
            return $"ID: {Id} | Nome: {Nome} | E-mail: {Email} | Senha: {Senha}";
        }
    }
}
