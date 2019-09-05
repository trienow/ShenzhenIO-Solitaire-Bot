using SHENZENSolitaire.Actor;

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
