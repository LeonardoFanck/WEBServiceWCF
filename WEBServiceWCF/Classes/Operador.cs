using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Operador
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public bool Admin { get; set; }
        public bool Status { get; set; }
    }
}