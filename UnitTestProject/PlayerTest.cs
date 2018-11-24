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
            Position shoulder = Positions.GetPositionByString("Épaule");

            player.ShootAt(shoulder);

            Assert.AreEqual(15, player.Score);
        }

        [TestMethod]
        public void TestPlayerShootAtWeaponPosition()
        {
            Player player = new Player("RandomPlayer");
            Position weapon = Positions.GetPositionByString("Arme");

            player.ShootAt(weapon);

            Assert.AreEqual(20, player.Score);
        }

        [TestMethod]
        public void TestPlayerShootAtTorsoPosition()
        {
            Player player = new Player("RandomPlayer");
            Position torso = Positions.GetPositionByString("Torse");

            player.ShootAt(torso);

            Assert.AreEqual(10, player.Score);
        }

        [TestMethod]
        public void TestPlayerShootAtBackPosition()
        {
            Player player = new Player("RandomPlayer");
            Position back = Positions.GetPositionByString("Dos");

            player.ShootAt(back);

            Assert.AreEqual(8, player.Score);
        }

        [TestMethod]
        public void TestPlayerShootAtWrongPosition()
        {
            Player player = new Player("RandomPlayer");
            Position undefinedPosition = Positions.GetPositionByString("afezger");

            player.ShootAt(undefinedPosition);

            Assert.AreEqual(0, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtShoulderPosition()
        {
            Player player = new Player("RandomPlayer");
            Position shoulder = Positions.GetPositionByString("Épaule");

            player.IsShootedAt(shoulder);

            Assert.AreEqual(-5, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtWeaponPosition()
        {
            Player player = new Player("RandomPlayer");
            Position weapon = Positions.GetPositionByString("Arme");

            player.IsShootedAt(weapon);

            Assert.AreEqual(-8, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtTorsoPosition()
        {
            Player player = new Player("RandomPlayer");
            Position torso = Positions.GetPositionByString("Torse");

            player.IsShootedAt(torso);

            Assert.AreEqual(-3, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtBackPosition()
        {
            Player player = new Player("RandomPlayer");
            Position back = Positions.GetPositionByString("Dos");

            player.IsShootedAt(back);

            Assert.AreEqual(-2, player.Score);
        }

        [TestMethod]
        public void TestPlayerIsShootedAtWrongPosition()
        {
            Player player = new Player("RandomPlayer");
            Position undefinedPosition = Positions.GetPositionByString("afezger");

            player.IsShootedAt(undefinedPosition);

            Assert.AreEqual(0, player.Score);
        }
    }
}
