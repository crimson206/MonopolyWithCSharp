using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class HouseBuildHandlerTests
    {

        public List<RealEstate> CreateFreeRealEstates(int realEstateCount, string color)
        {
            List<RealEstate> realEstates  = new List<RealEstate>();

            for (int i = 0; i < realEstateCount; i++)
            {
                string name = color + i;
                realEstates.Add(new RealEstate("RealEstate", 100, 50 + i * 20, new List<int> {10, 20, 30, 40, 50, 60, 70}, 50, color));
            }

            foreach (var realEstate in realEstates)
            {
                realEstate.SetGroup(realEstates.Cast<Property>().ToList());
            }

            return realEstates;
        }

        public void SetOwnerNumbers(List<RealEstate> realEstates, int playerNumber)
        {
            foreach (var realEstate in realEstates)
            {
                realEstate.SetOnwerPlayerNumber(playerNumber);
            }
        }

        public List<RealEstate> CreateRealEstatesWithTwoOwners()
        {
            List<RealEstate> freeRealEstates = this.CreateFreeRealEstates(realEstateCount:3, "Red");
            List<RealEstate> freeRealEstates2= this.CreateFreeRealEstates(realEstateCount:3, "Blue");
            List<RealEstate> realEstatesSum = freeRealEstates.Concat(freeRealEstates2).ToList();
            this.SetOwnerNumbers(freeRealEstates, 1);
            this.SetOwnerNumbers(freeRealEstates2, 2);

            return realEstatesSum;
        }

        public List<IRealEstateData> GetBuildableRealEstatesOfCurrentHouseBuilder(HouseBuildHandler houseBuildHandler)
        {
            List<IRealEstateData> realEstateDatas = houseBuildHandler.HouseBuildableRealEstatesOfOwners
                                                                    [(int)houseBuildHandler.CurrentHouseBuilder!];
            return realEstateDatas;
        }


        [TestMethod]
        public void SetBuildHouseHandler_With_FreeRealEstates_And_AreAnyBuildable_Is_False()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{100, 200, 300, 400};
            List<RealEstate> freeRealEstates = this.CreateFreeRealEstates(3, "Red");
            List<IRealEstateData> realEstateDatas = freeRealEstates.Cast<IRealEstateData>().ToList();

            houseBuildHandler.SetHouseBuildHandler(balances, realEstateDatas);

            Assert.AreEqual(houseBuildHandler.AreAnyBuildable, false);
        }
        [TestMethod]
        public void SetBuildHouseHandler_With_Owner_With_Buildable_Houses_But_With_NotEnough_Money_And_AreAnyBuildable_Is_False()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{40, 200, 300, 400};
            List<RealEstate> freeRealEstates = this.CreateFreeRealEstates(3, "Red");
            List<IRealEstateData> realEstateDatas = freeRealEstates.Cast<IRealEstateData>().ToList();
            this.SetOwnerNumbers(freeRealEstates, 0);

            houseBuildHandler.SetHouseBuildHandler(balances, realEstateDatas);

            Assert.AreEqual(houseBuildHandler.AreAnyBuildable, false);
        }
        [TestMethod]
        public void SetBuildHouseHandler_With_Owner_With_Buildable_Houses_With_Enough_Money_And_AreAnyBuildable_Is_True()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{50, 200, 300, 400};
            List<RealEstate> freeRealEstates = this.CreateFreeRealEstates(3, "Red");
            List<IRealEstateData> realEstateDatas = freeRealEstates.Cast<IRealEstateData>().ToList();
            this.SetOwnerNumbers(freeRealEstates, 0);

            houseBuildHandler.SetHouseBuildHandler(balances, realEstateDatas);

            Assert.AreEqual(houseBuildHandler.AreAnyBuildable, true);
        }
        [TestMethod]
        public void SetBuildHouseHandler_With_Onw_Buildable_Owner_And_BuildableHousesOfOwnerCount_Is_1()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{50, 200, 300, 400};
            List<RealEstate> freeRealEstates = this.CreateFreeRealEstates(3, "Red");
            List<IRealEstateData> realEstateDatas = freeRealEstates.Cast<IRealEstateData>().ToList();
            this.SetOwnerNumbers(freeRealEstates, 0);
            houseBuildHandler.SetHouseBuildHandler(balances, realEstateDatas);

            Assert.AreEqual(houseBuildHandler.HouseBuildableRealEstatesOfOwners.Count(), 1);
        }
        [TestMethod]
        public void SetBuildHandler_With_RealEstates_With_Different_BuildingCosts_And_They_Are_Partically_Added_To_BuildableRealEstates()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{80, 200, 300, 400};
            List<RealEstate> freeRealEstates = this.CreateFreeRealEstates(realEstateCount:3, "Red");
            List<IRealEstateData> realEstateDatas_buildingCosts_50_70_90 = freeRealEstates.Cast<IRealEstateData>().ToList();
            this.SetOwnerNumbers(freeRealEstates, 0);
            houseBuildHandler.SetHouseBuildHandler(balances, realEstateDatas_buildingCosts_50_70_90);

            Assert.AreEqual(houseBuildHandler.HouseBuildableRealEstatesOfOwners[0].Count(), 2);
        }
        [TestMethod]
        public void ChangeHouseBuild_With_Two_Participants_And_Check_Current_HouseBuilder()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{80, 200, 300, 400};
            List<RealEstate> freeRealEstates = this.CreateFreeRealEstates(realEstateCount:3, "Red");
            List<RealEstate> freeRealEstates2= this.CreateFreeRealEstates(realEstateCount:3, "Blue");
            List<RealEstate> realEstatesSum = freeRealEstates.Concat(freeRealEstates2).ToList();
            this.SetOwnerNumbers(freeRealEstates, 1);
            this.SetOwnerNumbers(freeRealEstates2, 2);
            houseBuildHandler.SetHouseBuildHandler(balances, realEstatesSum.Cast<IRealEstateData>().ToList());

            Assert.AreEqual(houseBuildHandler.CurrentHouseBuilder, 1);
            houseBuildHandler.ChangeHouseBuilder();
            Assert.AreEqual(houseBuildHandler.CurrentHouseBuilder, 2);
        }
        [TestMethod]
        public void ChangeHouseBuilder_With_Two_Participants_And_Keep_Checking_IsLastBuilder()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{80, 200, 300, 400};
            List<RealEstate> realEstatesWithTwoOwners = this.CreateRealEstatesWithTwoOwners();
            houseBuildHandler.SetHouseBuildHandler(balances, realEstatesWithTwoOwners.Cast<IRealEstateData>().ToList());

            Assert.AreEqual(houseBuildHandler.IsLastBuilder, false);
            houseBuildHandler.ChangeHouseBuilder();
            Assert.AreEqual(houseBuildHandler.IsLastBuilder, true);
        }
        [TestMethod]
        public void SetRealEstateToBuildHouse()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{80, 200, 300, 400};
            List<RealEstate> realEstatesWithTwoOwners = this.CreateRealEstatesWithTwoOwners();
            houseBuildHandler.SetHouseBuildHandler(balances, realEstatesWithTwoOwners.Cast<IRealEstateData>().ToList());
            List<IRealEstateData> currentBuildableRealEstateDatas = this.GetBuildableRealEstatesOfCurrentHouseBuilder(houseBuildHandler);
            IRealEstateData realEstateToBuildHouse = currentBuildableRealEstateDatas[0];

            houseBuildHandler.SetRealEstateToBuildHouse(realEstateToBuildHouse);

            Assert.AreEqual(houseBuildHandler.RealEstateToBuildHouse, realEstateToBuildHouse);
        }
        [TestMethod]
        public void SetRealEstateToBuildHouse_With_NotBuildable_RealEstate()
        {
            HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
            List<int> balances = new List<int>{80, 80, 300, 400};
            List<RealEstate> realEstatesWithTwoOwners_BuildingCosts_50_70_90 = this.CreateRealEstatesWithTwoOwners();
            houseBuildHandler.SetHouseBuildHandler(balances, realEstatesWithTwoOwners_BuildingCosts_50_70_90.Cast<IRealEstateData>().ToList());
            List<IRealEstateData> currentBuildableRealEstateDatas = this.GetBuildableRealEstatesOfCurrentHouseBuilder(houseBuildHandler);
            IRealEstateData realEstateWithExpensiveHouse = realEstatesWithTwoOwners_BuildingCosts_50_70_90[2];
            
            Assert.AreEqual(realEstateWithExpensiveHouse.BuildingCost > 80, true);
            Assert.ThrowsException<Exception>(() => houseBuildHandler.SetRealEstateToBuildHouse(realEstateWithExpensiveHouse));
        }
    }
}