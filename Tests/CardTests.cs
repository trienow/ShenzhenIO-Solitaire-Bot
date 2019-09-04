﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHENZENSolitaire;

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
    }
}