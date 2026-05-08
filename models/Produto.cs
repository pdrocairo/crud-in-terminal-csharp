using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.models {
    internal class Produto {
        private static int proximoId = 1;
        private int id;
        private string descricao;
        private double preco;
        private int estoque;
        private int idCategoria;

        public Produto(string descricao, double preco, int estoque, int idCategoria) {
            this.Id = proximoId++;
            this.Descricao = descricao;
            this.Preco = preco;
            this.Estoque = estoque;
            this.IdCategoria = idCategoria;
        }


        public string Descricao { get => descricao; set => descricao = value; }
        public double Preco { get => preco; set => preco = value; }
        public int Estoque { get => estoque; set => estoque = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public int Id { get => id; private set => id = value; }

        public override string ToString() {
            return $"ID: {Id} | Descricao: {Descricao} | Preco: {Preco} | Estoque: {Estoque} | ID da Categoria: {IdCategoria}";
        }
    }
}
