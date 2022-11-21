using System;
using System.Collections.Generic;

namespace XBLIP.AplhaBetaGame
{
    public interface IGameController<TStage, TMove>
    {
        bool IsStageTerminal(TStage inStage);
        long GetStaticEvaluationValue(TStage inStage);
        IEnumerable<TMove> GenerateMovesForStage(TStage inStage);
        void ExecuteMove(TStage inStage, TMove inMove);
        TStage CreateNewStage();
    }
}
