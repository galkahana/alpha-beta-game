using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{
    [DataContract]
    public class MatixSharedGameOptions
    {

        public MatixSharedGameOptions()
        {
            mSharedPlayers = ESharedPlayersOptions.eRowShared;
            mSharedGameName = "My Shared Game";
        }

        [DataMember]
        public string mSharedGameName;

        [DataMember]
        public ESharedPlayersOptions mSharedPlayers;

        public MatixSharedGameOptions Clone()
        {
            MatixSharedGameOptions options = new MatixSharedGameOptions();
            options.mSharedGameName = mSharedGameName;
            options.mSharedPlayers = mSharedPlayers;
            return options;
        }
    }
}
