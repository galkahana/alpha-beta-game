using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{
    [DataContract]
    [KnownType(typeof(long[]))]
    public class MatixBoard
    {
        public MatixBoard()
        {
            mBoardCells = new long[scBoardSize, scBoardSize]; // first dimension is columnes, second dimension is rows
        }
        
        public const long scBoardSize = 8;
        private const int scCellMaxValue = 100;

        private long[,] mBoardCells;

        // the WCF asshole of serializer won't have multidimensions array, that git.
        [DataMember]
        public long[] BoardCells
        {
            get {
                long[] flatArray = new long[scBoardSize * scBoardSize];
                for (long i = 0; i < scBoardSize; ++i)
                    for (long j = 0; j < scBoardSize; ++j)
                        flatArray[i * scBoardSize + j] = mBoardCells[i, j];
                return flatArray;
            }
            set {
                for (long i = 0; i < scBoardSize; ++i)
                    for (long j = 0; j < scBoardSize; ++j)
                        mBoardCells[i, j] = value[i * scBoardSize + j];
            }
        }

        public void GetIndexesAt(EMatixPlayerType inFirstPlayerType,
                        int inFirstPlayerSelection,
                        EMatixPlayerType inSecondPlayerType,
                        int inSecondPlayerSelection,
                        out int outColumnIndex,
                        out int outRowIndex)
        {
            if (inFirstPlayerType == inSecondPlayerType)
            {
                outColumnIndex = -1;
                outRowIndex = -1;
                return;
            }

            if (EMatixPlayerType.eMatixPlayerRows == inFirstPlayerType)
            {
                outRowIndex = inFirstPlayerSelection;
                outColumnIndex = inSecondPlayerSelection;
            }
            else
            {
                outRowIndex = inSecondPlayerSelection;
                outColumnIndex = inFirstPlayerSelection;
            }
        }


        public long GetScoreAt(EMatixPlayerType inFirstPlayerType, 
                                int inFirstPlayerSelection, 
                                EMatixPlayerType inSecondPlayerType, 
                                int inSecondPlayerSelection)
        {
            int rowIndex,columnIndex;

            GetIndexesAt(inFirstPlayerType,
                         inFirstPlayerSelection,
                         inSecondPlayerType,
                         inSecondPlayerSelection,
                         out columnIndex,
                         out rowIndex);

            return mBoardCells[columnIndex,rowIndex];
        }


        public long GetScoreAt(int inColumnIndex, int inRowIndex)
        {
            return mBoardCells[inColumnIndex,inRowIndex];
        }

        internal void CreateNewBoardValues()
        {
            Random rand = new Random();
            for (long i = 0; i < scBoardSize; ++i)
                for(long j = 0; j < scBoardSize; ++j)
                    mBoardCells[i,j] = rand.Next(scCellMaxValue);
        }

        
        internal void SetupBoardFromValues(MatixSharedGameServices.MatixBoard inBoard)
        {
            for (long i = 0; i < scBoardSize; ++i)
                for (long j = 0; j < scBoardSize; ++j)
                    mBoardCells[i, j] = inBoard.BoardCells[(int)(i * scBoardSize + j)];
        }
    }
}
