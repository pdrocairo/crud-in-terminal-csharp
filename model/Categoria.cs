using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.model {
    class Categoria {
        private static int proximoId = 1;
        private int id;
        private string descricao;

       
        public Categoria(string descricao) {
            this.id = proximoId++;
            this.Descricao = descricao;
        }

        public int Id { get => id; }
        public string Descricao { get => descricao; set => descricao = value; }

        public override string ToString() {
            return $"ID: {Id} | Descricao: {Descricao}";
        }
    }
}
