using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHENZENSolitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Player p = new Player(PlayingFields.A1);
            p.FindSolution();
        }
    }
}
