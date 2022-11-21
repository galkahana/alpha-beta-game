using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{

    [DataContract]
    public class MatixPlayer
    {
        private EMatixMoveType mMoveType;
        private EMatixPlayerType mPlayerType;
        private bool mIsRemote;
        private bool mIsRemoteAvailable;
        private string mRemotePlayerName;

        [DataMember]
        public EMatixMoveType MoveType
        {
            get { return mMoveType; }
            set { mMoveType = value;}
        }

        public bool IsAvailable
        {
            get { return !mIsRemote || mIsRemoteAvailable;}
        }

        [DataMember]
        public bool IsRemoteAvailable
        {
            get { return mIsRemoteAvailable; }
            set { mIsRemoteAvailable = value; }
        }

        [DataMember]
        public bool IsRemote
        {
            get { return mIsRemote; }
            set { mIsRemote = value; }
        }

        [DataMember]
        public EMatixPlayerType PlayerType
        {
            get { return mPlayerType; }
            set { mPlayerType = value; }
        }

        [DataMember]
        public string RemotePlayerName
        {
            get { return mRemotePlayerName; }
            set { mRemotePlayerName = value; }
        }
    }
}
