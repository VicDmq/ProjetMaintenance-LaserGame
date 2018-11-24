using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTestProject
{
    [TestClass]
    public class PositionsTest
    {
        private bool CheckAttributesValues(Position position, string expectedName, int expectedBonus, int expectedMalus)
        {
            return expectedName == position.Name
                && expectedBonus == position.Bonus
                && expectedMalus == position.Malus;
        }


        [TestMethod]
        public void TestGetPositionWithShoulderString()
        {
            Position position = Positions.GetPositionByString("Épaule");

            Assert.IsTrue(this.CheckAttributesValues(position, "Épaule", 15, 5));
        }

        [TestMethod]
        public void TestGetPositionWithWeaponString()
        {
            Position position = Positions.GetPositionByString("Arme");

            Assert.IsTrue(this.CheckAttributesValues(position, "Arme", 20, 8));
        }

        [TestMethod]
        public void TestGetPositionWithTorsoString()
        {
            Position position = Positions.GetPositionByString("Torse");

            Assert.IsTrue(this.CheckAttributesValues(position, "Torse", 10, 3));
        }

        [TestMethod]
        public void TestGetPositionWithBackString()
        {
            Position position = Positions.GetPositionByString("Dos");

            Assert.IsTrue(this.CheckAttributesValues(position, "Dos", 8, 2));
        }

        [TestMethod]
        public void TestGetPositionWithWrongString()
        {
            Position position = Positions.GetPositionByString("fzefezze");

            Assert.IsTrue(this.CheckAttributesValues(position, "", 0, 0));
        }
    }
}
