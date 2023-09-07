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
                        "WHERE idOperador = @ID";

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
    }
}