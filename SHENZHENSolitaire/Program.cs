using SHENZENSolitaire.Actor;
using SHENZENSolitaire.Extractor;
using SHENZENSolitaire.Game;
using System;
using System.Collections.Generic;
using System.Threading;

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
            List<GameState> moves = p.FindSolution();
        }
    }
}
