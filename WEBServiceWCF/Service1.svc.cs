using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WEBServiceWCF.Classes;
using WEBServiceWCF.DAO;

namespace WEBServiceWCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Service1" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Service1.svc ou Service1.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class Service1 : IService1
    {
        // --------------------- OPERADOR -------------------------------
        public string getNomeOperador(int ID)
        {
            string nome;
            OperadorDAO operador = new OperadorDAO();
            nome = operador.getNomeOperador(ID);

            return nome;
        }

        public int VerificaOperador(int ID)
        {
            int validacao;
            OperadorDAO operador = new OperadorDAO();
            validacao = operador.VerificaOperador(ID);

            return validacao;
        }

        public int VerificaLogin(int ID, int senha)
        {
            int validacao;
            OperadorDAO Operador = new OperadorDAO();
            validacao = Operador.VerificaLogin(ID, senha);

            return validacao;
        }

        // --------------------- CLIENTE -------------------------------
        public String GetNome(int id)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            Cliente cliente = new Cliente();

            var nome = clienteDAO.clienteGetNome(id);

            return nome;
        }

        public List<Cliente> All()
        {
            ClienteDAO clienteADO = new ClienteDAO();

            var clientes = new List<Cliente>();

            clientes = clienteADO.PegaTodosClientes();
            /*
            clientes.Add(new Cliente() { ID = 1, Nome = "Leonardo Fanck1", CPF = "041.669.710-00" });
            clientes.Add(new Cliente() { ID = 2, Nome = "Leonardo Fanck2", CPF = "041.669.710-00" });
            clientes.Add(new Cliente() { ID = 3, Nome = "Leonardo Fanck3", CPF = "041.669.710-00" });
            */

            return clientes;
        }

        // --------------------- CONFIGURAÇÕES GERAIS -------------------------------
        public ConfiguracoesGerais GetDadosConfiguracoesGerais()
        {
            ConfiguracoesGerais retorno;

            ConfiguracoesGeraisDAO configuracoesGeraisDAO = new ConfiguracoesGeraisDAO();
            retorno = configuracoesGeraisDAO.getDados();

            return retorno;
        }

        public int SalvarConfiguracoesGerais(ConfiguracoesGerais config)
        {
            int retorno;
            ConfiguracoesGeraisDAO configuracoesGeraisDAO = new ConfiguracoesGeraisDAO();

            retorno = configuracoesGeraisDAO.saveDados(config);

            return retorno;
        }

        // --------------------- PRODUTO -------------------------------
        public Produto GetProduto(int id)
        {
            Produto retorno;

            ProdutoDAO produtoDAO = new ProdutoDAO();
            retorno = produtoDAO.GetProduto(id);

            return retorno;
        }

        public Produto GetProdutoInicial()
        {
            Produto retorno;
            int IDProduto;

            ProdutoDAO produtoDAO = new ProdutoDAO();
            IDProduto = produtoDAO.getIDProdutoInicial();
            retorno = produtoDAO.GetProduto(IDProduto);

            return retorno;
        }
    }
}
