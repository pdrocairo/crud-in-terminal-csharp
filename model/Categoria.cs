using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.model {
    class Categoria {
        private int id;
        private int descricao;


        public Categoria(int id, int descricao) {
            this.Descricao = descricao;
            this.Id = id;

        }

        public int Id { get => id; set => id = value; }
        public int Descricao { get => descricao; set => descricao = value; }

        public override string ToString() {
            return $"ID: {Id} | Descricao: {Descricao}";
        }
    }
}
