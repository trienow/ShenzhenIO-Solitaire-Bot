using SHENZENSolitaire.Game;

namespace SHENZENSolitaire.ScreenHandler
{
    /// <summary>
    /// A playing <see cref="Card"/> with a match-error.
    /// </summary>
    class ExtractedCard : Card
    {
        public int Error { get; set; }
        public ExtractedCard(Card card, int error) : base(card.Value, card.Suit) => Error = error;
    }
}
