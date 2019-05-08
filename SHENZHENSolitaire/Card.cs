namespace SHENZENSolitaire
{
    public class Card
    {
        public byte Value { get; set; }
        public SuitEnum Suit { get; set; }

        public Card(byte value = 0, SuitEnum suit = SuitEnum.EMPTY)
        {
            Value = value;
            Suit = suit;
        }
    }
}
