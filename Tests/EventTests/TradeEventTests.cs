using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    public class TestSettingWithMockedClasses
    {
        public TradeEvent? tradeEvent;
        public List<Tile> tiles = new List<Tile>();
        public List<ITileData> tileDatas = new List<ITileData>();
        public List<Property> properties = new List<Property>();
        public PropertyManager propertyManager = new PropertyManager();
        public BankHandler bankHandler = new BankHandler();
        public EventFlow eventFlow = new EventFlow();
        public TradeHandler tradeHandler = new TradeHandler();
        public Delegator delegator = new Delegator();
        public InGameHandler inGameHandler = new InGameHandler(4);
        public HouseBuildEvent houseBuildEvent;
        public Mock<ITileManager> mockedTileManager = new Mock<ITileManager>();
        public Mock<IStatusHandlers> mockedStatusHandlers = new Mock<IStatusHandlers>();
        public Mock<IDataCenter> mockedDataCenter = new Mock<IDataCenter>();
        public Mock<IEconomyHandlers> mockedEconomyHandlers = new Mock<IEconomyHandlers>();
        public Mock<ITradeDecisionMaker> mockedTradeDecisionMaker = new Mock<ITradeDecisionMaker>();
        public Mock<IDecisionMakers> mockedDecisionMakers = new Mock<IDecisionMakers>();
        public Mock<IEvents> mockedEvents = new Mock<IEvents>();

        public TestSettingWithMockedClasses()
        {
            this.houseBuildEvent = new HouseBuildEvent(this.delegator, this.mockedDataCenter.Object, this.mockedStatusHandlers.Object);
            this.SetProperties();
            this.SetupMocks();
        }

        public void SetProperties()
        {
            for (int i = 0; i < 4; i++)
            {
                string name = string.Format("Red{0}", i);
                this.tiles.Add(new RealEstate(name, 100, 50, new List<int> {10, 20, 40, 60}, 50, "Red"));
            }

            for (int i = 0; i < 3; i++)
            {
                string name = string.Format("RailRoad{0}", i);
                this.tiles.Add(new RailRoad(name, 100, new List<int> {10, 20, 40}, 50));
            }

            for (int i = 0; i < 2; i++)
            {
                string name = string.Format("Utility{0}", i);
                this.tiles.Add(new Utility(name, 100, new List<int> {4, 10}, 50));
            }

            this.properties = (from tile in this.tiles select tile as Property).ToList();
            this.tileDatas = (from tile in this.tiles select tile as ITileData).ToList();
        }

        public void SetupMocks()
        {
            mockedTileManager.Setup(t => t.Tiles).Returns(tiles);
            mockedTileManager.Setup(t => t.Properties).Returns(properties);
            mockedTileManager.Setup(t => t.TileDatas).Returns(tileDatas);
            mockedTileManager.Setup(t => t.PropertyManager).Returns(propertyManager);
            mockedStatusHandlers.Setup(t => t.BankHandler).Returns(bankHandler);
            mockedStatusHandlers.Setup(t => t.EventFlow).Returns(eventFlow);
            mockedStatusHandlers.Setup(t => t.InGameHandler).Returns(inGameHandler);
            mockedDataCenter.Setup(t => t.Bank).Returns((IBankHandlerData)bankHandler);
            mockedDataCenter.Setup(t => t.EventFlow).Returns((IEventFlowData)eventFlow);
            mockedDataCenter.Setup(t => t.InGame).Returns((IInGameHandlerData)inGameHandler);
            mockedDataCenter.Setup(t => t.TradeHandler).Returns((ITradeHandlerData)tradeHandler);
            mockedEconomyHandlers.Setup(t => t.TradeHandler).Returns(tradeHandler);
            mockedDecisionMakers.Setup(t => t.TradeDecisionMaker).Returns(mockedTradeDecisionMaker.Object);
            mockedEvents.Setup(t => t.HouseBuildEvent).Returns(houseBuildEvent);
        }

        public void SetTradeEvent()
        {

            ITileManager tilemanager = mockedTileManager.Object;
            IStatusHandlers statusHandlers = mockedStatusHandlers.Object;
            IDataCenter dataCenter = mockedDataCenter.Object;
            IEconomyHandlers economyHandlers = mockedEconomyHandlers.Object;
            IDecisionMakers decisionMakers = mockedDecisionMakers.Object;

            TradeEvent tradeEvent =
                new TradeEvent(statusHandlers,
                            tilemanager,
                            dataCenter,
                            economyHandlers,
                            delegator,
                            decisionMakers);

            Mock<IEvents> mockedEvents = new Mock<IEvents>();
            mockedEvents.Setup(t => t.HouseBuildEvent).Returns(houseBuildEvent);
            tradeEvent.SetEvents(mockedEvents.Object);

            this.tradeEvent = tradeEvent;
        }
    }



    [TestClass]
    public class TradeEventTests
    {

    public void SetOnwerNumbersOfProperties(List<Property> properties)
    {
        List<int> ownerNumbers = new List<int> {0, 0, 1, 1, 2, 2, 3, 3};

        for (int i = 0; i < 8; i++)
        {
            properties[i].SetOnwerPlayerNumber(ownerNumbers[i]);
        }
    }

        [TestMethod]
        public void RoughTestForWholeProcess()
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < 4; i++)
            {
                string name = string.Format("Red{0}", i);
                tiles.Add(new RealEstate(name, 100, 50, new List<int> {10, 20, 40, 60}, 50, "Red"));
            }

            for (int i = 0; i < 3; i++)
            {
                string name = string.Format("RailRoad{0}", i);
                tiles.Add(new RailRoad(name, 100, new List<int> {10, 20, 40}, 50));
            }

            for (int i = 0; i < 2; i++)
            {
                string name = string.Format("Utility{0}", i);
                tiles.Add(new Utility(name, 100, new List<int> {4, 10}, 50));
            }

            List<Property> properties = new List<Property>();
            List<ITileData> tileDatas = new List<ITileData>();
            properties = (from tile in tiles select tile as Property).ToList();
            tileDatas = (from tile in tiles select tile as ITileData).ToList();

            PropertyManager propertyManager = new PropertyManager();
            BankHandler bankHandler = new BankHandler();
            EventFlow eventFlow = new EventFlow();
            TradeHandler tradeHandler = new TradeHandler();
            Delegator delegator = new Delegator();
            InGameHandler inGameHandler = new InGameHandler(4);

            Mock<ITileManager> mockedTileManager = new Mock<ITileManager>();
            mockedTileManager.Setup(t => t.Tiles).Returns(tiles);
            mockedTileManager.Setup(t => t.Properties).Returns(properties);
            mockedTileManager.Setup(t => t.TileDatas).Returns(tileDatas);
            mockedTileManager.Setup(t => t.PropertyManager).Returns(propertyManager);

            Mock<IStatusHandlers> mockedStatusHandlers = new Mock<IStatusHandlers>();
            mockedStatusHandlers.Setup(t => t.BankHandler).Returns(bankHandler);
            mockedStatusHandlers.Setup(t => t.EventFlow).Returns(eventFlow);
            mockedStatusHandlers.Setup(t => t.InGameHandler).Returns(inGameHandler);

            Mock<IDataCenter> mockedDataCenter = new Mock<IDataCenter>();
            mockedDataCenter.Setup(t => t.Bank).Returns((IBankHandlerData)bankHandler);
            mockedDataCenter.Setup(t => t.EventFlow).Returns((IEventFlowData)eventFlow);
            mockedDataCenter.Setup(t => t.InGame).Returns((IInGameHandlerData)inGameHandler);
            mockedDataCenter.Setup(t => t.TradeHandler).Returns((ITradeHandlerData)tradeHandler);

            Mock<IEconomyHandlers> mockedEconomyHandlers = new Mock<IEconomyHandlers>();
            mockedEconomyHandlers.Setup(t => t.TradeHandler).Returns(tradeHandler);

            Mock<ITradeDecisionMaker> mockedTradeDecisionMaker = new Mock<ITradeDecisionMaker>();
            ITradeDecisionMaker tradeDecisionMaker = mockedTradeDecisionMaker.Object;

            Mock<IDecisionMakers> mockedDecisionMakers = new Mock<IDecisionMakers>();
            mockedDecisionMakers.Setup(t => t.TradeDecisionMaker).Returns(tradeDecisionMaker);



            ITileManager tilemanager = mockedTileManager.Object;
            IStatusHandlers statusHandlers = mockedStatusHandlers.Object;
            IDataCenter dataCenter = mockedDataCenter.Object;
            IEconomyHandlers economyHandlers = mockedEconomyHandlers.Object;
            IDecisionMakers decisionMakers = mockedDecisionMakers.Object;


            TradeEvent tradeEvent =
                new TradeEvent(statusHandlers,
                            tilemanager,
                            dataCenter,
                            economyHandlers,
                            delegator,
                            decisionMakers);

            HouseBuildEvent houseBuildEvent = new HouseBuildEvent(delegator, dataCenter, statusHandlers);

            Mock<IEvents> mockedEvents = new Mock<IEvents>();
            mockedEvents.Setup(t => t.HouseBuildEvent).Returns(houseBuildEvent);
            
            tradeEvent.SetEvents(mockedEvents.Object);
            
            delegator.SetNextEvent(tradeEvent.StartEvent);
            Assert.AreEqual(delegator.NextEventName, "StartEvent");

            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "StartEvent");
        }
        [TestMethod]
        public void StartTrade_Without_TradableProperties_And_End_VerySoon()
        {
            TestSettingWithMockedClasses testSet = new TestSettingWithMockedClasses();
            testSet.SetTradeEvent();
            TradeEvent tradeEvent = testSet.tradeEvent!;
            Delegator delegator = testSet.delegator;

            delegator.SetNextEvent(tradeEvent.StartEvent);
            Assert.AreEqual(delegator.NextEventName, "StartEvent");

            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "StartEvent");
        }
        [TestMethod]
        public void StartTrade_With_TradableProperties_And_Follow_TradeEventFlow()
        {
            TestSettingWithMockedClasses testSet = new TestSettingWithMockedClasses();
            Delegator delegator = testSet.delegator;

            /// use real decisionmaker
            TradeDecisionMaker tradeDecisionMaker = new TradeDecisionMaker(testSet.mockedDataCenter.Object);
            testSet.mockedDecisionMakers.Setup(t => t.TradeDecisionMaker).Returns(tradeDecisionMaker);

            testSet.SetTradeEvent();
            TradeEvent tradeEvent = testSet.tradeEvent!;

            this.SetOnwerNumbersOfProperties(testSet.properties);

            delegator.SetNextEvent(tradeEvent.StartEvent);
            Assert.AreEqual(delegator.NextEventName, "StartEvent");

            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "SelectTradeTarget");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "SuggestTradeOwnerTradeCondition");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "MakeTradeTargetDecisionOnTradeAgreement");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "DoTrade");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "ChangeTradeOwner");
            delegator.RunEvent();
            Assert.AreEqual(delegator.NextEventName, "SelectTradeTarget");
            delegator.RunEvent();
            delegator.RunEvent();
            delegator.RunEvent();
            delegator.RunEvent();
        }
    }
}