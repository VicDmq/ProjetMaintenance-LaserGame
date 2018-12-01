using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTestProject
{
    [TestClass]
    public class PositionsTest
    {
        [TestMethod]
        public void TestGetPositionWithShoulderString()
        {
            Position expectedPosition = new Position("Épaule", 15, 5);
            Position position = Positions.GetPositionByString("Épaule");

            Assert.AreEqual(expectedPosition, position);
        }

        [TestMethod]
        public void TestGetPositionWithWeaponString()
        {
            Position expectedPosition = new Position("Arme", 20, 8);
            Position position = Positions.GetPositionByString("Arme");

            Assert.AreEqual(expectedPosition, position);
        }

        [TestMethod]
        public void TestGetPositionWithTorsoString()
        {
            Position expectedPosition = new Position("Torse", 10, 3);
            Position position = Positions.GetPositionByString("Torse");

            Assert.AreEqual(expectedPosition, position);
        }

        [TestMethod]
        public void TestGetPositionWithBackString()
        {
            Position expectedPosition = new Position("Dos", 8, 2);
            Position position = Positions.GetPositionByString("Dos");

            Assert.AreEqual(expectedPosition, position);
        }

        [TestMethod]
        public void TestGetPositionWithWrongString()
        {
            try
            {
                Position position = Positions.GetPositionByString("fzefezze");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Cette position n'existe pas", e.Message);
            }
        }
    }
}
