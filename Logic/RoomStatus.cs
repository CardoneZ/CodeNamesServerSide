using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public enum RoomStatus
    {
        [EnumMember]
        Waiting,
        [EnumMember]
        Started,
        [EnumMember]
        Finished
    }
}
