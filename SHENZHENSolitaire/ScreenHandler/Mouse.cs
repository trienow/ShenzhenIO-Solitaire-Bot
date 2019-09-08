using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SHENZENSolitaire.ScreenHandler
{
    public static class Mouse
    {
#pragma warning disable IDE1006 // Benennungsstile
        [DllImport("user32.dll")]
        private static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
#pragma warning restore IDE1006 // Benennungsstile

        public static void DragFromTo(int fromX, int fromY, int toX, int toY)
        {
            MoveMouseTo(fromX, fromY, MouseEventFlags.LEFTDOWN);
            Thread.Sleep(100);
            MoveMouseTo(toX, toY, MouseEventFlags.LEFTUP);
        }

        public static void ClickTo(int x, int y)
        {
            MoveMouseTo(x, y, MouseEventFlags.LEFTDOWN);
            Thread.Sleep(50);
            mouse_event(MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public static void MoveMouseTo(int x, int y, uint action)
        {
            GetCursorPos(out Point startPos);
            
            Vec2 currPos = startPos;
            Vec2 diff = new Vec2(x, y) - currPos;

            int steps = (int)(diff.MaxValue() / 4);

            if (steps > 0)
            {
                Vec2 delta = diff / steps;

                for (int i = 0; i < steps; i++)
                {
                    currPos += delta;
                    SetCursorPos(currPos.XInt, currPos.YInt);
                    Thread.Sleep(1);
                }
            }

            mouse_event(action, 0, 0, 0, 0);
        }
    }
}
