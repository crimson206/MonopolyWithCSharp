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

            EconomyHandlers economyHandlers = new EconomyHandlers();



            IDataCenter dataCenter = new DataCenter(statusHandlers, economyHandlers, tileManager);
            DecisionMakers decisionMakers = new DecisionMakers(dataCenter);
            Delegator delegator = new Delegator();
            MainEvent mainEvent = new MainEvent(statusHandlers, tileManager, decisionMakers, dataCenter, delegator, dice, random);
            decisionMakers.PropertyPurchaseDecisionMaker = mockedPropertyPurchaseDecisionMaker.Object;

            IAuctionDecisionMaker auctionDecisionMaker = mockedAuctionDecisionMaker.Object;

            AuctionEvent auctionEvent = new AuctionEvent(statusHandlers, tileManager, dataCenter, economyHandlers.AuctionHandler, delegator, decisionMakers);

            Events events = new Events(mainEvent,
                                    auctionEvent,
                                    new HouseBuildEvent(delegator, dataCenter, statusHandlers, tileManager, economyHandlers, decisionMakers),
                                    new TradeEvent(statusHandlers, tileManager, dataCenter, economyHandlers, delegator, decisionMakers),
                                    new SellItemEvent(delegator, dataCenter, statusHandlers, tileManager, economyHandlers, decisionMakers));

            mainEvent.SetEvents(events);
            auctionEvent.SetEvents(events);

            mainEvent.AddNextAction(mainEvent.StartEvent);


            Assert.AreEqual(delegator.NextActionName, "StartEvent");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "RollDice");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "MoveByRollDiceResultTotal");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "LandOnTile");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "MakeDecisionOnPurchaseOfProperty");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "DontPurchaseProperty");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "PassAuctionCondition");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "StartEvent");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "DecideInitialPrice");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "SetUpAuction");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "SuggestPriceInTurn");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "SuggestPriceInTurn");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "SuggestPriceInTurn");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "BuyWinnerProperty");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "EndEvent");
            delegator.RunAction();
            Assert.AreEqual(delegator.NextActionName, "CheckExtraTurn");
        }
    }
}
