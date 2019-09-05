using SHENZENSolitaire.Game;

namespace SHENZENSolitaire.Extractor
{
    class ExtractedCard : Card
    {
        public int Error { get; set; }
        public ExtractedCard(Card card, int error) : base(card.Value, card.Suit) => Error = error;
    }
}
