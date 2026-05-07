using crud_in_terminal_csharp.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.daos {
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
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == obj.Id) {
                    objetos[i] = obj;
                }
            }
        }

        public static void Excluir(Produto obj) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == obj.Id) {
                    objetos.RemoveAt(i);
                }
            }
        }
    }
}
