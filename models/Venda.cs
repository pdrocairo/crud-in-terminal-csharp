using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.models {
    internal class Venda {
        private static int proximoId = 1;
        private int id;
        private DateTime data;
        private bool carrinho;
        private double total;
        private int idCliente;

        public Venda(DateTime data, bool carrinho, double total, int idCliente) {
            this.Id = proximoId++;
            this.Data = data;
            this.Carrinho = carrinho;
            this.Total = total;
            this.IdCliente = idCliente;

        }

        public int Id { get => id; private set => id = value; }

        public DateTime Data { get => data; set => data = value; }

        public bool Carrinho { get => carrinho; set => carrinho = value; }

        public double Total { get => total; set => total = value; }

        public int IdCliente { get => idCliente; set => idCliente = value; }

        public override string ToString() {
            return $"ID: {Id} | Id do Cliente: {IdCliente} | Data: {Data} | Carrinho: {Carrinho} | Total: {Total}";
        }
    }
}
