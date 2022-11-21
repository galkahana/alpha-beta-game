using System;
using System.Collections.Generic;
using System.Text;
using XBLIP.AplhaBetaGame;

namespace AplhaBetaGameTester
{
    class TestStage : IAssignableObject<TestStage>
    {
        int mStageIndex;

        public int StageIndex
        {
            get { return mStageIndex; }
            set { mStageIndex = value; }
        }

        #region IAssignableObject<TestStage> Members

        public void Assign(TestStage inStage)
        {
            mStageIndex = inStage.StageIndex;
        }

        #endregion
    }
}
