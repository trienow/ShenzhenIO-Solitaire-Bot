using SHENZENSolitaire.Actor;
using SHENZENSolitaire.Extractor;
using SHENZENSolitaire.Game;
using System;
using System.Collections.Generic;

namespace SHENZENSolitaire
{
    class Program
    {
        static void Main(string[] args)
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
                foreach (GameState move in moves)
                {
                    Console.WriteLine(move);
                }
            }
            else
            {
                Console.WriteLine("Nix gefunden!");
            }

            Console.Read();
            Console.Read();
        }
    }
}
