using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class ListaEntrada
    {
        private int codigo;
        private string fornecedor;
        private string formaPGTO;
        private string data;
        private double custo;

        public ListaEntrada()
        {

        }

        public ListaEntrada(int codigo, string fornecedor, string formaPGTO, string data, double custo)
        {
            this.codigo = codigo;
            this.fornecedor = fornecedor;
            this.formaPGTO = formaPGTO;
            this.data = data;
            this.custo = custo;
        }

        public int getSetID
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string getSetFornecedor
        {
            get { return fornecedor; }
            set { fornecedor = value; }
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

        public double getSetCusto
        {
            get { return custo; }
            set { custo = value; }
        }
    }
}