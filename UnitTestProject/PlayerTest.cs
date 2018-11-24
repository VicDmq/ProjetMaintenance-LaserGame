using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTestProject
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestPlayerShootAtShoulderPosition()
        {
            Player player = new Player("RandomPlayer");
            player.ShootAt("Épaule");

            Assert.AreEqual(15, player.Score);
        }

        [TestMethod]
        public void TestPlayerShootAtWeaponPosition()
        {
            Player player = new Player("RandomPlayer");
            player.ShootAt("Arme");

            Assert.AreEqual(20, player.Score);
        }

        [TestMethod]
        public void TestPlayerShootAtTorsoPosition()
        {
            Player player = new Player("RandomPlayer");
            player.ShootAt("Torse");

            Assert.AreEqual(10, player.Score);
        }

        [TestMethod]
        public void TestPlayerShootAtBackPosition()
        {
            Player player = new Player("RandomPlayer");
            player.ShootAt("Dos");

            Assert.AreEqual(8, player.Score);
        }

        [TestMethod]
        public void TestPlayerShootAtWrongPosition()
        {
            Player player = new Player("RandomPlayer");
            player.ShootAt("afezger");

            Assert.AreEqual(0, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtShoulderPosition()
        {
            Player player = new Player("RandomPlayer");
            player.IsShootedAt("Épaule");

            Assert.AreEqual(-5, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtWeaponPosition()
        {
            Player player = new Player("RandomPlayer");
            player.IsShootedAt("Arme");

            Assert.AreEqual(-8, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtTorsoPosition()
        {
            Player player = new Player("RandomPlayer");
            player.IsShootedAt("Torse");

            Assert.AreEqual(-3, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtBackPosition()
        {
            Player player = new Player("RandomPlayer");
            player.IsShootedAt("Dos");

            Assert.AreEqual(-2, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtWrongPosition()
        {
            Player player = new Player("RandomPlayer");
            player.IsShootedAt("afezger");

            Assert.AreEqual(0, player.Score);
        }
    }
}
