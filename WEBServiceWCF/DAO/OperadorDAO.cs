using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;

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

        public string getNomeOperador(int ID)
        {
            SqlDataReader retornoDB;

            try
            {
                string retorno = "";
                string SQL = "SELECT nomeOperador " +
                             "FROM Operador " +
                             "WHERE idOperador = @ID";

                SqlConnection con = conexao.abrirConexao();
                SqlCommand cmd = new SqlCommand(SQL, con);
                
                cmd.Parameters.AddWithValue("@ID", ID);

                retornoDB = cmd.ExecuteReader();

                if (retornoDB.HasRows == true && retornoDB.Read() == true)
                {
                    retorno = retornoDB["nomeOperador"].ToString();
                }

                con = conexao.fecharConexao();

                return retorno;
            }
            catch (Exception)
            {
                return "Erro ao tentar pegar o nome do Operador!";
                throw;
            }
        }
    }
}