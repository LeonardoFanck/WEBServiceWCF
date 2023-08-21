using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class ProdutoDAO
    {
        private static ConexaoDB conexao = new ConexaoDB();
        private SqlDataReader retornoDB;
        private int timeOutSQL = conexao.timeOutSQL();

        public Produto GetProduto(int IDPesquisa)
        {
            try
            {
                string SQL;
                int ID;
                string Nome;
                int Categoria;
                double Valor;
                double Custo;
                bool Status;
                Produto produto;

                SQL = "SELECT * FROM Produtos WHERE CodProduto = @ID";

                SqlConnection con = conexao.abrirConexao();
                SqlCommand cmd = new SqlCommand(SQL, con);

                cmd.Parameters.AddWithValue("@ID", IDPesquisa);

                retornoDB = cmd.ExecuteReader();

                if (retornoDB.HasRows == true && retornoDB.Read() == true)
                {
                    ID = Convert.ToInt32(retornoDB["CodProduto"]);
                    Nome = retornoDB["NomeProduto"].ToString();
                    Categoria = Convert.ToInt32(retornoDB["CategoriaProduto"]);
                    Valor = Convert.ToDouble(retornoDB["ValorProduto"]);
                    Custo = Convert.ToDouble(retornoDB["CustoProduto"]);
                    Status = Convert.ToBoolean(retornoDB["StatusProduto"]);

                    produto = new Produto(ID, Nome, Categoria, Valor, Custo, Status);
                }
                else
                {
                    // NÃO ENCONTROU REGISTRO
                    produto = new Produto(-1, "NÃO ENCONTRADO", 0, 0, 0, false);
                }

                con = conexao.fecharConexao();

                return produto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getIDProdutoInicial()
        {
            int retorno;
            string SQL;

            SQL = "SELECT MAX(CodProduto) AS ID FROM Produtos";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true) {
                retorno = Convert.ToInt32(retornoDB["ID"]);
            }
            else
            {
                retorno = -1;
            }

            con = conexao.fecharConexao();

            return retorno;
        }
    }
}