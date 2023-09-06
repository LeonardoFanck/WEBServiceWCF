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
    public class CategoriaDAO
    {
        private ConexaoDB conexao = new ConexaoDB();

        public Categoria GetCategoria(int ID)
        {
            SqlConnection con = conexao.abrirConexao();
            string SQL;
            SqlCommand cmd;
            SqlDataReader retornoDB;
            SQL = "SELECT * FROM Categoria WHERE IdCategoria = @ID";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                Categoria categoria = new Categoria(
                    Convert.ToInt32(retornoDB["IdCategoria"]),
                    retornoDB["NomeCategoria"].ToString(),
                    Convert.ToBoolean(retornoDB["StatusCategoria"])
                    );

                con = conexao.fecharConexao();
                retornoDB.Close();

                return categoria;
            }
            else
            {
                con = conexao.fecharConexao();
                retornoDB.Close();
                throw new Exception("Nenhuma Categoria localizada para o ID: " + ID);
            }
        }

        public int getRegistroInicial()
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT MAX(IdCategoria) AS ID FROM Categoria";
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

            SQL = "SELECT MAX(IdCategoria)+1 AS ID FROM Categoria";
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

            SQL = "SELECT TOP 1 IdCategoria " +
                "FROM Categoria " +
                "WHERE IdCategoria > @ID " +
                "ORDER BY IdCategoria";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);

            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                int IDretorno = Convert.ToInt32(retornoDB["IdCategoria"]);

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

            SQL = "SELECT TOP 1 IdCategoria " +
                "FROM Categoria " +
                "WHERE IdCategoria < @ID " +
                "ORDER BY IdCategoria DESC";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.Parameters.AddWithValue("@ID", ID);
            retornoDB = cmd.ExecuteReader();

            if (retornoDB.HasRows == true && retornoDB.Read() == true)
            {
                int IDretorno = Convert.ToInt32(retornoDB["IdCategoria"]);

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

        public int validarNomeRegistroIgual(Categoria categoria)
        {
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd;
            string SQL;
            SqlDataReader retornoDB;

            SQL = "SELECT IdCategoria AS ID " +
                "FROM Categoria " +
                $"WHERE NomeCategoria LIKE '{categoria.getSetNome}' " +
                $"AND IdCategoria <> {categoria.getSetID}";

            cmd = new SqlCommand(SQL, con);
            cmd.CommandTimeout = conexao.timeOutSQL();
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

                return -1;
            }
        }

        public int salvarRegistro(Categoria categoria)
        {
            // SALVAR -> SP => cadastroFormaPGTO
            string parametro;
            int retornoDB;
            SqlConnection con = conexao.abrirConexao();
            SqlCommand cmd = new SqlCommand("cadastroCategoria", con);
            cmd.CommandTimeout = conexao.timeOutSQL();
            cmd.CommandType = CommandType.StoredProcedure;

            parametro = "RetornoOperacao";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.InputOutput;
            cmd.Parameters[parametro].Value = -1;

            parametro = "IdCategoria";
            cmd.Parameters.Add(parametro, SqlDbType.Int, 5);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = categoria.getSetID;

            parametro = "NomeCategoria";
            cmd.Parameters.Add(parametro, SqlDbType.NVarChar, 50);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = categoria.getSetNome;

            parametro = "StatusCategoria";
            cmd.Parameters.Add(parametro, SqlDbType.Bit, 1);
            cmd.Parameters[parametro].Direction = ParameterDirection.Input;
            cmd.Parameters[parametro].Value = categoria.getSetStatus;

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

        public List<Categoria> getAllCategorias()
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

            while (retornoDB.HasRows == true && (retornoDB.Read() == true))
            {
                categorias.Add(
                    new Categoria()
                    {
                        getSetID = Convert.ToInt32(retornoDB["IdCategoria"]),
                        getSetNome = retornoDB["NomeCategoria"].ToString(),
                        getSetStatus = Convert.ToBoolean(retornoDB["StatusCategoria"])
                    }
                );
            }

            retornoDB.Close();
            con = conexao.fecharConexao();

            return categorias;
        }
    }
}