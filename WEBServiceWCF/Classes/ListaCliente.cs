using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WEBServiceWCF.Classes
{
    public class ListaCliente
    {
        private int codigo;
        private string nome;
        private string documento;
        private string DtNasc;
        private bool status;

        public ListaCliente(int codigo, string nome, string documento, string dtNasc, bool status)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.documento = documento;
            DtNasc = dtNasc;
            this.status = status;
        }

        public ListaCliente() { }

        public int getSetCodigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string getSetNome
        {
            get { return nome; }
            set { nome = value; }
        }

        public string getSetDocumento
        {
            get { return documento; }
            set { documento = value; }
        }

        public string getSetDtNasc
        {
            get { return DtNasc; }
            set { DtNasc = value; }
        }

        public bool getSetStatus
        {
            get { return status; }
            set { status = value; }
        }
    }
}