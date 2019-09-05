using SHENZENSolitaire.Game;
using SHENZENSolitaire.Utils;
using System;
using System.Threading.Tasks;

namespace SHENZENSolitaire.Extractor
{
    public class ScreenExtractor
    {
        public static PlayingField ExtractField()
        {
            ImageHandler ih = new ImageHandler();
            PlayingField field = new PlayingField();

            int[] xs = new int[] { 733, 885, 1037, 1189, 1341, 1493, 1645, 1797 };
            int[] ys = new int[] { 579, 610, 641, 672, 703 };

            field.SetSlot(true, 0, ih.GetCardAt(xs[0], 315));
            field.SetSlot(true, 1, ih.GetCardAt(xs[1], 315));
            field.SetSlot(true, 2, ih.GetCardAt(xs[2], 315));

            field.SetSlot(true, 3, ih.GetCardAt(1301, 315));
            field.SetSlot(true, 4, ih.GetCardAt(xs[5], 315));
            field.SetSlot(true, 5, ih.GetCardAt(xs[6], 315));
            field.SetSlot(true, 6, ih.GetCardAt(xs[7], 315));

            Console.WriteLine("Got Top. Now getting field...");

            Parallel.For(0, 8, (col) =>
            {
                for (int row = 0; row < 5; row++)
                {
                    field.SetSlot(false, col, ih.GetCardAt(xs[col], ys[row]));
                }
            });

            PlayingFieldPrinter.Print(field);

            return field;
        }
    }
}
