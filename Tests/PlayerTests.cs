using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SHENZENSolitaire;

namespace Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void FindAllPossibleTurns()
        {
            Player.FindAllPossibleTurns(PlayingFields.A1).Count.Should().BePositive();
        }

        [TestMethod]
        public void FindASolution()
        {
            Player p = new Player(PlayingFields.A2);
            p.FindSolution().Count.Should().BeGreaterThan(5);
        }
    }
}
