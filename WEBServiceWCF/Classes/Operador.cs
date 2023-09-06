using System;
using System.Collections.Generic;
using System.Linq;
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

        public Operador() { }

        public Operador(int iD, string nome, string senha, bool admin, bool status)
        {
            ID = iD;
            Nome = nome;
            Senha = senha;
            Admin = admin;
            Status = status;
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
    }
}