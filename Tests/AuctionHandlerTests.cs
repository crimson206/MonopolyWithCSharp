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
        public void TestMethod1()
        {
            AuctionHandler auctionHandler = new AuctionHandler();
            int participantCount = 3;
            int initialPrice = 100;

            auctionHandler.SetAuctionCondition(participantCount, initialPrice);
            Assert.AreEqual(auctionHandler.SuggestedPrices.Max(), 100);

            /// suggest new price, and keep tracking the max price

            int newPrice = 120;

            auctionHandler.SuggestNewPriceInTurn(newPrice);

            Assert.AreEqual(auctionHandler.SuggestedPrices.Max(), newPrice);

            int priceSmallerThanMax = 100;

            Assert.AreNotEqual(auctionHandler.SuggestedPrices.Max(), priceSmallerThanMax);


            /// after suggesting, participantNumber changed
            int previousParticipantNumber = auctionHandler.CurrentParticipantNumber;
            auctionHandler.SuggestNewPriceInTurn(130);
            int currentParticipantNumber = auctionHandler.CurrentParticipantNumber;
            int auctionSize = auctionHandler.SuggestedPrices.Count();
            Assert.AreEqual(currentParticipantNumber, (previousParticipantNumber + 1)%auctionSize);

            /// p1 suggested 140, p2, p0 didn't suggest higher price, and SuggestNewPriceInTurn doesn't work any more
            /// handler sets winnerNumber
            int winnerNumber = auctionHandler.CurrentParticipantNumber;
            auctionHandler.SuggestNewPriceInTurn(140);
            auctionHandler.SuggestNewPriceInTurn(120);
            auctionHandler.SuggestNewPriceInTurn(110);
            auctionHandler.SuggestNewPriceInTurn(120);

            Assert.AreEqual(auctionHandler.isAuctionOn, false);
            Assert.AreEqual(auctionHandler.winnerNumber, winnerNumber);
            Assert.AreEqual(auctionHandler.FinalPrice, 140 );
            /// try to change by suggesting hight number after auction is done, doens't change anything
        
            auctionHandler.SuggestNewPriceInTurn(150);
            auctionHandler.SuggestNewPriceInTurn(170);
            auctionHandler.SuggestNewPriceInTurn(180);

            Assert.AreEqual(auctionHandler.isAuctionOn, false);
            Assert.AreEqual(auctionHandler.winnerNumber, winnerNumber);
            Assert.AreEqual(auctionHandler.FinalPrice, 140 );

            /// handler is reusable after setup
            
            auctionHandler.SetAuctionCondition(4, 100);
            
            /// check status first
            Assert.AreEqual(auctionHandler.isAuctionOn, true);
            Assert.AreEqual(auctionHandler.SuggestedPrices.Max(), 100);
            Assert.AreEqual(auctionHandler.FinalPrice, null);
            Assert.AreEqual(auctionHandler.winnerNumber, null);

            /// try another auction event
        
            auctionHandler.SuggestNewPriceInTurn(300);
            auctionHandler.SuggestNewPriceInTurn(240);
            auctionHandler.SuggestNewPriceInTurn(350);
            Assert.AreEqual(auctionHandler.SuggestedPrices.Max(), 350);

            auctionHandler.SuggestNewPriceInTurn(300);
            auctionHandler.SuggestNewPriceInTurn(240);
            auctionHandler.SuggestNewPriceInTurn(300);
            auctionHandler.SuggestNewPriceInTurn(240);
            Assert.AreEqual(auctionHandler.isAuctionOn, false);
        }
    }
}