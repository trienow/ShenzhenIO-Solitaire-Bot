using SHENZENSolitaire.Actor;
using SHENZENSolitaire.ScreenHandler;
using SHENZENSolitaire.Game;
using SHENZENSolitaire.Utils;
using System;
using System.Threading;

namespace SHENZENSolitaire
{
    class Program
    {
        static void Main()
        {
            Console.Title = "SHENZHEN IO Bot";

            Console.WriteLine("Press ENTER when the field can be extracted!");
            Console.Read();

            while (true)
            {
                Console.WriteLine("Grabbing field...");
                PlayingField field = ScreenExtractor.ExtractField();

                Player p = new Player(field);
                GameState finalState = p.FindSolution();

                if (finalState != null)
                {
                    Console.WriteLine($"Solution has {finalState.PathLength} steps!");
                    GameState[] moves = GameState.Linearize(finalState);
                    GameExecuter.FocusWindow();

                    for (int i = 0; i < moves.Length; i++)
                    {
                        GameStatePrinter.Print(moves[i]);

                        if (i > 0)
                        {
                            GameExecuter.ExecuteState(moves[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Solution found!");
                }

                Console.WriteLine("Sleping for 5sec...");
                Thread.Sleep(5000);

                GameExecuter.FocusWindow();
                GameExecuter.NewGame();
            }
        }
    }
}
