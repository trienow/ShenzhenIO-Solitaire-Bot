using SHENZENSolitaire.Game;

namespace Tests
{
    public static class PlayingFields
    {
        public static PlayingField A1
        {
            get
            {
                int col = 0;
                PlayingField pf = new PlayingField();

                pf.SetSlot(false, col, new Card(8, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(3, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(8, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(3, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(7, SuitEnum.BLACK));
                col++;

                pf.SetSlot(false, col, new Card(1, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(2, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(5, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(9, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(5, SuitEnum.RED));
                col++;

                pf.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(6, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(4, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(7, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
                col++;

                pf.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(1, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(1, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(5, SuitEnum.BLACK));
                col++;

                pf.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(2, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(9, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(0, SuitEnum.GREEN));
                col++;

                pf.SetSlot(false, col, new Card(9, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(0, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(0, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(0, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(6, SuitEnum.RED));
                col++;

                pf.SetSlot(false, col, new Card(3, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(0, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(0, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(2, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(0, SuitEnum.ROSE));
                col++;

                pf.SetSlot(false, col, new Card(8, SuitEnum.GREEN));
                pf.SetSlot(false, col, new Card(4, SuitEnum.RED));
                pf.SetSlot(false, col, new Card(6, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(4, SuitEnum.BLACK));
                pf.SetSlot(false, col, new Card(7, SuitEnum.RED));

                return pf;
            }
        }

        public static PlayingField A2
        {
            get
            {
                PlayingField pf = new PlayingField();

                pf.SetSlot(true, 0, Card.BLOCKED);
                pf.SetSlot(true, 1, Card.BLOCKED);
                pf.SetSlot(true, 2, Card.BLOCKED);
                pf.SetSlot(true, 3, new Card(0, SuitEnum.ROSE));
                pf.SetSlot(true, 4, new Card(7, SuitEnum.RED));
                pf.SetSlot(true, 5, new Card(7, SuitEnum.BLACK));
                pf.SetSlot(true, 6, new Card(7, SuitEnum.GREEN));

                pf.SetSlot(false, 0, new Card(8, SuitEnum.RED));
                pf.SetSlot(false, 0, new Card(9, SuitEnum.RED));

                pf.SetSlot(false, 1, new Card(8, SuitEnum.GREEN));
                pf.SetSlot(false, 1, new Card(9, SuitEnum.GREEN));

                pf.SetSlot(false, 2, new Card(8, SuitEnum.BLACK));
                pf.SetSlot(false, 2, new Card(9, SuitEnum.BLACK));

                return pf;
            }
        }

        public static PlayingField A3
        {
            get
            {
                PlayingField pf = new PlayingField();

                pf.SetSlot(true, 0, Card.BLOCKED);
                pf.SetSlot(true, 1, Card.BLOCKED);
                pf.SetSlot(true, 2, Card.BLOCKED);
                pf.SetSlot(true, 3, new Card(0, SuitEnum.ROSE));
                pf.SetSlot(true, 4, new Card(9, SuitEnum.RED));
                pf.SetSlot(true, 5, new Card(7, SuitEnum.BLACK));
                pf.SetSlot(true, 6, new Card(6, SuitEnum.GREEN));

                pf.SetSlot(false, 0, new Card(9, SuitEnum.GREEN));
                pf.SetSlot(false, 0, new Card(8, SuitEnum.BLACK));

                pf.SetSlot(false, 3, new Card(7, SuitEnum.GREEN));
                pf.SetSlot(false, 3, new Card(9, SuitEnum.BLACK));
                pf.SetSlot(false, 3, new Card(8, SuitEnum.GREEN));

                return pf;
            }
        }

        public static PlayingField A4
        {
            get
            {
                PlayingField pf = new PlayingField();

                pf.SetSlot(true, 0, Card.BLOCKED);
                pf.SetSlot(true, 1, Card.BLOCKED);
                pf.SetSlot(true, 2, Card.DRAGON_BLACK);
                pf.SetSlot(true, 3, new Card(0, SuitEnum.ROSE));
                pf.SetSlot(true, 4, new Card(9, SuitEnum.RED));
                pf.SetSlot(true, 5, new Card(5, SuitEnum.BLACK));
                pf.SetSlot(true, 6, new Card(4, SuitEnum.GREEN));

                pf.SetSlot(false, 0, new Card(9, SuitEnum.GREEN));
                pf.SetSlot(false, 0, new Card(8, SuitEnum.BLACK));

                pf.SetSlot(false, 1, Card.DRAGON_BLACK);

                pf.SetSlot(false, 2, Card.DRAGON_BLACK);

                pf.SetSlot(false, 3, new Card(7, SuitEnum.GREEN));
                pf.SetSlot(false, 3, new Card(9, SuitEnum.BLACK));
                pf.SetSlot(false, 3, new Card(8, SuitEnum.GREEN));

                pf.SetSlot(false, 4, Card.DRAGON_BLACK);


                pf.SetSlot(false, 7, new Card(5, SuitEnum.GREEN));
                pf.SetSlot(false, 7, new Card(6, SuitEnum.BLACK));

                return pf;
            }
        }

        public static PlayingField A5
        {
            get
            {
                PlayingField pf = new PlayingField();

                pf.SetSlot(true, 0, Card.DRAGON_GREEN);
                pf.SetSlot(true, 1, Card.BLOCKED);
                pf.SetSlot(true, 2, new Card(5, SuitEnum.GREEN));
                pf.SetSlot(true, 3, new Card(0, SuitEnum.ROSE));
                pf.SetSlot(true, 4, new Card(5, SuitEnum.RED));
                pf.SetSlot(true, 5, new Card(2, SuitEnum.BLACK));

                pf.SetSlot(false, 0, new Card(2, SuitEnum.GREEN));
                pf.SetSlot(false, 0, new Card(6, SuitEnum.BLACK));
                pf.SetSlot(false, 0, new Card(9, SuitEnum.GREEN));

                pf.SetSlot(false, 1, Card.DRAGON_BLACK);
                pf.SetSlot(false, 1, Card.DRAGON_BLACK);
                pf.SetSlot(false, 1, new Card(4, SuitEnum.BLACK));
                pf.SetSlot(false, 1, Card.DRAGON_GREEN);

                pf.SetSlot(false, 2, new Card(8, SuitEnum.BLACK));
                pf.SetSlot(false, 2, Card.DRAGON_BLACK);

                pf.SetSlot(false, 3, Card.DRAGON_BLACK);
                pf.SetSlot(false, 3, new Card(7, SuitEnum.GREEN));

                pf.SetSlot(false, 4, new Card(1, SuitEnum.GREEN));
                pf.SetSlot(false, 4, new Card(9, SuitEnum.BLACK));
                pf.SetSlot(false, 4, new Card(8, SuitEnum.RED));
                pf.SetSlot(false, 4, new Card(7, SuitEnum.BLACK));
                pf.SetSlot(false, 4, new Card(6, SuitEnum.GREEN));
                pf.SetSlot(false, 4, new Card(5, SuitEnum.BLACK));
                pf.SetSlot(false, 4, new Card(4, SuitEnum.GREEN));
                pf.SetSlot(false, 4, new Card(3, SuitEnum.BLACK));

                pf.SetSlot(false, 5, Card.DRAGON_GREEN);
                pf.SetSlot(false, 5, new Card(6, SuitEnum.RED));
                pf.SetSlot(false, 5, new Card(3, SuitEnum.GREEN));
                pf.SetSlot(false, 5, new Card(9, SuitEnum.RED));
                pf.SetSlot(false, 5, Card.DRAGON_GREEN);

                pf.SetSlot(false, 6, new Card(8, SuitEnum.GREEN));

                pf.SetSlot(false, 7, new Card(7, SuitEnum.RED));

                return pf;
            }
        }
        
        public static PlayingField A6
        {
            get
            {
                PlayingField pf = new PlayingField();

                pf.SetSlot(true, 0, Card.BLOCKED);
                pf.SetSlot(true, 1, Card.DRAGON_GREEN);
                pf.SetSlot(true, 2, new Card(4, SuitEnum.BLACK));
                pf.SetSlot(true, 3, Card.ROSE);
                pf.SetSlot(true, 4, new Card(4, SuitEnum.RED));
                pf.SetSlot(true, 5, new Card(2, SuitEnum.BLACK));
                pf.SetSlot(true, 6, new Card(9, SuitEnum.GREEN));

                pf.SetSlot(false, 0, Card.DRAGON_GREEN);

                pf.SetSlot(false, 1, new Card(6, SuitEnum.RED));
                pf.SetSlot(false, 1, Card.DRAGON_BLACK);
                pf.SetSlot(false, 1, new Card(3, SuitEnum.BLACK));
                pf.SetSlot(false, 1, new Card(9, SuitEnum.BLACK));

                pf.SetSlot(false, 2, Card.DRAGON_BLACK);
                pf.SetSlot(false, 2, new Card(6, SuitEnum.BLACK));
                pf.SetSlot(false, 2, new Card(8, SuitEnum.RED));
                pf.SetSlot(false, 2, Card.DRAGON_GREEN);
                pf.SetSlot(false, 2, Card.DRAGON_BLACK);

                pf.SetSlot(false, 3, Card.DRAGON_GREEN);
                pf.SetSlot(false, 3, new Card(7, SuitEnum.BLACK));

                pf.SetSlot(false, 4, new Card(5, SuitEnum.RED));

                pf.SetSlot(false, 5, new Card(9, SuitEnum.RED));
                pf.SetSlot(false, 5, Card.DRAGON_BLACK);
                pf.SetSlot(false, 5, new Card(7, SuitEnum.RED));

                pf.SetSlot(false, 6, new Card(8, SuitEnum.BLACK));

                pf.SetSlot(false, 7, new Card(5, SuitEnum.BLACK));

                return pf;
            }
        }


    }
}
