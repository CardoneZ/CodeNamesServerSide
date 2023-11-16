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
    [ServiceContract]
    public interface IPlayerManagerService
    {
        [OperationContract]
        void AddUserAccountToDatabase(string nickname, string email, string password);

        [OperationContract]
        Player Login(String nickname, String password);

        [OperationContract]
        void ShowUsersAccounts();

        [OperationContract]
        Dictionary<int, IFriendListServiceCallback> GetCurrentUsers();

    }

}
