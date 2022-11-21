using System;
using System.Collections.Generic;
using System.Text;
using XBLIP.AplhaBetaGame;

namespace MatixGameForm
{
    internal class MatixGame
    {
        public MatixGame()
        {
            mRowPlayer = null;
            mColumnPlayer = null;
            mCurrentGameStage = new MatixStage();
            mBoardGame = new MatixBoard();
            mRowGameMoveSearchController = new MatixGameMoveSearchController(EMatixPlayerType.eMatixPlayerRows);
            mColumnGameMoveSearchController = new MatixGameMoveSearchController(EMatixPlayerType.eMatixPlayerColumns);
            mBestMoveSearchMechanism = new AlphaBetaGame<MatixStage, MatixMove, MatixGameMoveSearchController>();
            mColumnPlayerProfiler = new PlayerProfiler<MatixStage, MatixMove, MatixGameMoveSearchController>();
            mRowPlayerProfiler = new PlayerProfiler<MatixStage, MatixMove, MatixGameMoveSearchController>();
            mMaximumSearchLevelForComputerPlayer = 0;
            
        }

        private MatixPlayer mRowPlayer;
        private MatixPlayer mColumnPlayer;
        private MatixStage mCurrentGameStage;
        private MatixBoard mBoardGame;
        private MatixGameMoveSearchController mRowGameMoveSearchController;
        private MatixGameMoveSearchController mColumnGameMoveSearchController;
        private AlphaBetaGame<MatixStage, MatixMove, MatixGameMoveSearchController> mBestMoveSearchMechanism;
        private PlayerProfiler<MatixStage, MatixMove, MatixGameMoveSearchController> mColumnPlayerProfiler;
        private PlayerProfiler<MatixStage, MatixMove, MatixGameMoveSearchController> mRowPlayerProfiler;
        private int mMaximumSearchLevelForComputerPlayer;


        public MatixBoard BoardGame
        {
            get { return mBoardGame; }
            set { mBoardGame = value; }
        }

        public MatixStage CurrentGameStage
        {
            get { return mCurrentGameStage; }
            set { mCurrentGameStage = value; }
        }

        public IEnumerable<int> GetAvailablePlayerIndexes(EMatixPlayerType inPlayerType)
        {
            foreach (int index in mCurrentGameStage.PlayerFeatures[inPlayerType].AvailableSelections.Keys)
            {
                yield return index;
            }
        }

        public IEnumerable<int> GetUnAvailablePlayerIndexes(EMatixPlayerType inPlayerType)
        {
            for (int i = 0; i < MatixBoard.scBoardSize; ++i)
            {
                if (!mCurrentGameStage.PlayerFeatures[inPlayerType].AvailableSelections.ContainsKey(i))
                    yield return i;
            }
        }

        public void StartNewGame(MatixPlayer inRowPlayer, 
                                 MatixPlayer inColumnPlayer, 
                                 EMatixPlayerType inStartingPlayer,
                                 int inMaximumSearchLevelForComputerPlayer,
                                 int inMaximumSearchLevelForProfiling)
        {
            mRowPlayer = inRowPlayer;
            mColumnPlayer = inColumnPlayer;

            mBoardGame.CreateNewBoardValues();
            mMaximumSearchLevelForComputerPlayer = inMaximumSearchLevelForComputerPlayer;
            mColumnPlayerProfiler.SetMaximumSearchLevel(inMaximumSearchLevelForProfiling);
            mRowPlayerProfiler.SetMaximumSearchLevel(inMaximumSearchLevelForProfiling);
            mCurrentGameStage.SetupForGameStart(mBoardGame,inStartingPlayer);
        }


        public bool IsGameEnded()
        {
            return mCurrentGameStage.IsStageTerminal();
        }

        public MatixPlayer GetGameWinner()
        {
            if(IsGameEnded())
            {
                long rowPlayerScore = GetPlayerScore(mRowPlayer);
                long columnPlayerScore = GetPlayerScore(mColumnPlayer);

                if (rowPlayerScore > columnPlayerScore)
                    return mRowPlayer;
                else if (rowPlayerScore < columnPlayerScore)
                    return mColumnPlayer;
                else
                    return null;
            }
            else
                return null;
        }

        public long GetPlayerScore(MatixPlayer inPlayer)
        {
            EMatixPlayerType playerType = GetPlayerTypeForPlayer(inPlayer);
            if(EMatixPlayerType.eNull == playerType)
                return -1;
            else
                return mCurrentGameStage.PlayerFeatures[playerType].PlayerScore;
        }

        private EMatixPlayerType GetPlayerTypeForPlayer(MatixPlayer inPlayer)
        {
            if (mRowPlayer == inPlayer)
                return EMatixPlayerType.eMatixPlayerRows;
            else if (mColumnPlayer == inPlayer)
                return EMatixPlayerType.eMatixPlayerColumns;
            else
                return EMatixPlayerType.eNull;
        }

        public bool ExecuteExternalMove(MatixMove inMove)
        {
            if (!IsGameEnded() && IsCurrentPlayerOKForExternalMove() && IsMoveValidForCurrentPlayer(inMove))
            {
                ExecuteMove(inMove);
                return true;
            }
            else
                return false;
        }

        private bool IsMoveValidForCurrentPlayer(MatixMove inMove)
        {
            bool foundOKMove = false;
            // pick any game search controller, both can generate moves
            foreach (MatixMove move in mRowGameMoveSearchController.GenerateMovesForStage(mCurrentGameStage))
            {
                foundOKMove = inMove.Equals(move);
                if (foundOKMove)
                    break;
            }
            return foundOKMove;
        }

        private bool IsCurrentPlayerOKForExternalMove()
        {
            return (GetCurrentPlayer().MoveType == EMatixMoveType.eMatixMoveManual ||
                    GetCurrentPlayer().IsRemote);
        }

        public MatixPlayer GetCurrentPlayer()
        {
            if (EMatixPlayerType.eMatixPlayerColumns == mCurrentGameStage.CurrentTurnPlayerType)
                return mColumnPlayer;
            else
                return mRowPlayer;
        }

        private void ExecuteMove(MatixMove inMove)
        {
            // profile
            if (EMatixPlayerType.eMatixPlayerColumns == mCurrentGameStage.CurrentTurnPlayerType)
                mColumnPlayerProfiler.EvaluatePlayerLevelFromMove(mCurrentGameStage, 
                                                                  inMove, 
                                                                  mColumnGameMoveSearchController,
                                                                  mRowGameMoveSearchController);
            else
                mRowPlayerProfiler.EvaluatePlayerLevelFromMove(mCurrentGameStage,
                                                                  inMove,
                                                                  mRowGameMoveSearchController,
                                                                  mColumnGameMoveSearchController);

            
            // each of the controllers is good, both can execute moves
            mRowGameMoveSearchController.ExecuteMove(mCurrentGameStage, inMove);

        }

        public int GetProfiledColumnPlayerLevel()
        {
            return mColumnPlayerProfiler.GetProfiledLevel();
        }

        public int GetProfiledRowPlayerLevel()
        {
            return mRowPlayerProfiler.GetProfiledLevel();
        }

        public bool ExecuteComputedMove(out MatixMove outExecutedMove)
        {
            if (!IsGameEnded() && IsCurrentPlayerOKForComputedMove())
            {
                ComputeBestMoveForCurrentPlayer(mMaximumSearchLevelForComputerPlayer,out outExecutedMove);
                ExecuteMove(outExecutedMove);
                return true;
            }
            else
            {
                outExecutedMove = null;
                return false;
            }
        }

        private bool IsCurrentPlayerOKForComputedMove()
        {
            return GetCurrentPlayer().MoveType == EMatixMoveType.eMatixMoveAutomatic;
        }

        private MatixGameMoveSearchController GetCurrentPlayerMoveSearchController()
        {
            if (EMatixPlayerType.eMatixPlayerColumns == mCurrentGameStage.CurrentTurnPlayerType)
                return mColumnGameMoveSearchController;
            else
                return mRowGameMoveSearchController;
        }

        public void ComputeBestMoveForCurrentPlayer(int inMaximumSearchLevel, out MatixMove outBestComputedMove)
        {
            mBestMoveSearchMechanism.SetMaximumSearchLevel(inMaximumSearchLevel);
            mBestMoveSearchMechanism.FindBestMove(GetCurrentPlayerMoveSearchController(), 
                                                  CurrentGameStage, 
                                                  out outBestComputedMove);   
        }

        internal void StartGameFromJoinedGame(MatixPlayer inRowPlayer, 
                                              MatixPlayer inColumnPlayer, 
                                              EMatixPlayerType inStartingPlayer, 
                                              int inMaximumSearchLevelForComputerPlayer,
                                              int inMaximumSearchLevelForProfiling,
                                              MatixSharedGameServices.MatixSharedGameInformation inSharedGameInformation)
        {
            mRowPlayer = inRowPlayer;
            mColumnPlayer = inColumnPlayer;

            mMaximumSearchLevelForComputerPlayer = inMaximumSearchLevelForComputerPlayer;
            mBoardGame.SetupBoardFromValues(inSharedGameInformation.SharedBoard);
            mColumnPlayerProfiler.SetMaximumSearchLevel(inMaximumSearchLevelForProfiling);
            mRowPlayerProfiler.SetMaximumSearchLevel(inMaximumSearchLevelForProfiling);
            mCurrentGameStage.SetupFromGameStage(mBoardGame, inStartingPlayer, inSharedGameInformation.GameStage);
        }

        internal void ResetProfiler()
        {
            mColumnPlayerProfiler.Reset();
            mRowPlayerProfiler.Reset();
        }
    }
}
