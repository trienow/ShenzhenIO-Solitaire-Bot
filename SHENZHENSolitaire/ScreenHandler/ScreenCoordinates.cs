namespace SHENZENSolitaire.ScreenHandler
{
    internal static class ScreenCoordinates
    {
        #region [CONSTS PLAYING FIELD]
        /// <summary>
        /// The x-coordinates of the cards on the screen from the center of the screen.
        /// </summary>
        private static readonly int[] X_CENTER_FIELD = new int[] { -547, -395, -243, -91, 61, 213, 365, 517 };// { 733, 885, 1037, 1189, 1341, 1493, 1645, 1797 };
        /// <summary>
        /// The y-coordinates of the cards on the screen from the center of the screen.
        /// </summary>
        private static readonly int[] Y_CENTER_FIELD = new int[] { -141, -110, -79, -48, -17, 14, 45, 76, 107, 138, 169 };// { 579, 610, 641, 672, 703, 734, 765, 796, 827, 858, 889 }; 
        #endregion

        #region [CONSTS TOP ROW]
        /// <summary>
        /// The x-coordinate of the flower slot on the screen from the center of the screen.
        /// </summary>
        private const int X_CENTER_FLOWER = 21; // 1301
        /// <summary>
        /// The y-coordinate of the top row on the screen from the center of the screen.
        /// </summary>
        private const int Y_CENTER_TOP = -405; // 315 
        #endregion

        #region [CONSTS DRAGON]
        /// <summary>
        /// The x-coordinate of the dragon buttons from the center of the screen.
        /// </summary>
        private const int X_CENTER_DRAGON = -72; // 1208
        /// <summary>
        /// The y-coordinate of the red dragon button from the center of the screen.
        /// </summary>
        private const int Y_CENTER_DRAGON_RED = -383; // 337
        /// <summary>
        /// The y-coordinate of the green dragon button from the center of the screen.
        /// </summary>
        private const int Y_CENTER_DRAGON_GREEN = -300; // 420
        /// <summary>
        /// The y-coordinate of the black dragon button from the center of the screen.
        /// </summary>
        private const int Y_CENTER_DRAGON_BLACK = -211; // 509 
        #endregion

        #region [CONSTS SPECIAL]
        /// <summary>
        /// The x-coordinate of the new-game button from the center of the screen.
        /// </summary>
        private const int X_CENTER_NEWGAME = 497; // 1759
        /// <summary>
        /// The y-coordinate of the new-game button from the center of the screen.
        /// </summary>
        private const int Y_CENTER_NEWGAME = 393; // 1113

        /// <summary>
        /// The x-coordinate of nothing from the center of the screen. This is only used to focus the window.
        /// </summary>
        private const int X_CENTER_NOTHING = -35; // 1245
        /// <summary>
        /// The y-coordinate of nothing from the center of the screen. This is only used to focus the window.
        /// </summary>
        private const int Y_CENTER_NOTHING = -337; // 383
        #endregion

        #region [PLAYING FIELD]
        /// <summary>
        /// The x-coordinates of the cards on the screen.
        /// </summary>
        public static readonly int[] XField = new int[8];
        /// <summary>
        /// The y-coordinates of the cards on the screen.
        /// </summary>
        public static readonly int[] YField = new int[11];
        #endregion

        #region [TOP ROW]
        /// <summary>
        /// The x-coordinate of the flower slot on the screen.
        /// </summary>
        public static int XFlower { get; private set; }
        /// <summary>
        /// The y-coordinate of the top row.
        /// </summary>
        public static int YTop { get; private set; }
        #endregion

        #region [DRAGON]
        /// <summary>
        /// The x-coordinate of the dragon buttons.
        /// </summary>
        public static int XDragon { get; private set; }
        /// <summary>
        /// The y-coordinate of the red dragon button.
        /// </summary>
        public static int YDragonRed { get; private set; }
        /// <summary>
        /// The y-coordinate of the black dragon button.
        /// </summary>
        public static int YDragonGreen { get; private set; }
        /// <summary>
        /// The y-coordinate of the black dragon button.
        /// </summary>
        public static int YDragonBlack { get; private set; }
        #endregion

        #region [SPECIAL]
        /// <summary>
        /// The x-coordinate of the new-game button.
        /// </summary>
        public static int XNewGame { get; private set; }
        /// <summary>
        /// The y-coordinate of the new-game button.
        /// </summary>
        public static int YNewGame { get; private set; }

        /// <summary>
        /// The x-coordinate of nothing.
        /// </summary>
        public static int XNothing { get; private set; }
        /// <summary>
        /// The y-coordinate of the nothing.
        /// </summary>
        public static int YNothing { get; private set; }
        #endregion

        /// <summary>
        /// By setting the screen size, the positions of the cards is calculated from the center of the screen.
        /// This might get messy, if the default resolution changed via the scale factor in the system settings.
        /// </summary>
        /// <param name="width">The width / horizontal screen resolution in pixels.</param>
        /// <param name="height">The height / vertical screen resolution in pixels.</param>
        public static void SetScreenSize(int width, int height)
        {
            int half = width / 2;
            for (int i = 0; i < X_CENTER_FIELD.Length; i++)
            {
                XField[i] = half + X_CENTER_FIELD[i];
            }
            XFlower = half + X_CENTER_FLOWER;
            XDragon = half + X_CENTER_DRAGON;

            XNewGame = half + X_CENTER_NEWGAME;
            XNothing = half + X_CENTER_NOTHING;

            half = height / 2;
            for (int i = 0; i < Y_CENTER_FIELD.Length; i++)
            {
                YField[i] = half + Y_CENTER_FIELD[i];
            }
            YTop = half + Y_CENTER_TOP;

            YDragonRed = half + Y_CENTER_DRAGON_RED;
            YDragonGreen = half + Y_CENTER_DRAGON_GREEN;
            YDragonBlack = half + Y_CENTER_DRAGON_BLACK;

            YNewGame = half + Y_CENTER_NEWGAME;
            YNothing = half + Y_CENTER_NOTHING;
        }
    }
}
