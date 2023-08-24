using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Exceptions
{
    public class ProdutoNaoLocalizadoException : Exception
    {
        public ProdutoNaoLocalizadoException() : base("Não foi possível Localizar o Produto")
        {
             
        }
    }
}