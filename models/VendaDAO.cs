using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace crud_in_terminal_csharp.models {
    internal class VendaDAO {
        private static List<Venda> objetos = new List<Venda>();

        public static void Inserir(Venda obj) {
            objetos.Add(obj);
        }

        public static List<Venda> Listar() {
            return objetos;
        }

        public static Venda? Listar_Id(int id) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == id) {
                    return objetos[i];
                }
            }
            return null;
        }

        public static void Atualizar(Venda obj) {
            Venda? c = VendaDAO.Listar_Id(obj.Id);
            if (c != null) {
                VendaDAO.objetos.Remove(c);
                VendaDAO.objetos.Add(obj);
                VendaDAO.Salvar();
            }
        }


        public static void Excluir(Venda obj) {
            Venda? c = VendaDAO.Listar_Id(obj.Id);
            if (c != null) {
                VendaDAO.objetos.Remove(c);
                VendaDAO.Salvar();
            }
        }

        public static void Salvar() {
            string jsonString = JsonSerializer.Serialize(VendaDAO.objetos, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("venda.json", jsonString);
        }

        public static void Abrir() {
            VendaDAO.objetos.Clear();

            if (File.Exists("venda.json")) {
                string jsonString = File.ReadAllText("venda.json");

                var itens = JsonSerializer.Deserialize<List<Venda>>(jsonString);

                if (itens != null) {
                    VendaDAO.objetos.AddRange(itens);
                }
            }
        }
    }
}
