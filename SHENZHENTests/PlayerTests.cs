using Microsoft.VisualStudio.TestTools.UnitTesting;
using SHENZENSolitaire;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHENZHENTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void FindAllPossibleTurns()
        {
            Player p = new Player(PlayingFields.A1);
            List<Turn> turns = p.FindAllPossibleTurns();
        }
    }
}
