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
        public string GetNomeOperador(int ID)
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

        // POSSIVELMENTE TIRAR DEPOIS
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

        public Cliente GetCliente(int id)
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            return clienteDAO.getCliente(id);
        }

        public Cliente GetClienteInicial()
        {
            int ID;

            ClienteDAO clienteDAO = new ClienteDAO();
            ID = clienteDAO.getUltimoRegistroID();

            return clienteDAO.getCliente(ID);
        }

        public int AvancarRegistroCliente(int id)
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            return clienteDAO.avancarRegistro(id);
        }

        public int VoltarRegistroCliente(int ID)
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            return clienteDAO.voltarRegistro(ID);
        }

        public int SalvarCliente(Cliente cliente, TipoClientes tipoCliente)
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            return clienteDAO.salvarCliente(cliente, tipoCliente);
        }

        public int GetProximoRegistroCliente()
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            return clienteDAO.getProximoRegistro();
        }

        // --------------------- TIPO CLIENTE -------------------------------
        public TipoClientes GetTipoClientes(int ID)
        {
            TipoClientesDAO tipoClientesDAO = new TipoClientesDAO();

            return tipoClientesDAO.getTipoClientes(ID);
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


        // --------------------- ESTADOS -------------------------------
        public List<Estados> GetListEstados()
        {
            EstadosDAO estadosDAO = new EstadosDAO();

            return estadosDAO.getListEstados();
        }

        // --------------------- FORMA PGTO -------------------------------
        public FormaPGTO GetFormaPGTO(int ID)
        {
            FormaPGTODAO formaPGTODAO = new FormaPGTODAO();

            return formaPGTODAO.GetFormaPGTO(ID);
        }

        public FormaPGTO GetRegistroInicialFormaPGTO()
        {
            FormaPGTODAO formaPGTODAO = new FormaPGTODAO();
            // PEGA O REGISTRO INCIAL E BUSCA O ULTIMO REGISTRO
            return formaPGTODAO.GetFormaPGTO(formaPGTODAO.getRegistroInicial());
        }

        public int GetProximoRegistroFormaPGTO()
        {
            FormaPGTODAO formaPGTODAO = new FormaPGTODAO();

            return formaPGTODAO.getProximoRegistro();
        }

        public int AvancarRegistroFormaPGTO(int ID)
        {
            FormaPGTODAO formaPGTODAO = new FormaPGTODAO();

            return formaPGTODAO.avancarRegistro(ID);
        }

        public int VoltarRegistroFormaPGTO(int ID)
        {
            FormaPGTODAO formaPGTODAO = new FormaPGTODAO();

            return formaPGTODAO.voltarRegistro(ID);
        }
        
        public int ValidarNomeFormaPGTO(FormaPGTO formaPGTO)
        {
            FormaPGTODAO formaPGTODAO = new FormaPGTODAO();

             return formaPGTODAO.validarNomeRegistroIgual(formaPGTO);
        }

        public int SalvarFormaPGTO(FormaPGTO formaPGTO)
        {
            FormaPGTODAO formaPGTODAO = new FormaPGTODAO();

            return formaPGTODAO.salvarRegistro(formaPGTO);
        }

        // --------------------- CATEGORIA -------------------------------
        public Categoria GetCategoria(int ID)
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();

            return categoriaDAO.GetCategoria(ID);
        }

        public Categoria GetRegistroInicialCategoria()
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            // PEGA O REGISTRO INCIAL E BUSCA O ULTIMO REGISTRO
            return categoriaDAO.GetCategoria(categoriaDAO.getRegistroInicial());
        }

        public int GetProximoRegistroCategoria()
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();

            return categoriaDAO.getProximoRegistro();
        }

        public int AvancarRegistroCategoria(int ID)
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();

            return categoriaDAO.avancarRegistro(ID);
        }

        public int VoltarRegistroCategoria(int ID)
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();

            return categoriaDAO.voltarRegistro(ID);
        }
        
        public int ValidarNomeCategoria(Categoria categoria)
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();

             return categoriaDAO.validarNomeRegistroIgual(categoria);
        }

        public int SalvarCategoria(Categoria categoria)
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();

            return categoriaDAO.salvarRegistro(categoria);
        }

        public List<Categoria> GetAllCategorias()
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();

            return categoriaDAO.getAllCategorias();
        }
    }
}
