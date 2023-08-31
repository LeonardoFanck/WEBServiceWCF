using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class TipoClientesDAO
    {
        private ConexaoDB conexao = new ConexaoDB();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader retornoDB;
        private TipoClientes tipoClientes;
        public string mensagem = "";

        public TipoClientes getTipoClientes(int ID)
        {
            cmd.CommandText = "SELECT * FROM TipoClientes WHERE IdCliente = @ID";
            cmd.Parameters.AddWithValue("@ID", ID);

            cmd.Connection = conexao.abrirConexao();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                tipoClientes = new TipoClientes(
                    Convert.ToBoolean(retornoDB["TipoCliente"]),
                    Convert.ToBoolean(retornoDB["TipoFornecedor"])
                    );

                retornoDB.Close();
                conexao.fecharConexao();

                return tipoClientes;
            }
            else
            {
                retornoDB.Close();
                conexao.fecharConexao();
                throw new Exception("Nenhum tipo localizado!");
            }
        }
    }
}