using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    //Aquí van las firmas de la lógica de la partida en juego
    [ServiceContract(CallbackContract = typeof(IGameServiceCallback))]

    public interface IGameManagerService
    {

        [OperationContract]
        bool CreateRoom(string hostUsername, string roomId);

        [OperationContract]
        string GenerateRoomCode();

        [OperationContract]
        bool CheckQuota(string roomId);

        [OperationContract]
        List<Logic.Player> RecoverRoomPlayers(string roomId);

        [OperationContract]
        void Connect(string username, string roomId, string message);

        [OperationContract]
        void Disconnect(string username, string roomId, string message);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string username, string roomId);

    }

    [ServiceContract]
    public interface IGameServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallBack(string message);

    }


}
