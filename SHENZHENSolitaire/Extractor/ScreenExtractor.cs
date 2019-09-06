using SHENZENSolitaire.Game;
using SHENZENSolitaire.Utils;
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
            int[] ys = new int[] { 579, 610, 641, 672, 703, 734, 765, 796, 827, 858, 889 };

            field.SetSlot(true, 0, ih.GetCardAt(xs[0], 315));
            field.SetSlot(true, 1, ih.GetCardAt(xs[1], 315));
            field.SetSlot(true, 2, ih.GetCardAt(xs[2], 315));

            field.SetSlot(true, 3, ih.GetCardAt(1301, 315));
            
            for (int x = 5; x < 8; x++)
            {
                ExtractedCard bestExCard = ih.GetCardAt(xs[x], 315);
                for (int y = 314; y >= 300; y--) //<- The higher the card number is, the higher the card is placed on screen!
                {
                    ExtractedCard exCard = ih.GetCardAt(xs[x], y);
                    if (exCard.Error < bestExCard.Error)
                    {
                        bestExCard = exCard;
                    }
                }

                field.SetSlot(true, 7, bestExCard); //<- The playing field will place the card in the correct slot
            }

            Parallel.For(0, 8, (col) =>
            {
                for (int row = 0; row < 10; row++)
                {
                    field.SetSlot(false, col, ih.GetCardAt(xs[col], ys[row]));
                }
            });

            PlayingFieldPrinter.Print(field);

            return field;
        }
    }
}
