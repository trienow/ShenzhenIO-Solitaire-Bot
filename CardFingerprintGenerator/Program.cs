using SHENZENSolitaire.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace CardFingerprintGenerator
{
    class Program
    {
        public static readonly Card[] POSSIBLE_STATES = new Card[]
        {
            Card.EMPTY,
            new Card(1, SuitEnum.EMPTY), //<- The table
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

        static void Main(string[] args)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("./lut.cs", false, Encoding.UTF8))
                {
                    sw.AutoFlush = true;

                    sw.WriteLine("private readonly Card[] cards = new Card[]");
                    sw.WriteLine("{");

                    foreach (Card c in POSSIBLE_STATES)
                    {
                        byte cardValue = c.Value;
                        if (c.Suit == SuitEnum.EMPTY && c.Value > 0) //<- Used for the table image
                        {
                            cardValue = 0;
                        }

                        sw.WriteLine($"\tnew Card({cardValue}, SuitEnum.{c.Suit.ToString()}),");
                    }

                    sw.WriteLine("};");
                    sw.WriteLine();
                    sw.WriteLine("private readonly byte[][] cardColors = new byte[][]");
                    sw.WriteLine("{");


                    foreach (Card c in POSSIBLE_STATES)
                    {
                        sw.Write($"\tnew byte[]{{ ");

                        string resourceName = string.Concat(GetPrefix(c), c.Value);
                        if (!(Properties.Resources.ResourceManager.GetObject(resourceName) is Bitmap bmp))
                        {
                            throw new KeyNotFoundException($"Unknown Resource type: {resourceName}");
                        }

                        byte[] colors = new byte[16 * 16 * 3];
                        string comma = string.Empty;
                        for (int x = 0; x < 16; x++)
                        {
                            for (int y = 0; y < 16; y++)
                            {
                                Color color = bmp.GetPixel(x, y);
                                sw.Write($"{comma}{color.R,-3}, {color.G,-3}, {color.B,-3}");
                                comma = ", ";
                            }
                        }

                        sw.WriteLine(" },");
                    }

                    sw.WriteLine("};");
                    sw.Flush();
                }
                Console.WriteLine("DONE");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.Read();
            }
            Console.Read();
        }

        static char GetPrefix(Card c)
        {
            switch (c.Suit)
            {
                case SuitEnum.EMPTY: return 'e';
                case SuitEnum.BLOCKED: return 'x';
                case SuitEnum.ROSE: return 'f';
                case SuitEnum.RED: return 'r';
                case SuitEnum.GREEN: return 'g';
                case SuitEnum.BLACK: return 'b';
                default: throw new KeyNotFoundException("Unknown Suit");
            }
        }
    }
}
