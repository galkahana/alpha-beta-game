using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using XBLIP.AplhaBetaGame;


namespace MatixGameForm
{
    /*
     *  A Matix stage should contain the following to describe completly the current game state: 
     * 
     *  1. A reference to a board game
     *  2. The available rows and columns indexes [let's use a set for that]
     *  3. The current player identity [rows/columns]
     *  4. The latest selection [it's identity is opposite to the currently player selection]
     *  5. points count for each of the players [rows/columns]
     */

    [DataContract]
    public class MatixStage : IAssignableObject<MatixStage>
    {
        public MatixStage()
        {
            mBoard = null;
            mPlayerFeatures = new Dictionary<EMatixPlayerType,PlayerFeatures>();

            PlayerFeatures columnPlayer = new PlayerFeatures();
            columnPlayer.PlayerType = EMatixPlayerType.eMatixPlayerColumns;
            mPlayerFeatures.Add(EMatixPlayerType.eMatixPlayerColumns, columnPlayer);
            PlayerFeatures rowPlayer = new PlayerFeatures();
            rowPlayer.PlayerType = EMatixPlayerType.eMatixPlayerRows;
            mPlayerFeatures.Add(EMatixPlayerType.eMatixPlayerRows, rowPlayer);

            mCurrentTurnPlayerType = EMatixPlayerType.eNull;
            
        }

        private MatixBoard mBoard;

        
        private Dictionary<EMatixPlayerType, PlayerFeatures> mPlayerFeatures;
        private EMatixPlayerType mCurrentTurnPlayerType;
        private EMatixPlayerType mLatestSelectionType;
        private int mLatestSelectionIndex;


        public void SetupForGameStart(MatixBoard inBoard,EMatixPlayerType inStartingPlayer)
        {
            Random rand = new Random();

            mBoard = inBoard;
            mPlayerFeatures[EMatixPlayerType.eMatixPlayerColumns].SetupForGameStart();
            mPlayerFeatures[EMatixPlayerType.eMatixPlayerRows].SetupForGameStart();
            mCurrentTurnPlayerType = inStartingPlayer;

            int rowInitialSelection = rand.Next(8);
            int columnInitialSelection = rand.Next(8);
            mPlayerFeatures[EMatixPlayerType.eMatixPlayerColumns].AvailableSelections.Remove(columnInitialSelection);
            mPlayerFeatures[EMatixPlayerType.eMatixPlayerRows].AvailableSelections.Remove(rowInitialSelection);

            mLatestSelectionType = EMatixPlayerType.eMatixPlayerColumns == mCurrentTurnPlayerType?
                                    EMatixPlayerType.eMatixPlayerRows : EMatixPlayerType.eMatixPlayerColumns;
            mLatestSelectionIndex = EMatixPlayerType.eMatixPlayerColumns == mLatestSelectionType ? 
                                    columnInitialSelection : rowInitialSelection;
        }

        public MatixBoard Board
        {
            get { return mBoard; }
        }

        [DataMember]
        public Dictionary<EMatixPlayerType, PlayerFeatures> PlayerFeatures
        {
            get { return mPlayerFeatures; }
            set { mPlayerFeatures = value; }
        }

        [DataMember]
        public EMatixPlayerType CurrentTurnPlayerType
        {
            get { return mCurrentTurnPlayerType; }
            set { mCurrentTurnPlayerType = value; }
        }

        [DataMember]
        public EMatixPlayerType LatestSelectionType
        {
            get { return mLatestSelectionType; }
            set { mLatestSelectionType = value; }
        }

        [DataMember]
        public int LatestSelectionIndex
        {
            get { return mLatestSelectionIndex; }
            set { mLatestSelectionIndex = value; }
        }

        public bool IsStageTerminal()
        {
            return HasNoMoreMoves();
        }

        private bool HasNoMoreMoves()
        {
            return PlayerHasNoMoreMoves(EMatixPlayerType.eMatixPlayerColumns) &&
                    PlayerHasNoMoreMoves(EMatixPlayerType.eMatixPlayerRows);
        }

        private bool PlayerHasNoMoreMoves(EMatixPlayerType inMatixPlayerType)
        {
            return PlayerFeatures[inMatixPlayerType].AvailableSelections.Count == 0;
        }

        #region IAssignableObject<MatixStage> Members

        public void Assign(MatixStage inStage)
        {
            mBoard = inStage.mBoard;

            mPlayerFeatures[EMatixPlayerType.eMatixPlayerColumns].Assign(
                    inStage.mPlayerFeatures[EMatixPlayerType.eMatixPlayerColumns]);
            mPlayerFeatures[EMatixPlayerType.eMatixPlayerRows].Assign(
                    inStage.mPlayerFeatures[EMatixPlayerType.eMatixPlayerRows]);
            mCurrentTurnPlayerType = inStage.mCurrentTurnPlayerType;
            mLatestSelectionIndex = inStage.mLatestSelectionIndex;
            mLatestSelectionType = inStage.mLatestSelectionType;
        }

        #endregion

        internal void SetupFromGameStage(MatixBoard inBoardGame, 
                                         EMatixPlayerType inStartingPlayer, 
                                         MatixSharedGameServices.MatixStage inStage)
        {
            mBoard = inBoardGame;
            mPlayerFeatures[EMatixPlayerType.eMatixPlayerColumns].SetupFromPlayer(
                                                                        inStage.PlayerFeatures[MatixSharedGameServices.EMatixPlayerType.eMatixPlayerColumns]);
            mPlayerFeatures[EMatixPlayerType.eMatixPlayerRows].SetupFromPlayer(
                                                                        inStage.PlayerFeatures[MatixSharedGameServices.EMatixPlayerType.eMatixPlayerRows]);
            mCurrentTurnPlayerType = (inStage.CurrentTurnPlayerType == MatixSharedGameServices.EMatixPlayerType.eMatixPlayerColumns ?
                                        EMatixPlayerType.eMatixPlayerColumns :
                                        EMatixPlayerType.eMatixPlayerRows);
            mLatestSelectionType = (inStage.LatestSelectionType == MatixSharedGameServices.EMatixPlayerType.eMatixPlayerColumns ?
                                        EMatixPlayerType.eMatixPlayerColumns :
                                        EMatixPlayerType.eMatixPlayerRows);
            mLatestSelectionIndex = inStage.LatestSelectionIndex;
        }
    }
}
