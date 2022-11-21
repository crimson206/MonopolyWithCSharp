using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    
    [TestClass]
    public class SellItemHandlerTests
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

        public List<RealEstate> CreateRealEstatesWithTwoOwners(int playerNumber1, int playerNumber2)
        {
            List<RealEstate> freeRealEstates = this.CreateFreeRealEstates(realEstateCount:3, "Red");
            List<RealEstate> freeRealEstates2= this.CreateFreeRealEstates(realEstateCount:3, "Blue");
            List<RealEstate> realEstatesSum = freeRealEstates.Concat(freeRealEstates2).ToList();
            this.SetOwnerNumbers(freeRealEstates, playerNumber1);
            this.SetOwnerNumbers(freeRealEstates2, playerNumber2);

            return realEstatesSum;
        }

        public List<IRealEstateData> GetBuildableRealEstatesOfCurrentHouseBuilder(HouseBuildHandler houseBuildHandler)
        {
            List<IRealEstateData> realEstateDatas = houseBuildHandler.HouseBuildableRealEstatesOfOwners
                                                                    [(int)houseBuildHandler.CurrentHouseBuilder!];
            return realEstateDatas;
        }
        [TestMethod]
        public void SetPlayerToSellItem_WhoHasNothingToSell_And_SoldableItems_Are_Empty()
        {
            SellItemHandler sellItemHandler = new SellItemHandler();
            List<IPropertyData> properties = this.CreateFreeRealEstates(realEstateCount:4, color:"Red").Cast<IPropertyData>().ToList();

            sellItemHandler.SetPlayerToSellItems(playerNumber:0, properties);

            Assert.AreEqual(sellItemHandler.MortgagibleItems.Count(), 0);
            Assert.AreEqual(sellItemHandler.RealEstatesWithDistructableHouse.Count(), 0);
            Assert.AreEqual(sellItemHandler.SoldableItemWithAuction.Count(), 0);
        }
    
        [TestMethod]
        public void SetPlayerToSellItem_WhoHasRealEstatesWithDistructableHouse()
        {
            SellItemHandler sellItemHandler = new SellItemHandler();
            List<RealEstate> realEstates = this.CreateRealEstatesWithTwoOwners(0, 1).ToList();
            foreach (var realEstate in realEstates)
            {
                realEstate.BuildHouse();
            }

            List<IPropertyData> propertyDatas = realEstates.Cast<IPropertyData>().ToList();

            List<IPropertyData> realEstatesWithDistructableHouseOwnedByPlayer0 =
                (from realEstate in propertyDatas.Cast<IRealEstateData>()
                where realEstate.IsHouseDistructable is true && realEstate.OwnerPlayerNumber == 0
                select realEstate).Cast<IPropertyData>().ToList();

            sellItemHandler.SetPlayerToSellItems(playerNumber:0, propertyDatas);

            foreach (var property in sellItemHandler.RealEstatesWithDistructableHouse)
            {
                Assert.AreEqual(sellItemHandler.RealEstatesWithDistructableHouse.Contains(property), true);
            }
            
        }
        [TestMethod]
        public void SetPlayerToSellItem_WhoHasMortgagibleProperties()
        {
            SellItemHandler sellItemHandler = new SellItemHandler();
            List<IPropertyData> properties = this.CreateRealEstatesWithTwoOwners(0, 1).Cast<IPropertyData>().ToList();
            List<IPropertyData> MortgagiblePropertiesOwnedByPlayer0 =
                (from realEstate in properties.Cast<IRealEstateData>()
                where realEstate.IsMortgagible is true && realEstate.OwnerPlayerNumber == 0
                select realEstate).Cast<IPropertyData>().ToList();

            sellItemHandler.SetPlayerToSellItems(playerNumber:0, properties);

            foreach (var property in sellItemHandler.MortgagibleItems)
            {
                Assert.AreEqual(MortgagiblePropertiesOwnedByPlayer0.Contains(property), true);
            }
        }
        [TestMethod]
        public void SetPlayerToSellItem_WhoHasAuctionableItems()
        {
            SellItemHandler sellItemHandler = new SellItemHandler();
            List<IPropertyData> properties = this.CreateRealEstatesWithTwoOwners(0, 1).Cast<IPropertyData>().ToList();
            List<IPropertyData> propertiesWhichAreSoldableWithAuction =
                (from property in properties
                where property.OwnerPlayerNumber == 0 && property.IsSoldableWithAuction
                select property).ToList();

            sellItemHandler.SetPlayerToSellItems(playerNumber:0, properties);

            foreach (var property in sellItemHandler.SoldableItemWithAuction)
            {
                Assert.AreEqual(propertiesWhichAreSoldableWithAuction.Contains(property), true);
            }
        }
        [TestMethod]
        public void UpdatePropertyListIfPropertyStatusIsChanged()
        {
            SellItemHandler sellItemHandler = new SellItemHandler();
            List<IPropertyData> properties = this.CreateRealEstatesWithTwoOwners(0, 1).Cast<IPropertyData>().ToList();
            List<IPropertyData> propertiesWhichAreSoldableWithAuction =
                (from property in properties
                where property.OwnerPlayerNumber == 0 && property.IsSoldableWithAuction
                select property).ToList();

            sellItemHandler.SetPlayerToSellItems(playerNumber:0, properties);

            foreach (var propertyData in propertiesWhichAreSoldableWithAuction)
            {
                Property property = (Property)propertyData;
                property.SetOnwerPlayerNumber(null);
            }
            
            Assert.AreEqual(sellItemHandler.SoldableItemWithAuction.Count(), 0);
        }
    }
}