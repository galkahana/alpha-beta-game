using System;
using System.Collections.Generic;
using System.Text;
using XBLIP.AplhaBetaGame;

namespace AplhaBetaGameTester
{
    class TestController : IGameController<TestStage,TestMove>
    {
        #region IGameController<TestStage,TestMove> Members

        private readonly int[] mTerminalStagesScores = new int[] {10,11,9,12,14,15,13,14,5,2,4,1,3,22,20,21};


        public bool IsStageTerminal(TestStage inStage)
        {
            return inStage.StageIndex > 14;
        }

        public long GetStaticEvaluationValue(TestStage inStage)
        {
            if (inStage.StageIndex > 14 && inStage.StageIndex < 31)
                return mTerminalStagesScores[inStage.StageIndex - 15];
            else
                return -1;
        }

        public IEnumerable<TestMove> GenerateMovesForStage(TestStage inStage)
        {
            for (int i = 0; i < 2; ++i)
            {
                yield return new TestMove(inStage.StageIndex, inStage.StageIndex * 2 + (i + 1));
            }
        }

        public void ExecuteMove(TestStage inStage, TestMove inMove)
        {
            inStage.StageIndex = inMove.ToIndex;
        }

        public TestStage CreateNewStage()
        {
            return new TestStage();
        }

        #endregion
    }
}
