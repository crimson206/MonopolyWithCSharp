using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    [TestClass]
    public class AuctionEventTests
    {
        [TestMethod]
        public void RoughTestForWholeProcess()
        {
            List<Tile> tiles = new List<Tile>();
            tiles.Add(new RealEstate("Red0", 100, 50, new List<int> {10, 20, 40, 60}, 50, "Red"));
            tiles.Add(new RealEstate("Red1", 100, 50, new List<int> {10, 20, 40, 60}, 50, "Red"));
            tiles.Add(new RealEstate("Red2", 100, 50, new List<int> {10, 20, 40, 60}, 50, "Red"));

            List<ITileData> tileDatas = new List<ITileData>();
            foreach (var tile in tiles)
            {
                ITileData tileData = (ITileData)tile;
                tileDatas.Add(tileData);
            }

            PropertyManager propertyManager = new PropertyManager();
            Random random = new Random();

            Mock<IDice> mockedDice = new Mock<IDice>();
            Mock<ITileManager> mockedTileManager = new Mock<ITileManager>();
            Mock<IAuctionDecisionMaker> mockedAuctionDecisionMaker = new Mock<IAuctionDecisionMaker>();
            Mock<IPropertyPurchaseDecisionMaker> mockedPropertyPurchaseDecisionMaker
            = new Mock<IPropertyPurchaseDecisionMaker>();            

            IDice dice = mockedDice.Object;
            ITileManager tileManager = mockedTileManager.Object;

            mockedDice.Setup(dice => dice.Roll(random)).Returns(new int[2] { 1, 1 });
            mockedTileManager.Setup(t => t.Tiles).Returns(tiles);
            mockedTileManager.Setup(t => t.TileDatas).Returns(tileDatas);
            mockedTileManager.Setup(t => t.PropertyManager).Returns(propertyManager);
            mockedPropertyPurchaseDecisionMaker.Setup(t => t.MakeDecisionOnPurchase(0)).Returns(false);
            for (int i = 0; i < 4; i++)
            {
                mockedAuctionDecisionMaker.Setup(t => t.SuggestPrice(i)).Returns(0);   
            }

            StatusHandlers statusHandlers = new StatusHandlers();

            AuctionHandler auctionHandler = new AuctionHandler();

            DecisionMakers decisionMakers = new DecisionMakers();
            decisionMakers.PropertyPurchaseDecisionMaker = mockedPropertyPurchaseDecisionMaker.Object;

            IDataCenter dataCenter = new DataCenter(statusHandlers, auctionHandler, tileManager);
            Delegator delegator = new Delegator();
            MainEvent mainEvent = new MainEvent(statusHandlers, tileManager, decisionMakers, delegator, dice, random);

            IAuctionDecisionMaker auctionDecisionMaker = mockedAuctionDecisionMaker.Object;

            AuctionEvent auctionEvent = new AuctionEvent(statusHandlers, tileManager, dataCenter, auctionHandler, delegator, decisionMakers);

            Events events = new Events(mainEvent, auctionEvent);

            mainEvent.SetEvents(events);
            auctionEvent.SetEvents(events);

            mainEvent.AddNextEvent(mainEvent.StartTurn);


            Assert.AreEqual(delegator.NextEventName, "StartTurn");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "RollDice");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "MoveByRollDiceResultTotal");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "LandOnTile");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "MakeDecisionOnPurchaseOfProperty");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "DontPurchaseProperty");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "StartAuction");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "DecideInitialPrice");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "SetUpAuction");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "SuggestPriceInTurn");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "SuggestPriceInTurn");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "SuggestPriceInTurn");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "EndAuction");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "CheckExtraTurn");

        }
    }
}
