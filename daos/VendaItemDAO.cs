using crud_in_terminal_csharp.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace crud_in_terminal_csharp.daos {
    internal class VendaItemDAO {
        private static List<VendaItem> objetos = new List<VendaItem>();

        public static void Inserir(VendaItem obj) {
            objetos.Add(obj);
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
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == obj.Id) {
                    objetos[i] = obj;
                }
            }
        }

        public static void Excluir(VendaItem obj) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == obj.Id) {
                    objetos.RemoveAt(i);
                }
            }
        }
    }
}
