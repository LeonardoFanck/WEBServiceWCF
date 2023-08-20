using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class Produto
    {
        private int ID;
        private string Nome;
        private int Categoria;
        private double Valor;
        private double Custo;
        private bool Status;

        public Produto() { }

        public Produto(int id,string nome, int categoria, double valor, double custo, bool status)
        {
            this.ID = id;
            this.Nome = nome;
            this.Categoria = categoria;
            this.Valor = valor;
            this.Custo = custo;
            this.Status = status;
        }

        public int getSetID
        {
            get { return this.ID; }
            set { this.ID = value; }
        }

        public string getSetNome
        {
            get { return this.Nome; }
            set { this.Nome = value; }
        }

        public int getSetCategoria
        {
            get { return this.Categoria; }
            set { this.Categoria = value;}
        }

        public double getSetValor
        {
            get { return this.Valor; }
            set { this.Valor = value; }
        }

        public double getSetCusto { 
            get { return this.Custo; }
            set { this.Custo = value; }
        }

        public bool getSetStatus
        {
            get { return this.Status; }
            set { this.Status = value; }
        }
    }
}