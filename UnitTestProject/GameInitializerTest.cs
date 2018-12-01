using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Reflection;

namespace UnitTestProject
{
    [TestClass]
    public class GameInitializerTest
    {
        private void InvokeArgumentsValidation(string[] args)
        {
            var privateAndStaticMethodToTest = typeof(GameInitializer).GetMethod("ArgumentsValidation",
                        BindingFlags.NonPublic | BindingFlags.Static);

            privateAndStaticMethodToTest.Invoke(null, new object[] { args });
        }

        [TestMethod]
        public void TestArgumentsValidationWithCorrectArgsLength()
        {
            string[] args = new string[] { "Arg1", "Arg2", "Arg3" };

            try
            {
                this.InvokeArgumentsValidation(args);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestArgumentsValidationWithIncorrectArgsLength()
        {
            string[] args = new string[] { "Arg1", "Arg2", "Arg3", "Arg4" };

            try
            {
                this.InvokeArgumentsValidation(args);
            }
            catch (Exception e)
            {
                //On fait appel à InnerException car la méthode Invoke récupère l'exception jetée et en lance une nouvelle
                //InnerException correspond à l'exception jetée par ValidationArguments
                Assert.AreEqual("Nombre d'arguments incorrect", e.InnerException.Message);
            }
        }

        private Player InvokeFindPlayerByName(Scoreboards scoreboards, string playerName)
        {
            var privateAndStaticMethodToTest = typeof(GameInitializer).GetMethod("FindPlayerByName",
                        BindingFlags.NonPublic | BindingFlags.Static);

            return (Player)privateAndStaticMethodToTest.Invoke(null, new object[] { scoreboards, playerName });
        }

        [TestMethod]
        public void TestFindPlayerByNameWithPlayerNameDefined()
        {
            Scoreboards scoreboards = new Scoreboards();

            Player newPlayer = new Player("Clément");
            scoreboards.GamePlayers.Add(newPlayer);

            Player playerReturned = this.InvokeFindPlayerByName(scoreboards, "Clément");

            Assert.AreEqual(newPlayer, playerReturned);
        }

        [TestMethod]
        public void TestFindPlayerByNameWithPlayerNameUndefined()
        {
            Scoreboards scoreboards = new Scoreboards();

            Player playerReturned = this.InvokeFindPlayerByName(scoreboards, "Clément");

            Assert.AreEqual(1, scoreboards.GamePlayers.Count);
            Assert.AreEqual(playerReturned, scoreboards.GamePlayers[0]);
        }
    }
}
