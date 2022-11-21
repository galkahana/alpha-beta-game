using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{
    [DataContract]
    public enum EMatixMoveType
    {
        [EnumMember]
        eMatixMoveManual,
        [EnumMember]
        eMatixMoveAutomatic
    };
}
