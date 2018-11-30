using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTestProject
{
    [TestClass]
    public class ScoreboardTest
    {
        Scoreboards scoreboards = new Scoreboards();

        [TestInitialize]
        public void InitializeTests()
        {
            
            List<Player> players = new List<Player>{
                new Player("Quentin"),
                new Player("Clément"),
                new Player("Victor")
            };

            scoreboards.GamePlayers.AddRange(players);

            List<Interaction> interactions = new List<Interaction> {
                new Interaction(new Player("Quentin"), new Player("Clément"), Positions.GetPositionByString("Dos")),
                new Interaction(new Player("Quentin"), new Player("Clément"), Positions.GetPositionByString("Dos")),
                new Interaction(new Player("Quentin"), new Player("Clément"), Positions.GetPositionByString("Arme"))
            };

            scoreboards.GameInteractions.AddRange(interactions);
        }

        [TestMethod]
        public void TestPlayerShootedByAtPosition()
        {
            int timesShooted = scoreboards.PlayerShootedByAtPosition("Quentin", "Clément", "Dos");
            Assert.AreEqual(2, timesShooted);
        }


        [TestMethod]
        public void TestPlayerShootedBy()
        {
            int[] shotExpected = new int[] { 0, 1, 0, 2 };
            int[] shot = scoreboards.PlayerShootedBy("Quentin", "Clément");
            CollectionAssert.AreEqual(shotExpected, shot);
        }

        [TestMethod]
        public void TestGetTargetPossibilities()
        {
            string[] targetExpected = new string[] { "Clément", "Victor" };
            string[] target = scoreboards.GetTargetPossibilities("Quentin");
            CollectionAssert.AreEqual(targetExpected, target);
        }

        [TestMethod]
        public void TestActionPlayer()
        {
            string[] interactionShotListExpected = new string[] { "Clément,0,1,0,2,3", "Victor,0,0,0,0,0" };
            string[] interactionShotList = scoreboards.ActionPlayer("Quentin");
            CollectionAssert.AreEqual(interactionShotListExpected, interactionShotList);
        }

        [TestMethod]
        public void TestCalculScoreTarget()
        {
            int[] positions = scoreboards.PlayerShootedBy("Quentin", "Clément");
            int scoreTarget = scoreboards.CalculScoreTarget(positions);
            Assert.AreEqual(3, scoreTarget);
        }
    }
}
