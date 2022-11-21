using System;
using System.Collections.Generic;
using System.Text;

namespace XBLIP.AplhaBetaGame
{
    /* This class tries to profile a player. Profiling a player is a process that tries to find a matching
     * "computer" player for the input player. It does so by utilizing the computer own decision mechanism
     * and comparing it to the player move decision, based on how a computer would evaluate this move. In essence
     * the process tries to match computers search level to the player. As this may flactuate between moves the
     * profiler should be an ongoing profiler matching a certain player for a given period of time. This way
     * the computer will be able to better match its own level to the player level.
     * 
     * The process is this:
     * 1. For each move that the player does the profiler should recieve the move and the stage before the move is made.
     * 2. Based on the stage and the move the computer does two things:
     *  2.1 Search for the best move for the given stage. The search is being done for all levels from 1 to the maximum search
     *  level. The result would be a list of moves and scores. This would be "what the computer would have done" according to a
     *  given search level
     *  2.2 Then the computer would try to evaluate how "good" the move is, or rather, how close it is a to a computer
     *  decision of one of the levels. To do this it will run again over the same levels it did in (2.1) minus 1, in order
     *  to match the same levels in the previous investigation. To compare with the previous level (1), the computer will
     *  use the current score past the player move execution.
     *  2.3 Now that we have both results a comparison may be done. For each level of in (2.1) score we'll measure the distance to the
     *  (2.2) score, and add it to a ready array. This would be the array to determine which level the player uses.
     *  The lowest score in this array (which contains the distances between 2.2 and 2.1) marks the closest level
     *  to the player level (according to this heuristic). And this will be the profiled level.
     *  2.4 To serve as a continuing analysis the array level is saved in the status, and in 2.3 the results are added to it
     *  At each time the level with the minimum score is the profiled level. Normalization would be good from time to time
     *  to avoid arriving a numeral limit.
     *
     */


    public class PlayerProfiler<TStage, TMove, TGameController>
                        where TGameController : IGameController<TStage, TMove>
                        where TStage : IAssignableObject<TStage>
                        where TMove : ICloneable
    {


        public const int scDefaultSearchLevel = 6;
        
        private int mMaxSearchLevel;
        private int mMaxItem;
        private AlphaBetaGame<TStage,TMove,TGameController> mBestMoveSearch;

        // note that since level 0 is meaningless, that item at position N marks level N + 1;
        private List<long> mLevelsScores; 

        public PlayerProfiler()
        {
            mMaxSearchLevel = scDefaultSearchLevel;
            mBestMoveSearch = new AlphaBetaGame<TStage, TMove, TGameController>();
            mLevelsScores = new List<long>();
            ResetLevelsScores();
        }

        public void Reset()
        {
            ResetLevelsScores();
        }

        public void SetMaximumSearchLevel(int inMaxSearchLevel)
        {
            if (inMaxSearchLevel != mMaxSearchLevel)
            {
                mMaxSearchLevel = inMaxSearchLevel;
                mMaxItem = mMaxSearchLevel;
                ResetLevelsScores();
            }
        }

        private void ResetLevelsScores()
        {
            if (mLevelsScores.Count > mMaxItem)
                mLevelsScores.RemoveRange(mMaxItem, mLevelsScores.Count - mMaxItem);
            for (int i = 0; i < mLevelsScores.Count; ++i)
                mLevelsScores[i] = 0;
            for (int i = mLevelsScores.Count; i < mMaxItem; ++i)
                mLevelsScores.Add(0);
        }

        public int EvaluatePlayerLevelFromMove(TStage inStageToExecuteMoveOn, 
                                               TMove inMoveToExecute, 
                                               TGameController inThisPlayerGameController,
                                               TGameController inOtherPlayerGameController)
        {
            long[] currentStageMovesScores = new long[mMaxItem];
            long[] afterMoveScores = new long[mMaxItem];
            TStage bufferStage = inThisPlayerGameController.CreateNewStage();

            ComputeStageScores(inStageToExecuteMoveOn, inThisPlayerGameController, currentStageMovesScores);
            bufferStage.Assign(inStageToExecuteMoveOn);
            inThisPlayerGameController.ExecuteMove(bufferStage, inMoveToExecute);
            ComputeOponentStageScore(bufferStage, inOtherPlayerGameController, afterMoveScores);

            AccumulateScores(currentStageMovesScores,afterMoveScores);
            return GetProfiledLevel();
        }

        private void AccumulateScores(long[] currentStageMovesScores, long[] afterMoveScores)
        {
            for (int i = 0; i < mLevelsScores.Count; ++i)
                mLevelsScores[i] += Math.Abs(currentStageMovesScores[i] - afterMoveScores[i]);
            NormalizeScores();
        }

        private void NormalizeScores()
        {
            long minValue = long.MaxValue;

            foreach (long levelScore in mLevelsScores)
            {
                if (levelScore < minValue)
                    minValue = levelScore;
            }

            for (int i = 0; i < mLevelsScores.Count; ++i)
                mLevelsScores[i] -= minValue;
        }

        public int GetProfiledLevel()
        {
            // Since normalization happens every time, the value of the 
            // lowest level should be 0.
            int profiledLevel = -1;

            // note that the minimum of equals is taken
            for (int i = 0; i < mLevelsScores.Count; ++i)
            {
                if (mLevelsScores[i] == 0)
                {
                    // remember that the item at N marks level N + 1
                    profiledLevel = i + 1;
                    break;
                }
            }
            return profiledLevel;
        }

        private void ComputeOponentStageScore(TStage inAfterPlayerMoveStage, 
                                              TGameController inGameController, 
                                              long[] inAfterMoveScores)
        {


            TMove aMove;
            long resultValue;

            // the first level gets the score matching the current level score.
            inAfterMoveScores[0] = -inGameController.GetStaticEvaluationValue(inAfterPlayerMoveStage);
            if(inGameController.IsStageTerminal(inAfterPlayerMoveStage))
            {
                for (int i = 1; i < mLevelsScores.Count; ++i)
                    inAfterMoveScores[i] = inAfterMoveScores[0];
            }
            else
            {
                for (int i = 1; i < mLevelsScores.Count; ++i)
                {
                    // searched level should be one off of the original, so that it would match the "state of mind"
                    // to what it was before the player move was made. Since the result value is according to the oponent
                    // its value should be made negative
                    mBestMoveSearch.SetMaximumSearchLevel(i);

                    mBestMoveSearch.FindBestMove(inGameController,
                                                 inAfterPlayerMoveStage,
                                                 out aMove,
                                                 out resultValue);
                    inAfterMoveScores[i] = -resultValue;
                }
            }
        }

        private void ComputeStageScores(TStage inStageToExemine, 
                                        TGameController inGameController, 
                                        long[] inLevelsBestMoveScores)
        {
            TMove aMove;

            for (int i = 0; i < mLevelsScores.Count; ++i)
            {
                mBestMoveSearch.SetMaximumSearchLevel(i + 1);

                mBestMoveSearch.FindBestMove(inGameController, 
                                             inStageToExemine, 
                                             out aMove, 
                                             out inLevelsBestMoveScores[i]);
            }
        }

    }
}
