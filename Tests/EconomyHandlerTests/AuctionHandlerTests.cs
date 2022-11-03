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
            List<bool> participants = new List<bool> {true, true, false, true};
            auctionHandler.SetAuctionCondition(participants, initialAuctionerNumber:3 , initialPrice:100);

            int expectedPositiveSuggestedPriceCount = 3;
            Assert.AreEqual(auctionHandler.SuggestedPrices.Where(price => price >= 0).Count(), expectedPositiveSuggestedPriceCount);
        }
        [TestMethod]
        public void Set_AuctionCondision_By_Generating_SuggestedPrices_With_Initial_Price()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            List<bool> participants = new List<bool> {true, true, false, true};
            int initialPrice = 100;
            auctionHandler.SetAuctionCondition(participants, initialAuctionerNumber:3, initialPrice);

            int expectedInitialPrice = 100;

            Assert.AreEqual(auctionHandler.SuggestedPrices[3], expectedInitialPrice); 

        }

        public AuctionHandler Get_AuctionHandler_With_4Player_But_3Participants_And_InitialPlayerNumber_3_InitialPrice_100()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            List<bool> participants = new List<bool> {true, true, false, true};
            auctionHandler.SetAuctionCondition(participants, 3, 100);
            return auctionHandler;
        }
        [TestMethod]
        public void Is_SuggestedPrices_Protected_From_Change_Of_Its_Copy()
        {
            AuctionHandler auctionHandler = Get_AuctionHandler_With_4Player_But_3Participants_And_InitialPlayerNumber_3_InitialPrice_100();

            List<int> copy = auctionHandler.SuggestedPrices;
            copy[0] = 20;

            int expectedValueIfItWasNotProtected = 20;
            Assert.AreNotEqual(auctionHandler.SuggestedPrices[0], expectedValueIfItWasNotProtected);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_For_One_Round()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_4Player_But_3Participants_And_InitialPlayerNumber_3_InitialPrice_100();
            int[] suggestedPrices = new int[] { 200, 100, 240, 300};
            int[] expectedPrices = new int[] { 200, 100, 240, 300};

            int j = 3;
            for (int i = 0; i < 4; i++)
            {
                if ((i + 3) % 4 != 2)
                {
                    j++;
                }
                else
                {
                    j += 2;
                }
                auctionHandler.SuggestNewPriceInTurn(suggestedPrices[i]);
                Assert.AreEqual(auctionHandler.SuggestedPrices[j % 4], expectedPrices[i]);


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
            List<bool> participants = new List<bool> {true, true, false, true};
            
            auctionHandler.SetAuctionCondition(participants, 3, initialPrice:100);

            bool expectedIsAuctionOn = true;
            Assert.AreEqual(auctionHandler.IsAuctionOn, expectedIsAuctionOn);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_With_Smaller_Prices_Than_Max_Price_To_Close_Auction()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_4Player_But_3Participants_And_InitialPlayerNumber_3_InitialPrice_100();
            int[] suggestedPrices = new int[] { 200, 100, 100, 140};

            for (int i = 0; i < 4; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(suggestedPrices[i]);
            }

            bool expectedIsAuctionOn = false;
            Assert.AreEqual(auctionHandler.IsAuctionOn, expectedIsAuctionOn);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_Always_With_Lower_Price_Than_Initial_Price_To_Force_First_Participant_To_Win()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_4Player_But_3Participants_And_InitialPlayerNumber_3_InitialPrice_100();
            int[] suggestedPrices = new int[] { 50, 50, 60};

            for (int i = 0; i < 3; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(suggestedPrices[i]);
            }

            int expectedWinnerNumber = 0;
            Assert.AreEqual(auctionHandler.WinnerNumber, expectedWinnerNumber);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_And_The_Last_Suggested_Price_Is_Updated_Even_If_The_Auction_Is_Closed()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_4Player_But_3Participants_And_InitialPlayerNumber_3_InitialPrice_100();
            int lastParticipantNum = 2;
            int lastSuggestedPrice = 200;

            for (int i = 0; i < 3; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(lastSuggestedPrice);
            }

            int expectedLastSuggestedPrice = 200;
            Assert.AreEqual(auctionHandler.SuggestedPrices[lastParticipantNum], expectedLastSuggestedPrice);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_To_With_Prices_To_Close_Auction_And_Initla_Max_Price_Is_Final_Price()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_4Player_But_3Participants_And_InitialPlayerNumber_3_InitialPrice_100();

            for (int i = 0; i < 4; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(200);
            }

            int expectedFinalPrice = 200;
            Assert.AreEqual(auctionHandler.FinalPrice, expectedFinalPrice);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_With_Higher_Price_Than_Last_Price_To_Make_Several_Rounds()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_4Player_But_3Participants_And_InitialPlayerNumber_3_InitialPrice_100();
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
