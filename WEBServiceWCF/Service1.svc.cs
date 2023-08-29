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
            OperadorDAO operador = new OperadorDAO();

            return operador.getNomeOperador(ID);
        }

        public int VerificaOperador(int ID)
        {
            OperadorDAO operador = new OperadorDAO();

            return operador.VerificaOperador(ID);
        }

        public int VerificaLogin(int ID, int senha)
        {
            OperadorDAO Operador = new OperadorDAO();

            return Operador.VerificaLogin(ID, senha);
        }

        // --------------------- CLIENTE -------------------------------
        public String GetNome(int id)
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            return clienteDAO.clienteGetNome(id);
        }

        public List<Cliente> All()
        {
            ClienteDAO clienteADO = new ClienteDAO();

            return clienteADO.PegaTodosClientes();
        }

        // --------------------- CONFIGURAÇÕES GERAIS -------------------------------
        public ConfiguracoesGerais GetDadosConfiguracoesGerais()
        {
            ConfiguracoesGeraisDAO configuracoesGeraisDAO = new ConfiguracoesGeraisDAO();

            return configuracoesGeraisDAO.getDados();
        }

        public int SalvarConfiguracoesGerais(ConfiguracoesGerais config)
        {
            ConfiguracoesGeraisDAO configuracoesGeraisDAO = new ConfiguracoesGeraisDAO();

            return configuracoesGeraisDAO.saveDados(config);
        }

        // --------------------- PRODUTO -------------------------------
        public Produto GetProduto(int id)
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            return produtoDAO.GetProduto(id);
        }

        public Produto GetProdutoInicial()
        {  
            int IDProduto;

            ProdutoDAO produtoDAO = new ProdutoDAO();
            IDProduto = produtoDAO.getIDProdutoInicial();

            return produtoDAO.GetProduto(IDProduto);
        }

        public int AvancarRegistroProduto(int id)
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            return produtoDAO.avancarRegistro(id);
        }

        public int VoltarRegistroProduto(int ID)
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            return produtoDAO.voltarRegistro(ID);
        }

        public int GetEstoqueProduto(int ID)
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            return produtoDAO.getEstoque(ID);
        }

        public int SalvarProduto(Produto produto)
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            return produtoDAO.salvarProduto(produto);
        }

        public int GetProximoRegistroProduto()
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();
            
            return produtoDAO.getProximoRegistro();
        }

        public List<Categoria> GetListNomeCategoria()
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            return produtoDAO.GetCategorias();
        }


        // --------------------- ESTADOS -------------------------------
        public List<Estados> getListEstados()
        {
            EstadosDAO estadosDAO = new EstadosDAO();

            return estadosDAO.getListEstados();
        }
    }
}
