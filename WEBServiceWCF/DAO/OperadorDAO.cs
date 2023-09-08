using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class OperadorDAO
    {
        private static ConexaoDB conexao = new ConexaoDB();

        public int VerificaOperador(int ID)
        {
            int retorno;
            try
            {
                SqlConnection con = conexao.abrirConexao();
                SqlCommand cmd = new SqlCommand("VerificaOperador", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("RetornoOperacao", SqlDbType.Int, 1);
                cmd.Parameters["RetornoOperacao"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["RetornoOperacao"].Value = -1;
                cmd.Parameters.Add("ID", SqlDbType.Int, 1);
                cmd.Parameters["ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["ID"].Value = ID;

                cmd.ExecuteNonQuery();

                retorno = Convert.ToInt32(cmd.Parameters["RetornoOperacao"].Value);

                con = conexao.fecharConexao();

                return retorno;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public int VerificaLogin(int ID, int senha)
        {
            int retorno;
            try
            {
                SqlConnection con = conexao.abrirConexao();
                SqlCommand cmd = new SqlCommand("VerificarLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("RetornoOperacao", SqlDbType.Int, 1);
                cmd.Parameters["RetornoOperacao"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["RetornoOperacao"].Value = -1;

                cmd.Parameters.Add("ID", SqlDbType.Int, 1);
                cmd.Parameters["ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["ID"].Value = ID;

                cmd.Parameters.Add("SENHA", SqlDbType.Int, 4);
                cmd.Parameters["SENHA"].Direction = ParameterDirection.Input;
                cmd.Parameters["SENHA"].Value = senha;

                cmd.ExecuteNonQuery();

                retorno = Convert.ToInt32(cmd.Parameters["RetornoOperacao"].Value);

                con = conexao.fecharConexao();

                return retorno;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public Operador getOperador(int ID)
        {
            SqlDataReader retornoDB;

            
            string SQL = "SELECT * " +
                        "FROM Operador " +
                        "INNER JOIN OperadorPermissaoTela ON Operador.IdOperador = OperadorPermissaoTela.IdOperador " +
                        "WHERE Operador.idOperador = @ID";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
                
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                Operador operador = new Operador(
                    Convert.ToInt32(retornoDB["IdOperador"]),
                    retornoDB["nomeOperador"].ToString(),
                    retornoDB["senhaOperador"].ToString(),
                    Convert.ToBoolean(retornoDB["adminOperador"]),
                    Convert.ToBoolean(retornoDB["statusOperador"]),
                    Convert.ToBoolean(retornoDB["CadastroOperador"]),
                    Convert.ToBoolean(retornoDB["CadastroCategoria"]),
                    Convert.ToBoolean(retornoDB["CadastroCliente"]),
                    Convert.ToBoolean(retornoDB["CadastroProduto"]),
                    Convert.ToBoolean(retornoDB["CadastroFormaPGTO"]),
                    Convert.ToBoolean(retornoDB["TabelaUsuario"]),
                    Convert.ToBoolean(retornoDB["Pedidos"]),
                    Convert.ToBoolean(retornoDB["Entrada"])
                    );

                con = conexao.fecharConexao();
                retornoDB.Close();

                return operador;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Nenhum Operador Encontrado!");
            }
        }

        public int getRegistroInicial()
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT MAX(IdOperador) AS ID FROM Operador";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                int retorno = Convert.ToInt32(retornoDB["ID"]);

                con = conexao.fecharConexao();
                retornoDB.Close();

                return retorno;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
                throw new Exception("Nenhum registro Localizado");
            }
        }

        public int getProximoRegistro()
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT MAX(IdOperador)+1 AS ID FROM Operador";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                int retorno = Convert.ToInt32(retornoDB["ID"]);

                con = conexao.fecharConexao();
                retornoDB.Close();

                return retorno;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
                throw new Exception("Nenhum registro Localizado");
            }
        }

        public int avancarRegistro(int ID)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT TOP 1 IdOperador AS ID " +
                "FROM Operador " +
                "WHERE IdOperador > @ID " +
                "ORDER BY IdOperador";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                int IDretorno = Convert.ToInt32(retornoDB["ID"]);

                con = conexao.fecharConexao();
                retornoDB.Close();

                return IDretorno;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Nenhum registro localizado posteriormente ao ID: " + ID);
            }
        }

        public int voltarRegistro(int ID)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT TOP 1 IdOperador AS ID " +
                "FROM Operador " +
                "WHERE IdOperador < @ID " +
                "ORDER BY IdOperador DESC";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                int IDretorno = Convert.ToInt32(retornoDB["ID"]);

                con = conexao.fecharConexao();
                retornoDB.Close();

                return IDretorno;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Nenhum registro localizado anteriormente ao ID: " + ID);
            }
        }

        public int salvarRegistro(Operador operador)
        {
            // SALVAR -> SP => cadastroOperador
            string parametro;
            int retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand("cadastroOperador", con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.CommandType = CommandType.StoredProcedure;

            parametro = "RetornoOperacao";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.InputOutput;
            cmd.Parameters[parametro].Value = -1;

            parametro = "ID";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetID;

            parametro = "Nome";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 50);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetNome;

            parametro = "Senha";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 4);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetSenha;

            parametro = "Admin";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetAdmin;

            parametro = "Status";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetStatus;

            parametro = "TelaCadOperador";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetCadastroOperador;

            parametro = "TelaCadCategoria";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetCadastroCategoria;

            parametro = "TelaCadCliente";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetCadastroCliente;

            parametro = "TelaCadProduto";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetCadastroProduto;

            parametro = "TelaCadFormaPGTO";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetCadastroFormaPGTO;

            parametro = "TelaTabelaUsuario";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetTabelaUsuario;

            parametro = "TelaPedidos";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetPedidos;

            parametro = "TelaEntrada";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = operador.getSetEntrada;

            cmd.ExecuteNonQuery();

            retornoDB = Convert.ToInt32(cmd.Parameters["RetornoOperacao"].Value);

            if (retornoDB != 2)
            {
                con = conexao.fecharConexao();

                return retornoDB;
            }
            else
            {
                throw new Exception("Ocorreu um erro ao tentar Finalizar o processo!");
            }
        }
    }
}