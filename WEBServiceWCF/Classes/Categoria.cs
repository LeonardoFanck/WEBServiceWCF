﻿using System;
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