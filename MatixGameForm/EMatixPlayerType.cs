using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{
    [DataContract]
    public enum EMatixPlayerType
    {
        [EnumMember]
        eNull,
        [EnumMember]
        eMatixPlayerRows,
        [EnumMember]
        eMatixPlayerColumns
    };
}
