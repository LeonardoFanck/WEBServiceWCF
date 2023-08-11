using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class ClienteDAO
    {
        private ConexaoDB conexao = new ConexaoDB();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader retornoDB;
        public string mensagem = "";

        public string clienteGetNome(int id)
        {
            string nome = "";

            cmd.CommandText = "SELECT IdCliente, CliNome FROM Clientes WHERE IdCliente = @id";
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.Connection = conexao.abrirConexao();
                retornoDB = cmd.ExecuteReader();

                if (retornoDB.HasRows == true && retornoDB.Read() == true)
                {
                    nome = retornoDB["CliNome"].ToString();
                }
                else
                {
                    nome = "Não encontrado";
                }

                return nome;
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                // FINALIZA TUDO
                retornoDB.Close();
                conexao.fecharConexao();
            }
        }

        public List<Cliente> PegaTodosClientes()
        {
            var clientes = new List<Cliente>();
            cmd.CommandText = "SELECT IdCliente, CliNome, CliCPF FROM Clientes";

            try
            {
                cmd.Connection = conexao.abrirConexao();
                retornoDB = cmd.ExecuteReader();

                while (retornoDB.HasRows == true && retornoDB.Read() == true)
                {
                    clientes.Add(
                        new Cliente()
                        {
                            ID = (int)retornoDB["IdCliente"],
                            Nome = retornoDB["CliNome"].ToString(),
                            CPF = retornoDB["CliCPF"].ToString()
                        });
                }

                return clientes;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                retornoDB.Close();
                conexao.fecharConexao();
            }
        }
    }
}