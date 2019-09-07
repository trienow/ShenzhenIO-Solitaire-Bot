namespace SHENZENSolitaire.ScreenHandler
{
    struct Vec2
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public int XInt { get => (int)X; }
        public int YInt { get => (int)Y; }

        public Vec2(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public decimal MaxValue()
        {
            decimal x = X < 0 ? -X : X;
            decimal y = Y < 0 ? -Y : Y;
            return x > y ? x : y;
        }

        public override string ToString()
        {
            return $"[{X} {Y}]";
        }

        public static Vec2 operator +(in Vec2 a, in Vec2 b)
        {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }
        public static Vec2 operator -(in Vec2 a, in Vec2 b)
        {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }
        public static Vec2 operator /(in Vec2 a, in decimal b)
        {
            return new Vec2(a.X / b, a.Y / b);
        }
    }
}
