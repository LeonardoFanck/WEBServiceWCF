using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WEBServiceWCF.Banco;
using WEBServiceWCF.Classes;
using System.Drawing;

namespace WEBServiceWCF.DAO
{
    public class ConfiguracoesGeraisDAO
    {
        private static ConexaoDB conexao = new ConexaoDB();
        private SqlDataReader retornoDB;
        private int timeOutSQL = 0;

        public ConfiguracoesGerais getDados()
        {
            try
            {
                string SQL = "";
                double MaxDescontoPedido;
                double MaxDescontoItemPedido;
                bool PermiteVendaNegativa;
                bool PermiteAlteracaoDeValor;
                ConfiguracoesGerais config;

                SQL = "SELECT * " +
                      "FROM ConfiguracoesGerais";

                SqlConnection con = conexao.abrirConexao();
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandTimeout = timeOutSQL;

                retornoDB = cmd.ExecuteReader();

                if (retornoDB.HasRows == true && retornoDB.Read() == true)
                {
                    // TEM COLUNAS NO BANCO
                    MaxDescontoPedido = Convert.ToDouble(retornoDB["MaxDescontoPedido"]);
                    MaxDescontoItemPedido = Convert.ToDouble(retornoDB["MaxDescontoItemPedido"]);
                    PermiteVendaNegativa = !Convert.ToBoolean(retornoDB["VendaItemNegativo"]);
                    PermiteAlteracaoDeValor = !Convert.ToBoolean(retornoDB["AlterarValorItem"]);

                    config = new ConfiguracoesGerais(MaxDescontoPedido, MaxDescontoItemPedido, PermiteVendaNegativa, PermiteAlteracaoDeValor);
                }
                else
                {
                    // NÃO TEM NENHUMA COLUNA NO BANCO
                    config = new ConfiguracoesGerais(-1, -1, false, false);
                }

                con = conexao.fecharConexao();

                return config;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int saveDados(ConfiguracoesGerais config)
        {
            try
            {
                int retorno;
                string parametro;
                int valor;

                SqlConnection con = conexao.abrirConexao();
                SqlCommand cmd = new SqlCommand("AtualizaConfiguracoesGerais", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = timeOutSQL;

                // ADICIONA OS PARAMETROS NO COMANDO SQL
                parametro = "RetornoOperacao";
                cmd.Parameters.Add(parametro, SqlDbType.Int, 1);
                cmd.Parameters[parametro].Direction = ParameterDirection.InputOutput;
                cmd.Parameters[parametro].Value = -1;

                parametro = "MaxDescontoItemPedido";
                cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 10);
                cmd.Parameters[parametro].Direction = ParameterDirection.Input;
                cmd.Parameters[parametro].Value = config.getSetMaxDescontoItensPedido;

                parametro = "MaxDescontoPedido";
                cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 10);
                cmd.Parameters[parametro].Direction = ParameterDirection.Input;
                cmd.Parameters[parametro].Value = config.getSetMaxDescontoPedido;

                valor = TransformaBollEmInt(config.getSetVendaItemNegativo);
                parametro = "VendaItemNegativo";
                cmd.Parameters.Add(parametro, SqlDbType.SmallInt, 1);
                cmd.Parameters[parametro].Direction = ParameterDirection.Input;
                cmd.Parameters[parametro].Value = valor;
                
                valor = TransformaBollEmInt(config.getSetAlterarValorItem);
                parametro = "AlterarValorItem";
                cmd.Parameters.Add(parametro, SqlDbType.SmallInt, 1);
                cmd.Parameters[parametro].Direction = ParameterDirection.Input;
                cmd.Parameters[parametro].Value = valor;

                parametro = "MaxDescontoItemEntrada";
                cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 10);
                cmd.Parameters[parametro].Direction = ParameterDirection.Input;
                cmd.Parameters[parametro].Value = -1;

                parametro = "MaxDescontoEntrada";
                cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 10);
                cmd.Parameters[parametro].Direction = ParameterDirection.Input;
                cmd.Parameters[parametro].Value = -1;
                // EXECUTA A SP
                cmd.ExecuteNonQuery();

                // PEGA O RETORNO DA SP
                retorno = Convert.ToInt32(cmd.Parameters["RetornoOperacao"].Value);
                
                // FECHA A CONEXÃO
                con = conexao.fecharConexao();

                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int TransformaBollEmInt(bool valorBool)
        {
            int retorno;

            if (valorBool == true)
            {
                retorno = 0;
            }
            else
            {
                retorno = 1;
            }

            return retorno;
        }
    }
}