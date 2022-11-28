using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AuctionHandlerTests
    {
        [TestMethod]
        public void Initialize_Basic_Constructor_An_Object_With_Null_Final_Price()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            
            int? expectedFinalPrice = null;
            Assert.AreEqual(auctionHandler.FinalPrice, expectedFinalPrice);
        }
        [TestMethod]
        public void Initialize_Basic_Constructor_An_Object_With_Null_Winner_Number()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            
            int? expectedFinalWinnerNumber = null;
            Assert.AreEqual(auctionHandler.FinalPrice, expectedFinalWinnerNumber);
        }

        [TestMethod]
        public void Set_AuctionCondition_By_Generating_SuggestedPrices_With_Target_Participants_Count()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            List<int> participantNumbers = new List<int> { 3, 0, 1 };
            RailRoad railRoad = new RailRoad("RailRoad", 100, new List<int> {10, 20, 30}, 50);
            auctionHandler.SetAuctionCondition(participantNumbers, initialPrice:100, railRoad);

            int expectedPositiveSuggestedPriceCount = 1;
            Assert.AreEqual(auctionHandler.SuggestedPrices.Values.Where(price => price > 0).Count(), expectedPositiveSuggestedPriceCount);
        }
        [TestMethod]
        public void Set_AuctionCondision_By_Generating_SuggestedPrice_With_Initial_Price_With_Initial_Participant()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            List<int> participantNumbers = new List<int> { 3, 0, 1 };
            RailRoad railRoad = new RailRoad("RailRoad", 100, new List<int> {10, 20, 30}, 50);
            auctionHandler.SetAuctionCondition(participantNumbers, initialPrice:100, railRoad);

            int expectedInitialPrice = 100;
            Assert.AreEqual(auctionHandler.SuggestedPrices[3], expectedInitialPrice); 
        }
        public AuctionHandler Create_AuctionHandler_Withs_Participants_3_0_1_And_InitialPrice_100()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            List<int> participantNumbers = new List<int> { 3, 0, 1 };
            RailRoad railRoad = new RailRoad("RailRoad", 100, new List<int> {10, 20, 30}, 50);
            auctionHandler.SetAuctionCondition(participantNumbers, initialPrice:100, railRoad);
            return auctionHandler;
        }
        [TestMethod]
        public void Is_SuggestedPrices_Protected_From_Change_Of_Its_Copy()
        {
            AuctionHandler auctionHandler = Create_AuctionHandler_Withs_Participants_3_0_1_And_InitialPrice_100();

            Dictionary<int,int> copy = auctionHandler.SuggestedPrices;
            copy[0] = 20;

            int expectedValueIfItWasNotProtected = 20;
            Assert.AreNotEqual(auctionHandler.SuggestedPrices[0], expectedValueIfItWasNotProtected);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_For_One_Round()
        {
            AuctionHandler auctionHandler = this.Create_AuctionHandler_Withs_Participants_3_0_1_And_InitialPrice_100();
            int[] suggestedPrices = new int[] { 200, 100, 240};
            int[] expectedPrices = new int[] { 200, 100, 240};

            for (int i = 0; i < 3; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(suggestedPrices[i]);
                Dictionary<int, int> copyOfSuggestedPrices = auctionHandler.SuggestedPrices;
                Assert.AreEqual(copyOfSuggestedPrices.Values.ToList()[(i+1)%3], expectedPrices[i]);
            }
        }
        [TestMethod]
        public void Do_Not_Set_Auction_Condition_And_IsAuctionOn_Is_False()
        {
            AuctionHandler auctionHandler = new AuctionHandler();

            bool expectedIsAuctionOn = false;
            Assert.AreEqual(auctionHandler.IsAuctionOn, expectedIsAuctionOn);
        }
        [TestMethod]
        public void Set_Auction_Condition_To_Make_IsAuctionOn_True()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            List<int> participantNumbers = new List<int> { 3, 0, 1 };
            RailRoad railRoad = new RailRoad("RailRoad", 100, new List<int> {10, 20, 30}, 50);
            auctionHandler.SetAuctionCondition(participantNumbers, initialPrice:100, railRoad);

            bool expectedIsAuctionOn = true;
            Assert.AreEqual(auctionHandler.IsAuctionOn, expectedIsAuctionOn);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_With_Smaller_Prices_Than_Max_Price_To_Close_Auction()
        {
            AuctionHandler auctionHandler = this.Create_AuctionHandler_Withs_Participants_3_0_1_And_InitialPrice_100();
            int[] suggestedPrices = new int[] { 200, 60, 100};

            for (int i = 0; i < 3; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(suggestedPrices[i]);
            }

            bool expectedIsAuctionOn = false;
            Assert.AreEqual(auctionHandler.IsAuctionOn, expectedIsAuctionOn);
            Assert.AreEqual(auctionHandler.WinnerNumber, 0);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_Always_With_Lower_Price_Than_Initial_Price_To_Force_First_Participant_To_Win()
        {
            AuctionHandler auctionHandler = this.Create_AuctionHandler_Withs_Participants_3_0_1_And_InitialPrice_100();
            int[] suggestedPrices = new int[] {50, 50};

            for (int i = 0; i < 2; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(suggestedPrices[i]);
            }

            int expectedWinnerNumber = 3;
            Assert.AreEqual(auctionHandler.WinnerNumber, expectedWinnerNumber);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_And_The_Last_Suggested_Price_Is_Updated_Even_If_The_Auction_Is_Closed()
        {
            AuctionHandler auctionHandler = this.Create_AuctionHandler_Withs_Participants_3_0_1_And_InitialPrice_100();
            int lastParticipantNum = 1;
            int lastSuggestedPrice = 200;

            for (int i = 0; i < 3; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(lastSuggestedPrice);
            }

            int expectedLastSuggestedPrice = 200;
            Assert.AreEqual(auctionHandler.SuggestedPrices[lastParticipantNum], expectedLastSuggestedPrice);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_With_Same_Prices_To_Close_Auction_And_Initla_Max_Price_Is_Final_Price()
        {
            AuctionHandler auctionHandler = this.Create_AuctionHandler_Withs_Participants_3_0_1_And_InitialPrice_100();

            for (int i = 0; i < 3; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(200);
            }

            int expectedFinalPrice = 200;
            Assert.AreEqual(auctionHandler.FinalPrice, expectedFinalPrice);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_With_Higher_Price_Than_Last_Price_To_Make_Several_Rounds()
        {
            AuctionHandler auctionHandler = this.Create_AuctionHandler_Withs_Participants_3_0_1_And_InitialPrice_100();
            int constantIncreaseOfSuggestedPrice = 10;

            for (int i = 0; i < 20; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(100 + i * constantIncreaseOfSuggestedPrice);
            }

            bool expectedIsAuctionOn = true;
            Assert.AreEqual(auctionHandler.IsAuctionOn, expectedIsAuctionOn);
        }
    }
}
