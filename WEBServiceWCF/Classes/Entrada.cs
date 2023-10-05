using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Entrada
    {
        private int ID;
        private int Cliente;
        private int FormaPGTO;
        private double Custo;
        private double Desconto;
        private double CustoTotal;
        private string Data;

        public Entrada() { }

        public Entrada(int iD, string data, int cliente, int formaPGTO, double custo, double desconto, double custoTotal)
        {
            ID = iD;
            Data = data;
            Cliente = cliente;
            FormaPGTO = formaPGTO;
            Custo = custo;
            Desconto = desconto;
            CustoTotal = custoTotal;
        }

        public int getSetID
        {
            get { return ID; }
            set { ID = value; }
        }

        public int getSetCliente
        {
            get { return Cliente; }
            set { Cliente = value; }
        }

        public int getSetFormaPGTO
        {
            get { return FormaPGTO; }
            set { FormaPGTO = value; }
        }

        public double getSetCusto
        {
            get { return Custo; }
            set { Custo = value; }
        }

        public double getSetDesconto
        {
            get { return Desconto; }
            set { Desconto = value; }
        }

        public double getSetCustoTotal
        {
            get { return CustoTotal; }
            set { CustoTotal = value; }
        }

        public string getSetData
        {
            get { return Data; }
            set { Data = value; }
        }
    }
}