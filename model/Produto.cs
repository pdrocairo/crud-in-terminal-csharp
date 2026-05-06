using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.model {
    class Produto {
        private int id;
        private string descricao;
        private double preco;
        private int estoque;
        private int idCategoria;


        public Produto(int id, string descricao, double preco, int estoque, int idCategoria) {
            this.Id = id;
            this.Descricao = descricao;
            this.Preco = preco;
            this.Estoque = estoque;
            this.IdCategoria = idCategoria;
        }

        public int Id { get => id; set => id = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public double Preco { get => preco; set => preco = value; }
        public int Estoque { get => estoque; set => estoque = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }

        public override string ToString() {
            return $"ID: {Id} | Descricao: {Descricao} | Preco: {Preco} | Estoque: {Estoque} | ID da Categoria: {IdCategoria}";
        }
    }
}
