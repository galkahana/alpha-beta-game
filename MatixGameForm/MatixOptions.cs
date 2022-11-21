using System;
using System.Collections.Generic;
using System.Text;

namespace MatixGameForm
{
    class MatixOptions
    {
        public const int scMaximumSearchLevelMatix = 3;

        public MatixOptions()
        {
            mWhoStartsTheGame = EMatixPlayerType.eMatixPlayerColumns;
            mRowMoveType = EMatixMoveType.eMatixMoveAutomatic;
            mColumnMoveType = EMatixMoveType.eMatixMoveManual;
            mMaximumMoveSearchLevel = scMaximumSearchLevelMatix;
            mMaximumHintMoveSearchLevel = scMaximumSearchLevelMatix;
            mMaximumProfilerSearchLevel = scMaximumSearchLevelMatix;

        }

        public EMatixPlayerType mWhoStartsTheGame;
        public EMatixMoveType mRowMoveType;
        public EMatixMoveType mColumnMoveType;
        public int mMaximumMoveSearchLevel;
        public int mMaximumHintMoveSearchLevel;
        public int mMaximumProfilerSearchLevel;
    }
}
