using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SHENZENSolitaire.Game;

namespace Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void HashCode()
        {
            Card c1 = new Card(9, SuitEnum.ROSE);
            (c1.GetHashCode() == c1.GetFingerprint()).Should().BeTrue();

            c1 = new Card(5, SuitEnum.BLACK);
            (c1.GetHashCode() == c1.GetFingerprint()).Should().BeTrue();
        }

        [TestMethod]
        public void ComparingEqual()
        {
            (Card.DRAGON_BLACK > new Card(suit: SuitEnum.BLACK)).Should().BeFalse();
            (Card.DRAGON_BLACK < new Card(suit: SuitEnum.BLACK)).Should().BeFalse();
            (Card.DRAGON_BLACK <= new Card(suit: SuitEnum.BLACK)).Should().BeTrue();
            (Card.DRAGON_BLACK == new Card(suit: SuitEnum.BLACK)).Should().BeTrue();
            (Card.DRAGON_BLACK != new Card(suit: SuitEnum.BLACK)).Should().BeFalse();
        }

        [TestMethod]
        public void ComparingValues()
        {
            Card a = new Card(1, SuitEnum.RED);
            Card b = new Card(2, SuitEnum.RED);

            (a > b).Should().BeFalse();
            (a < b).Should().BeTrue();
            (a <= b).Should().BeTrue();
            (a >= b).Should().BeFalse();
            (a == b).Should().BeFalse();
            (a != b).Should().BeTrue();
        }

        [TestMethod]
        public void ComparingSuits()
        {
            Card a = new Card(2, SuitEnum.GREEN);
            Card b = new Card(2, SuitEnum.RED);

            (a > b).Should().BeTrue();
            (a < b).Should().BeFalse();
            (a <= b).Should().BeFalse();
            (a >= b).Should().BeTrue();
            (a == b).Should().BeFalse();
            (a != b).Should().BeTrue();
        }
    }
}
