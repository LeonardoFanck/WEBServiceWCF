using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class PedidoItens
    {
        private int ID;
        private int IDPedido;
        private int Produto;
        private double Valor;
        private int Quantidade;
        private double Desconto;
        private double ValorTotal;
        private string NomeProduto;

        public PedidoItens() { }

        public PedidoItens(int iD, int iDPedido, int produto, double valor, int quantidade, double desconto, double valorTotal, string nomeProduto)
        {
            ID = iD;
            IDPedido = iDPedido;
            Produto = produto;
            Valor = valor;
            Quantidade = quantidade;
            Desconto = desconto;
            ValorTotal = valorTotal;
            NomeProduto = nomeProduto;
        }

        public int getSetItemID
        {
            get { return ID; }
            set { ID = value; }
        }

        public int getSetPedidoID
        {
            get { return IDPedido; }
            set { IDPedido = value; }
        }

        public int getSetProduto
        {
            get { return Produto; }
            set { Produto = value; }
        }

        public double getSetItemValor
        {
            get { return Valor; }
            set { Valor = value; }
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

        public double getSetItemValorTotal
        {
            get { return ValorTotal; }
            set { ValorTotal = value; }
        }

        public string getSetItemNomeProduto
        {
            get { return NomeProduto; }
            set { NomeProduto = value; }
        }
    }
}