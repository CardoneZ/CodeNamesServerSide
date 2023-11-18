using DataModels;
using Logic;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    //Aquí van las implementaciones de todos los servicios del juego
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]

    public partial class GameService : IPlayerManagerService, IFriendListService
    {
        public Dictionary<int, IFriendListServiceCallback> currentUsers = new Dictionary<int, IFriendListServiceCallback>();

        public void AddUserAccountToDatabase(string username, string email, string password)
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var user = new DataModels.Player
                {
                    Username = username,
                    Email = email,
                    Password = password
                };

                databaseContext.PlayerSet.Add(user);
                databaseContext.SaveChanges();
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void ShowUsersAccounts()
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var user = new DataModels.Player();
                var query = from b in databaseContext.PlayerSet
                            orderby b.IdPlayer
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Username);
                    Console.WriteLine(item.Email);
                }
            }
        }

        public DataModels.Player Login(string nickname, string password)
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var playerAcountt = databaseContext.PlayerSet
                   .FirstOrDefault(u => u.Username == nickname && u.Password == password);

                return playerAcountt;

            }


        }


        public Dictionary<int, IFriendListServiceCallback> GetCurrentUsers()
        {
            return currentUsers;
        }

        public List<FriendshipRequest> GetFriendshipRequests(int idPlayer)
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var request = databaseContext.FriendshipRequestSet
                    .Where(fr => fr.IdReceiverPlayer == idPlayer)
                    .ToList();
                return request;
            }
        }
    }

        public partial class GameService : IFriendListService
    {
        /*public void SendFriendshipRequest(int idSender, int idReceiver)
        {
            using (var databaseContext = new CodeNamesBDEntities())
            {
                var frienshipRequest = new DataModels.FriendshipRequest
                {
                    IdReceiverPlayer = idReceiver,
                    IdSenderPlayer = idSender
                };

                databaseContext.FriendshipRequestSet.Add(frienshipRequest);
                databaseContext.SaveChanges();
            }
        }

        public void ResponseToFriendshipRequest(String response, int idRequest)
        {
            var databaseContext = new CodeNamesBDEntities();
            FriendshipRequest frienshipRequest = new FriendshipRequest();
            using (databaseContext)
            {
                frienshipRequest = databaseContext.FriendshipRequestSet.FirstOrDefault(u => u.IdFriendshipRequest == idRequest);
            }

            if (response == "accept")
            {
                using (databaseContext)
                {
                    var friendship = new DataModels.Friendship
                    {
                        IdPlayer1 = frienshipRequest.IdReceiverPlayer,
                        IdPlayer2 = frienshipRequest.IdSenderPlayer
                    };

                    databaseContext.FriendshipSet.Add(friendship);
                    databaseContext.SaveChanges();
                }
            }

            databaseContext.FriendshipRequestSet.Remove(frienshipRequest);
            databaseContext.SaveChanges();
        }

        public List<DataModels.Player> GetGlobalUsers(int idUser)
        {
            List<DataModels.Player> userList = new List<DataModels.Player>();
            int currentPlayerId = idUser;

            using (var databaseContext = new CodeNamesBDEntities())
            {
                userList = databaseContext.PlayerSet.Where(u => u.IdPlayer != currentPlayerId).ToList();

            }
            return userList;

        }

        public void GetFriends(int idUser)
        {
            List<DataModels.Player> friendList = new List<DataModels.Player>();

            using (var databaseContext = new CodeNamesBDEntities())
            {
                var friendIds = databaseContext.FriendshipSet
                    .Where(f => f.IdPlayer1 == idUser || f.IdPlayer2 == idUser)
                    .Select(f => f.IdPlayer1 == idUser ? f.IdPlayer2 : f.IdPlayer1)
                    .ToList();

                friendList = databaseContext.PlayerSet
                    .Where(p => friendIds.Contains(p.IdPlayer))
                    .ToList();
            }
            System.ServiceModel.OperationContext.Current.GetCallbackChannel<IFriendListServiceCallback>().ShowFriends(friendList);
        }*/

        public int MakeFriendRequest(int IdPlayer, string namePlayer)
        {
            int Result = 0;
            try
            {
                using (var Context = new CodeNamesBDEntities())
                {
                    Console.WriteLine(namePlayer);

                    if (CheckAlreadyFriends(IdPlayer, namePlayer) == 0)
                    {
                        Result = 1;
                    }
                    else
                    {
                            var SecondPlayer = Context.PlayerSet.Where(P => P.Username == namePlayer).FirstOrDefault();

                            if (SecondPlayer != null)
                            {
                                if (Context.FriendshipRequestSet
                                    .Where(r => r.IdSenderPlayer == IdPlayer && r.IdReceiverPlayer == SecondPlayer.IdPlayer)
                                    .FirstOrDefault() == null)
                                {
                                    FriendshipRequest Request = new FriendshipRequest();
                                    Request.IdSenderPlayer = IdPlayer;
                                    Request.IdReceiverPlayer = SecondPlayer.IdPlayer;

                                    // Asignar referencias a SenderPlayer y ReceiverPlayer
                                    Request.SenderPlayer = Context.PlayerSet.Find(IdPlayer);
                                    Request.ReceiverPlayer = SecondPlayer;

                                    Context.FriendshipRequestSet.Add(Request);

                                    Result = Context.SaveChanges();
                                    NotifyRequest(SecondPlayer.IdPlayer);
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en MakeFriendRequest: " + ex.Message);
            }
            Console.WriteLine(Result);
            return Result;
        }

       public void SavePlayerSession(int idPlayer)
        {
            IFriendListServiceCallback context = System.ServiceModel.OperationContext.Current.GetCallbackChannel<IFriendListServiceCallback>();
            currentUsers.Add(idPlayer, context);
            NotifyFriendOnline(idPlayer);
            Console.WriteLine(currentUsers.Count);
            foreach (var kvp in currentUsers)
            {
                Console.WriteLine($"Clave: {kvp.Key}, Valor: {kvp.Value.ToString()}");
            }
        }

        public void RemovePlayerSession(int idPlayer)
        {
            if (currentUsers.ContainsKey(idPlayer))
            {
                currentUsers.Remove(idPlayer);
            }
        }



        public int AcceptFriendRequest(int IdRequest)
        {
            int result = 0;
            try
            {
                using (var context = new CodeNamesBDEntities())
                {
                    var request = context.FriendshipRequestSet.Where(r => r.IdFriendshipRequest == IdRequest).First();
                    Friendship friendship = new Friendship();
                    friendship.IdPlayer1 = request.IdSenderPlayer;
                    friendship.IdPlayer2 = request.IdReceiverPlayer;
                    context.FriendshipSet.Add(friendship);
                    context.FriendshipRequestSet.Remove(request);
                    result = context.SaveChanges();
                    NotifyFriendOnline(friendship.IdPlayer1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en AcceptFriendRequest: " + ex.Message);
            }
            return result;
        }

        public int RejectFriendRequest(int IdRequest)
        {
            int result = 0;
            try
            {
                using (var context = new CodeNamesBDEntities())
                {
                    var request = context.FriendshipRequestSet.Where(r => r.IdFriendshipRequest == IdRequest).First();
                    context.FriendshipRequestSet.Remove(request);
                    result = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en RejectFriendRequest: " + ex.Message);
            }
            return result;
        }

        private int CheckAlreadyFriends(int idPlayer, string playerName)
        {
            int result = 0;
            try
            {
                using (var context = new CodeNamesBDEntities())
                {
                    var player = context.PlayerSet.Where(p => p.Username == playerName).FirstOrDefault();
                    if (player != null)
                    {
                        var check = context.FriendshipSet.Where(fs => (fs.IdPlayer1 == idPlayer || fs.IdPlayer2== player.IdPlayer) && (fs.IdPlayer1 == idPlayer || fs.IdPlayer2 == player.IdPlayer)).FirstOrDefault();
                        if (check == null)
                        {
                            result = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CheckAlreadyFriends: " + ex.Message);
            }
            return result;
        }

        private void NotifyRequest(int idPlayer)
        {
            foreach (var user in currentUsers)
            {
                if (idPlayer == user.Key)
                {
                    Console.WriteLine(idPlayer);
                    user.Value.UpdateFriendRequest(idPlayer);
                    Console.WriteLine("Notifica la solicitud de amistad");
                }
            }
        }

        public List<Logic.Player> GetFriends(int idPlayer)
        {
            List<DataModels.Player> friendList = new List<DataModels.Player>();
            List<Logic.Player> playerFriendsLogic = new List<Logic.Player>();
            using (var context = new CodeNamesBDEntities())
            {
                var friendIds = context.FriendshipSet
                    .Where(f => f.IdPlayer1 == idPlayer || f.IdPlayer2 == idPlayer)
                    .Select(f => f.IdPlayer1 == idPlayer ? f.IdPlayer2 : f.IdFriendship)
                    .ToList();

                friendList = context.PlayerSet
                    .Where(p => friendIds.Contains(p.IdPlayer))
                    .ToList();
                 playerFriendsLogic = ConvertToLogicPlayers(friendList);
                
            }
            return playerFriendsLogic;
        }

        public List<Logic.Player> ConvertToLogicPlayers(List<DataModels.Player> dataModelPlayers)
        {
            List<Logic.Player> logicPlayers = new List<Logic.Player>();

            foreach (var dataModelPlayer in dataModelPlayers)
            {
                Logic.Player logicPlayer = new Logic.Player
                {
                    PlayerId = dataModelPlayer.IdPlayer,
                    Nickname = dataModelPlayer.Username,
                    IsOnline = false 
                };

                logicPlayers.Add(logicPlayer);
            }

            return logicPlayers;
        }


        public void OnlineStatus(int idPlayer)
        {
            var friends = GetFriends(idPlayer);
            Dictionary<int, IFriendListServiceCallback> players = GetCurrentUsers();

            foreach (var friend in friends)
            {
                if (players.ContainsKey(friend.PlayerId))
                {
                    friend.IsOnline = true;
                }
                else
                {
                    friend.IsOnline = false;
                }
            }
        }

        public void UpdatePlayerSession(int idPlayer)
        {
            IFriendListServiceCallback context = System.ServiceModel.OperationContext.Current.GetCallbackChannel<IFriendListServiceCallback>();
            currentUsers[idPlayer] = context;
        }

        private void NotifyFriendOnline(int idPlayer)
        {
            var friends = GetFriends(idPlayer);
            foreach (var friend in friends)
            {
                if (currentUsers.ContainsKey(friend.PlayerId))
                {
                    currentUsers[friend.PlayerId].UpdateFriendDisplay();
                }
            }
        }
    }   
    // ES EL REPO DE CARDONE
    public partial class GameService : IGameManagerService
    {
        private List<Logic.Room> globalRooms = new List<Room>();

        public bool CheckQuota(string roomId)
        {
            throw new NotImplementedException();
        }

        public void Connect(string username, string roomId, string message)
        {
            Logic.Player player = new Logic.Player()
            {
                Nickname = username,
                AOperationContext = System.ServiceModel.OperationContext.Current,
                
            };

            var room = globalRooms.FirstOrDefault(r => r.Id.Equals(roomId));
            if (room.Players.Count() > 0)
            {
                SendMessage($": {player.Nickname} {message}!", player.Nickname, roomId);
            }
            room.Players.Add(player);
            room.CurrentPlayers++;
        }

        public bool CreateRoom(string hostUsername, string roomId)
        {
            Room newRoom = new Logic.Room()
            {
                Id = roomId,
                HostUsername = hostUsername,
                MatchStatus = RoomStatus.Waiting,
                CurrentPlayers = 0,
                Players = new List<Logic.Player>(),

            };

            globalRooms.Add(newRoom);
            return true;
        }
        public Room GetRoom(String roomId)
        {
            var room = globalRooms.FirstOrDefault(r => r.Id == roomId);
            return room;
        }

        public void Disconnect(string username, string roomId, string message)
        {
            Logic.Player player = null;
            var room = globalRooms.FirstOrDefault(r => r.Id.Equals(roomId));

            if (room != null)
            {
                player = room.Players.FirstOrDefault(i => i.Nickname.Equals(username));
            }

            if (player != null)
            {
                room.Players.Remove(player);
                room.CurrentPlayers--;
                if (room.Players.Count() == 0)
                {
                    globalRooms.Remove(room);
                }
                else
                {
                    SendMessage($": {player.Nickname} {message}!", player.Nickname, roomId);
                }
            }
        }

        public string GenerateRoomCode()
        {
            Guid roomId = Guid.NewGuid();
            return roomId.ToString();
            
        }

        public List<Logic.Player> RecoverRoomPlayers(string roomId)
        {
            var roomPlayersList = globalRooms.FirstOrDefault(r => r.Id.Equals(roomId)).Players;
            return roomPlayersList;
        }

        public void SendMessage(string message, string username, string roomId)
        {
            var room = globalRooms.FirstOrDefault(r => r.Id.Equals(roomId));
            if (room != null && room.Players != null)
            {
                foreach (var player in room.Players)
                {
                    string answer = DateTime.Now.ToShortTimeString();
                    var anotherPlayer = room.Players.FirstOrDefault(i => i.Nickname.Equals(username));
                    if (anotherPlayer != null)
                    {
                        answer += $": {anotherPlayer.Nickname} ";
                    }
                    answer += message;
                    player.AOperationContext.GetCallbackChannel<IGameServiceCallback>().MessageCallBack(answer);
                }
            }
        }
    }

    partial class GameService : ILobbyService
    {
        public void SubscribeToUserEvents()
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeFromUserEvents()
        {
            throw new NotImplementedException();
           
        }
    }



}



