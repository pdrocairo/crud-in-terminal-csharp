using crud_in_terminal_csharp.model;

using System;

namespace crud_in_terminal_csharp.daos {
    class ClienteDAO {
        private static List<Cliente> objetos = [];

        public static void InserirCliente(Cliente cliente)
        {
            objetos.Add(cliente);
        }

        public static List<Cliente> ListarClientes()
        {
            return objetos;
        }

        public static bool AtualizarClientes(Cliente cliente)
        {
            for (int i = 0; i < objetos.Count; i++)
            {
                if (objetos[i].Id == cliente.Id)
                {
                    objetos[i] = cliente;
                    return true;
                }

                
            }
            return false;
        }

        public static bool ExcluirCliente(Cliente cliente)
        {
            for (int i = 0; i < objetos.Count; i++)
            {
                if (objetos[i].Id == cliente.Id)
                {
                    objetos.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}

