using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RealEstateTests
    {
        [TestMethod]
        public void Are_RealEstate_Groupable()
        {
            RealEstate realEstate1 = new RealEstate("Real1", 100, new List<int> {10,20,40, 80, 200, 400, 800}, 50, "Red", 0);
            RealEstate realEstate2 = new RealEstate("Real2", 100, new List<int> {10,20,40, 80, 200, 400, 800}, 50, "Red", 0);
            RealEstate realEstate3 = new RealEstate("Real3", 100, new List<int> {10,20,40, 80, 200, 400, 800}, 50, "Red", 0);

            List<RealEstate> colorGroup = new List<RealEstate> {realEstate1, realEstate2, realEstate3};

            /// set group
            realEstate1.SetGroup(0, colorGroup);
            realEstate2.SetGroup(0, colorGroup);
            realEstate3.SetGroup(0, colorGroup);

            Assert.AreEqual(realEstate1.CurrentRent, 10);
            Assert.AreEqual(realEstate1.Buildable, false);

            /// sell realEstate
            realEstate1.SetOnwerPlayerNumber(0, 1);
            Assert.AreEqual(realEstate1.OwnerPlayerNumber, 1);
            Assert.ThrowsException<Exception>(() => realEstate1.BuildHouse(0));
            Assert.ThrowsException<Exception>(() => realEstate1.SetOnwerPlayerNumber(1,1));

            /// make is mortgaged
            realEstate1.SetIsMortgaged(0, true);
            Assert.AreEqual(realEstate1.IsMortgaged, true);
            Assert.AreEqual(realEstate1.CurrentRent, 0);

            /// recoverty from mortgaged
            realEstate1.SetIsMortgaged(0, false);
            Assert.AreEqual(realEstate1.IsMortgaged, false);
            Assert.AreEqual(realEstate1.CurrentRent, 10);
 
            /// make monopoly
            realEstate2.SetOnwerPlayerNumber(0, 1);
            realEstate3.SetOnwerPlayerNumber(0, 1);
            Assert.AreEqual(realEstate1.CurrentRent, 20);
            Assert.AreEqual(realEstate1.Buildable, true);

            /// build a house
            realEstate1.BuildHouse(0);
            Assert.AreEqual(realEstate1.CurrentRent, 40);
            Assert.AreEqual(realEstate1.NumOfHouses, 1);
            Assert.ThrowsException<Exception>(() => realEstate1.BuildHouse(0));
            Assert.AreEqual(realEstate1.Distructable, true);
            Assert.AreEqual(realEstate2.Distructable, false);
            Assert.ThrowsException<Exception>(() => realEstate1.SetIsMortgaged(0, true));

            /// make buidable again
            realEstate2.BuildHouse(0);
            realEstate3.BuildHouse(0);
            Assert.AreEqual(realEstate1.Buildable, true);

            /// build a house
            realEstate1.BuildHouse(0);
            Assert.AreEqual(realEstate1.CurrentRent, 80);

            /// check buildable, distructable again
            Assert.AreEqual(realEstate1.Buildable, false);
            Assert.AreEqual(realEstate1.Distructable, true);

            /// make numHouse 5
            realEstate2.BuildHouse(0);
            realEstate3.BuildHouse(0);
            for (int i = 0; i < 3; i++)
            {
                realEstate1.BuildHouse(0);
                realEstate2.BuildHouse(0);
                realEstate3.BuildHouse(0);
                Assert.AreEqual(realEstate1.CurrentRent, realEstate1.Rents[i+4]);
            }

            /// building one more throws exception
            Assert.ThrowsException<Exception>( () => realEstate1.BuildHouse(0));
        }
    }
}