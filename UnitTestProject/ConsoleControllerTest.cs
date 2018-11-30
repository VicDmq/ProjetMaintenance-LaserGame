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

            var privateHelpMethod = typeof(ConsoleController).GetMethod("SetGameScoreboards",
            BindingFlags.NonPublic | BindingFlags.Instance);

            //Ici on invoque la méthode sur un objet ou l'interface a déjà été instancié
            privateHelpMethod.Invoke(consoleController, new object[] { new ScoreboardsImplementationTest()});
           // consoleController.SetGameScoreboards(new ScoreboardsImplementationTest());
        }

        [TestMethod]
        public void TestParseCommandWithExitArg()
        {
            string result = consoleController.ExecuteCommand("exit");
            Assert.AreEqual("exit", result);
        }

        [TestMethod]
        public void TestParseCommandWithHelpArgAndIScoreboardsIsInstanciated()
        {
            string result = consoleController.ExecuteCommand("help");

            var privateHelpMethod = typeof(ConsoleController).GetMethod("Help",
                        BindingFlags.NonPublic | BindingFlags.Instance);

            //Ici on invoque la méthode sur un objet ou l'interface a déjà été instancié
            string expectedValue = (string)privateHelpMethod.Invoke(consoleController, new object[] { });

            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void TestParseCommandWithHelpArgAndIScoreboardsIsNotInstanciated()
        {
            string result = new ConsoleController().ExecuteCommand("help");

            var privateHelpMethod = typeof(ConsoleController).GetMethod("Help",
                        BindingFlags.NonPublic | BindingFlags.Instance);

            //Ici on invoque la méthode sur un objet ou l'interface n'a pas été instancié
            string expectedValue = (string)privateHelpMethod.Invoke(new ConsoleController(), new object[] { });

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
            string result = consoleController.ExecuteCommand("new ../../LaserGame/Games/Game1");

            Assert.AreEqual("Votre nouvelle partie a bien été lue", result);
        }

        [TestMethod]
        public void TestParseCommandWithWrongNewFileArgs()
        {
            string result = consoleController.ExecuteCommand("new FileA");

            Assert.AreEqual("Le fichier \"FileA.txt\" n'a pas pu être trouvé", result);
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

        [TestMethod]
        public void Test4ArgumentsIsIncorrect()
        {
            string result = consoleController.ExecuteCommand("Arg1 Arg2 Arg3 Arg4");

            Assert.AreEqual("Le nombre d'arguments (4) n'est pas correct", result);
        }

        [TestMethod]
        public void TestMoreThan5ArgumentsIsIncorrect()
        {
            string result = consoleController.ExecuteCommand("Arg1 Arg2 Arg3 Arg4 Arg5 Arg6");

            Assert.AreEqual("Le nombre d'arguments (6) n'est pas correct", result);
        }

        [TestMethod]
        public void TestWith1ArgAndArgIsIncorrect()
        {
            string result = consoleController.ExecuteCommand("Arg1");

            Assert.AreEqual("Le terme \"Arg1\" n'est pas reconnu", result);
        }

        [TestMethod]
        public void TestWith2ArgsAndFirstArgIsIncorrect()
        {
            string result = consoleController.ExecuteCommand("Arg1 Arg2");

            Assert.AreEqual("Le terme \"Arg1\" n'est pas reconnu", result);
        }

        [TestMethod]
        public void TestWith3ArgsAndSecondArgIsIncorrect()
        {
            string result = consoleController.ExecuteCommand("Arg1 Arg2 Arg3");

            Assert.AreEqual("Le terme \"Arg2\" n'est pas reconnu", result);
        }

        [TestMethod]
        public void TestWith5ArgsAndSecondArgIsIncorrect()
        {
            string result = consoleController.ExecuteCommand("Arg1 Arg2 Arg3 At Arg5");

            Assert.AreEqual("Le terme \"Arg2\" n'est pas reconnu", result);
        }

        [TestMethod]
        public void TestWith5ArgsAndFourthArgIsIncorrect()
        {
            string result = consoleController.ExecuteCommand("Arg1 shootBy Arg3 Arg4 Arg5");

            Assert.AreEqual("Le terme \"Arg4\" n'est pas reconnu", result);
        }

        [TestMethod]
        public void TestWith5ArgsAndSecondArgAndFourthArgAreIncorrect()
        {
            string result = consoleController.ExecuteCommand("Arg1 Arg2 Arg3 Arg4 Arg5");

            Assert.AreEqual("Les termes \"Arg2\" et \"Arg4\" ne sont pas reconnus", result);
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
