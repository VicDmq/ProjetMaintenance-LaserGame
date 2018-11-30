using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using LaserGame;
using System.Reflection;

namespace UnitTestProject
{
    [TestClass]
    public class ConsoleControllerTest
    {
        ConsoleController consoleController;

        [TestInitialize]
        public void InitializeTests()
        {
            consoleController = new ConsoleController();
            consoleController.SetGameScoreboards(new ScoreboardsImplementationTest());
        }

        [TestMethod]
        public void TestParseCommandWithExitArg()
        {
            string result = consoleController.ExecuteCommand("exit");
            Assert.AreEqual("exit", result);
        }

        [TestMethod]
        public void TestParseCommandWithHelpArg()
        {
            string result = consoleController.ExecuteCommand("help");

            var privateHelpMethod = typeof(ConsoleController).GetMethod("Help",
                        BindingFlags.NonPublic | BindingFlags.Instance);

            string expectedValue = (string)privateHelpMethod.Invoke(consoleController, new object[] { });

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void TestParseCommandWithGlobalArg()
        {
            string result = consoleController.ExecuteCommand("global");

            Assert.AreEqual("Executing GetGlobalScore", result);
        }

        [TestMethod]
        public void TestParseCommandWithNewFileArgs()
        {
            string result = consoleController.ExecuteCommand("new Game1");

            Assert.AreEqual("Votre nouvelle partie a bien été lue", result);
        }

        [TestMethod]
        public void TestParseCommandWithScoreArgs()
        {
            string result = consoleController.ExecuteCommand("score Quentin");

            Assert.AreEqual("Executing GetPlayerScore with arg : Quentin", result);
        }

        [TestMethod]
        public void TestParseCommandWithShootByAnd2Args()
        {
            string result = consoleController.ExecuteCommand("shootBy Quentin");

            Assert.AreEqual("Executing GetShootByPlayer with arg : Quentin", result);
        }

        [TestMethod]
        public void TestParseCommandWithShootAtAnd2Args()
        {
            string result = consoleController.ExecuteCommand("shootAt Quentin");

            Assert.AreEqual("Executing GetShootAtPlayer with arg : Quentin", result);
        }

        [TestMethod]
        public void TestParseCommandWithAllStatArgs()
        {
            string result = consoleController.ExecuteCommand("allStat Quentin");

            Assert.AreEqual("Executing GetAllPlayerStatistics with arg : Quentin", result);
        }

        [TestMethod]
        public void TestParseCommandWithShootByAnd3Args()
        {
            string result = consoleController.ExecuteCommand("Quentin shootBy Clément");

            Assert.AreEqual("Executing GetShootByPlayer with args : Quentin and Clément", result);
        }

        [TestMethod]
        public void TestParseCommandWithShootAtAnd3Args()
        {
            string result = consoleController.ExecuteCommand("Quentin shootAt Clément");

            Assert.AreEqual("Executing GetShootAtPlayer with args : Quentin and Clément", result);
        }

        [TestMethod]
        public void TestParseCommandWithShootByAnd5Args()
        {
            string result = consoleController.ExecuteCommand("Quentin shootBy Clément At Dos");

            Assert.AreEqual("Executing GetShootByPlayerAtPosition with args : Quentin and Clément and Dos", result);
        }

        [TestMethod]
        public void TestParseCommandWithShootAtAnd5Args()
        {
            string result = consoleController.ExecuteCommand("Quentin shootAt Clément At Dos");

            Assert.AreEqual("Executing GetShootAtPlayerAtPosition with args : Quentin and Clément and Dos", result);
        }

    }

    public class ScoreboardsImplementationTest : IScoreboards
    {
        public string GetGlobalScore()
        {
            return "Executing GetGlobalScore";
        }

        public string GetScorePlayer(string playerName)
        {
            return "Executing GetPlayerScore with arg : " + playerName;
        }

        public string GetShootByPlayers(string playerName)
        {
            return "Executing GetShootByPlayer with arg : " + playerName;
        }

        public string GetShootAtPlayers(string playerName)
        {
            return "Executing GetShootAtPlayer with arg : " + playerName;
        }

        public string GetAllPlayerStatistics(string playerName)
        {
            return "Executing GetAllPlayerStatistics with arg : " + playerName;
        }

        public string GetShootByPlayer(string playerName, string shooterName)
        {
            return "Executing GetShootByPlayer with args : " + playerName + " and " + shooterName;
        }

        public string GetShootAtPlayer(string playerName, string targetName)
        {
            return "Executing GetShootAtPlayer with args : " + playerName + " and " + targetName;
        }

        public string GetShootByPlayerAtPosition(string playerName, string shooterName, string positionName)
        {
            return "Executing GetShootByPlayerAtPosition with args : " + playerName + " and " + shooterName + " and " + positionName;
        }

        public string GetShootAtPlayerAtPosition(string playerName, string targetName, string positionName)
        {
            return "Executing GetShootAtPlayerAtPosition with args : " + playerName + " and " + targetName + " and " + positionName;
        }
    }
}
