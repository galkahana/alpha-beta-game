using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{
    [DataContract]
    public enum ESharedPlayersOptions
    {
        [EnumMember]
        eRowShared,
        [EnumMember]
        eColumnShared,
        [EnumMember]
        eBothShared,
        [EnumMember]
        eNoneShared
    }
}
