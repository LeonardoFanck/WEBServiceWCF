using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Cliente
    {
        private int ID;
        private string Nome;
        private string CPF;
        private string Email;
        private string DtNascimento;
        private string Estado;
        private string Cidade;
        private string Bairro;
        private string Endereco;
        private string Numero;
        private bool Status;
        private int Moradia;

        public Cliente() { }

        public Cliente(int iD, string nome, string cPF, string email, string dtNascimento, string estado, string cidade, string bairro, string endereco, string numero, bool status, int moradia)
        {
            ID = iD;
            Nome = nome;
            CPF = cPF;
            Email = email;
            DtNascimento = dtNascimento;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;
            Endereco = endereco;
            Numero = numero;
            Status = status;
            Moradia = moradia;
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

        public string getSetCPF
        {
            get { return CPF; }
            set { CPF = value; }
        }

        public string getSetEmail
        {
            get { return Email; }
            set { Email = value; }
        }

        public string getSetDtNascimento
        {
            get { return DtNascimento; }
            set { DtNascimento = value; }
        }

        public string getSetEstado
        {
            get { return Estado; }
            set { Estado = value; }
        }

        public string getSetCidade
        {
            get { return Cidade; }
            set { Cidade = value; }
        }

        public string getSetBairro
        {
            get { return Bairro; }
            set { Bairro = value; }
        }

        public string getSetEndereco
        {
            get { return Endereco; }
            set { Endereco = value; }
        }

        public string getSetNumero
        {
            get { return Numero; }
            set { Numero = value; }
        }

        public bool getSetStatus
        {
            get { return Status; }
            set { Status = value; }
        }

        public int getSetMoradia
        {
            get { return Moradia; }
            set { Moradia = value; }
        }
    }
}