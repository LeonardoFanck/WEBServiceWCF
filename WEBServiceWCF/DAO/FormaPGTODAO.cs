using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class FormaPGTODAO
    {
        private ConexaoDB conexao = new ConexaoDB();

        public FormaPGTO GetFormaPGTO(int ID)
        {
            SqlConnection con = conexao.abrirConexao();
            string SQL;
            SqlCommand cmd;
            SqlDataReader retornoDB;
            SQL = "SELECT * FROM FormaPgto WHERE IdFormaPgt = @ID";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                FormaPGTO formaPGTO = new FormaPGTO(
                    Convert.ToInt32(retornoDB["IdFormaPgt"]),
                    retornoDB["NomeFormaPgt"].ToString(),
                    Convert.ToBoolean(retornoDB["StatusFormaPgt"])
                    );

                con = conexao.fecharConexao();
                retornoDB.Close();

                return formaPGTO;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
                throw new Exception("Nenhuma Forma de Pagamento localizada");
            }
        }

        public FormaPGTO getRegistroInicial()
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT MAX(IdFormaPgt) FROM FormaPgto";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                FormaPGTO formaPGTO = new FormaPGTO(
                    Convert.ToInt32(retornoDB["IdFormaPgt"]),
                    retornoDB["NomeFormaPgt"].ToString(),
                    Convert.ToBoolean(retornoDB["StatusFormaPgt"])
                    );

                con = conexao.fecharConexao();
                retornoDB.Close();

                return formaPGTO;
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

            SQL = "SELECT TOP 1 IdFormaPgt " +
                "FROM FormaPgto " +
                "WHERE IdFormaPgt > @ID " +
                "ORDER BY IdFormaPgt";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                int IDretorno = Convert.ToInt32(retornoDB["IdFormaPgt"]);
                
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

            SQL = "SELECT TOP 1 IdFormaPgt " +
                "FROM FormaPgto " +
                "WHERE IdFormaPgt < @ID " +
                "ORDER BY IdFormaPgt DESC";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                int IDretorno = Convert.ToInt32(retornoDB["IdFormaPgt"]);

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

        public int salvarRegistro(FormaPGTO formaPGTO)
        {
            // SALVAR -> SP => cadastroFormaPGTO
            string parametro;
            int retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand("cadastroFormaPGTO", con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.CommandType = CommandType.StoredProcedure;

            parametro = "RetornoOperacao";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.InputOutput;
            cmd.Parameters[parametro].Value = -1;

            parametro = "IdFormaPGTO";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = formaPGTO.getSetID;

            parametro = "NomeFormaPGTO";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 50);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = formaPGTO.getSetNome;

            parametro = "StatusFormaPGTO";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = formaPGTO.getSetStatus;

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