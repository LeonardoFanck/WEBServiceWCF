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

            cmd.CommandText = "SELECT CliNome FROM Clientes WHERE IdCliente = @id";
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

                retornoDB.Close();
                conexao.fecharConexao();

                return nome;
            }
            catch (SqlException e)
            {
                retornoDB.Close();
                conexao.fecharConexao();
                throw;
            }
        }

        public List<Cliente> PegaTodosClientes()
        {
            var clientes = new List<Cliente>();
            cmd.CommandText = "SELECT *, convert(varchar, CliDtNascimento, 103) AS DtNasc FROM Clientes";

            try
            {
                cmd.Connection = conexao.abrirConexao();
                retornoDB = cmd.ExecuteReader();

                while (retornoDB.HasRows == true && retornoDB.Read() == true)
                {
                    clientes.Add(
                        new Cliente(
                            Convert.ToInt32(retornoDB["IdCliente"]),
                            retornoDB["CliNome"].ToString(),
                            retornoDB["CliCPF"].ToString(),
                            retornoDB["CliEmail"].ToString(),
                            retornoDB["DtNasc"].ToString(),
                            retornoDB["CliEstado"].ToString(),
                            retornoDB["CliCidade"].ToString(),
                            retornoDB["CliBairro"].ToString(),
                            retornoDB["CliEndereco"].ToString(),
                            retornoDB["CliNumero"].ToString(),
                            Convert.ToBoolean(retornoDB["CliStatus"]),
                            Convert.ToInt32(retornoDB["Moradia"])
                            ));
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

        public Cliente getCliente(int ID)
        {
            string SQL;
            SqlConnection sqlConnection = conexao.abrirConexao();
            SqlCommand cmd;
            SqlDataReader retornoDB;

            SQL = "SELECT *, convert(varchar, CliDtNascimento, 103) AS DtNasc FROM Clientes WHERE IdCliente = @ID";
            
            cmd = new SqlCommand(SQL, sqlConnection);
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();
            
            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                Cliente cliente = new Cliente(
                    Convert.ToInt32(retornoDB["IdCliente"]),
                    retornoDB["CliNome"].ToString(),
                    retornoDB["CliCPF"].ToString(),
                    retornoDB["CliEmail"].ToString(),
                    retornoDB["DtNasc"].ToString(),
                    retornoDB["CliEstado"].ToString(),
                    retornoDB["CliCidade"].ToString(),
                    retornoDB["CliBairro"].ToString(),
                    retornoDB["CliEndereco"].ToString(),
                    retornoDB["CliNumero"].ToString(),
                    Convert.ToBoolean(retornoDB["CliStatus"]),
                    Convert.ToInt32(retornoDB["Moradia"])
                    );

                return cliente;
            }
            else
            {
                throw new Exception("Nenhum cliente localizado!");
            }
        }
        
        public int getUltimoRegistroID()
        {
            int retorno;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT MAX(IdCliente) AS ID FROM CLientes";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                retorno = Convert.ToInt32(retornoDB["ID"]);

                retornoDB.Close();
                con = conexao.fecharConexao();

                return retorno;
            }
            else
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                throw new Exception("Nenhum Cliente Localizado"); // COLOCAR UMA EXCEÇÃO DEPOIS
            }
        }

        public int avancarRegistro(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT TOP 1 IdCliente FROM CLientes WHERE IdCliente > @ID ORDER BY IdCliente";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                return Convert.ToInt32(retornoDB["CodProduto"]);
            }
            else
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                throw new Exception("Cliente Não Localizado");
            }
        }
        public int voltarRegistro(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT TOP 1 IdCliente FROM CLientes WHERE IdCliente < @ID ORDER BY IdCliente DESC";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                return Convert.ToInt32(retornoDB["CodProduto"]);
            }
            else
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                throw new Exception("Cliente Não Localizado");
            }
        }

        public int getProximoRegistro()
        {
            SqlDataReader retornoDB;
            string SQL;
            SQL = "SELECT MAX(IdCliente)+1 AS ID FROM CLientes";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
                return Convert.ToInt32(retornoDB["ID"]);
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
                throw new Exception("Nenhum Produto Cadastrado");
            }
        }
    }
}