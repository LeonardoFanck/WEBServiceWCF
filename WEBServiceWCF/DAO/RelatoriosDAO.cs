using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class RelatoriosDAO
    {
        private ConexaoDB conexao = new ConexaoDB();

        public List<PedidoComDados> getRelatorioPedido(string dtInicio, string dtFinal, string cliente, string PGTO)
        {
            SqlConnection con = conexao.abrirConexao();
            string SQL;
            SqlCommand cmd;
            SqlDataReader retornoDB;

            SQL = "SELECT Pedido.IdPedidos, CONVERT(VARCHAR(10), CONVERT(DATE, Pedido.PedidoData, 126), 103) AS PedidoData, Cli.CliNome, PGTO.NomeFormaPgt, Pedido.PedidoValor, Pedido.PedidoDesconto, Pedido.PedidoValorTotal " +
                "FROM Pedido " +
                "INNER JOIN Clientes AS Cli ON Cli.IdCliente = Pedido.PedidoIdCli " +
                "INNER JOIN FormaPgto AS PGTO ON PGTO.IdFormaPgt = Pedido.PedidoIdPgto " +
                $"WHERE PedidoData >= CONVERT(VARCHAR(10), CONVERT(DATE, '{dtInicio}', 103), 126) " +
                $"AND PedidoData <= CONVERT(VARCHAR(10), CONVERT(DATE, '{dtFinal}', 103), 126)";

            if (!cliente.Equals("0"))
            {
                SQL = $"{SQL} AND PedidoIdCLi = {cliente}";
            }

            if (!PGTO.Equals("0"))
            {
                SQL = $"{SQL} AND PedidoIdPgto = {PGTO}";
            }

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true)
            {
                List<PedidoComDados> lista = new List<PedidoComDados>();

                while (retornoDB.Read())
                {
                    lista.Add(new PedidoComDados(
                        Convert.ToInt32(retornoDB["IdPedidos"]),
                        retornoDB["PedidoData"].ToString(),
                        retornoDB["CliNome"].ToString(),
                        retornoDB["NomeFormaPgt"].ToString(),
                        Convert.ToDouble(retornoDB["PedidoValor"]),
                        Convert.ToDouble(retornoDB["PedidoDesconto"]),
                        Convert.ToDouble(retornoDB["PedidoValorTotal"])
                    ));
                }

                con = conexao.fecharConexao();
                retornoDB.Close();

                return lista;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
                throw new Exception("Nenhuma Resultado Localizado");
            }
        }
    }
}