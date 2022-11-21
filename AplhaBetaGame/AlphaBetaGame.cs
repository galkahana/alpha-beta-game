using System;
using System.Collections.Generic;

namespace XBLIP.AplhaBetaGame
{

    public class AlphaBetaGame<TStage, TMove, TGameController>
                                where TGameController : IGameController<TStage,TMove>
                                where TStage : IAssignableObject<TStage>
                                where TMove : ICloneable
    {
        // 2. i'd like abort control added to controller...or any other events i can thing of.
        // 3. enter some policy goverend terminations. i want to automate depth terminations
        //    so that later i can make them more interesting.
        
        public const int scSearchLevelInfinity = -1;

        private int mMaxSearchLevel;
        private int mCurrentSearchLevel;

        public AlphaBetaGame()
        {
            mMaxSearchLevel = scSearchLevelInfinity;
        }


        public void SetMaximumSearchLevel(int inMaxSearchLevel)
        {
            mMaxSearchLevel = inMaxSearchLevel;
        }

        public void FindBestMove(TGameController inController,
                                 TStage inStage,
                                 out TMove outMove)
        {
            long result;
            FindBestMove(inController, inStage, out outMove, out result);
        }

        public void FindBestMove(TGameController inController,
                                 TStage inStage,
                                 out TMove outMove,
                                 out long outMoveValue)
        {
            outMove = default(TMove);
            mCurrentSearchLevel = 0;
            long currentValue = long.MinValue;
            if (!inController.IsStageTerminal(inStage) && mCurrentSearchLevel < mMaxSearchLevel)
            {
                TStage bufferStage = inController.CreateNewStage();
                ++mCurrentSearchLevel;

                foreach (TMove move in inController.GenerateMovesForStage(inStage))
                {
                    bufferStage.Assign(inStage);
                    inController.ExecuteMove(bufferStage, move);
                    long alphaBetaValue = Evalute(inController,
                                                  bufferStage,
                                                  currentValue,
                                                  long.MaxValue,
                                                  EStageMinMaxType.eStageMin);
                    if (alphaBetaValue > currentValue)
                    {
                        outMove = (TMove)move.Clone();
                        currentValue = alphaBetaValue;
                    }
                }

                --mCurrentSearchLevel;
            }
            outMoveValue = currentValue;
        }

        private enum EStageMinMaxType
        {
            eStageMax,
            eStageMin
        };

        private long Evalute(TGameController inController,
                             TStage inStage,
                             long inAlphaValue,
                             long inBetaValue,
                             EStageMinMaxType inStageMinMaxType)
        {
            if (inController.IsStageTerminal(inStage) || (mMaxSearchLevel == mCurrentSearchLevel))
            {
                return inController.GetStaticEvaluationValue(inStage);
            }
            else
            {
                IEnumerable<TMove> moves = inController.GenerateMovesForStage(inStage);
                long result;
                ++mCurrentSearchLevel;

                if (EStageMinMaxType.eStageMax == inStageMinMaxType)
                    result = EvaluateNonTerminalMaxNode(inController,inStage, moves, inAlphaValue, inBetaValue);
                else
                    result = EvaluateNonTerminalMinNode(inController,inStage, moves, inAlphaValue, inBetaValue);
                --mCurrentSearchLevel;
                return result;
            }
        }

        private long EvaluateNonTerminalMaxNode(TGameController inController,
                                                TStage inStage,
                                                IEnumerable<TMove> inMoves,
                                                long inAlphaValue,
                                                long inBetaValue)
        {
            bool didCutoff = false;
            TStage bufferStage = inController.CreateNewStage();

            foreach (TMove move in inMoves)
            {
                bufferStage.Assign(inStage);
                inController.ExecuteMove(bufferStage, move);
                long alphaBetaValue = Evalute(inController, 
                                              bufferStage, 
                                              inAlphaValue, 
                                              inBetaValue, 
                                              EStageMinMaxType.eStageMin);
                if(alphaBetaValue > inAlphaValue)
                {
                    inAlphaValue = alphaBetaValue;
                    if (inAlphaValue >= inBetaValue)
                    {
                        didCutoff = true;
                        break;
                    }
                }
            }
            if (didCutoff)
                return inBetaValue;
            else
                return inAlphaValue;
        }

        private long EvaluateNonTerminalMinNode(TGameController inController,
                                                TStage inStage,
                                                IEnumerable<TMove> inMoves,
                                                long inAlphaValue,
                                                long inBetaValue)
        {
            bool didCutoff = false;
            TStage bufferStage = inController.CreateNewStage();

            foreach (TMove move in inMoves)
            {
                bufferStage.Assign(inStage);
                inController.ExecuteMove(bufferStage, move);
                long alphaBetaValue = Evalute(inController, 
                                              bufferStage, 
                                              inAlphaValue, 
                                              inBetaValue, 
                                              EStageMinMaxType.eStageMax);
                if (alphaBetaValue < inBetaValue)
                {
                    inBetaValue = alphaBetaValue;
                    if (inAlphaValue >= inBetaValue)
                    {
                        didCutoff = true;
                        break;
                    }
                }
            }
            if (didCutoff)
                return inAlphaValue;
            else
                return inBetaValue;
        }
    }
}
