using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SHENZENSolitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread.Sleep(5000);
            //Console.WriteLine(new ScreenExtractor().GetColors());
            Player p = new Player(PlayingFields.A1);
            p.FindSolution();
        }
    }
}
