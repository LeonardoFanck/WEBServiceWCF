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
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            
            return con;
        }

        public SqlConnection fecharConexao()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            
            return con;
        }

        public int timeOutSQL()
        {
            return 0;
        }

    }
}