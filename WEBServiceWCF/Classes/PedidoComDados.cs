using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class PedidoComDados
    {
        private int ID;
        private string Data;
        private string Cliente;
        private string FormaPGTO;
        private double Valor;
        private double Desconto;
        private double ValorTotal;

        public PedidoComDados(){}

        public PedidoComDados(int iD, string data, string cliente, string formaPGTO, double valor, double desconto, double valorTotal)
        {
            ID = iD;
            Data = data;
            Cliente = cliente;
            FormaPGTO = formaPGTO;
            Valor = valor;
            Desconto = desconto;
            ValorTotal = valorTotal;
        }

        public int getSetID
        {
            get { return ID; }
            set { ID = value; }
        }

        public string getSetData
        {
            get { return Data; }
            set { Data = value; }
        }

        public string getSetCliente
        {
            get { return Cliente; }
            set { Cliente = value; }
        }

        public string getSetFormaPGTO
        {
            get { return FormaPGTO; }
            set { FormaPGTO = value; }
        }

        public double getSetValor
        {
            get { return Valor; }
            set { Valor = value; }
        }

        public double getSetDesconto
        {
            get { return Desconto; }
            set { Desconto = value; }
        }

        public double getSetValorTotal
        {
            get { return ValorTotal; }
            set { ValorTotal = value; }
        }
    }
}