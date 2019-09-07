using SHENZENSolitaire.Game;
using SHENZENSolitaire.Utils;
using System.Threading.Tasks;

namespace SHENZENSolitaire.ScreenHandler
{
    public class ScreenExtractor
    {
        public const int Y_TOP = 315;
        public const int X_FLOWER = 1301;
        public static readonly int[] XS = new int[] { 733, 885, 1037, 1189, 1341, 1493, 1645, 1797 };
        public static readonly int[] YS = new int[] { 579, 610, 641, 672, 703, 734, 765, 796, 827, 858, 889 };

        public static PlayingField ExtractField()
        {
            ImageHandler ih = new ImageHandler();
            PlayingField field = new PlayingField();


            field.SetSlot(true, 0, ih.GetCardAt(XS[0], Y_TOP));
            field.SetSlot(true, 1, ih.GetCardAt(XS[1], Y_TOP));
            field.SetSlot(true, 2, ih.GetCardAt(XS[2], Y_TOP));

            field.SetSlot(true, 3, ih.GetCardAt(X_FLOWER, Y_TOP));

            for (int x = 5; x < 8; x++)
            {
                ExtractedCard bestExCard = GetCardInOutputSlot(ih, x);

                field.SetSlot(true, 7, bestExCard); //<- The playing field will place the card in the correct slot
            }

            Parallel.For(0, 8, (col) =>
            {
                for (int row = 0; row < 10; row++)
                {
                    field.SetSlot(false, col, ih.GetCardAt(XS[col], YS[row]));
                }
            });

            PlayingFieldPrinter.Print(field);

            return field;
        }

        private static ExtractedCard GetCardInOutputSlot(ImageHandler ih, int x)
        {
            ExtractedCard bestExCard = ih.GetCardAt(XS[x], 315);
            for (int y = 314; y >= 300; y--) //<- The higher the card number is, the higher the card is placed on screen!
            {
                ExtractedCard exCard = ih.GetCardAt(XS[x], y);
                if (exCard.Error < bestExCard.Error)
                {
                    bestExCard = exCard;
                }
            }

            return bestExCard;
        }

        private static Card[] RealOutputSlots()
        {
            Card[] cards = new Card[3];
            ImageHandler ih = new ImageHandler();

            for (int x = 5; x < 8; x++)
            {
                cards[x - 5] = GetCardInOutputSlot(ih, x);
            }

            return cards;
        }

        internal static Vec2 TranslateSlotToPos(bool top, int col, int row, Card movedCard)
        {
            Vec2 coord = new Vec2(XS[col], Y_TOP);
            if (top)
            {
                if (col == 3)
                {
                    coord.X = X_FLOWER;
                }
                else if(col > 3)
                {
                    Card[] realOutpus = RealOutputSlots();
                    for (int i = 0; i < realOutpus.Length; i++)
                    {
                        if (realOutpus[i].Suit == movedCard.Suit)
                        {
                            coord.X = XS[5 + i];
                            break;
                        }
                    }
                }
            }
            else
            {
                coord.X = XS[col];
                coord.Y = YS[row];
            }

            return coord;
        }
    }
}
