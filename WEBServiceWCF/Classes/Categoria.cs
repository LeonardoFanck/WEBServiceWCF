using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Categoria
    {
        private int ID;
        private string Nome;
        private bool Status;

        public Categoria() { }

        public Categoria(int iD, string nome, bool status)
        {
            ID = iD;
            Nome = nome;
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

        public bool getSetStatus
        {
            get { return Status; }
            set { Status = value; }
        }
    }
}