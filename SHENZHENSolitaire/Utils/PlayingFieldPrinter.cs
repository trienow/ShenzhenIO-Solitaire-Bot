using SHENZENSolitaire.Game;
using System;
using System.Linq;

namespace SHENZENSolitaire.Utils
{
    public class PlayingFieldPrinter
    {
        public static void PrintTurn(Turn turn)
        {
            if (turn.MergeDragons != default)
            {
                SetConsoleColor(turn.MergeDragons);
                Console.Write($"Merge {turn.MergeDragons} Dragons");
            }
            else
            {
                if (turn.FromTop)
                {
                    Console.Write($"Buffer {turn.FromColumn + 1} to ");
                }
                else
                {
                    Console.Write($"Column {turn.FromColumn + 1} to ");
                }

                if (turn.ToTop)
                {
                    if (turn.ToColumn < 3)
                    {
                        Console.Write($"buffer slot {turn.ToColumn + 1}.");
                    }
                    else
                    {
                        Console.Write($"an output slot.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"column {turn.ToColumn + 1}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintCard(Card card)
        {
            SetConsoleColor(card.Suit);
            Console.Write($"  {GetPrefix(card.Suit)}{card.Value}  ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void Print(PlayingField field)
        {
            for (int col = 0; col < PlayingField.COLUMNS_TOP; col++)
            {
                if (col == 3)
                {
                    SetConsoleColor(SuitEnum.EMPTY);
                    Console.Write("     ");
                }

                Card c = field[col];
                PrintCard(c);

                SetConsoleColor(SuitEnum.EMPTY);
                Console.Write(" ");

                if (col == 3)
                {
                    Console.Write("  ");
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            int[] rowsLut = new int[PlayingField.COLUMNS_FIELD];
            for (byte col = 0; col < PlayingField.COLUMNS_FIELD; col++)
            {
                rowsLut[col] = field.GetColumnLength(col);
            }

            int maxRow = rowsLut.Max();
            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < PlayingField.COLUMNS_FIELD; col++)
                {
                    if (row >= rowsLut[col])
                    {
                        SetConsoleColor(SuitEnum.EMPTY);
                        Console.Write($"       ");
                    }
                    else
                    {
                        Card c = field[col, row];
                        PrintCard(c);

                        SetConsoleColor(SuitEnum.EMPTY);
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void SetConsoleColor(SuitEnum suit)
        {
            switch (suit)
            {
                case SuitEnum.BLOCKED:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;

                case SuitEnum.ROSE:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                case SuitEnum.RED:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                case SuitEnum.GREEN:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                case SuitEnum.BLACK:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                case SuitEnum.EMPTY:
                default:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }

        private static char GetPrefix(SuitEnum suit)
        {
            switch (suit)
            {
                case SuitEnum.ROSE: return 'F';
                case SuitEnum.RED: return 'R';
                case SuitEnum.GREEN: return 'G';
                case SuitEnum.BLACK: return 'B';
                case SuitEnum.EMPTY:
                case SuitEnum.BLOCKED:
                default: return ' ';
            }
        }
    }
}
