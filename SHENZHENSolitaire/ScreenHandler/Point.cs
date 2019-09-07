using System.Runtime.InteropServices;

namespace SHENZENSolitaire.ScreenHandler
{
    [StructLayout(LayoutKind.Sequential)]
    struct Point
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return $"[{x} {y}]";
        }

        public static implicit operator Vec2(Point point)
        {
            return new Vec2(point.x, point.y);
        }
    }
}
