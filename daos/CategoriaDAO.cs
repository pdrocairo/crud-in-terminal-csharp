using crud_in_terminal_csharp.model;
using System;
using System.Collections.Generic;
using System.Text;

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
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == obj.Id) {
                    objetos[i] = obj;
                }
            }
        }

        public static void Excluir(Categoria obj) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == obj.Id) {
                    objetos.RemoveAt(i);
                }
            }
        }
    }
}
