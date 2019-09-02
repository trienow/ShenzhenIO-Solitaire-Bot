using System;
using System.Collections.Generic;

namespace SHENZENSolitaire
{
    public class PlayingField : ICloneable
    {
        public const int COLUMNS_TOP = 7;
        public const int COLUMNS_FIELD = 8;
        private const int MAX_ROWS_FIELD = 10;

        private readonly Card[] topArea = new Card[COLUMNS_TOP];
        private readonly List<Card>[] fieldArea = new List<Card>[COLUMNS_FIELD]; //Array: Column | List: Row

        public Card this[int col]
        {
            get => topArea[col];
        }

        public Card this[int col, int row]
        {
            get => fieldArea[col][row];
        }

        public int GetColumnLength(int col) => fieldArea[col].Count;

        /// <summary>
        /// Inits a new empty playing field
        /// </summary>
        public PlayingField()
        {
            for (int i = 0; i < COLUMNS_TOP; i++)
            {
                topArea[i] = new Card();
            }

            for (int i = 0; i < COLUMNS_FIELD; i++)
            {
                fieldArea[i] = new List<Card>(MAX_ROWS_FIELD);
            }
        }

        /// <summary>
        /// Inits a new playing field and copies the values over from the given one
        /// </summary>
        /// <param name="field">The field to copy from</param>
        public PlayingField(PlayingField field) : this()
        {
            for (int col = 0; col < COLUMNS_TOP; col++)
            {
                this.topArea[col] = field.topArea[col];
            }

            for (int col = 0; col < COLUMNS_FIELD; col++)
            {
                foreach (Card card in field.fieldArea[col])
                {
                    this.fieldArea[col].Add(card);
                }
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

            for (int i = 0; i < COLUMNS_FIELD; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int nextCardIndex = rnd.Next(deck.Count);
                    fieldArea[i].Add(deck[nextCardIndex]);
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
                fieldArea[column].Add(card);
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
            int cardCount = fieldArea[column].Count; //<- Column length
            bool result = true;

            //If the requested card is the last card
            if (row + 1 < cardCount)
            {
                Card lastCard = fieldArea[column][row];
                if (lastCard.Value > 0)
                {
                    //If it isn't and it is not a dragon, go through the stack
                    for (int i = row + 1; i < cardCount; i++)
                    {
                        Card thisCard = fieldArea[column][i];

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
            int toCol = turn.ToColumn;
            int fromCol = turn.FromColumn;

            Card fromCard = turn.FromTop ? this.topArea[fromCol] : this.fieldArea[fromCol][this.GetColumnLength(fromCol) - 1];
            int destinationColumnLength = this.fieldArea[toCol].Count;
            bool allowed = true; //<- Let's start off allowing everything and going from there

            allowed = fromCard.Suit != SuitEnum.BLOCKED && fromCard.Suit != SuitEnum.EMPTY; //<- If it is not a system-suit
            allowed = turn.FromTop && fromCol < 3 || this.IsMovable(fromCol, turn.FromRow); //<- If it is movable at all

            if (allowed && turn.ToTop)
            {
                allowed = allowed && (turn.FromTop || turn.FromRow == destinationColumnLength - 1); //<- Only one card may go to the top

                allowed = allowed && !(toCol == 4 && fromCard.Suit != SuitEnum.RED); //<- The first output should be red
                allowed = allowed && !(toCol == 5 && fromCard.Suit != SuitEnum.BLACK); //<- The second output should be black
                allowed = allowed && !(toCol == 6 && fromCard.Suit != SuitEnum.GREEN); //<- The third output should be green

                allowed = allowed && !(turn.FromTop && fromCol < 3 && toCol < 3); //<- Buffer swapping should not be permitted
                allowed = allowed && toCol < 4 && this.topArea[toCol].Suit == SuitEnum.EMPTY; //<- The first four slots must be empty
                allowed = allowed && !(toCol == 3 && fromCard.Suit != SuitEnum.ROSE); //<- The fourth slot also requires the from card to be a rose
            }

            if (allowed && !turn.ToTop && destinationColumnLength > 0)
            {
                int cardBunch = this.fieldArea[fromCol].Count - turn.FromRow;
                allowed = allowed && destinationColumnLength + cardBunch < MAX_ROWS_FIELD; //<- If the destination column is not already full

                //If the destination column can accept the card at all
                Card toCard = this.fieldArea[toCol][destinationColumnLength - 1];
                allowed = fromCard.Value + 1 == toCard.Value && fromCard.Suit != toCard.Suit;
            }

            return allowed;
        }

        /// <summary>
        /// Performs a turn on the field
        /// </summary>
        /// <param name="turn">The action to perform</param>
        public void PerformTurnUnchecked(Turn turn)
        {
            int toCol = turn.ToColumn;
            int fromCol = turn.FromColumn;

            if (turn.FromTop)
            {
                if (turn.ToTop)
                {
                    topArea[toCol] = topArea[fromCol];
                }
                else
                {
                    fieldArea[toCol].Add(topArea[fromCol]);
                    topArea[fromCol] = new Card();
                }
            }
            else
            {
                int fromRow = turn.FromRow;
                if (turn.ToTop)
                {
                    topArea[toCol] = fieldArea[fromCol][fromRow];
                }
                else
                {
                    while (fieldArea[fromCol].Count == fromRow)
                    {
                        fieldArea[toCol].Add(fieldArea[fromCol][fromRow]);
                        fieldArea[fromCol].RemoveAt(fromRow);
                    }
                }

                fieldArea[fromCol].RemoveAt(fromRow);
            }
        }

        public object Clone()
        {
            return new PlayingField(this);
        }
    }
}
