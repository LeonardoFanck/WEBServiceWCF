using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class ListaPedido
    {
        private int codigo;
        private string cliente;
        private string formaPGTO;
        private string data;
        private double valor;

        public ListaPedido()
        {

        }

        public ListaPedido(int codigo, string cliente, string formaPGTO, string data, double valor)
        {
            this.codigo = codigo;
            this.cliente = cliente;
            this.formaPGTO = formaPGTO;
            this.data = data;
            this.valor = valor;
        }

        public int getSetCodigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string getSetcliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public string getSetformaPGTO
        {
            get { return formaPGTO; }
            set { formaPGTO = value; }
        }

        public string getSetData
        {
            get { return data; }
            set { data = value; }
        }

        public double getSetValor
        {
            get { return valor; }
            set { valor = value; }
        }

    }
}