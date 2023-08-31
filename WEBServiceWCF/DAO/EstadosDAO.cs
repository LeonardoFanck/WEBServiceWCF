using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class EstadosDAO
    {
        private ConexaoDB conexao = new ConexaoDB();

        public EstadosDAO() { }

        public List<Estados> getListEstados()
        {
            string SQL;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            SqlDataReader retornoDB;
            List<Estados> estados = new List<Estados>();

            SQL = "SELECT * FROM Estados";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();
            if (retornoDB.HasRows == true)
            {
                while (retornoDB.HasRows == true && (retornoDB.Read() == true))
                {
                    estados.Add(
                        new Estados()
                        {
                            getSetID = Convert.ToInt32(retornoDB["IdEstados"]),
                            getSetNome = retornoDB["NomeEstado"].ToString(),
                            getSetUF = retornoDB["UF"].ToString()
                        }
                    );
                }

                retornoDB.Close();
                con = conexao.fecharConexao();

                return estados;
            }
            else
            {
                throw new Exception ("Nenhum Estado Encontrado!"); 
            }
        }
    }
}