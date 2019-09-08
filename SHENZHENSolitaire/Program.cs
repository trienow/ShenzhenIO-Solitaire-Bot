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

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine();
            }

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
                    Console.WriteLine($"Lösung in {finalState.PathLength} Schritten!");
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
                    Console.WriteLine("Nichts gefunden!");
                }

                Console.WriteLine("Sleeping 10s...");
                Thread.Sleep(10000);

                GameExecuter.NewGame();
            }
        }
    }
}
