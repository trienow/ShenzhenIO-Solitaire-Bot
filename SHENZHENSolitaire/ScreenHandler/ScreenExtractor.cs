using SHENZENSolitaire.Game;
using SHENZENSolitaire.Utils;
using System.Threading.Tasks;

namespace SHENZENSolitaire.ScreenHandler
{
    public class ScreenExtractor
    {
        /// <summary>
        /// Captures the cards from the real game from the screen.
        /// </summary>
        /// <returns>All card positions.</returns>
        public static PlayingField ExtractField()
        {
            ImageHandler ih = new ImageHandler();
            PlayingField field = new PlayingField();

            field.SetSlot(true, 0, ih.GetCardAt(ScreenCoordinates.XField[0], ScreenCoordinates.YTop));
            field.SetSlot(true, 1, ih.GetCardAt(ScreenCoordinates.XField[1], ScreenCoordinates.YTop));
            field.SetSlot(true, 2, ih.GetCardAt(ScreenCoordinates.XField[2], ScreenCoordinates.YTop));

            field.SetSlot(true, 3, ih.GetCardAt(ScreenCoordinates.XFlower, ScreenCoordinates.YTop));

            for (int x = 5; x < 8; x++)
            {
                ExtractedCard bestExCard = GetCardInOutputSlot(ih, x);

                field.SetSlot(true, 7, bestExCard); //<- The playing field will place the card in the correct slot
            }

            Parallel.For(0, 8, (col) =>
            {
                for (int row = 0; row < 10; row++)
                {
                    field.SetSlot(false, col, ih.GetCardAt(ScreenCoordinates.XField[col], ScreenCoordinates.YField[row]));
                }
            });

            PlayingFieldPrinter.Print(field);

            return field;
        }

        /// <summary>
        /// Gets the playing card from one of the output slots.
        /// </summary>
        /// <param name="ih">The <see cref="ImageHandler"/> to use to capture the screen.</param>
        /// <param name="x">The top slot number.</param>
        /// <returns>The card, that is most likely at the given position.</returns>
        private static ExtractedCard GetCardInOutputSlot(ImageHandler ih, int x)
        {
            ExtractedCard bestExCard = ih.GetCardAt(ScreenCoordinates.XField[x], 315);
            for (int y = 314; y >= 300; y--) //<- The higher the card number is, the higher the card is placed on screen!
            {
                ExtractedCard exCard = ih.GetCardAt(ScreenCoordinates.XField[x], y);
                if (exCard.Error < bestExCard.Error)
                {
                    bestExCard = exCard;
                }
            }

            return bestExCard;
        }

        /// <summary>
        /// Gets the three cards in the output slot.
        /// </summary>
        /// <returns>The three top-most discarded cards.</returns>
        private static Card[] RealOutputSlots()
        {
            Card[] cards = new Card[3];
            ImageHandler ih = new ImageHandler();

            //The first output is slot 5.
            for (int x = 5; x < 8; x++)
            {
                cards[x - 5] = GetCardInOutputSlot(ih, x);
            }

            return cards;
        }

        /// <summary>
        /// Maps my slot numbering to a pixel coordinate.
        /// </summary>
        /// <param name="top">Whether or not <paramref name="col"/> and <paramref name="row"/> are in the top row.</param>
        /// <param name="col">The column / stack of the card targeted.</param>
        /// <param name="row">The row of the card targeted.</param>
        /// <param name="movedCard">Only relevant for the top row. The output slots can vary. This makes sure, that the correct cards go to the correct slot in my representation.</param>
        /// <returns>A coordinate in screen space.</returns>
        internal static Vec2 TranslateSlotToPos(bool top, int col, int row, Card movedCard)
        {
            Vec2 coord = new Vec2(ScreenCoordinates.XField[col], ScreenCoordinates.YTop);
            if (top)
            {
                if (col == 3)
                {
                    coord.X = ScreenCoordinates.XFlower;
                }
                else if (col > 3)
                {
                    Card[] realOutpus = RealOutputSlots();
                    for (int i = 0; i < realOutpus.Length; i++)
                    {
                        if (realOutpus[i].Suit == movedCard.Suit)
                        {
                            coord.X = ScreenCoordinates.XField[5 + i];
                            break;
                        }
                    }
                }
            }
            else
            {
                coord.X = ScreenCoordinates.XField[col];
                coord.Y = ScreenCoordinates.YField[row];
            }

            coord += new Vec2(5, 5);

            return coord;
        }
    }
}
