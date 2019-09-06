using SHENZENSolitaire.Actor;
using SHENZENSolitaire.Extractor;
using SHENZENSolitaire.Game;
using SHENZENSolitaire.Utils;
using System;

namespace SHENZENSolitaire
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Press ENTER when the field can be extracted!");
            Console.Read();
            Console.WriteLine("Grabbing field...");
            PlayingField field = ScreenExtractor.ExtractField();
            Console.WriteLine("Ok?");
            Console.Read();
            Console.Read();

            Player p = new Player(field);
            GameState finalState = p.FindSolution();

            if (finalState != null)
            {
                GameState[] moves = GameState.Linearize(finalState);

                for (int i = 0; i < moves.Length; i++)
                {
                    GameStatePrinter.Print(moves[i]);
                    string feedback = Console.ReadLine();

                    if (feedback == "b" && i > 0) i -= 2;
                }
            }
            else
            {
                Console.WriteLine("Nichts gefunden!");
            }

            Console.Read();
            Console.Read();
        }
    }
}
