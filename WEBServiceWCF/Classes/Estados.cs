using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Estados
    {
        private int ID;
        private string Nome;
        private string UF;

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

        public string getSetUF
        {
            get { return UF; }
            set { UF = value; }
        }
    }
}