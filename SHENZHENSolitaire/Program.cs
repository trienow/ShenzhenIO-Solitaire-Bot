using SHENZENSolitaire.Actor;
using SHENZENSolitaire.Extractor;
using SHENZENSolitaire.Game;
using System;
using System.Threading;

namespace SHENZENSolitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(5000);
            Console.WriteLine("Grabbing field...");
            PlayingField field = ScreenExtractor.ExtractField();
            Console.Read();
            Player p = new Player(field);
            p.FindSolution();
        }
    }
}
