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
    public class EntradasDAO
    {
        private ConexaoDB conexao = new ConexaoDB();

        public Entrada getEntrada(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "SELECT * FROM Entrada WHERE IdEntrada = @ID";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                Entrada entrada = new Entrada(
                    Convert.ToInt32(retornoDB["IdEntrada"]),
                    retornoDB["Data"].ToString(),
                    Convert.ToInt32(retornoDB["EntradaIdCli"]),
                    Convert.ToInt32(retornoDB["EntradaIdFormaPgto"]),
                    Convert.ToDouble(retornoDB["EntradaCusto"]),
                    Convert.ToDouble(retornoDB["EntradaDesconto"]),
                    Convert.ToDouble(retornoDB["EntradaCustoTotal"])
                    );

                con = conexao.fecharConexao();
                retornoDB.Close();

                return entrada;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Registro N° " + ID + " não localizado!");
            }
        }

        public List<EntradaItens> getEntradaItens(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "SELECT Itens.*, Prod.NomeProduto " +
                "FROM ItensEntrada AS Itens " +
                "INNER JOIN Produtos AS Prod ON Itens.IdProduto = Prod.CodProduto " +
                $"WHERE Itens.IdEntrada = {ID}";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            var Itens = new List<EntradaItens>();

            while (retornoDB.HasRows == true && retornoDB.Read() == true)
            {				
                Itens.Add(new EntradaItens(
                Convert.ToInt32(retornoDB["IdItensEntrada"]),
                Convert.ToInt32(retornoDB["IdEntrada"]),
                Convert.ToInt32(retornoDB["IdProduto"]),
                Convert.ToDouble(retornoDB["CustoProduto"]),
                Convert.ToInt32(retornoDB["QuantidadeProduto"]),
                Convert.ToDouble(retornoDB["DescontoProduto"]),
                Convert.ToDouble(retornoDB["CustoTotalProduto"]),
                retornoDB["NomeProduto"].ToString()
                ));
            }

            con = conexao.fecharConexao();
            retornoDB.Close();

            return Itens;
        }

        public EntradaComDados getEntradaComDados(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "SELECT Entrada.IdEntrada, Entrada.Data, Cli.CliNome, PGTO.NomeFormaPgt, Entrada.EntradaCusto, Entrada.EntradaDesconto, Entrada.EntradaCustoTotal " +
                "FROM Entrada " +
                "INNER JOIN Clientes AS Cli ON Cli.IdCliente = Entrada.EntradaIdCli " +
                "INNER JOIN FormaPgto AS PGTO ON PGTO.IdFormaPgt = Entrada.EntradaIdFormaPgto " +
                $"WHERE Entrada.IdEntrada = {ID}";
            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                EntradaComDados entrada = new EntradaComDados(
                    Convert.ToInt32(retornoDB["IdEntrada"]),
                    retornoDB["Data"].ToString(),
                    retornoDB["CliNome"].ToString(),
                    retornoDB["NomeFormaPgt"].ToString(),
                    Convert.ToDouble(retornoDB["EntradaCusto"]),
                    Convert.ToDouble(retornoDB["EntradaDesconto"]),
                    Convert.ToDouble(retornoDB["EntradaCustoTotal"])
                    );

                con = conexao.fecharConexao();
                retornoDB.Close();

                return entrada;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Registro N° " + ID + " não localizado!");
            }
        }

        public int getRegistroInicial()
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT MAX(IdEntrada) AS ID FROM Entrada";
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

            SQL = "SELECT ISNULL(MAX(IdEntrada)+1, 1) AS ID FROM Entrada";
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

            SQL = "SELECT TOP 1 IdEntrada AS ID " +
                "FROM Entrada " +
                "WHERE IdEntrada > @ID " +
                "ORDER BY IdEntrada";

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

            SQL = "SELECT TOP 1 IdEntrada AS ID " +
                "FROM Entrada " +
                "WHERE IdEntrada < @ID " +
                "ORDER BY IdEntrada DESC";

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

        public void salvarItensEntrada(EntradaItens itens)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
					
            SQL = "INSERT INTO ItensEntrada (IdEntrada, IdProduto, CustoProduto, QuantidadeProduto, DescontoProduto, CustoTotalProduto) " +
                "VALUES (@Entrada, @Produto, @Custo, @Quantidade, @Desconto, @CustoTotal)";

            cmd = new SqlCommand(SQL, con);

            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@Entrada", itens.getSetEntradaID);
            cmd.Parameters.AddWithValue("@Produto", itens.getSetProduto);
            cmd.Parameters.AddWithValue("@Custo", itens.getSetItemCusto);
            cmd.Parameters.AddWithValue("@Quantidade", itens.getSetQuantidade);
            cmd.Parameters.AddWithValue("@Desconto", itens.getSetItemDesconto);
            cmd.Parameters.AddWithValue("@CustoTotal", itens.getSetItemCustoTotal);

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

        public void excluirItem(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "DELETE FROM ItensEntrada " +
                "WHERE IdItensEntrada = @ID";

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

        public void excluirItens(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "DELETE FROM ItensEntrada " +
                "WHERE IdEntrada = @ID";

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

                throw new Exception("Ocorreu um erro ao tentar excluir os Itens!");
            }
        }

        public double VerificarCusto(int ID)
        {
            string SQL;
            SqlDataReader retornoDB;
            double custo;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;

            SQL = "SELECT SUM(CustoTotalProduto) AS Custo FROM ItensEntrada " +
                "WHERE IdEntrada = @ID";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                custo = Convert.ToDouble(retornoDB["Custo"]);

                con = conexao.fecharConexao();
                retornoDB.Close();

                return custo;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();

                throw new Exception("Ocorreu um erro ao tentar buscar o valor do Pedido de Entrada!");
            }
        }

        public int finalizarEntrada(Entrada entrada)
        {
            // SALVAR -> SP => FinalizaEntrada
            string parametro;
            int retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand("FinalizaEntrada", con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.CommandType = CommandType.StoredProcedure;

            parametro = "RetornoOperacao";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.InputOutput;
            cmd.Parameters[parametro].Value = -1;

            parametro = "ID";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = entrada.getSetID;

            parametro = "Data";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 10);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = entrada.getSetData;

            parametro = "Cliente";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = entrada.getSetCliente;

            parametro = "FormaPGTO";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = entrada.getSetFormaPGTO;

            parametro = "Custo";
            cmd.Parameters.Add(parametro, SqlDbType.Decimal, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Precision = 18;
            cmd.Parameters[parametro].Scale = 2;
            cmd.Parameters[parametro].Value = entrada.getSetCusto;

            parametro = "Desconto";
            cmd.Parameters.Add(parametro, SqlDbType.Decimal, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Precision = 18;
            cmd.Parameters[parametro].Scale = 2;
            cmd.Parameters[parametro].Value = entrada.getSetDesconto;

            parametro = "CustoTotal";
            cmd.Parameters.Add(parametro, SqlDbType.Decimal, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Precision = 18;
            cmd.Parameters[parametro].Scale = 2;
            cmd.Parameters[parametro].Value = entrada.getSetCustoTotal;

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