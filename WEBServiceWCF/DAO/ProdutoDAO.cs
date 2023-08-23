using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;
using WEBServiceWCF.Exceptions;

namespace WEBServiceWCF.DAO
{
    public class ProdutoDAO
    {
        private static ConexaoDB conexao = new ConexaoDB();
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
                SqlDataReader retornoDB;

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
                    //produto = new Produto(-1, "NÃO ENCONTRADO", 0, 0, 0, false);
                    throw new ProdutoNaoLocalizadoException();
                }

                retornoDB.Close();
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
            SqlDataReader retornoDB;

            SQL = "SELECT MAX(CodProduto) AS ID FROM Produtos";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true) {
                retorno = Convert.ToInt32(retornoDB["ID"]);
            }
            else
            {
                retorno = -1; // COLOCAR UMA EXCEÇÃO DEPOIS
            }

            retornoDB.Close();
            con = conexao.fecharConexao();

            return retorno;
        }

        public int avancarRegistro(int IDProduto)
        {
            string SQL;
            int ID;
            SqlDataReader retornoDB;

            SQL = "SELECT TOP 1 * FROM Produtos WHERE CodProduto > @ID ORDER BY CodProduto";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand (SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            cmd.Parameters.AddWithValue("@ID", IDProduto);

            retornoDB = cmd.ExecuteReader();
            
            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                ID = Convert.ToInt32(retornoDB["CodProduto"]);
            }
            else
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                throw new Exception("Produto Não Localizado");
            }
            
            retornoDB.Close();
            con = conexao.fecharConexao();

            return ID;
        }

        public int voltarRegistro(int IDProduto)
        {
            string SQL;
            int ID;
            SqlDataReader retornoDB;

            SQL = "SELECT TOP 1 * FROM Produtos WHERE CodProduto < @ID ORDER BY CodProduto DESC";

            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            cmd.Parameters.AddWithValue("@ID", IDProduto);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                ID = Convert.ToInt32(retornoDB["CodProduto"]);
            }
            else
            {
                retornoDB.Close();
                con = conexao.fecharConexao();
                throw new Exception("Produto Não Localizado");
            }

            retornoDB.Close();
            con = conexao.fecharConexao();

            return ID;
        }

        public List<Categoria> GetCategorias()
        {
            string SQL;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            SqlDataReader retornoDB;
            List<Categoria> categorias = new List<Categoria>();

            SQL = "SELECT * FROM Categoria";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            while (retornoDB.HasRows == true && (retornoDB.Read() == true)) {
                categorias.Add(
                    new Categoria()
                    {
                        ID = Convert.ToInt32(retornoDB["IdCategoria"]),
                        Nome = retornoDB["NomeCategoria"].ToString(),
                        Status = Convert.ToBoolean(retornoDB["StatusCategoria"])
                    }
                );
            }

            retornoDB.Close();
            con = conexao.fecharConexao();

            return categorias;
        }

        public int getEstoque(int ID)
        {
            int retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand("VerificaEstoqueProduto", con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("RetornoOperacao", SqlDbType.Int, 1);
            cmd.Parameters["RetornoOperacao"].Direction = ParameterDirection.InputOutput;
            cmd.Parameters["RetornoOperacao"].Value = -1;

            cmd.Parameters.Add("IdProduto", SqlDbType.Int, 9999);
            cmd.Parameters["IdProduto"].Direction = ParameterDirection.Input;
            cmd.Parameters["IdProduto"].Value = ID;

            cmd.ExecuteNonQuery();

            retornoDB = Convert.ToInt32(cmd.Parameters["RetornoOperacao"].Value);

            con = conexao.fecharConexao();

            return retornoDB;
        }
    }
}