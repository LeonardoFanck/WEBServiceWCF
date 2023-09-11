using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Pedido
    {
        private int ID;
        private int Cliente;
        private int FormaPGTO;
        private double Valor;
        private double Desconto;
        private double ValorTotal;
        private string Data;

        public Pedido() { }

        public Pedido(int iD, int cliente, int formaPGTO, double valor, double desconto, double valorTotal, string data)
        {
            ID = iD;
            Cliente = cliente;
            FormaPGTO = formaPGTO;
            Valor = valor;
            Desconto = desconto;
            ValorTotal = valorTotal;
            Data = data;
        }

        public int getSetID
        {
            get { return ID; }
            set { ID = value; }
        }

        public int getSetCliente {  
            get { return Cliente; } 
            set {  Cliente = value; }
        }

        public int getSetFormaPGTO
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

        public string getSetData
        {
            get { return Data; }
            set { Data = value; }
        }
    }
}