using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace crud_in_terminal_csharp.model {
    class VendaItem {
        private int id;
        private int quantidade;
        private double preco;
        private int idVenda;
        private int idProduto;


        public VendaItem(int id, int quantidade, double preco, int idVenda, int idProduto) {

            this.Id = id;
            this.Quantidade = quantidade;
            this.Preco = preco;
            this.IdVenda = idVenda;
            this.IdProduto = idProduto;
        }

        public int Id { get => id; set => id = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }
        public double Preco { get => preco; set => preco = value; }
        public int IdVenda { get => idVenda; set => idVenda = value; }
        public int IdProduto { get => idProduto; set => idProduto = value; }

        public override string ToString() {
            return $"ID: {Id} | Quantidade: {Quantidade} | Preco: {Preco} | ID da Venda: {IdVenda} | ID do Produto: {IdProduto}";
        }
    }
}
