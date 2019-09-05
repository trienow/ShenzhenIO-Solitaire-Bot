using SHENZENSolitaire.Game;
using System;
using System.Linq;

namespace SHENZENSolitaire.Utils
{
    public class PlayingFieldPrinter
    {
        public static void Print(PlayingField field)
        {
            for (int col = 0; col < PlayingField.COLUMNS_TOP; col++)
            {

                if (col == 3)
                {
                    SetConsoleColor(SuitEnum.EMPTY);
                    Console.Write("  ");
                }

                Card c = field[col];
                SetConsoleColor(c.Suit);
                Console.Write($"  {GetPrefix(c.Suit)}{c.Value}  ");

                SetConsoleColor(SuitEnum.EMPTY);
                Console.Write(" ");

                if (col == 3)
                {
                    Console.Write(" ");
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
                    if (row > rowsLut[col])
                    {
                        SetConsoleColor(SuitEnum.EMPTY);
                        Console.Write($"      ");
                    }
                    else
                    {
                        Card c = field[col, row];
                        SetConsoleColor(c.Suit);
                        Console.Write($"  {GetPrefix(c.Suit)}{c.Value}  ");

                        SetConsoleColor(SuitEnum.EMPTY);
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
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
