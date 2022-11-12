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
            RealEstate realEstate1 = new RealEstate("Real1", 100, 60,  new List<int> {10,20,40, 80, 200, 400, 800}, 50, "Red");
            RealEstate realEstate2 = new RealEstate("Real2", 100, 60, new List<int> {10,20,40, 80, 200, 400, 800}, 50, "Red");
            RealEstate realEstate3 = new RealEstate("Real3", 100, 60, new List<int> {10,20,40, 80, 200, 400, 800}, 50, "Red");

            List<Property> colorGroup = new List<Property> {realEstate1, realEstate2, realEstate3};

            /// set group
            realEstate1.SetGroup(colorGroup);
            realEstate2.SetGroup(colorGroup);
            realEstate3.SetGroup(colorGroup);

            Assert.AreEqual(realEstate1.CurrentRent, 10);
            Assert.AreEqual(realEstate1.IsHouseBuildable, false);
            Assert.AreEqual(realEstate1.IsTradable, false);

            /// sell realEstate
            realEstate1.SetOnwerPlayerNumber(1);
            Assert.AreEqual(realEstate1.OwnerPlayerNumber, 1);
            Assert.ThrowsException<Exception>(() => realEstate1.BuildHouse());
            Assert.AreEqual(realEstate1.IsTradable, true);

            /// make is mortgaged
            realEstate1.SetIsMortgaged(true);
            Assert.AreEqual(realEstate1.IsMortgaged, true);
            Assert.AreEqual(realEstate1.CurrentRent, 0);
            Assert.AreEqual(realEstate1.IsTradable, false);

            /// recoverty from mortgaged
            realEstate1.SetIsMortgaged(false);
            Assert.AreEqual(realEstate1.IsMortgaged, false);
            Assert.AreEqual(realEstate1.CurrentRent, 10);
            Assert.AreEqual(realEstate1.IsTradable, true);
 
            /// make monopoly
            realEstate2.SetOnwerPlayerNumber(1);
            realEstate3.SetOnwerPlayerNumber(1);
            Assert.AreEqual(realEstate1.CurrentRent, 20);
            Assert.AreEqual(realEstate1.IsHouseBuildable, true);

            /// build a house
            realEstate1.BuildHouse();
            Assert.AreEqual(realEstate1.CurrentRent, 40);
            Assert.AreEqual(realEstate1.HouseCount, 1);
            
            foreach (var realEstate in colorGroup)
            {
                Assert.AreEqual(realEstate.IsTradable, false);
            }

            Assert.ThrowsException<Exception>(() => realEstate1.BuildHouse());
            Assert.AreEqual(realEstate1.IsHouseDistructable, true);
            Assert.AreEqual(realEstate2.IsHouseDistructable, false);
            Assert.ThrowsException<Exception>(() => realEstate1.SetIsMortgaged(true));

            /// make buidable again
            realEstate2.BuildHouse();
            realEstate3.BuildHouse();
            Assert.AreEqual(realEstate1.IsHouseBuildable, true);

            /// build a house
            realEstate1.BuildHouse();
            Assert.AreEqual(realEstate1.CurrentRent, 80);

            /// check buildable, distructable again
            Assert.AreEqual(realEstate1.IsHouseBuildable, false);
            Assert.AreEqual(realEstate1.IsHouseDistructable, true);

            /// make numHouse 5
            realEstate2.BuildHouse();
            realEstate3.BuildHouse();
            for (int i = 0; i < 3; i++)
            {
                realEstate1.BuildHouse();
                realEstate2.BuildHouse();
                realEstate3.BuildHouse();
                Assert.AreEqual(realEstate1.CurrentRent, realEstate1.Rents[i+4]);
            }

            /// building one more throws exception
            Assert.ThrowsException<Exception>( () => realEstate1.BuildHouse());

            /// distruct a house
            realEstate1.DistructHouse();
            Assert.AreEqual(realEstate1.HouseCount, 4);
            Assert.ThrowsException<Exception>( () => realEstate1.DistructHouse());
        }
    }
}