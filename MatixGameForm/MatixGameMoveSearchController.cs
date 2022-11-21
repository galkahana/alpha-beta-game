using System;
using System.Collections.Generic;
using System.Text;
using XBLIP.AplhaBetaGame;

namespace MatixGameForm
{
    internal class MatixGameMoveSearchController : IGameController<MatixStage, MatixMove>
    {
        public MatixGameMoveSearchController(EMatixPlayerType inPlayerSearchingFor)
        {
            mPlayerSearchingFor = inPlayerSearchingFor;
            mOtherPlayerType = GetOtherPlayerType(inPlayerSearchingFor);
        }

        private EMatixPlayerType GetOtherPlayerType(EMatixPlayerType inPlayerType)
        {
            return (EMatixPlayerType.eMatixPlayerColumns == inPlayerType ?
                                                                            EMatixPlayerType.eMatixPlayerRows :
                                                                            EMatixPlayerType.eMatixPlayerColumns);
        }

        private EMatixPlayerType mPlayerSearchingFor;
        private EMatixPlayerType mOtherPlayerType;

        #region IGameController<MatixStage,MatixMove> Members

        public bool IsStageTerminal(MatixStage inStage)
        {
            return inStage.IsStageTerminal();
        }

        public long GetStaticEvaluationValue(MatixStage inStage)
        {
            return inStage.PlayerFeatures[mPlayerSearchingFor].PlayerScore - 
                            inStage.PlayerFeatures[mOtherPlayerType].PlayerScore;
        }

        public IEnumerable<MatixMove> GenerateMovesForStage(MatixStage inStage)
        {
            MatixMove aMove = new MatixMove();
            PlayerFeatures currentPlayerFeatures = inStage.PlayerFeatures[inStage.CurrentTurnPlayerType];
            foreach(int availableIndex in currentPlayerFeatures.AvailableSelections.Keys)
            {
                aMove.SelectionIndex = availableIndex;
                yield return aMove;
            }
        }

        public void ExecuteMove(MatixStage inStage, MatixMove inMove)
        {
            PlayerFeatures currentPlayerFeatures = inStage.PlayerFeatures[inStage.CurrentTurnPlayerType];
            currentPlayerFeatures.PlayerScore += 
                inStage.Board.GetScoreAt(inStage.CurrentTurnPlayerType, inMove.SelectionIndex,
                                         inStage.LatestSelectionType, inStage.LatestSelectionIndex);
            currentPlayerFeatures.AvailableSelections.Remove(inMove.SelectionIndex);
            inStage.LatestSelectionIndex = inMove.SelectionIndex;
            inStage.LatestSelectionType = inStage.CurrentTurnPlayerType;
            inStage.CurrentTurnPlayerType = GetOtherPlayerType(inStage.CurrentTurnPlayerType);
        }

        public MatixStage CreateNewStage()
        {
            return new MatixStage();
        }

        #endregion
    }
}
