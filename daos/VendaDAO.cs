using crud_in_terminal_csharp.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.daos {
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
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == obj.Id) {
                    objetos[i] = obj;
                }
            }
        }

        public static void Excluir(Venda obj) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == obj.Id) {
                    objetos.RemoveAt(i);
                }
            }
        }
    }
}
