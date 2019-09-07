namespace SHENZENSolitaire.ScreenHandler
{
    public static class MouseEventFlags
    {
        public const uint LEFTDOWN = 0x00000002;
        public const uint LEFTUP = 0x00000004;
        public const uint MIDDLEDOWN = 0x00000020;
        public const uint MIDDLEUP = 0x00000040;
        public const uint MOVE = 0x00000001;
        public const uint ABSOLUTE = 0x00008000;
        public const uint RIGHTDOWN = 0x00000008;
        public const uint RIGHTUP = 0x00000010;

        public const uint ABS_LEFTDOWN = ABSOLUTE | LEFTDOWN;
        public const uint ABS_LEFTUP = ABSOLUTE | LEFTUP;
    }
}
