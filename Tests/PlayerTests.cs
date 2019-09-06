using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SHENZENSolitaire.Actor;

namespace Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void FindAllPossibleTurns()
        {
            Player.FindTurns(PlayingFields.A1).Count.Should().BePositive();
        }

        /// <summary>
        /// Ez
        /// </summary>
        [TestMethod]
        public void FindSolutionA2()
        {
            Player p = new Player(PlayingFields.A2);
            p.FindSolution().PathLength.Should().BeGreaterThan(5);
        }

        /// <summary>
        /// Ez
        /// </summary>
        [TestMethod]
        public void FindSolutionA3()
        {
            Player p = new Player(PlayingFields.A3);

            GameState solution = p.FindSolution();
            solution.PathLength.Should().BePositive();
        }

        /// <summary>
        /// With a dragon-merge
        /// </summary>
        [TestMethod]
        public void FindSolutionA4()
        {
            Player p = new Player(PlayingFields.A3);

            GameState solution = p.FindSolution();
            solution.PathLength.Should().BePositive();
        }
    }
}
