using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace crud_in_terminal_csharp.models {
    internal class ProdutoDAO {
        private static List<Produto> objetos = new List<Produto>();

        public static void Inserir(Produto obj) {
            objetos.Add(obj);
        }

        public static List<Produto> Listar() {
            return objetos;
        }

        public static Produto? Listar_Id(int id) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == id) {
                    return objetos[i];
                }
            }
            return null;
        }

        public static void Atualizar(Produto obj) {
            Produto? c = ProdutoDAO.Listar_Id(obj.Id);
            if (c != null) {
                ProdutoDAO.objetos.Remove(c);
                ProdutoDAO.objetos.Add(obj);
                ProdutoDAO.Salvar();
            }
        }


        public static void Excluir(Produto obj) {
            Produto? c = ProdutoDAO.Listar_Id(obj.Id);
            if (c != null) {
                ProdutoDAO.objetos.Remove(c);
                ProdutoDAO.Salvar();
            }
        }

        public static void Salvar() {
            string jsonString = JsonSerializer.Serialize(ProdutoDAO.objetos, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("produto.json", jsonString);
        }

        public static void Abrir() {
            ProdutoDAO.objetos.Clear();

            if (File.Exists("produto.json")) {
                string jsonString = File.ReadAllText("produto.json");

                var itens = JsonSerializer.Deserialize<List<Produto>>(jsonString);

                if (itens != null) {
                    ProdutoDAO.objetos.AddRange(itens);
                }
            }
        }
    }
}
