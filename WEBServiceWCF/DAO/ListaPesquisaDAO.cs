using System;
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
                SQL = $"SELECT {SQL} WHERE {tipoPesquisa} LIKE '%{pesquisa}%' ORDER BY {tipoPesquisa}";
            }
            else
            {
                SQL = $"SELECT TOP 25 {SQL} ORDER BY IdPedidos";
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

        public List<ListaCliente> ListaClientes(string tipoPesquisa, string pesquisa, bool inativo)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            tipoPesquisa = validarTipoPesquisaCliente(tipoPesquisa);

            SQL = $"IdCliente, CliNome, CliCPF, CONVERT(VARCHAR(10), CONVERT(DATE, CliDtNascimento, 126), 103) AS Data, CliStatus " +
                "FROM Clientes";

            if (pesquisa != "")
            {
                SQL = $"SELECT {SQL} WHERE {tipoPesquisa} LIKE '%{pesquisa}%'";
            }
            else
            {
                SQL = $"SELECT TOP 25 {SQL}";
            }

            if (inativo == false)
            {
                if (pesquisa != "")
                {
                    SQL = $"{SQL} AND CliStatus = 0 ORDER BY {tipoPesquisa}";
                }
                else
                {
                    SQL = $"{SQL} WHERE CliStatus = 0 ORDER BY IdCliente";
                }
            }

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true)
            {
                List<ListaCliente> lista = new List<ListaCliente>();

                while (retornoDB.Read() == true)
                {
                    lista.Add(new ListaCliente(
                        Convert.ToInt32(retornoDB["IdCliente"]),
                        retornoDB["CliNome"].ToString(),
                        retornoDB["CliCPF"].ToString(),
                        retornoDB["Data"].ToString(),
                        Convert.ToBoolean(retornoDB["CliStatus"])
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

        private string validarTipoPesquisaCliente(string tipo)
        {
            if (tipo.Equals("Codigo"))
            {
                return "IdCliente";
            }
            else if (tipo.Equals("Nome"))
            {
                return "CliNome";
            }
            else if (tipo.Equals("Documento"))
            {
                return "CliCPF";
            }
            else if (tipo.Equals("Dt Nasc"))
            {
                return "CONVERT(VARCHAR(10), CONVERT(DATE, CliDtNascimento, 126), 103)";
            }
            else if (tipo.Equals("Inativo"))
            {
                return "CASE CliStatus WHEN 0 THEN 'Não' ELSE 'Sim' END";
            }

            return "Cli.CliNome";
        }

        public List<FormaPGTO> ListaFormaPGTO(string tipoPesquisa, string pesquisa, bool inativo)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            tipoPesquisa = validarTipoPesquisaFormaPGTO(tipoPesquisa);

            SQL = $"* " +
                "FROM FormaPgto";

            if (pesquisa != "")
            {
                SQL = $"SELECT {SQL} WHERE {tipoPesquisa} LIKE '%{pesquisa}%'";
            }
            else
            {
                SQL = $"SELECT TOP 25 {SQL}";
            }

            if (inativo == false)
            {
                if (pesquisa != "")
                {
                    SQL = $"{SQL} AND StatusFormaPgt = 0 ORDER BY {tipoPesquisa}";
                }
                else
                {
                    SQL = $"{SQL} WHERE StatusFormaPgt = 0 ORDER BY IdFormaPgt";
                }
            }

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true)
            {
                List<FormaPGTO> lista = new List<FormaPGTO>();

                while (retornoDB.Read() == true)
                {
                    lista.Add(new FormaPGTO(
                        Convert.ToInt32(retornoDB["IdFormaPgt"]),
                        retornoDB["NomeFormaPgt"].ToString(),
                        Convert.ToBoolean(retornoDB["StatusFormaPgt"])
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

        private string validarTipoPesquisaFormaPGTO(string tipo)
        {
            if (tipo.Equals("Codigo"))
            {
                return "IdFormaPgt";
            }
            else if (tipo.Equals("Nome"))
            {
                return "NomeFormaPgt";
            }
            else if (tipo.Equals("Inativo"))
            {
                return "CASE StatusFormaPgt WHEN 0 THEN 'Não' ELSE 'Sim' END";
            }

            return "NomeFormaPgt";
        }

        public List<ListaProduto> ListaProdutos(string tipoPesquisa, string pesquisa, bool inativo)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;
            ProdutoDAO produtoDAO = new ProdutoDAO();

            tipoPesquisa = validarTipoPesquisaProduto(tipoPesquisa);

            SQL = $"Prod.CodProduto, Prod.NomeProduto, Cat.NomeCategoria, Prod.ValorProduto, Prod.CustoProduto, Prod.StatusProduto " +
                $"FROM Produtos AS Prod " +
                $"INNER JOIN Categoria AS Cat ON Cat.IdCategoria = Prod.CategoriaProduto";

            if (pesquisa != "")
            {
                SQL = $"SELECT {SQL} WHERE {tipoPesquisa} LIKE '%{pesquisa}%'";
            }
            else
            {
                SQL = $"SELECT TOP 25 {SQL}";
            }

            if (inativo == false)
            {
                if (pesquisa != "")
                {
                    SQL = $"{SQL} AND Prod.StatusProduto = 0 ORDER BY  {tipoPesquisa}";
                }
                else
                {
                    SQL = $"{SQL} WHERE Prod.StatusProduto = 0 ORDER BY CodProduto";
                }
            }

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true)
            {
                List<ListaProduto> lista = new List<ListaProduto>();

                while (retornoDB.Read() == true)
                {
                    lista.Add(new ListaProduto(
                        Convert.ToInt32(retornoDB["CodProduto"]),
                        retornoDB["NomeProduto"].ToString(),
                        retornoDB["NomeCategoria"].ToString(),
                        Convert.ToDouble(retornoDB["ValorProduto"]),
                        Convert.ToDouble(retornoDB["CustoProduto"]),
                        produtoDAO.getEstoque(Convert.ToInt32(retornoDB["CodProduto"])),
                        Convert.ToBoolean(retornoDB["StatusProduto"])
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

        private string validarTipoPesquisaProduto(string tipo)
        {
            if (tipo.Equals("Codigo"))
            {
                return "Prod.CodProduto";
            }
            else if (tipo.Equals("Nome"))
            {
                return "Prod.NomeProduto";
            }
            else if (tipo.Equals("Categoria"))
            {
                return "Cat.NomeCategoria";
            }
            else if (tipo.Equals("Valor"))
            {
                return "Prod.ValorProduto";
            }
            else if (tipo.Equals("Custo"))
            {
                return "Prod.CustoProduto";
            }
            else if (tipo.Equals("Inativo"))
            {
                return "CASE Prod.StatusProduto WHEN 0 THEN 'Não' ELSE 'Sim' END";
            }

            return "Cli.CliNome";
        }

        public List<Categoria> ListaCategorias(string tipoPesquisa, string pesquisa, bool inativo)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            tipoPesquisa = validarTipoPesquisaCategoria(tipoPesquisa);

            SQL = $"* " +
                "FROM Categoria";

            if (pesquisa != "")
            {
                SQL = $"SELECT {SQL} WHERE {tipoPesquisa} LIKE '%{pesquisa}%'";
            }
            else
            {
                SQL = $"SELECT TOP 25 {SQL}";
            }

            if (inativo == false)
            {
                if (pesquisa != "")
                {
                    SQL = $"{SQL} AND StatusCategoria = 0 ORDER BY  {tipoPesquisa}";
                }
                else
                {
                    SQL = $"{SQL} WHERE StatusCategoria = 0 ORDER BY IdCategoria";
                }
            }

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true)
            {
                List<Categoria> lista = new List<Categoria>();

                while (retornoDB.Read() == true)
                {
                    lista.Add(new Categoria(
                        Convert.ToInt32(retornoDB["IdCategoria"]),
                        retornoDB["NomeCategoria"].ToString(),
                        Convert.ToBoolean(retornoDB["StatusCategoria"])
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

        private string validarTipoPesquisaCategoria(string tipo)
        {
            if (tipo.Equals("Codigo"))
            {
                return "IdCategoria";
            }
            else if (tipo.Equals("Nome"))
            {
                return "NomeCategoria";
            }
            else if (tipo.Equals("Inativo"))
            {
                return "CASE StatusCategoria WHEN 0 THEN 'Não' ELSE 'Sim' END";
            }

            return "NomeCategoria";
        }

        public List<Operador> ListaOperador(string tipoPesquisa, string pesquisa, bool inativo)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            tipoPesquisa = validarTipoPesquisaOperador(tipoPesquisa);

            SQL = $"* " +
                "FROM Operador";

            if (pesquisa != "")
            {
                SQL = $"SELECT {SQL} WHERE {tipoPesquisa} LIKE '%{pesquisa}%'";
            }
            else
            {
                SQL = $"SELECT TOP 25 {SQL}";
            }

            if (inativo == false)
            {
                if (pesquisa != "")
                {
                    SQL = $"{SQL} AND statusOperador = 0 ORDER BY  {tipoPesquisa}";
                }
                else
                {
                    SQL = $"{SQL} WHERE statusOperador = 0 ORDER BY IdOperador";
                }
            }

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true)
            {
                List<Operador> lista = new List<Operador>();

                while (retornoDB.Read() == true)
                {
                    lista.Add(new Operador(
                        Convert.ToInt32(retornoDB["IdOperador"]),
                        retornoDB["nomeOperador"].ToString(),
                        Convert.ToBoolean(retornoDB["adminOperador"]),
                        Convert.ToBoolean(retornoDB["statusOperador"])
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

        private string validarTipoPesquisaOperador(string tipo)
        {		
            if (tipo.Equals("Codigo"))
            {
                return "IdOperador";
            }
            else if (tipo.Equals("Nome"))
            {
                return "nomeOperador";
            }
            else if (tipo.Equals("Admin"))
            {
                return "CASE adminOperador WHEN 0 THEN 'Não' ELSE 'Sim' END";
            }
            else if (tipo.Equals("Inativo"))
            {
                return "CASE statusOperador WHEN 0 THEN 'Não' ELSE 'Sim' END";
            }

            return "nomeOperador";
        }
    }
}