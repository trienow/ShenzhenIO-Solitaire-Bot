using System;
using System.Collections.Generic;
using System.Linq;

namespace SHENZENSolitaire.Game
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
            Card.DRAGON_RED,
            Card.DRAGON_RED,
            Card.DRAGON_RED,
            Card.DRAGON_RED,
            Card.DRAGON_GREEN,
            Card.DRAGON_GREEN,
            Card.DRAGON_GREEN,
            Card.DRAGON_GREEN,
            Card.DRAGON_BLACK,
            Card.DRAGON_BLACK,
            Card.DRAGON_BLACK,
            Card.DRAGON_BLACK,
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
        private static readonly Card[] DECK_BUFFER = new Card[]
        {
            Card.EMPTY,
            Card.BLOCKED,
            new Card(suit: SuitEnum.ROSE),
            Card.DRAGON_RED,
            Card.DRAGON_GREEN,
            Card.DRAGON_BLACK,
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

        /// <summary>
        /// Gets a card in the specified column of the top part of the playing field
        /// </summary>
        /// <param name="col">The interested column</param>
        /// <returns>The card in the specified slot</returns>
        public Card this[int col]
        {
            get => this.topArea[col];
        }

        /// <summary>
        /// Gets a card at the defined position in the field
        /// </summary>
        /// <param name="col">The column of the stack of cards.</param>
        /// <param name="row">The card index in the column</param>
        /// <returns>The card object</returns>
        public Card this[int col, int row]
        {
            get => this.fieldArea[col][row];
        }

        /// <summary>
        /// Gets the length of a stack of cards.
        /// </summary>
        /// <param name="col">The column of the playing field to the stack size from</param>
        /// <returns>The card count of the stack</returns>
        public int GetColumnLength(byte col) => this.fieldArea[col].Count;

        /// <summary>
        /// Inits a new empty playing field
        /// </summary>
        public PlayingField()
        {
            for (int i = 0; i < COLUMNS_TOP; i++)
            {
                this.topArea[i] = new Card();
            }

            for (int i = 0; i < COLUMNS_FIELD; i++)
            {
                this.fieldArea[i] = new List<Card>(MAX_ROWS_FIELD);
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
                this.topArea[f] = Card.Unpack(fingerprint[f]);
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

                this.fieldArea[col].Add(Card.Unpack(fingerprint[f]));
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
                    this.fieldArea[i].Add(DECK[nextCardIndex]);
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
                if (column > 2)
                {
                    switch (card.Suit)
                    {
                        case SuitEnum.ROSE: topArea[3] = card; break;
                        case SuitEnum.RED: topArea[4] = card; break;
                        case SuitEnum.BLACK: topArea[5] = card; break;
                        case SuitEnum.GREEN: topArea[6] = card; break;
                    }
                }
                else
                {
                    this.topArea[column] = card;
                }
            }
            else if (card.Suit != SuitEnum.EMPTY && card.Suit != SuitEnum.BLOCKED)
            {
                this.fieldArea[column].Add(card);
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
            int cardCount = this.fieldArea[column].Count; //<- Column length
            bool result = true;

            //If the requested card is the last card
            if (row + 1 < cardCount)
            {
                Card lastCard = this.fieldArea[column][row];
                if (lastCard.Value > 0)
                {
                    //If it isn't and it is not a dragon, go through the stack
                    for (int i = row + 1; i < cardCount; i++)
                    {
                        Card thisCard = this.fieldArea[column][i];

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
            bool allowed = true; //<- Let's start off allowing everything and going from there

            if (turn.MergeDragons == default(SuitEnum))
            {
                byte toCol = turn.ToColumn;
                byte fromCol = turn.FromColumn;

                Card fromCard = turn.FromTop ? this.topArea[fromCol] : this.fieldArea[fromCol][turn.FromRow];
                int cardBunch = this.fieldArea[fromCol].Count - turn.FromRow;

                allowed = fromCard.Suit != SuitEnum.BLOCKED && fromCard.Suit != SuitEnum.EMPTY; //<- If it is not a system-suit
                allowed = turn.FromTop && fromCol < 3 || this.IsMovable(fromCol, turn.FromRow); //<- If it is movable at all

                if (allowed && turn.ToTop)
                {
                    allowed = turn.FromTop || cardBunch == 1; //<- Only one card may go to the top
                    allowed = allowed && !(toCol > 3 && fromCard.Value - 1 != topArea[toCol].Value); //<- Output slots should only allow rising values

                    switch (toCol)
                    {
                        case 4: allowed = allowed && fromCard.Suit == SuitEnum.RED; break; //<- The first output should be red
                        case 5: allowed = allowed && fromCard.Suit == SuitEnum.BLACK; break; //<- The second output should be black
                        case 6: allowed = allowed && fromCard.Suit == SuitEnum.GREEN; break; //<- The third output should be green
                    }

                    allowed = allowed && !(turn.FromTop && fromCol < 3 && toCol < 3); //<- Buffer swapping should not be permitted
                    allowed = allowed && !(toCol < 4 && this.topArea[toCol].Suit != SuitEnum.EMPTY); //<- The first four slots must be empty
                    allowed = allowed && !(toCol == 3 && fromCard.Suit != SuitEnum.ROSE); //<- The fourth slot also requires the from card to be a rose
                }

                int destinationColumnLength = this.fieldArea[toCol].Count;
                if (allowed && !turn.ToTop && destinationColumnLength > 0)
                {
                    allowed = destinationColumnLength + cardBunch < MAX_ROWS_FIELD; //<- If the destination column is not already full

                    allowed = allowed && fromCard.Value != 0; //<- Stacking Dragons onto anything is not allowed

                    //If the destination column can accept the card at all
                    Card toCard = this.fieldArea[toCol][destinationColumnLength - 1];
                    allowed = allowed && fromCard.Value + 1 == toCard.Value && fromCard.Suit != toCard.Suit; //<- Rule for stacking numbers
                }
            }
            else
            {
                allowed = this.FindMergableDragons().Contains(turn.MergeDragons);
            }

            return allowed;
        }

        /// <summary>
        /// Returns <see langword="true"/>, when dragons are available for merging
        /// </summary>
        /// <param name="color">The dragon color to search for</param>
        /// <returns><see langword="true"/> when dragons can be merged</returns>
        public SuitEnum[] FindMergableDragons()
        {
            int globalFreeBufferSlot = 0;
            int[] coloredFreeBufferSlots = new int[3];
            int totalFreeBufferSlots = 0;
            for (byte col = 0; col < COLUMNS_BUFFER; col++)
            {
                Card c = this.topArea[col];
                if (c == Card.EMPTY)
                {
                    globalFreeBufferSlot++;
                    totalFreeBufferSlots++;
                }
                else if (c == Card.DRAGON_RED)
                {
                    coloredFreeBufferSlots[0]++;
                    totalFreeBufferSlots++;
                }
                else if (c == Card.DRAGON_GREEN)
                {
                    coloredFreeBufferSlots[1]++;
                    totalFreeBufferSlots++;
                }
                else if (c == Card.DRAGON_BLACK)
                {
                    coloredFreeBufferSlots[2]++;
                    totalFreeBufferSlots++;
                }
            }

            List<SuitEnum> mergableSuits = new List<SuitEnum>(totalFreeBufferSlots);
            int[] openDragons = new int[] { coloredFreeBufferSlots[0], coloredFreeBufferSlots[1], coloredFreeBufferSlots[2], };
            if (totalFreeBufferSlots > 0)
            {
                for (byte col = 0; col < COLUMNS_FIELD; col++)
                {
                    int row = this.GetColumnLength(col) - 1;

                    if (row == -1) continue;

                    if (this.fieldArea[col][row] == Card.DRAGON_RED && this.IsMovable(col, row))
                    {
                        openDragons[0]++;
                    }
                    else if (this.fieldArea[col][row] == Card.DRAGON_GREEN && this.IsMovable(col, row))
                    {
                        openDragons[1]++;
                    }
                    else if (this.fieldArea[col][row] == Card.DRAGON_BLACK && this.IsMovable(col, row))
                    {
                        openDragons[2]++;
                    }
                }

                //Convert found dragons to their suits
                for (byte i = 0; i < 3; i++)
                {
                    if (openDragons[i] == 4 && (coloredFreeBufferSlots[i] > 0 || globalFreeBufferSlot > 0))
                    {
                        globalFreeBufferSlot -= coloredFreeBufferSlots[i] > 0 ? 0 : 1;
                        switch (i)
                        {
                            case 0: mergableSuits.Add(SuitEnum.RED); break;
                            case 1: mergableSuits.Add(SuitEnum.GREEN); break;
                            case 2: mergableSuits.Add(SuitEnum.BLACK); break;
                        }
                    }
                }
            }

            return mergableSuits.ToArray();
        }

        /// <summary>
        /// Performs an unchecked merge on the field. Check with <see cref="CanMergeDragons(SuitEnum)"/>.
        /// </summary>
        /// <param name="color"></param>
        protected void PerformDragonMerge(SuitEnum color)
        {
            bool hasBlocked = false;
            Card dragonCard = new Card(0, color);
            int dragonsFound = 0;

            //Reserve and clear dragons from the top part
            for (int col = 0; col < 3; col++)
            {
                if (this.topArea[col] == dragonCard)
                {
                    if (hasBlocked)
                    {
                        this.topArea[col] = new Card(0, SuitEnum.EMPTY);
                    }
                    else
                    {
                        this.topArea[col] = new Card(0, SuitEnum.BLOCKED);
                        hasBlocked = true;
                        dragonsFound++;
                    }
                }
                else if (this.topArea[col].Suit == SuitEnum.EMPTY)
                {
                    if (!hasBlocked)
                    {
                        this.topArea[col] = new Card(0, SuitEnum.BLOCKED);
                        hasBlocked = true;
                    }
                }
            }

            //Clear dragons from the field
            for (byte col = 0; col < COLUMNS_FIELD; col++)
            {
                int columnLength = this.GetColumnLength(col);
                for (int row = columnLength - 1; row >= 0; row--)
                {
                    if (this.fieldArea[col][row] == dragonCard)
                    {
                        this.fieldArea[col].RemoveAt(row);
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
        public PlayingField PerformTurn(Turn turn)
        {
            PlayingField field = new PlayingField(this);

            if (turn.MergeDragons == default(SuitEnum))
            {
                int toCol = turn.ToColumn;
                int fromCol = turn.FromColumn;

                if (turn.FromTop)
                {
                    if (turn.ToTop)
                    {
                        field.topArea[toCol] = field.topArea[fromCol];
                    }
                    else
                    {
                        field.fieldArea[toCol].Add(field.topArea[fromCol]);
                    }
                    field.topArea[fromCol] = Card.EMPTY;
                }
                else
                {
                    int fromRow = turn.FromRow;
                    if (turn.ToTop)
                    {
                        field.topArea[toCol] = field.fieldArea[fromCol][fromRow];
                        field.fieldArea[fromCol].RemoveAt(fromRow);
                    }
                    else
                    {
                        while (field.fieldArea[fromCol].Count > fromRow)
                        {
                            field.fieldArea[toCol].Add(field.fieldArea[fromCol][fromRow]);
                            field.fieldArea[fromCol].RemoveAt(fromRow);
                        }
                    }
                }
            }
            else
            {
                field.PerformDragonMerge(turn.MergeDragons);
            }

            return field;
        }

        /// <summary>
        /// Indicates a finished game
        /// </summary>
        /// <returns>Returns true, when all output slots have been filled with the right cards</returns>
        public bool IsGameOver()
        {
            return this.topArea[3].Suit == SuitEnum.ROSE && this.topArea[4].Value == 9 && this.topArea[5].Value == 9 && this.topArea[6].Value == 9;
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
                length += this.GetColumnLength(col);
            }

            byte[] fingerprint = new byte[length];
            int f = 0;

            foreach (Card c in DECK_BUFFER)
            {
                //Fill the array with the buffer area, ignoring different permutations
                for (byte col = 0; col < COLUMNS_BUFFER; col++)
                {
                    if (c == this.topArea[col])
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
                fingerprint[f] = this.topArea[col].GetFingerprint();
                f++;
            }

            //Now on to the playing field
            fingerprint[f] = 0xFF;
            f++;

            foreach (List<Card> column in this.fieldArea)
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

        public byte[][] MakeFingerprints()
        {
            byte[][] stacks = new byte[COLUMNS_FIELD + 1][];

            for (byte col = 0; col < COLUMNS_FIELD; col++)
            {
                int columnLength = GetColumnLength(col);
                stacks[col] = new byte[columnLength];
                for (byte row = 0; row < columnLength; row++)
                {
                    stacks[col][row] = fieldArea[col][row].GetFingerprint();
                }
            }

            //The buffer should remain the same every permutation
            stacks[COLUMNS_FIELD] = new byte[COLUMNS_TOP];
            byte f = 0;
            foreach (Card c in DECK_BUFFER)
            {
                //Fill the array with the buffer area, ignoring different permutations
                for (byte col = 0; col < COLUMNS_BUFFER; col++)
                {
                    if (c == this.topArea[col])
                    {
                        stacks[COLUMNS_FIELD][f] = c.GetFingerprint();
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

            //Transfer the outputs
            for (; f < COLUMNS_TOP; f++)
            {
                stacks[COLUMNS_FIELD][f] = topArea[f].GetFingerprint();
            }

            return stacks;
        }
    }
}
