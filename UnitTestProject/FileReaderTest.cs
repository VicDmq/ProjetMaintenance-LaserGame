using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileStream;
using System.Reflection;

namespace UnitTestProject
{
    [TestClass]
    public class FileReaderTest
    {
        private readonly string[] expectedArgs = new string[] { "Clément", "Victor", "Épaule" };

        private bool CheckIfArgsAreCorrect(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (expectedArgs[i] != args[i])
                    return false;
            }

            return true;
        }

        private string[] GetAndInvokeSplitLineIntoStringArgsMethods(string input)
        {
            // On récupère via reflection la méthode à invoker
            var privateAndStaticMethodToTest = typeof(FileReader).GetMethod("SplitLineIntoStringArgs",
                        BindingFlags.NonPublic | BindingFlags.Static);

            // Le premier paramètre est null car la classe contenant cette méthode est statique
            return (string[])privateAndStaticMethodToTest.Invoke(null, new object[] { input });
        }

        [TestMethod]
        public void TestSplitLineIntoStringArgsWithInputWithoutMystake()
        {
            string inputWithoutMystake = "Clément : Victor : Épaule";
            string[] args = this.GetAndInvokeSplitLineIntoStringArgsMethods(inputWithoutMystake);

            Assert.IsTrue(CheckIfArgsAreCorrect(args));
        }

        [TestMethod]
        public void TestSplitLineIntoStringArgsWithInputWithMystakes()
        {
            string inputWithMystakes = "Clément     ::Victor:  : Épaule";
            string[] args = this.GetAndInvokeSplitLineIntoStringArgsMethods(inputWithMystakes);

            Assert.IsTrue(CheckIfArgsAreCorrect(args));
        }
    }
}
