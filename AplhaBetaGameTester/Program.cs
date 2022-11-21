using System;
using System.Collections.Generic;
using System.Text;
using XBLIP.AplhaBetaGame;

namespace AplhaBetaGameTester
{
    class Program
    {
        

        static void Main(string[] args)
        {
            TestController aController = new TestController();
            TestStage aStage = new TestStage();
            aStage.StageIndex = 0;
            TestMove resultMove;

            AlphaBetaGame<TestStage, TestMove, TestController> aGame = new AlphaBetaGame<TestStage, TestMove, TestController>();

            aGame.FindBestMove(aController, aStage, out resultMove);

            if (resultMove == null)
                Console.WriteLine("Failed to find move");
            else
                Console.WriteLine("Selected Move from {0} to {1}", resultMove.FromIndex, resultMove.ToIndex);
            Console.ReadKey(true);
        }
    }
}
