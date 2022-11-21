using System;
using System.Collections.Generic;
using System.Text;

namespace AplhaBetaGameTester
{
    class TestMove : ICloneable
    {
        public TestMove(int inFromIndex,int inToIndex)
        {
            mFromIndex = inFromIndex;
            mToIndex = inToIndex;
        }


        private int mFromIndex;
        private int mToIndex;

        public int FromIndex
        {
            get { return mFromIndex; }
        }

        public int ToIndex
        {
            get { return mToIndex; }
        }

        #region ICloneable Members

        public object Clone()
        {
            return new TestMove(mFromIndex, mToIndex);
        }

        #endregion
    }
}
