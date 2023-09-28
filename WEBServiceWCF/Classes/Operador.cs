using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Operador
    {
        private int ID;
        private string Nome;
        private string Senha;
        private bool Admin;
        private bool Status;
        private bool CadastroOperador;
        private bool CadastroCategoria;
        private bool CadastroCliente;
        private bool CadastroProduto;
        private bool CadastroFormaPGTO;
        private bool TabelaUsuario;
        private bool Pedidos;
        private bool Entrada;

        public Operador() { }

        public Operador(int id, string nome, bool admin, bool status)
        {
            ID = id;
            Nome = nome;
            Admin = admin;
            Status = status;
        }

        public Operador(int iD, string nome, string senha, bool admin, bool status, bool cadastroOperador, bool cadastroCategoria, bool cadastroCliente, bool cadastroProduto, bool cadastroFormaPGTO, bool tabelaUsuario, bool pedidos, bool entrada)
        {
            ID = iD;
            Nome = nome;
            Senha = senha;
            Admin = admin;
            Status = status;
            CadastroOperador = cadastroOperador;
            CadastroCategoria = cadastroCategoria;
            CadastroCliente = cadastroCliente;
            CadastroProduto = cadastroProduto;
            CadastroFormaPGTO = cadastroFormaPGTO;
            TabelaUsuario = tabelaUsuario;
            Pedidos = pedidos;
            Entrada = entrada;
        }

        public int getSetID
        {
            get { return ID; }
            set { ID = value; }
        }

        public string getSetNome
        {
            get { return Nome; }
            set { Nome = value; }
        }

        public string getSetSenha
        {
            get { return Senha; }
            set { Senha = value; }
        }

        public bool getSetAdmin
        {
            get { return Admin; }
            set { Admin = value; }
        }

        public bool getSetStatus
        {
            get { return Status; }
            set { Status = value; }
        }

        public bool getSetCadastroOperador
        {
            get { return CadastroOperador; }
            set {  CadastroOperador = value;}
        }
        public bool getSetCadastroCategoria
        {
            get { return CadastroCategoria; }
            set { CadastroCategoria = value; }
        }
        public bool getSetCadastroCliente
        {
            get { return CadastroCliente; }
            set { CadastroCliente = value; }
        }
        public bool getSetCadastroProduto
        {
            get { return CadastroProduto; } 
            set {  CadastroProduto = value; }

        }
        public bool getSetCadastroFormaPGTO
        {
            get { return CadastroFormaPGTO; }
            set { CadastroFormaPGTO = value; }
        }

        public bool getSetTabelaUsuario
        {
            get { return TabelaUsuario; }
            set {  TabelaUsuario = value; }
        }
        public bool getSetPedidos
        {
            get { return Pedidos; }
            set { Pedidos = value; }
        }
        public bool getSetEntrada
        {
            get { return Entrada; } 
            set {  Entrada = value; }
        }
    }
}