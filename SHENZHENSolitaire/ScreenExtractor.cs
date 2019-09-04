using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SHENZENSolitaire
{
    public class ScreenExtractor
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int ReleaseDC(IntPtr window, IntPtr dc);

        private static Color GetColorAt(int x, int y)
        {
            int a = (int)GetARGBAt(x, y);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        private static uint GetARGBAt(int x, int y)
        {
            //Thanks to bitterblue: https://stackoverflow.com/a/24759418/7396282
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            uint a = GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return a;
        }

        public string GetColors()
        {
            string colors = "";

            int xBaseBase = 733;
            int yBaseBase = 579;

            for (int i = 0; i < 5; i++)
            {
                int xBase = xBaseBase;
                int yBase = yBaseBase + 31 * i;
                colors += $"x: {xBase} y: {yBase} color: ";
                double dr = 0, dg = 0, db = 0;
                for (int x = xBase; x < xBase + 16; x++)
                {
                    for (int y = yBase; y < yBase + 16; y++)
                    {
                        uint a = GetARGBAt(x, y);
                        dr += ((a >> 0) & 0xff) / 256.0;
                        dg += ((a >> 8) & 0xff) / 256.0;
                        db += ((a >> 16) & 0xff) / 256.0;
                    }
                }

                colors += $"{(uint)dr} {(uint)dg} {(uint)db}\r\n";
            }


            return colors;
        }
    }
}
