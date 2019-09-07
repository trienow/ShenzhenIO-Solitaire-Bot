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
            Console.WriteLine("Press ENTER when the field can be extracted!");
            Console.Read();

            while (true)
            {
                Console.WriteLine("Grabbing field...");
                PlayingField field = ScreenExtractor.ExtractField();
                //Console.WriteLine("Ok?");
                //Console.Read();
                //Console.Read();

                Player p = new Player(field);
                GameState finalState = p.FindSolution();


                if (finalState != null)
                {
                    Console.WriteLine($"Lösung in {finalState.PathLength} Schritten!");
                    GameState[] moves = GameState.Linearize(finalState);

                    for (int i = 0; i < moves.Length; i++)
                    {
                        GameStatePrinter.Print(moves[i]);

                        if (i > 0)
                        {
                            GameExecuter.ExecuteState(moves[i]);
                        }

                        //string feedback = Console.ReadLine();
                        //if (feedback == "b" && i > 0) i -= 2;
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

            //Console.WriteLine("DONE");
            //Console.Read();
            //Console.Read();
        }
    }
}
