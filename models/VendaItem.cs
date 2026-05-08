using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.models {
    internal class VendaItem {
        private static int proximoId = 1;
        private int id;
        private int quantidade;
        private double preco;
        private int idVenda;
        private int idProduto;


        public VendaItem(int quantidade, double preco, int idVenda, int idProduto) {

            this.Id = proximoId++;
            this.Quantidade = quantidade;
            this.Preco = preco;
            this.IdVenda = idVenda;
            this.IdProduto = idProduto;
        }

        public int Quantidade { get => quantidade; set => quantidade = value; }
        public double Preco { get => preco; set => preco = value; }
        public int IdVenda { get => idVenda; set => idVenda = value; }
        public int IdProduto { get => idProduto; set => idProduto = value; }
        public int Id { get => id; private set => id = value; }

        public override string ToString() {
            return $"ID: {Id} | Quantidade: {Quantidade} | Preco: {Preco} | ID da Venda: {IdVenda} | ID do Produto: {IdProduto}";
        }
    }
}
