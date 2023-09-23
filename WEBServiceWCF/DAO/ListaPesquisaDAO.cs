﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.PeerResolvers;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF.DAO
{
    public class ListaPesquisaDAO
    {
        private ConexaoDB conexao = new ConexaoDB();
  
        public List<ListaPedido> ListaPedidos(string tipoPesquisa, string pesquisa)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            tipoPesquisa = validarTipoPesquisaPedido(tipoPesquisa);

            SQL = "Pedido.IdPedidos, Cli.CliNome, PGTO.NomeFormaPgt, CONVERT(VARCHAR(10), CONVERT(DATE, Pedido.PedidoData, 126), 103) AS Data, Pedido.PedidoValorTotal " +
                "FROM Pedido " +
                "INNER JOIN Clientes AS Cli ON Cli.IdCliente = Pedido.PedidoIdCli " +
                "INNER JOIN FormaPgto AS PGTO ON PGTO.IdFormaPgt = Pedido.PedidoIdPgto";

            if (pesquisa != "")
            {
                SQL = $"SELECT {SQL} WHERE {tipoPesquisa} LIKE '%{pesquisa}%'";
            }
            else
            {
                SQL = $"SELECT TOP 25 {SQL}";
            }

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true)
            {
                List<ListaPedido> lista = new List<ListaPedido>();

                while (retornoDB.Read() == true)
                {
                    lista.Add(new ListaPedido(
                        Convert.ToInt32(retornoDB["IdPedidos"]),
                        retornoDB["CliNome"].ToString(),
                        retornoDB["NomeFormaPgt"].ToString(),
                        retornoDB["Data"].ToString(),
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

                throw new Exception("Nenhum registro encontrado!");
            }
        }

        private string validarTipoPesquisaPedido(string tipo)
        {
            if (tipo.Equals("Codigo"))
            {
                return "Pedido.IdPedidos"; 
            }
            else if (tipo.Equals("Cliente"))
            {
                return "Cli.CliNome";
            }
            else if (tipo.Equals("Forma PGTO"))
            {
                return "PGTO.NomeFormaPgt";
            }
            else if (tipo.Equals("Data"))
            {
                return "CONVERT(VARCHAR(10), CONVERT(DATE, Pedido.PedidoData, 126), 103)";
            }
            else if (tipo.Equals("Valor"))
            {
                return "Pedido.PedidoValorTotal";
            }

            return "Cli.CliNome";
        }
    }
}