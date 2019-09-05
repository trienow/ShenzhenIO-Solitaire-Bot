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

            Card[] outputCards = new Card[] { ih.GetCardAt(xs[5], 315), ih.GetCardAt(xs[6], 315), ih.GetCardAt(xs[7], 315) };
            for (int i = 0; i < 3; i++)
            {
                switch (outputCards[i].Suit)
                {
                    case SuitEnum.RED: field.SetSlot(true, 4, outputCards[i]); break;
                    case SuitEnum.BLACK: field.SetSlot(true, 5, outputCards[i]); break;
                    case SuitEnum.GREEN: field.SetSlot(true, 6, outputCards[i]); break;
                }
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
