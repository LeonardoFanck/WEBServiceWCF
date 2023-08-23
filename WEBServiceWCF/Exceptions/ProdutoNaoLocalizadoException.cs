using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Exceptions
{
    public class ProdutoNaoLocalizadoException : Exception
    {
        public ProdutoNaoLocalizadoException()
        {
            throw new Exception("Não foi possível Localizar o Produto");
        }
    }
}