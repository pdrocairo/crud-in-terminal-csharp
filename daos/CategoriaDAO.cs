using crud_in_terminal_csharp.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;

namespace crud_in_terminal_csharp.daos {
    internal class CategoriaDAO {
        private static List<Categoria> objetos = new List<Categoria>();

        public static void Inserir(Categoria obj) {
            objetos.Add(obj);
        }

        public static List<Categoria> Listar() {
            return objetos;
        }

        public static Categoria? Listar_Id(int id) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == id) {
                    return objetos[i];
                }
            }
            return null;
        }

        public static void Atualizar(Categoria obj) {
            Categoria? c = CategoriaDAO.Listar_Id(obj.Id);
            if (c != null) {
                CategoriaDAO.objetos.Remove(c);
                CategoriaDAO.objetos.Add(obj);
                CategoriaDAO.Salvar();
            }
        }


        public static void Excluir(Categoria obj) {
            Categoria? c = CategoriaDAO.Listar_Id(obj.Id);
            if (c != null) {
                CategoriaDAO.objetos.Remove(c);
                CategoriaDAO.Salvar();
            }
        }

        public static void Salvar() {
            string jsonString = JsonSerializer.Serialize(CategoriaDAO.objetos, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("categoria.json", jsonString);
        }



        public static void Abrir() {
            CategoriaDAO.objetos.Clear();

            if (File.Exists("categoria.json")) {
                string jsonString = File.ReadAllText("categoria.json");

                var itens = JsonSerializer.Deserialize<List<Categoria>>(jsonString);

                if (itens != null) {
                    CategoriaDAO.objetos.AddRange(itens);
                }
            }
        }
    }
}
