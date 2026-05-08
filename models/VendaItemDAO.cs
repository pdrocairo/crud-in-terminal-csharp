using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace crud_in_terminal_csharp.models {
    internal class VendaItemDAO {
        private static List<VendaItem> objetos = new List<VendaItem>();

        public static void Inserir(VendaItem obj) {
            objetos.Add(obj);
            Salvar();
        }

        public static List<VendaItem> Listar() {
            return objetos;
        }

        public static VendaItem? Listar_Id(int id) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == id) {
                    return objetos[i];
                }
            }
            return null;
        }

        public static void Atualizar(VendaItem obj) {
            VendaItem? c = VendaItemDAO.Listar_Id(obj.Id);
            if (c != null) {
                VendaItemDAO.objetos.Remove(c);
                VendaItemDAO.objetos.Add(obj);
                VendaItemDAO.Salvar();
            }
        }


        public static void Excluir(VendaItem obj) {
            VendaItem? c = VendaItemDAO.Listar_Id(obj.Id);
            if (c != null) {
                VendaItemDAO.objetos.Remove(c);
                VendaItemDAO.Salvar();
            }
        }

        public static void Salvar() {
            string jsonString = JsonSerializer.Serialize(VendaItemDAO.objetos, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("vendaitem.json", jsonString);
        }

        public static void Abrir() {
            VendaItemDAO.objetos.Clear();

            if (File.Exists("vendaitem.json")) {
                string jsonString = File.ReadAllText("vendaitem.json");

                var itens = JsonSerializer.Deserialize<List<VendaItem>>(jsonString);

                if (itens != null) {
                    VendaItemDAO.objetos.AddRange(itens);
                }
            }
        }

        public static List<VendaItem> ObterPorVenda(int vendaId) {
            return objetos.Where(i => i.IdVenda == vendaId).ToList();
        }

        public static List<VendaItem> ObterPorProduto(int produtoId) {
            return objetos.Where(i => i.IdProduto == produtoId).ToList();
        }

        public static Produto? ObterProdutoDoItem(VendaItem item) {
            return ProdutoDAO.Listar_Id(item.IdProduto);
        }

        public static Venda? ObterVendaDoItem(VendaItem item) {
            return VendaDAO.Listar_Id(item.IdVenda);
        }
    }
}
