using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringFormatter;

namespace UnitTestProject
{
    [TestClass]
    public class ScoreboardsFormatterTest
    {
        [TestMethod]
        public void TestFormatPositionsNamesToRawString()
        {
            string[] positionsNames = new string[] { "Épaule", "Arme", "Torse", "Dos" };

            string raw = ScoreboardsFormatter.FormatPositionsNamesToRawString(positionsNames);

            Assert.AreEqual("Épaule  Arme  Torse  Dos  ", raw);
        }

        [TestMethod]
        public void TestFormatIntArrayToRawString()
        {
            string[] positionsNames = new string[] { "Épaule", "Arme", "Torse", "Dos" };
            int[] array = new int[] { 11, 5, 6, 15 };

            string raw = ScoreboardsFormatter.FormatIntArrayToRawString(positionsNames, array);

            Assert.AreEqual("  11     5      6     15  ", raw);
        }

        private string InvokeGetCell(int cellLength, string cellContent)
        {
            var privateAndStaticMethodToTest = typeof(ScoreboardsFormatter).GetMethod("GetCell",
                        BindingFlags.NonPublic | BindingFlags.Static);

            return (string)privateAndStaticMethodToTest.Invoke(null, new object[] { cellLength, cellContent });
        }

        [TestMethod]
        public void TestDisplayCellWithImpairedSize()
        {
            string cell = this.InvokeGetCell(7, "*");

            Assert.AreEqual("   *   ", cell);
        }

        [TestMethod]
        public void TestDisplayCellWithImpairedSizeAndCellContentOfSizeEquals2()
        {
            string cell = this.InvokeGetCell(7, "**");

            Assert.AreEqual("   **  ", cell);
        }

        [TestMethod]
        public void TestDisplayCellWithPairedSize()
        {
            string cell = this.InvokeGetCell(6, "*");

            Assert.AreEqual("  *   ", cell);
        }

        [TestMethod]
        public void TestDisplayCellWithPairedSizeAndCellContentOfSizeEquals2()
        {
            string cell = this.InvokeGetCell(6, "**");

            Assert.AreEqual("  **  ", cell);
        }
    }
}
