using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Categoria
    {
        public int ID;
        public string Nome;
        public bool Status;

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