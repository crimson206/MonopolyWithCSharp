using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void UtilityTestsAll()
        {
            IUtility utility1 = new Utility("utility1", 100, new List<int> {20, 40}, 50);
            IUtility utility2 = new Utility("utility2", 100, new List<int> {20, 40}, 50);

            /// Make a group
            List<IProperty> utilities = new List<IProperty> {utility1, utility2};
            utility1.SetGroup(utilities);
            utility2.SetGroup(utilities);

            /// check current rent
            Assert.AreEqual(utility1.CurrentRent, 20);

            /// buy a railroad, rent is still the same
            utility1.SetOwnerPlayerNumber(1);
            Assert.AreEqual(utility1.CurrentRent, 20);
            Assert.AreEqual(utility2.CurrentRent, 20);

            /// same owner increases the rent
            utility2.SetOwnerPlayerNumber(1);
            Assert.AreEqual(utility1.CurrentRent, 40);
            Assert.AreEqual(utility2.CurrentRent, 40);
        }
    }
}