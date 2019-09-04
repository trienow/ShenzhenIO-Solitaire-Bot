using SHENZENSolitaire;

namespace Tests
{
    public static class PlayingFields
    {
        private static readonly PlayingField a1 = new PlayingField();
        private static readonly PlayingField a2 = new PlayingField();

        static PlayingFields()
        {
            int col = 0;

            #region [A1]
            a1.SetSlot(false, col, new Card(8, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(3, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(8, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(3, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(7, SuitEnum.BLACK));
            col++;

            a1.SetSlot(false, col, new Card(1, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(2, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(5, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(9, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(5, SuitEnum.RED));
            col++;

            a1.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(6, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(4, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(7, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
            col++;

            a1.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(1, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(1, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(5, SuitEnum.BLACK));
            col++;

            a1.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(2, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(9, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
            col++;

            a1.SetSlot(false, col, new Card(9, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(0, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(0, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(6, SuitEnum.RED));
            col++;

            a1.SetSlot(false, col, new Card(3, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(0, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(0, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(2, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(0, SuitEnum.ROSE));
            col++;

            a1.SetSlot(false, col, new Card(8, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(4, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(6, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(4, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(7, SuitEnum.RED));
            #endregion

            #region [A2]
            a2.SetSlot(true, 0, Card.BLOCKED);
            a2.SetSlot(true, 1, Card.BLOCKED);
            a2.SetSlot(true, 2, Card.BLOCKED);
            a2.SetSlot(true, 3, new Card(0, SuitEnum.ROSE));
            a2.SetSlot(true, 4, new Card(7, SuitEnum.RED));
            a2.SetSlot(true, 5, new Card(7, SuitEnum.BLACK));
            a2.SetSlot(true, 6, new Card(7, SuitEnum.GREEN));

            a2.SetSlot(false, 0, new Card(8, SuitEnum.RED));
            a2.SetSlot(false, 0, new Card(9, SuitEnum.RED));

            a2.SetSlot(false, 1, new Card(8, SuitEnum.GREEN));
            a2.SetSlot(false, 1, new Card(9, SuitEnum.GREEN));

            a2.SetSlot(false, 2, new Card(8, SuitEnum.BLACK));
            a2.SetSlot(false, 2, new Card(9, SuitEnum.BLACK)); 
            #endregion
        }

        public static PlayingField A1 { get => new PlayingField(a1); }
        public static PlayingField A2 { get => new PlayingField(a2); }
    }
}
