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
        public void Set_AuctionCondision_By_Generating_SuggestedPrices_With_Wanted_Size()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            int wantedSize = 4;
            auctionHandler.SetAuctionCondition(wantedSize, initialPrice:100);

            int expectedSize = 4;
            Assert.AreEqual(auctionHandler.SuggestedPrices.Count(), expectedSize);
        }
        [TestMethod]
        public void Set_AuctionCondision_By_Generating_SuggestedPrices_With_Initial_Price()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            int size = 4;
            int initialPrice = 100;
            auctionHandler.SetAuctionCondition(participantCount:size, initialPrice);

            int expectedInitialPrice = 100;
            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(auctionHandler.SuggestedPrices[i], expectedInitialPrice); 
            }
        }

        public AuctionHandler Get_AuctionHandler_With_Size_4_And_InitialPrice_100()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            auctionHandler.SetAuctionCondition(4, 100);
            return auctionHandler;
        }
        [TestMethod]
        public void Is_SuggestedPrices_Protected_From_Change_Of_Its_Copy()
        {
            AuctionHandler auctionHandler = Get_AuctionHandler_With_Size_4_And_InitialPrice_100();

            List<int> copy = auctionHandler.SuggestedPrices;
            copy[0] = 20;

            int expectedValueIfItWasNotProtected = 20;
            Assert.AreNotEqual(auctionHandler.SuggestedPrices[0], expectedValueIfItWasNotProtected);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_For_One_Round()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_Size_4_And_InitialPrice_100();
            int[] suggestedPrices = new int[] { 200, 100, 240, 300};
            int[] expectedPrices = new int[] { 200, 100, 240, 300};

            for (int i = 0; i < 4; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(suggestedPrices[i]);
                Assert.AreEqual(auctionHandler.SuggestedPrices[i], expectedPrices[i]);
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
            
            auctionHandler.SetAuctionCondition(participantCount:4,initialPrice:100);

            bool expectedIsAuctionOn = true;
            Assert.AreEqual(auctionHandler.IsAuctionOn, expectedIsAuctionOn);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_With_Smaller_Prices_Than_Max_Price_To_Close_Auction()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_Size_4_And_InitialPrice_100();
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
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_Size_4_And_InitialPrice_100();
            int[] suggestedPrices = new int[] { 50, 50, 40, 60};

            for (int i = 0; i < 4; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(suggestedPrices[i]);
            }

            int expectedWinnerNumber = 0;
            Assert.AreEqual(auctionHandler.WinnerNumber, expectedWinnerNumber);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_And_The_Last_Suggested_Price_Is_Updated_Even_If_The_Auction_Is_Closed()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_Size_4_And_InitialPrice_100();
            int lastParticipantNum = auctionHandler.SuggestedPrices.Count() - 1;
            int lastSuggestedPrice = 200;

            for (int i = 0; i < 4; i++)
            {
                auctionHandler.SuggestNewPriceInTurn(lastSuggestedPrice);
            }

            int expectedLastSuggestedPrice = 200;
            Assert.AreEqual(auctionHandler.SuggestedPrices[lastParticipantNum], expectedLastSuggestedPrice);
        }
        [TestMethod]
        public void Seggest_NewPriceInTurn_To_With_Prices_To_Close_Auction_And_Initla_Max_Price_Is_Final_Price()
        {
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_Size_4_And_InitialPrice_100();

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
            AuctionHandler auctionHandler = this.Get_AuctionHandler_With_Size_4_And_InitialPrice_100();
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
