namespace SHENZENSolitaire
{
    public class Card
    {
        /// <summary>
        /// 0 - Dragon
        /// n - Playing Card
        /// </summary>
        public byte Value { get; private set; }

        public SuitEnum Suit { get; private set; }

        public Card(byte value = 0, SuitEnum suit = SuitEnum.EMPTY)
        {
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"Card: {Suit} {Value}";
        }
    }
}
