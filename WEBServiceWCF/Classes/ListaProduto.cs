using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class ListaProduto
    {
        private int id;
        private string nome;
        private string categoria;
        private double valor;
        private double custo;
        private long estoque;
        private bool status;

        public ListaProduto()
        {
        }

        public ListaProduto(int id, string nome, string categoria, double valor, double custo, long estoque, bool status)
        {
            this.id = id;
            this.nome = nome;
            this.categoria = categoria;
            this.valor = valor;
            this.custo = custo;
            this.estoque = estoque;
            this.status = status;
        }

        public int getSetID
        {
            get { return id; }
            set { id = value; }
        }

        public string getSetNome
        {
            get { return nome; }
            set { nome = value; }
        }

        public string getSetCategoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public double getSetValor
        {
            get { return valor; }
            set { valor = value; }
        }

        public double getSetCusto
        {
            get { return custo; }
            set { custo = value; }
        }

        public long getSetEstoque
        {
            get { return estoque; }
            set { estoque = value; }
        }

        public bool getSetStatus
        {
            get { return status; }
            set { status = value; }
        }

    }
}