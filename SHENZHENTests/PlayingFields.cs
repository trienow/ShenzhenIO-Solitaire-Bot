using SHENZENSolitaire;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHENZHENTests
{
    public static class PlayingFields
    {
        private static readonly PlayingField a1 = new PlayingField();

        static PlayingFields()
        {
            int col = 0;
            a1.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(6, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(9, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(5, SuitEnum.RED));
            col++;

            a1.SetSlot(false, col, new Card(4, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(5, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(7, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(2, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(4, SuitEnum.BLACK));
            col++;

            a1.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(8, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(0, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(9, SuitEnum.GREEN));
            col++;

            a1.SetSlot(false, col, new Card(3, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(3, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(1, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(0, SuitEnum.ROSE));
            a1.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
            col++;

            a1.SetSlot(false, col, new Card(1, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(4, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(2, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(6, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(5, SuitEnum.BLACK));
            col++;

            a1.SetSlot(false, col, new Card(0, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(7, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(9, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(1, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(8, SuitEnum.RED));
            col++;

            a1.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(7, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(6, SuitEnum.BLACK));
            a1.SetSlot(false, col, new Card(8, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
            col++;

            a1.SetSlot(false, col, new Card(2, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(3, SuitEnum.GREEN));
            a1.SetSlot(false, col, new Card(0, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(0, SuitEnum.RED));
            a1.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
        }

        public static PlayingField A1 { get => new PlayingField(a1); }
    }
}
