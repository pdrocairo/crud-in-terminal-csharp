using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace crud_in_terminal_csharp.models {
    internal class ClienteDAO {
        public static List<Cliente> objetos = new List<Cliente>();

        public static void Inserir(Cliente cliente) {
            objetos.Add(cliente);
        }

        public static List<Cliente> Listar() {
            return objetos;
        }

        public static Cliente? Listar_Id(int id) {
            for (int i = 0; i < objetos.Count; i++) {
                if (objetos[i].Id == id) {
                    return objetos[i];
                }
            }
            return null;
        }

        public static void Atualizar(Cliente obj) {
            Cliente? c = ClienteDAO.Listar_Id(obj.Id);
            if (c != null) {
                ClienteDAO.objetos.Remove(c);
                ClienteDAO.objetos.Add(obj);
                ClienteDAO.Salvar();
            }
        }


        public static void Excluir(Cliente obj) {
            Cliente? c = ClienteDAO.Listar_Id(obj.Id);
            if (c != null) {
                ClienteDAO.objetos.Remove(c);
                ClienteDAO.Salvar();
            }
        }

        public static void Salvar() {
            string jsonString = JsonSerializer.Serialize(ClienteDAO.objetos, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("cliente.json", jsonString);
        }



        public static void Abrir() {
            ClienteDAO.objetos.Clear();

            if (File.Exists("cliente.json")) {
                string jsonString = File.ReadAllText("cliente.json");

                var itens = JsonSerializer.Deserialize<List<Cliente>>(jsonString);

                if (itens != null) {
                    ClienteDAO.objetos.AddRange(itens);
                }
            }
        }
    }
}
