using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class ConfiguracoesGerais
    {
        private double MaxDescontoPedido;
        private double MaxDescontoItensPedido;
        private bool VendaItemNegativo;
        private bool AlterarValorItem;

        public ConfiguracoesGerais() { }

        public ConfiguracoesGerais(double maxDescontoPedido, double maxDescontoItensPedido, bool vendaItemNegativo, bool alterarValorItem)
        {
            this.MaxDescontoPedido = maxDescontoPedido;
            this.MaxDescontoItensPedido = maxDescontoItensPedido;
            this.VendaItemNegativo = vendaItemNegativo;
            this.AlterarValorItem = alterarValorItem;
        }

        public double getSetMaxDescontoPedido
        {
            get
            {
                return this.MaxDescontoPedido;
            }
            set
            {
                this.MaxDescontoPedido = value;
            }
        }

        public double getSetMaxDescontoItensPedido
        {
            get
            {
                return this.MaxDescontoItensPedido;
            }
            set
            {
                this.MaxDescontoItensPedido = value;
            }
        }

        public bool getSetVendaItemNegativo
        {
            get
            {
                return this.VendaItemNegativo;
            }
            set
            {
                this.VendaItemNegativo = value;
            }
        }

        public bool getSetAlterarValorItem
        {
            get
            {
                return this.AlterarValorItem;
            }
            set
            {
                this.AlterarValorItem = value;
            }
        }
    }
}