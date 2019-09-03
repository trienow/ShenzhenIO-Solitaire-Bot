using System;
using System.Collections.Generic;

namespace SHENZENSolitaire
{
    public class PlayingField
    {
        public const byte COLUMNS_TOP = 7;
        public const byte COLUMNS_FIELD = 8;
        public const byte COLUMNS_BUFFER = 3;
        public const byte MAX_ROWS_FIELD = 10;

        private readonly Card[] topArea = new Card[COLUMNS_TOP];
        private readonly List<Card>[] fieldArea = new List<Card>[COLUMNS_FIELD]; //Array: Column | List: Row
        private static readonly Card[] DECK = new Card[]
        {
            new Card(suit: SuitEnum.ROSE),
            new Card(suit: SuitEnum.RED),
            new Card(suit: SuitEnum.RED),
            new Card(suit: SuitEnum.RED),
            new Card(suit: SuitEnum.RED),
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
        public static readonly Card[] DECK_BUFFER;

        static PlayingField()
        {
            int deckLen = DECK.Length;
            DECK_BUFFER = new Card[deckLen + 2];
            DECK_BUFFER[0] = new Card(suit: SuitEnum.BLOCKED);
            Array.Copy(DECK, 0, DECK_BUFFER, 0, deckLen);
            DECK_BUFFER[deckLen + 1] = new Card(suit: SuitEnum.EMPTY);
        }

        /// <summary>
        /// Gets a card in the specified column of the top part of the playing field
        /// </summary>
        /// <param name="col">The interested column</param>
        /// <returns>The card in the specified slot</returns>
        public Card this[int col]
        {
            get => topArea[col];
        }

        /// <summary>
        /// Gets a card at the defined position in the field
        /// </summary>
        /// <param name="col">The column of the stack of cards.</param>
        /// <param name="row">The card index in the column</param>
        /// <returns>The card object</returns>
        public Card this[int col, int row]
        {
            get => fieldArea[col][row];
        }

        /// <summary>
        /// Gets the length of a stack of cards.
        /// </summary>
        /// <param name="col">The column of the playing field to the stack size from</param>
        /// <returns>The card count of the stack</returns>
        public int GetColumnLength(byte col) => fieldArea[col].Count;

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
        /// Unpacks the <paramref name="fingerprint"/> to recreate the playing field
        /// </summary>
        /// <param name="fingerprint">The fingerprint <see cref="byte"/> array to load</param>
        public PlayingField(byte[] fingerprint) : this()
        {
            int f = 0;
            for (; f < COLUMNS_TOP; f++)
            {
                topArea[f] = Card.Unpack(fingerprint[f]);
            }

            int col = 0;
            f++;
            for (; f < fingerprint.Length; f++)
            {
                if (fingerprint[f] == 0xFF)
                {
                    col++;

                    if (col >= COLUMNS_FIELD) break;
                    continue;
                }

                fieldArea[col].Add(Card.Unpack(fingerprint[f]));
            }
        }

        /// <summary>
        /// Generates a random field
        /// </summary>
        public void RandomField()
        {
            Random rnd = new Random();
            List<Card> deck = new List<Card>(DECK);

            for (int i = 0; i < COLUMNS_FIELD; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int nextCardIndex = rnd.Next(deck.Count);
                    fieldArea[i].Add(DECK[nextCardIndex]);
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
            byte toCol = turn.ToColumn;
            byte fromCol = turn.FromColumn;

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
        /// Returns <see langword="true"/>, when dragons are available for merging
        /// </summary>
        /// <param name="color">The dragon color to search for</param>
        /// <returns><see langword="true"/> when dragons can be merged</returns>
        public bool CanMergeDragons(SuitEnum color)
        {
            Card dragonCard = new Card(0, color);
            int visibleDragons = 0;
            bool hasEmptyBufferSlot = false;
            for (int i = 0; i < 3; i++)
            {
                if (topArea[i] == dragonCard)
                {
                    hasEmptyBufferSlot = true;
                    visibleDragons++;
                }
                else if (!hasEmptyBufferSlot && topArea[i].Suit == SuitEnum.EMPTY)
                {
                    hasEmptyBufferSlot = true;
                }
            }

            bool result = false;
            if (hasEmptyBufferSlot)
            {
                for (byte col = 0; col < COLUMNS_FIELD; col++)
                {
                    int columnLength = GetColumnLength(col);
                    for (int row = 0; row < columnLength; row++)
                    {
                        if (fieldArea[col][row] == dragonCard)
                        {
                            if (IsMovable(col, row))
                            {
                                if (++visibleDragons == 4)
                                {
                                    result = true;
                                    goto END;
                                }
                            }
                            else
                            {
                                goto END;
                            }
                        }
                    }
                }
            }

        END: return result;
        }

        /// <summary>
        /// Performs an unchecked merge on the field. Check with <see cref="CanMergeDragons(SuitEnum)"/>.
        /// </summary>
        /// <param name="color"></param>
        public void PerformDragonMerge(SuitEnum color)
        {
            bool hasBlocked = false;
            Card dragonCard = new Card(0, color);
            int dragonsFound = 0;

            //Reserve and clear dragons from the top part
            for (int col = 0; col < 3; col++)
            {
                if (topArea[col] == dragonCard)
                {
                    if (hasBlocked)
                    {
                        topArea[col] = new Card(0, SuitEnum.EMPTY);
                    }
                    else
                    {
                        topArea[col] = new Card(0, SuitEnum.BLOCKED);
                        hasBlocked = true;
                        dragonsFound++;
                    }
                }
                else if (topArea[col].Suit == SuitEnum.EMPTY)
                {
                    if (!hasBlocked)
                    {
                        topArea[col] = new Card(0, SuitEnum.BLOCKED);
                        hasBlocked = true;
                    }
                }
            }

            //Clear dragons from the field
            for (byte col = 0; col < COLUMNS_FIELD; col++)
            {
                int columnLength = GetColumnLength(col);
                for (int row = columnLength - 1; row >= 0; row--)
                {
                    if (fieldArea[col][row] == dragonCard)
                    {
                        fieldArea[col].RemoveAt(row);
                        dragonsFound++;
                    }
                }

                if (dragonsFound == 4)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Performs an unchecked turn on the field. Check with <see cref="IsTurnAllowed(Turn)"/>.
        /// </summary>
        /// <param name="turn">The action to perform</param>
        public void PerformTurn(Turn turn)
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

        /// <summary>
        /// Indicates a finished game
        /// </summary>
        /// <returns>Returns true, when all output slots have been filled with the right cards</returns>
        public bool IsGameOver()
        {
            return topArea[3].Suit == SuitEnum.ROSE && topArea[4].Value == 9 && topArea[5].Value == 9 && topArea[6].Value == 9;
        }

        /// <summary>
        /// Creates a <see cref="byte[]"/> representing this object
        /// </summary>
        /// <returns>A <see cref="byte"/> representation of this playing field</returns>
        public byte[] MakeFingerprint()
        {
            //Calculate the length of the array
            int length = COLUMNS_TOP + 1 + COLUMNS_FIELD + 1;
            for (byte col = 0; col < COLUMNS_FIELD; col++)
            {
                length += GetColumnLength(col);
            }

            byte[] fingerprint = new byte[length];
            int f = 0;

            foreach (Card c in DECK_BUFFER)
            {
                //Fill the array with the buffer area, ignoring different permutations
                for (byte col = 0; col < COLUMNS_BUFFER; col++)
                {
                    if (c == topArea[col])
                    {
                        fingerprint[f] = c.GetFingerprint();
                        f++;

                        if (c.Value > 0 || c.Suit == SuitEnum.ROSE)
                        {
                            break; //<- Since we can only have one of each colored number / rose
                        }
                    }
                }

                if (f == 3)
                {
                    break;
                }
            }

            //Now transfer the output slots
            for (byte col = COLUMNS_BUFFER; col < COLUMNS_TOP; col++)
            {
                fingerprint[f] = topArea[col].GetFingerprint();
                f++;
            }

            //Now on to the playing field
            fingerprint[f] = 0xFF;
            f++;

            foreach (List<Card> column in fieldArea)
            {
                foreach (Card c in column)
                {
                    fingerprint[f] = c.GetFingerprint();
                    f++;
                }

                fingerprint[f] = 0xFF;
                f++;
            }

            return fingerprint;
        }
    }
}
