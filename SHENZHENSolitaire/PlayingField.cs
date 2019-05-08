using System;
using System.Collections.Generic;
using System.Linq;

namespace SHENZENSolitaire
{
    public class PlayingField
    {
        private Card[] topArea = new Card[7];
        private List<Card>[] field = new List<Card>[8];

        /// <summary>
        /// Inits a new empty playing field
        /// </summary>
        public PlayingField()
        {
            for (int i = 0; i < topArea.Length; i++)
            {
                topArea[i] = new Card();
            }

            for (int i = 0; i < field.Length; i++)
            {
                field[i] = new List<Card>();
            }
        }

        /// <summary>
        /// Generates a random field
        /// </summary>
        public void RandomField()
        {
            Random rnd = new Random();
            List<Card> deck = new List<Card>()
            {
                new Card(suit: SuitEnum.ROSE),
                new Card(suit: SuitEnum.RED),
                new Card(suit: SuitEnum.RED),
                new Card(suit:  SuitEnum.RED),
                new Card(suit:  SuitEnum.RED),
                new Card(suit: SuitEnum.GREEN),
                new Card(suit: SuitEnum.GREEN),
                new Card(suit: SuitEnum.GREEN),
                new Card(suit: SuitEnum.GREEN),
                new Card(suit: SuitEnum.BLACK),
                new Card(suit: SuitEnum.BLACK),
                new Card(suit: SuitEnum.BLACK),
                new Card(suit: SuitEnum.BLACK),
                new Card(1, SuitEnum.RED),
                new Card(2, SuitEnum.RED),
                new Card(3, SuitEnum.RED),
                new Card(4, SuitEnum.RED),
                new Card(5, SuitEnum.RED),
                new Card(6, SuitEnum.RED),
                new Card(7, SuitEnum.RED),
                new Card(8, SuitEnum.RED),
                new Card(9, SuitEnum.RED),
                new Card(1, SuitEnum.GREEN),
                new Card(2, SuitEnum.GREEN),
                new Card(3, SuitEnum.GREEN),
                new Card(4, SuitEnum.GREEN),
                new Card(5, SuitEnum.GREEN),
                new Card(6, SuitEnum.GREEN),
                new Card(7, SuitEnum.GREEN),
                new Card(8, SuitEnum.GREEN),
                new Card(9, SuitEnum.GREEN),
                new Card(1, SuitEnum.BLACK),
                new Card(2, SuitEnum.BLACK),
                new Card(3, SuitEnum.BLACK),
                new Card(4, SuitEnum.BLACK),
                new Card(5, SuitEnum.BLACK),
                new Card(6, SuitEnum.BLACK),
                new Card(7, SuitEnum.BLACK),
                new Card(8, SuitEnum.BLACK),
                new Card(9, SuitEnum.BLACK),
            };

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int nextCardIndex = rnd.Next(deck.Count);
                    field[i].Add(deck[nextCardIndex]);
                    deck.RemoveAt(nextCardIndex);
                }
            }
        }

        /// <summary>
        /// Forces a card into a specified column.
        /// </summary>
        /// <param name="topSlots">Whether or not the card should be put in the top slots or not</param>
        /// <param name="column">The column or top slot index to put the card in.</param>
        /// <param name="card">The card to put into the specified column</param>
        public void SetSlot(bool topSlots, int column, Card card)
        {
            if (topSlots)
            {
                topArea[column] = card;
            }
            else
            {
                field[column].Add(card);
            }
        }

        /// <summary>
        /// Checks whether or not a card can be moved at all, without checking where it could go.
        /// </summary>
        /// <param name="column">The column in the field</param>
        /// <param name="row">The row in the column in the field</param>
        /// <returns>Returns true, when the card is grabbable</returns>
        public bool IsMovable(int column, int row)
        {
            int cardCount = ColumnLength(column);
            bool result = true;

            //If the requested card is the last card
            if (row + 1 < cardCount)
            {
                Card lastCard = field[column][row];
                if (lastCard.Value > 0)
                {
                    //If it isn't and it is not a dragon, go through the stack
                    for (int i = row + 1; i < cardCount; i++)
                    {
                        Card thisCard = field[column][i];

                        if (thisCard.Value == 0 || thisCard.Suit == lastCard.Suit || thisCard.Value + 1 != lastCard.Value)
                        {
                            result = false;
                            break;
                        }

                        lastCard = thisCard;
                    }
                }
                else
                {
                    //If the requested card isn't last and it is a dragon, it is not movable.
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks whether or not a movement from and to is valid.
        /// </summary>
        /// <param name="turn">The turn to perform checks for</param>
        /// <returns>True, when the turn is allows by the rules</returns>
        public bool IsTurnAllowed(Turn turn)
        {
            if (turn.FromTop || IsMovable(turn.FromColumn, turn.FromRow))
            {

            }
        }

        /// <summary>
        /// Returns the length of a column in the field
        /// </summary>
        /// <param name="column">The column in the field</param>
        /// <returns>Returns the stack count</returns>
        public int ColumnLength(int column) => field[column].Count;
    }
}
