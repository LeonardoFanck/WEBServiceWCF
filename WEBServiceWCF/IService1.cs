using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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
    }
}
