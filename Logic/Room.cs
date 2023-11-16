using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [DataContract]
    public class Room
    {
        private string id;
        private string hostUsername;
        private RoomStatus matchStatus;
        private const int MAX_PLAYERS = 8;
        private const int MIN_PLAYERS = 4;
        private int currentPlayers = 0;
        private List<Player> players;

        #region Properties
        [DataMember]
        public string Id { get { return id; } set { id = value; } }
        [DataMember]
        public string HostUsername { get { return hostUsername; } set { hostUsername = value; } }
        [DataMember]
        public RoomStatus MatchStatus { get { return matchStatus; } set { matchStatus = value; } }

        [DataMember]
        public int MAX_PLAYERS1 => MAX_PLAYERS;
        [DataMember]
        public int MIN_PLAYERS1 => MIN_PLAYERS;
        [DataMember]
        public int CurrentPlayers { get { return currentPlayers; } set { currentPlayers = value; } }
       
        [DataMember]
        public List<Player> Players { get { return players; } set { players = value; } }
        #endregion

        public bool HasSpace()
        {
            return currentPlayers < MAX_PLAYERS;
        }

        public bool HadMinPlayersToStart()
        {
            return currentPlayers >= MIN_PLAYERS;
        }
    }
}
