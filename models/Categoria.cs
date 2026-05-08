using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.models {
    internal class Categoria {
        private static int proximoId = 1;
        private int id;
        private string descricao;


        public Categoria(string descricao) {
            this.Id = proximoId++;
            this.Descricao = descricao;
        }


        public string Descricao { get => descricao; set => descricao = value; }
        public int Id { get => id; private set => id = value; }

        public override string ToString() {
            return $"ID: {Id} | Descricao: {Descricao}";
        }
    }
}
