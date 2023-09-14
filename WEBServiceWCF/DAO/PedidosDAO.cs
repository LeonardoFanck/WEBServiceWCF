using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Security.Tokens;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class PedidosDAO
    {
        private ConexaoDB conexao = new ConexaoDB();

        public Pedido getPedido(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "SELECT * FROM Pedido WHERE IdPedidos = @ID";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read()  == true)
            {
                Pedido pedido = new Pedido(
                    Convert.ToInt32(retornoDB["IdPedidos"]),
                    Convert.ToInt32(retornoDB["PedidoIdCli"]),
                    Convert.ToInt32(retornoDB["PedidoIdPgto"]),
                    Convert.ToDouble(retornoDB["PedidoValor"]),
                    Convert.ToDouble(retornoDB["PedidoDesconto"]),
                    Convert.ToDouble(retornoDB["PedidoValorTotal"]),
                    retornoDB["PedidoData"].ToString()
                    );

                con = conexao.fecharConexao();
                retornoDB.Close();

                return pedido;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Registro N° " + ID + " não localizado!");
            }
        }

        public List<PedidoItens> getPedidoItens(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "SELECT Itens.*, Produtos.NomeProduto " +
                "FROM ItensPedido AS Itens " +
                "INNER JOIN Produtos ON Itens.CodProduto = Produtos.CodProduto " +
                "WHERE Itens.CodPedido = @ID";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            
                var Itens = new List<PedidoItens>();

                while (retornoDB.HasRows == true && retornoDB.Read() == true)
                {
                    Itens.Add(new PedidoItens(
                    Convert.ToInt32(retornoDB["CodItensPedido"]),
                    Convert.ToInt32(retornoDB["CodPedido"]),
                    Convert.ToInt32(retornoDB["CodProduto"]),
                    Convert.ToDouble(retornoDB["ValorProduto"]),
                    Convert.ToInt32(retornoDB["QuantidadeProduto"]),
                    Convert.ToDouble(retornoDB["DescontoProduto"]),
                    Convert.ToDouble(retornoDB["ValorTotalProduto"]),
                    retornoDB["NomeProduto"].ToString()
                    ));
                }

                con = conexao.fecharConexao();
                retornoDB.Close();

                return Itens;
            
        }

        public int getRegistroInicial()
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT MAX(IdPedidos) AS ID FROM Pedido";
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

            SQL = "SELECT MAX(IdPedidos)+1 AS ID FROM Pedido";
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

            SQL = "SELECT TOP 1 IdPedidos AS ID " +
                "FROM Pedido " +
                "WHERE IdPedidos > @ID " +
                "ORDER BY IdPedidos";

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

            SQL = "SELECT TOP 1 IdPedidos AS ID " +
                "FROM Pedido " +
                "WHERE IdPedidos < @ID " +
                "ORDER BY IdPedidos DESC";

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

        public void salvarItensPedido(PedidoItens itens)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "INSERT INTO ItensPedido (CodPedido, CodProduto, ValorProduto, QuantidadeProduto, DescontoProduto, ValorTotalProduto) " +
                "VALUES (@Pedido, @Produto, @Valor, @Quantidade, @Desconto, @ValorTotal)";

            cmd = new SqlCommand(SQL, con);

            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@Pedido", itens.getSetPedidoID);
            cmd.Parameters.AddWithValue("@Produto", itens.getSetProduto);
            cmd.Parameters.AddWithValue("@Valor", itens.getSetItemValor);
            cmd.Parameters.AddWithValue("@Quantidade", itens.getSetQuantidade);
            cmd.Parameters.AddWithValue("@Desconto", itens.getSetItemDesconto);
            cmd.Parameters.AddWithValue("@ValorTotal", itens.getSetItemValorTotal);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            { 
                con = conexao.fecharConexao();
                retornoDB.Close();
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Ocorreu um erro ao tentar inserir o Item!");
            }
        }

        public void excluirItemPedido(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "DELETE FROM ItensPedido " +
                "WHERE CodItensPedido = @ID";

            cmd = new SqlCommand(SQL, con);

            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Ocorreu um erro ao tentar excluir o Item!");
            }
        }

        public void excluirItensPedido(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "DELETE FROM ItensPedido " +
                "WHERE CodPedido = @ID";

            cmd = new SqlCommand(SQL, con);

            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Ocorreu um erro ao tentar excluir o Item!");
            }
        }
    }
}