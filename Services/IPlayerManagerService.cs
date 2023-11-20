using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
//Aquí van las firmas de la lógica relacionada a la gestión del usuario

{
    [ServiceContract(CallbackContract = typeof(IPlayerManagerServiceCallbak))]
    public interface IPlayerManagerService
    {
        [OperationContract]
        void AddUserAccountToDatabase(string nickname, string email, string password);

        [OperationContract]
        Player Login(String nickname, String password);

        [OperationContract]
        Dictionary<int, IPlayerManagerServiceCallbak> GetCurrentUsers();

        [OperationContract]
        void SavePlayerSession(int idPlayer);
        [OperationContract]
        void RemovePlayerSession(int idPlayer);

        [OperationContract]
        void UpdatePlayerSession(int idPlayer);
    }
    [ServiceContract]
    public interface IPlayerManagerServiceCallbak
    {

    }

}
