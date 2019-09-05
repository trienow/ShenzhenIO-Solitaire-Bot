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
        static void Main(string[] args)
        {
            try
            {

                using (StreamWriter sw = new StreamWriter("./lut.cs", false, Encoding.UTF8))
                {
                    sw.AutoFlush = true;

                    sw.WriteLine("private readonly Card[] cards = new Card[]");
                    sw.WriteLine("{");

                    foreach (Card c in PlayingField.DECK_BUFFER)
                    {
                        sw.WriteLine($"\tnew Card({c.Value}, SuitEnum.{c.Suit.ToString()}),");
                    }

                    sw.WriteLine("};");
                    sw.WriteLine();
                    sw.WriteLine("private readonly byte[][] cardColors = new byte[][]");
                    sw.WriteLine("{");


                    foreach (Card c in PlayingField.DECK_BUFFER)
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
