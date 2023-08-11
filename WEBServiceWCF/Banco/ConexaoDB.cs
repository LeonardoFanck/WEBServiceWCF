using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Banco
{
    public class ConexaoDB
    {
        private SqlConnection con = new SqlConnection();

        public ConexaoDB()
        {
            con.ConnectionString = @"Data Source=LEO\LEONARDODB;Initial Catalog=LEONARDODB;User ID=sa;Password=sae";
        }

        public SqlConnection abrirConexao()
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return con;
        }

        public SqlConnection fecharConexao()
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return con;
        }


    }
}