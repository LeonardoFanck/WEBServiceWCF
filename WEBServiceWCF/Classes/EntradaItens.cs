using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class EntradaItens
    {
        private int ID;
        private int IDEntrada;
        private int Produto;
        private double Custo;
        private int Quantidade;
        private double Desconto;
        private double CustoTotal;
        private string NomeProduto;

        public EntradaItens() { }

        public EntradaItens(int iD, int idEntrada, int produto, double custo, int quantidade, double desconto, double custoTotal, string nomeProduto)
        {
            ID = iD;
            IDEntrada = idEntrada;
            Produto = produto;
            Custo = custo;
            Quantidade = quantidade;
            Desconto = desconto;
            CustoTotal = custoTotal;
            NomeProduto = nomeProduto;
        }

        public int getSetItemID
        {
            get { return ID; }
            set { ID = value; }
        }

        public int getSetEntradaID
        {
            get { return IDEntrada; }
            set { IDEntrada = value; }
        }

        public int getSetProduto
        {
            get { return Produto; }
            set { Produto = value; }
        }

        public double getSetItemCusto
        {
            get { return Custo; }
            set { Custo = value; }
        }

        public int getSetQuantidade
        {
            get { return Quantidade; }
            set { Quantidade = value; }
        }

        public double getSetItemDesconto
        {
            get { return Desconto; }
            set { Desconto = value; }
        }

        public double getSetItemCustoTotal
        {
            get { return CustoTotal; }
            set { CustoTotal = value; }
        }

        public string getSetItemNomeProduto
        {
            get { return NomeProduto; }
            set { NomeProduto = value; }
        }
    }
}