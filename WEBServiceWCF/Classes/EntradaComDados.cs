using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class EntradaComDados
    {
        private int ID;
        private string Data;
        private string Cliente;
        private string FormaPGTO;
        private double Custo;
        private double Desconto;
        private double CustoTotal;

        public EntradaComDados() { }

        public EntradaComDados(int iD, string data, string cliente, string formaPGTO, double custo, double desconto, double custoTotal)
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
    }
}