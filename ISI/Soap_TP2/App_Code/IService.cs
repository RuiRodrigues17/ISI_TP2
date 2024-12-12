using Soap.Models;
using System.Collections.Generic;
using System.ServiceModel;
namespace Soap.Services
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
         List<Transporte> GetAllTransportes();

        [OperationContract]
        Transporte GetTransporteById(int Id);

        [OperationContract]
        int InsertTransporte(Transporte transporte);

        [OperationContract]
        int UpdateTransporte(int Id, decimal temperaturaAtual);

        [OperationContract]
        int DeleteTransporte(int id);
    }
}
