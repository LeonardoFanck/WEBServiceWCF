using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class TipoClientes
    {
        private bool TipoCliente;
        private bool TipoFornecedor;

        public TipoClientes() { }

        public TipoClientes(bool tipoCliente, bool tipoFornecedor)
        {
            TipoCliente = tipoCliente;
            TipoFornecedor = tipoFornecedor;
        }

        public bool getSetTipoCliente
        {
            get { return TipoCliente; }
            set { TipoCliente = value; }
        }

        public bool getSetTipoFornecedor
        {
            get { return TipoFornecedor; }
            set { TipoFornecedor = value; }
        }
    }
}