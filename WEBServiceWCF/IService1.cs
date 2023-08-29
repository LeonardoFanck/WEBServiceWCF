using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WEBServiceWCF.Classes;

namespace WEBServiceWCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IService1" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IService1
    {
        // ---- OPERADOR ----
        [OperationContract]
        string getNomeOperador(int ID);

        [OperationContract]
        int VerificaOperador(int ID);

        [OperationContract]
        int VerificaLogin(int ID, int senha);



        // ---- CLIENTE ----
        [OperationContract]
        String GetNome(int id);



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

        [OperationContract]
        List<Categoria> GetListNomeCategoria();



        // ----- ESTADOS -----
        [OperationContract]
        List<Estados> getListEstados();
    }
}
