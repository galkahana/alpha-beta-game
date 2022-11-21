using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{
    [DataContract]
    public class PlayerFeatures
    {

        public PlayerFeatures()
        {
            mAvailableSelections = new Dictionary<int,int>();
        }

        private EMatixPlayerType mPlayerType;
        private long mPlayerScore;
        private Dictionary<int,int> mAvailableSelections;

        [DataMember]
        public EMatixPlayerType PlayerType
        {
            get { return mPlayerType; }
            set { mPlayerType = value; }
        }

        [DataMember]
        public Dictionary<int,int> AvailableSelections
        {
            get { return mAvailableSelections; }
            set { mAvailableSelections = value; }
        }

        [DataMember]
        public long PlayerScore
        {
            get { return mPlayerScore; }
            set { mPlayerScore = value; }
        }

        public void SetupForGameStart()
        {
            mPlayerScore = 0;
            mAvailableSelections.Clear();
            for(int i=0;i<MatixBoard.scBoardSize;++i)
                mAvailableSelections.Add(i,i);
        }

        public void Assign(PlayerFeatures playerFeatures)
        {
            mPlayerScore = playerFeatures.mPlayerScore;
            mAvailableSelections.Clear();
            foreach (KeyValuePair<int,int> keyValuePair in playerFeatures.mAvailableSelections)
                mAvailableSelections.Add(keyValuePair.Key,keyValuePair.Value);
        }

        
        internal void SetupFromPlayer(MatixSharedGameServices.PlayerFeatures inPlayerFeatures)
        {
            mPlayerScore = inPlayerFeatures.PlayerScore;
            mAvailableSelections.Clear();
            foreach (KeyValuePair<int, int> keyValuePair in inPlayerFeatures.AvailableSelections)
                mAvailableSelections.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
