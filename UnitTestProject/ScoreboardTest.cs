using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Reflection;

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
                new Interaction(new Player("Quentin"), new Player("Clément"), Positions.GetPositionByString("Arme")),
                new Interaction(new Player("Clément"), new Player("Quentin"), Positions.GetPositionByString("Épaule")),
                new Interaction(new Player("Clément"), new Player("Quentin"), Positions.GetPositionByString("Dos")),
                new Interaction(new Player("Victor"), new Player("Quentin"), Positions.GetPositionByString("Arme")),
                new Interaction(new Player("Quentin"), new Player("Victor"), Positions.GetPositionByString("Arme"))
            };

            scoreboards.GameInteractions.AddRange(interactions);
        }

        [TestMethod]
        public void TestFindPlayerByName()
        {
            Player player = scoreboards.FindPlayerByName("Quentin");

            Assert.AreEqual(this.scoreboards.GamePlayers[0], player);
        }

        [TestMethod]
        public void TestFindPlayerByWrongName()
        {
            try
            {
                Player player = scoreboards.FindPlayerByName("Romain");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Le joueur \"Romain\" n'existe pas", e.Message);
            }
        }

        private int InvokeShootAtPlayerAtPosition(Player shooter, Player target, Position position)
        {
            var privateMethodToTest = typeof(Scoreboards).GetMethod("ShootAtPlayerAtPosition",
                        BindingFlags.NonPublic | BindingFlags.Instance);

            return (int)privateMethodToTest.Invoke(scoreboards, new object[] { shooter, target, position });
        }

        [TestMethod]
        public void TestShootAtPlayerAtPosition()
        {
            Player quentin = this.scoreboards.GamePlayers[0];
            Player clement = this.scoreboards.GamePlayers[1];
            Position dos = Positions.GetPositionByString("Dos");

            int nbTimesQuentin = this.InvokeShootAtPlayerAtPosition(quentin, clement, dos);

            Assert.AreEqual(2, nbTimesQuentin);
        }

        private int[] InvokeShootAtPlayer(Player shooter, Player target)
        {
            var privateMethodToTest = typeof(Scoreboards).GetMethod("ShootAtPlayer",
                        BindingFlags.NonPublic | BindingFlags.Instance);

            return (int[])privateMethodToTest.Invoke(scoreboards, new object[] { shooter, target });
        }

        [TestMethod]
        public void TestShootAtPlayer()
        {
            Player quentin = this.scoreboards.GamePlayers[0];
            Player clement = this.scoreboards.GamePlayers[1];
            Position dos = Positions.GetPositionByString("Dos");

            int[] expected = new int[] { 0, 1, 0, 2 };
            int[] nbTimesPerPosition = this.InvokeShootAtPlayer(quentin, clement);

            CollectionAssert.AreEqual(expected, nbTimesPerPosition);
        }

        private List<Player> InvokeGetOtherPlayers(Player player)
        {
            var privateMethodToTest = typeof(Scoreboards).GetMethod("GetOtherPlayers",
                        BindingFlags.NonPublic | BindingFlags.Instance);

            return (List<Player>)privateMethodToTest.Invoke(scoreboards, new object[] { player });
        }

        [TestMethod]
        public void TestGetTargetPossibilities()
        {
            Player quentin = this.scoreboards.GamePlayers[0];
            List<Player> expected = new List<Player>
            {
                this.scoreboards.GamePlayers[1],
                this.scoreboards.GamePlayers[2]
            };

            List<Player> potentialTargets = this.InvokeGetOtherPlayers(quentin);

            CollectionAssert.AreEqual(expected, potentialTargets);
        }

        private List<int[]> InvokeShootAtOrByPlayers(Player player, bool playerIsShooterInsteadOfTarget)
        {
            var privateMethodToTest = typeof(Scoreboards).GetMethod("ShootAtOrByPlayers",
                        BindingFlags.NonPublic | BindingFlags.Instance);

            return (List<int[]>)privateMethodToTest.Invoke(scoreboards, new object[] { player, playerIsShooterInsteadOfTarget});
        }

        [TestMethod]
        public void TestShootAtPlayers()
        {
            Player quentin = this.scoreboards.GamePlayers[0];
            List<int[]> expected = new List<int[]> { new int[] { 0, 1, 0, 2 }, new int[] { 0, 1, 0, 0 } };

            List<int[]> nbTimesPerPlayerAndPerPositions = this.InvokeShootAtOrByPlayers(quentin, true);

            for (int i = 0; i < nbTimesPerPlayerAndPerPositions.Count; i++)
                CollectionAssert.AreEqual(expected[i], nbTimesPerPlayerAndPerPositions[i]);
        }

        [TestMethod]
        public void TestShootByPlayers()
        {
            Player quentin = this.scoreboards.GamePlayers[0];
            List<int[]> expected = new List<int[]> { new int[] { 1, 0, 0, 1 }, new int[] { 0, 1, 0, 0 } };

            List<int[]> nbTimesPerPlayerAndPerPositions = this.InvokeShootAtOrByPlayers(quentin, false);

            for (int i = 0; i < nbTimesPerPlayerAndPerPositions.Count; i++)
                CollectionAssert.AreEqual(expected[i], nbTimesPerPlayerAndPerPositions[i]);
        }

        private int InvokeGetScoreOfAllPositions(int[] nbTimesPerPosition, bool playerIsShooterInsteadOfTarget)
        {
            var privateMethodToTest = typeof(Scoreboards).GetMethod("GetScoreOfAllPositions",
                        BindingFlags.NonPublic | BindingFlags.Instance);

            return (int)privateMethodToTest.Invoke(scoreboards, new object[] { nbTimesPerPosition, playerIsShooterInsteadOfTarget });
        }

        [TestMethod]
        public void TestCalculBonusScore()
        {
            Player quentin = this.scoreboards.GamePlayers[0];
            Player clement = this.scoreboards.GamePlayers[1];

            int[] nbTimesPerPosition = this.InvokeShootAtPlayer(quentin, clement);
            int quentinBonus = this.InvokeGetScoreOfAllPositions(nbTimesPerPosition, true);

            Assert.AreEqual(36, quentinBonus);
        }

        [TestMethod]
        public void TestCalculMalusScore()
        {
            Player quentin = this.scoreboards.GamePlayers[0];
            Player clement = this.scoreboards.GamePlayers[1];

            int[] nbTimesPerPosition = this.InvokeShootAtPlayer(quentin, clement);
            int clementMalus = this.InvokeGetScoreOfAllPositions(nbTimesPerPosition, false);

            Assert.AreEqual(-12, clementMalus);
        }
    }
}
