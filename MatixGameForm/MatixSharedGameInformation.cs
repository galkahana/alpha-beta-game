using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{
    [DataContract]
    public class MatixSharedGameInformation
    {
        private EMatixPlayerType mStartingPlayer;
        private MatixBoard mSharedBoard;
        private MatixStage mGameStage;

        [DataMember]
        public EMatixPlayerType StartingPlayer
        {
            get { return mStartingPlayer; }
            set { mStartingPlayer = value; }
        }

        [DataMember]
        public MatixBoard SharedBoard
        {
            get { return mSharedBoard; }
            set { mSharedBoard = value; }

        }

        [DataMember]
        public MatixStage GameStage
        {
            get { return mGameStage; }
            set { mGameStage = value; }
        }


    }
}
