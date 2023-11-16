using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
//Aquí van las firmas de la lógica de la sala de partida

{
    [ServiceContract(CallbackContract = typeof(ILobbyServiceCallback))]
    public interface ILobbyService
    {
        [OperationContract(IsOneWay = true)]
        void SubscribeToUserEvents();

        [OperationContract(IsOneWay = true)]
        void UnsubscribeFromUserEvents();


        // Otras operaciones de servicio...
    }

    [ServiceContract]
    public interface ILobbyServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void UserLoggedIn(string username);

        [OperationContract(IsOneWay = true)]
        void UserLoggedOut(string username);

        // Otros métodos de callback...
    }
}
