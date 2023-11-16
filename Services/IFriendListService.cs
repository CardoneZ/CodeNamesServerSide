using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract(CallbackContract = typeof(IFriendListServiceCallback))]
    public interface IFriendListService
    {
        [OperationContract]
        int MakeFriendRequest(int IdPlayer, string namePlayer);

        [OperationContract]
        void SavePlayerSession(int idPlayer);
        [OperationContract]
        void RemovePlayerSession(int idPlayer);

        [OperationContract]
        int AcceptFriendRequest(int IdRequest);

        [OperationContract]
        int RejectFriendRequest(int IdRequest);

        [OperationContract]
         void UpdatePlayerSession(int idPlayer);

        [OperationContract]
        List<Logic.Player> GetFriends(int idPlayer);

        [OperationContract]
        List<DataModels.FriendshipRequest> GetFriendshipRequests(int idPlayer);
    }

    [ServiceContract]
    public interface IFriendListServiceCallback
    {
        [OperationContract]
        void NotifyFriendRequest(int idPlayer);

        [OperationContract]
        void NotifyFriendOnline(int idPlayer);

        [OperationContract]
        void UpdateFriendRequest(int idPlayer);

        [OperationContract]
        void UpdateFriendDisplay();

    }

    [DataContractFormat]
    public class Friend
    {
        [DataMember]

        public Boolean IsOnline { get; set; }

        [DataMember]
        public string FriendName { get; set; }
    }
}
