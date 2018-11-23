using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Player player = new Player("Victor");
            player.incrementScore();
            Assert.AreEqual(5, player.getScore());
        }
    }
}
