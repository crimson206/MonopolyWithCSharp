using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TradeHandlerTests
    {

        public List<IProperty> CreateNotOwnedProperties()
        {
            List<IProperty> properties = new List<IProperty>();
            
            for (int i = 0; i < 4; i++)
            {
                string name = string.Format("Red{0}", i);
                properties.Add(new RealEstate(name, 100, 50, new List<int> {10, 20, 40, 60}, 50, "Red"));
            }

            for (int i = 0; i < 3; i++)
            {
                string name = string.Format("RailRoad{0}", i);
                properties.Add(new RailRoad(name, 100, new List<int> {10, 20, 40}, 50));
            }

            for (int i = 0; i < 2; i++)
            {
                string name = string.Format("Utility{0}", i);
                properties.Add(new Utility(name, 100, new List<int> {4, 10}, 50));
            }

            return properties;
        }

        public List<IProperty> CreatePropertiesPartialyOwnedByPlayer1()
        {
            List<IProperty> properties =  this.CreateNotOwnedProperties();
            for (int i = 0; i < 3; i++)
            {
                properties[i].SetOwnerPlayerNumber(1);
            }
            return properties;
        }

        public List<IProperty> CreatePropertiesOwnedByPlayer0_1_2_3()
        {
            List<IProperty> properties =  this.CreateNotOwnedProperties();

            int rotatedPlayerNumber = 0;
            foreach (var item in properties)
            {
                item.SetOwnerPlayerNumber(rotatedPlayerNumber);
                rotatedPlayerNumber = (rotatedPlayerNumber + 1) % 4;
            }
            return properties;
        }

        public TradeHandler CreateTradeHandlerWithTradeConditions()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {1, 2, 3, 0};
            List<IProperty> properties = this.CreatePropertiesOwnedByPlayer0_1_2_3();
            List<int> expectedTradeOwners = new List<int> {1, 2, 3, 0};
            tradeHandler.SetTrade(participantNumbers, properties);
            tradeHandler.SetTradeTarget(2);
            int indexOfIPropertyToGet = 0;;
            int indexOfIPropertyToGive = 0;
            int additionalMoney = 100;
            tradeHandler.SetPropertyTradeOwnerIsWillingToGive(indexOfIPropertyToGive);
            tradeHandler.SetPropertyTradeOwnerWantsFromTarget(indexOfIPropertyToGet);
            tradeHandler.SetAdditionalMoneyTradeOwnerIsWillingToAdd(additionalMoney);
            tradeHandler.SetIsTradeAgreed(true);

            return tradeHandler;
        }

        [TestMethod]
        public void SetTrade_Where_Only_Player1_Owns_Some_Properties_And_Player2_3_0_Can_Trade()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {1, 2, 3, 0};
            List<IProperty> properties = this.CreatePropertiesPartialyOwnedByPlayer1();
            List<int> expectedSelectableTradeTargetCount = new List<int> {0, 1, 1, 1};

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    tradeHandler.SetTrade(participantNumbers, properties);
                }
                else
                {
                    tradeHandler.ChangeTradeOwner();
                }

                Assert.AreEqual(tradeHandler.SelectableTargetNumbers.Count(), expectedSelectableTradeTargetCount[i]);
            }
        }
        [TestMethod]
        public void Get_SelectableTargetNumbers_And_The_Only_Target_Is_Player1_Who_Has_Tradable_Properties()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {0, 1, 2, 3};
            List<IProperty> properties = this.CreatePropertiesPartialyOwnedByPlayer1();
            tradeHandler.SetTrade(participantNumbers, properties);

            int expectedSelectableTargetNumber = 1;
            Assert.AreEqual(tradeHandler.SelectableTargetNumbers[0], expectedSelectableTargetNumber);
            Assert.AreEqual(tradeHandler.SelectableTargetNumbers.Count(), 1);
        }
        [TestMethod]
        public void SetTradeTarget_And_Get_TradablePropertiesOfTradeTarget()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {0, 1, 2, 3};
            List<IProperty> properties = this.CreateNotOwnedProperties();
            for (int i = 0; i < 3; i++)
            {
                properties[i].SetOwnerPlayerNumber(1);
            }
            tradeHandler.SetTrade(participantNumbers, properties);

            int indexOfTradeTarget = 0;

            tradeHandler.SetTradeTarget(indexOfTradeTarget);

            Assert.AreEqual(tradeHandler.TradablePropertiesOfTradeTarget.Count(), 3);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(tradeHandler.TradablePropertiesOfTradeTarget[i].OwnerPlayerNumber, 1);
            }
        }
        [TestMethod]
        public void SetTradeTarget_With_Evenly_OwnedProperties_And_Check_If_All_Players_Can_Trade()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {1, 2, 3, 0};
            List<IProperty> properties = this.CreatePropertiesOwnedByPlayer0_1_2_3();
            List<int> expectedTradeOwners = new List<int> {1, 2, 3, 0};

            tradeHandler.SetTrade(participantNumbers, properties);

            Assert.AreEqual(tradeHandler.CurrentTradeOwner, expectedTradeOwners[0]);
            for (int i = 0; i < 3; i++)
            {
                tradeHandler.ChangeTradeOwner();
                Assert.AreEqual(tradeHandler.CurrentTradeOwner, expectedTradeOwners[i+1]);
            }
        }
        [TestMethod]
        public void SetTrade_And_ChangeTradeOwner_MoerTimes_Numver_Of_Tradable_Participants_And_Throw_Exception()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {1, 2, 3, 0};
            List<IProperty> properties = this.CreatePropertiesOwnedByPlayer0_1_2_3();
            List<int> expectedTradeOwners = new List<int> {1, 2, 3, 0};

            tradeHandler.SetTrade(participantNumbers, properties);
            for (int i = 0; i < 3; i++)
            {
                tradeHandler.ChangeTradeOwner();
            }

            Assert.ThrowsException<Exception>(() => tradeHandler.ChangeTradeOwner());
        }
        [TestMethod]
        public void SeggestTradeConditions()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {1, 2, 3, 0};
            List<IProperty> properties = this.CreatePropertiesOwnedByPlayer0_1_2_3();
            List<int> expectedTradeOwners = new List<int> {1, 2, 3, 0};
            tradeHandler.SetTrade(participantNumbers, properties);
            tradeHandler.SetTradeTarget(2);
            int indexOfIPropertyToGet = 0;;
            int indexOfIPropertyToGive = 0;
            int additionalMoney = 100;
            tradeHandler.SetPropertyTradeOwnerIsWillingToGive(indexOfIPropertyToGive);
            tradeHandler.SetPropertyTradeOwnerWantsFromTarget(indexOfIPropertyToGet);
            tradeHandler.SetAdditionalMoneyTradeOwnerIsWillingToAdd(additionalMoney);


            Assert.AreEqual(tradeHandler.PropertyTradeOwnerToGet, tradeHandler.TradablePropertiesOfTradeTarget[indexOfIPropertyToGet]);
            Assert.AreEqual(tradeHandler.PropertyTradeOwnerToGive, tradeHandler.TradablePropertiesOfTradeOwner[indexOfIPropertyToGive]);
            Assert.AreEqual(tradeHandler.MoneyOwnerWillingToAddOnTrade, additionalMoney);
        }
        [TestMethod]
        public void SetIsTradeAgreed_When_TradeCondition_Was_Set()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {1, 2, 3, 0};
            List<IProperty> properties = this.CreatePropertiesOwnedByPlayer0_1_2_3();
            List<int> expectedTradeOwners = new List<int> {1, 2, 3, 0};
            tradeHandler.SetTrade(participantNumbers, properties);
            tradeHandler.SetTradeTarget(2);
            int indexOfIPropertyToGet = 0;;
            int indexOfIPropertyToGive = 0;
            int additionalMoney = 100;
            tradeHandler.SetPropertyTradeOwnerIsWillingToGive(indexOfIPropertyToGive);
            tradeHandler.SetPropertyTradeOwnerWantsFromTarget(indexOfIPropertyToGet);
            tradeHandler.SetAdditionalMoneyTradeOwnerIsWillingToAdd(additionalMoney);
            tradeHandler.SetIsTradeAgreed(true);

            Assert.AreEqual(tradeHandler.IsTradeAgreed, true);
        }
        [TestMethod]
        public void SetIsTradeAgreed_Without_TradeCondition_And_Throw_Exception()
        {
            TradeHandler tradeHandler = new TradeHandler();
            List<int> participantNumbers = new List<int> {1, 2, 3, 0};
            List<IProperty> properties = this.CreatePropertiesOwnedByPlayer0_1_2_3();
            List<int> expectedTradeOwners = new List<int> {1, 2, 3, 0};
            tradeHandler.SetTrade(participantNumbers, properties);
            tradeHandler.SetTradeTarget(2);

            Assert.ThrowsException<Exception>(() => tradeHandler.SetIsTradeAgreed(true));
        }
        [TestMethod]
        public void Check_If_CreatedTradeHandlers_Conditions_Were_Set()
        {
            TradeHandler tradeHandler = this.CreateTradeHandlerWithTradeConditions();

            Assert.AreNotEqual(tradeHandler.PropertyTradeOwnerToGet, null);
            Assert.AreNotEqual(tradeHandler.PropertyTradeOwnerToGive, null);
            Assert.AreNotEqual(tradeHandler.IsTradeAgreed, null);
            Assert.AreNotEqual(tradeHandler.TradablePropertiesOfTradeTarget, null);
            Assert.AreNotEqual(tradeHandler.MoneyOwnerWillingToAddOnTrade, 0);
        }
        [TestMethod]
        public void ChangeTradeOwner_Reset_Conditions()
        {
            TradeHandler tradeHandler = this.CreateTradeHandlerWithTradeConditions();

            tradeHandler.ChangeTradeOwner();

            Assert.AreEqual(tradeHandler.PropertyTradeOwnerToGet, null);
            Assert.AreEqual(tradeHandler.PropertyTradeOwnerToGive, null);
            Assert.AreEqual(tradeHandler.IsTradeAgreed, null);
            Assert.AreEqual(tradeHandler.TradablePropertiesOfTradeTarget.Count(), 0);
            Assert.AreEqual(tradeHandler.MoneyOwnerWillingToAddOnTrade, 0);
        }

        [TestMethod]
        public void SetTrade_And_Reset_Conditions()
        {
            TradeHandler tradeHandler = this.CreateTradeHandlerWithTradeConditions();
            List<IProperty> properties = this.CreateNotOwnedProperties();
            List<int> participantNumbers = new List<int> {1, 2, 3, 0};

            tradeHandler.SetTrade(participantNumbers, properties);

            Assert.AreEqual(tradeHandler.PropertyTradeOwnerToGet, null);
            Assert.AreEqual(tradeHandler.PropertyTradeOwnerToGive, null);
            Assert.AreEqual(tradeHandler.IsTradeAgreed, null);
            Assert.AreEqual(tradeHandler.TradablePropertiesOfTradeTarget.Count(), 0);
            Assert.AreEqual(tradeHandler.MoneyOwnerWillingToAddOnTrade, 0);
        }
    }
}
