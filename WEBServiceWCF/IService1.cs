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
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IService1" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IService1
    {
        // ---- OPERADOR ----
        [OperationContract]
        Operador GetOperador(int ID);

        [OperationContract]
        int VerificaOperador(int ID);

        [OperationContract]
        int VerificaLogin(int ID, int senha);

        [OperationContract]
        Operador GetRegistroInicialOperador();

        [OperationContract]
        int GetProximoRegistroOperador();

        [OperationContract]
        int AvancarRegistroOperador(int ID);

        [OperationContract]
        int VoltarRegistroOperador(int ID);

        [OperationContract]
        int SalvarOperador(Operador operador);

        // ---- CLIENTE ----
        [OperationContract]
        Cliente GetCliente(int ID);
        //String GetNome(int id);

        [OperationContract]
        Cliente GetClienteInicial();

        [OperationContract]
        int AvancarRegistroCliente(int ID);

        [OperationContract]
        int VoltarRegistroCliente(int ID);

        [OperationContract]
        int SalvarCliente(Cliente cliente, TipoClientes tipoCliente);

        [OperationContract]
        int GetProximoRegistroCliente();


        // ----- TIPO CLIENTES ------
        [OperationContract]
        TipoClientes GetTipoClientes(int ID);


        // ---- CONFIGURAÇÕES GERAIS -----
        [OperationContract]
        ConfiguracoesGerais GetDadosConfiguracoesGerais();

        [OperationContract]
        int SalvarConfiguracoesGerais(ConfiguracoesGerais config);



        // ----- PRODUTO -----
        [OperationContract]
        Produto GetProduto(int id);

        [OperationContract]
        Produto GetProdutoInicial();

        [OperationContract]
        int AvancarRegistroProduto(int ID);

        [OperationContract]
        int VoltarRegistroProduto(int ID);

        [OperationContract]
        int GetEstoqueProduto(int ID);

        [OperationContract]
        int SalvarProduto(Produto produto);

        [OperationContract]
        int GetProximoRegistroProduto();



        // ----- ESTADOS -----
        [OperationContract]
        List<Estados> GetListEstados();



        // ----- FORMA PGTO -----
        [OperationContract]
        FormaPGTO GetFormaPGTO(int ID);

        [OperationContract]
        FormaPGTO GetRegistroInicialFormaPGTO();

        [OperationContract]
        int GetProximoRegistroFormaPGTO();

        [OperationContract]
        int AvancarRegistroFormaPGTO(int ID);

        [OperationContract]
        int VoltarRegistroFormaPGTO(int ID);

        [OperationContract]
        int ValidarNomeFormaPGTO(FormaPGTO formaPGTO);

        [OperationContract]
        int SalvarFormaPGTO(FormaPGTO formaPGTO);



        // ----- CATEGORIA -----
        [OperationContract]
        Categoria GetCategoria(int ID);

        [OperationContract]
        Categoria GetRegistroInicialCategoria();

        [OperationContract]
        int GetProximoRegistroCategoria();

        [OperationContract]
        int AvancarRegistroCategoria(int ID);

        [OperationContract]
        int VoltarRegistroCategoria(int ID);

        [OperationContract]
        int ValidarNomeCategoria(Categoria categoria);

        [OperationContract]
        int SalvarCategoria(Categoria categoria);

        [OperationContract]
        List<Categoria> GetAllCategorias();



        // ----- PEDIDOS -----
        [OperationContract]
        Pedido GetPedido(int ID);

        [OperationContract]
        List<PedidoItens> GetPedidoItens(int ID);

        [OperationContract]
        Pedido GetRegistroInicialPedido();

        [OperationContract]
        int GetProximoRegistroPedido();

        [OperationContract]
        int AvancarRegistroPedido(int ID);

        [OperationContract]
        int VoltarRegistroPedido(int ID);

        [OperationContract]
        void salvarItensPedido(PedidoItens item);

        [OperationContract]
        void excluirItemPedido(int ID);

        [OperationContract]
        void excluirItensPedido(int ID);

        [OperationContract]
        double VerificarValorPedido(int ID);

        [OperationContract]
        int FinalizarPedido(Pedido pedido);



        // ----- LISTA DE PESQUISA -------
        [OperationContract]
        List<ListaPedido> GetListaPedidos(string tipoPesquisa, string pesquisa);
    }
}
