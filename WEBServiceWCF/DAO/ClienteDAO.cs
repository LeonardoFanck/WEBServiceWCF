using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
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


        // POSSIVELMENTE VOU TIRAR
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

            SQL = "SELECT TOP 1 IdCliente AS ID FROM CLientes WHERE IdCliente > @ID ORDER BY IdCliente";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);
            int retorno;
            cmd.CommandTimeout = conexao.timeOutSQL();

            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                retorno = Convert.ToInt32(retornoDB["ID"]);
            }
            else
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                throw new Exception("Cliente Não Localizado");
            }

            retornoDB.Close();
            con = conexao.fecharConexao();

            return retorno;
        }
        public int voltarRegistro(int ID)
        {
            string SQL;
            int retorno;
            SqlDataReader retornoDB;

            SQL = "SELECT TOP 1 IdCliente AS ID FROM CLientes WHERE IdCliente < @ID ORDER BY IdCliente DESC";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                retorno = Convert.ToInt32(retornoDB["ID"]);
            }
            else
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                throw new Exception("Cliente Não Localizado");
            }

            retornoDB.Close();
            con = conexao.fecharConexao();

            return retorno;
        }

        public int getProximoRegistro()
        {
            SqlDataReader retornoDB;
            string SQL;
            int retorno;
            SQL = "SELECT MAX(IdCliente)+1 AS ID FROM CLientes";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                retorno = Convert.ToInt32(retornoDB["ID"]);
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
                throw new Exception("Nenhum Produto Cadastrado");
            }

            con = conexao.fecharConexao();
            retornoDB.Close();

            return retorno;
        }

        public int salvarCliente(Cliente cliente, TipoClientes tipoCliente)
        {
            int retornoDB;
            string parametro;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand("adicionaCliente", con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("RetornoOperacao", SqlDbType.Int, 1);
            cmd.Parameters["RetornoOperacao"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["RetornoOperacao"].Value = -1;

            parametro = "CliNome";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 50);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetNome;

            parametro = "CPF";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 14);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetCPF;

            parametro = "EMAIL";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 50);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetEmail;

            parametro = "DtNasc";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 10);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetDtNascimento;

            parametro = "Estado";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 50);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetEstado;

            parametro = "Cidade";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 50);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetCidade;

            parametro = "Bairro";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 30);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetBairro;

            parametro = "Endereco";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 50);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetEndereco;

            parametro = "Numero";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 4);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetNumero;

            parametro = "Status";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetStatus;

            parametro = "Moradia";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetMoradia;

            parametro = "ID";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 20);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = cliente.getSetID;

            parametro = "TipoCliente";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = tipoCliente.getSetTipoCliente;

            parametro = "TipoFornecedor";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = tipoCliente.getSetTipoFornecedor;

            cmd.ExecuteNonQuery();

            retornoDB = Convert.ToInt32(cmd.Parameters["RetornoOperacao"].Value);

            con = conexao.fecharConexao();

            return retornoDB;
        }
    }
}