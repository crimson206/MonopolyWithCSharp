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
            Utility utility1 = new Utility("utility1", 100, new List<int> {20, 40}, 50, 0);
            Utility utility2 = new Utility("utility2", 100, new List<int> {20, 40}, 50, 0);

            /// Make a group
            List<Property> utilities = new List<Property> {utility1, utility2};
            utility1.SetGroup(0, utilities);
            utility2.SetGroup(0, utilities);

            /// check current rent
            Assert.AreEqual(utility1.CurrentRent, 20);

            /// buy a railroad, rent is still the same
            utility1.SetOnwerPlayerNumber(0, 1);
            Assert.AreEqual(utility1.CurrentRent, 20);
            Assert.AreEqual(utility2.CurrentRent, 20);

            /// same owner increases the rent
            utility2.SetOnwerPlayerNumber(0, 1);
            Assert.AreEqual(utility1.CurrentRent, 40);
            Assert.AreEqual(utility2.CurrentRent, 40);
        }
    }
}