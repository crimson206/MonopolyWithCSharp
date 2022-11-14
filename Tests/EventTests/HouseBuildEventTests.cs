using Moq;

namespace Tests
{
    public class HouseBuildEventTestSet
    {
        public HouseBuildEvent houseBuildEvent;
        public Delegator delegator = new Delegator();
        public EventFlow eventFlow = new EventFlow();
        public HouseBuildHandler houseBuildHandler = new HouseBuildHandler();
        public Mock<IDataCenter> mockedDataCenter = new Mock<IDataCenter>();
        public Mock<IStatusHandlers> mockedStatusHandlers = new Mock<IStatusHandlers>();
        public Mock<ITileManager> mockedTileManager = new Mock<ITileManager>();
        public Mock<IEconomyHandlers> mockedEconomyHandlers = new Mock<IEconomyHandlers>();
        public Mock<IDecisionMakers> mockedDecisionMakers = new Mock<IDecisionMakers>();
        public Mock<IBankHandler> mockedBankHandler = new Mock<IBankHandler>();
        public Mock<IHouseBuildDecisionMaker> mockedHouseBuildDecisionMaker = new Mock<IHouseBuildDecisionMaker>();
        public Mock<IPropertyManager> mockedPropertyManager = new Mock<IPropertyManager>();
        public Mock<IEvents> mockedEvents = new Mock<IEvents>();
        public Mock<IMainEvent> mockedMainEvent = new Mock<IMainEvent>();
        public List<int> defaultBalances = new List<int> {100, 100, 100, 100};
        public List<bool> defaultAreInGame = new List<bool> { true, true, true, true };

        public HouseBuildEventTestSet()
        {
            this.SetMockedClassesInitialy();

            this.houseBuildEvent = new HouseBuildEvent(
                this.delegator,
                this.mockedDataCenter.Object,
                this.mockedStatusHandlers.Object,
                this.mockedTileManager.Object,
                this.mockedEconomyHandlers.Object,
                this.mockedDecisionMakers.Object
            );

            this.houseBuildEvent.SetEvents(this.mockedEvents.Object);
        }

        private void SetMockedClassesInitialy()
        {
            this.mockedDataCenter.Setup(t => t.Bank.Balances).Returns(this.defaultBalances);
            this.mockedDataCenter.Setup(t => t.HouseBuildHandler).Returns((IHouseBuildHandlerData)this.houseBuildHandler);
            this.mockedDataCenter.Setup(t => t.InGame.AreInGame).Returns(this.defaultAreInGame);

            this.mockedStatusHandlers.Setup(t => t.BankHandler).Returns(this.mockedBankHandler.Object);
            this.mockedStatusHandlers.Setup(t => t.EventFlow).Returns(this.eventFlow);

            this.mockedTileManager.Setup(t => t.PropertyManager).Returns(this.mockedPropertyManager.Object);

            this.mockedEconomyHandlers.Setup(t => t.HouseBuildHandler).Returns(this.houseBuildHandler);
            
            this.mockedEvents.Setup(t => t.MainEvent).Returns(this.mockedMainEvent.Object);
        
            this.mockedDecisionMakers.Setup(t => t.HouseBuildDecisionMaker).Returns(this.mockedHouseBuildDecisionMaker.Object);
        }

        public void SetTiles(List<Tile> tiles)
        {
            this.mockedDataCenter.Setup(t => t.TileDatas).Returns(tiles.Cast<ITileData>().ToList());
            this.mockedTileManager.Setup(t => t.Tiles).Returns(tiles);
        }
    }


    [TestClass]
    public class HouseBuildEventTests
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

        public HouseBuildEventTestSet CreateTestSetWhereNextActionIsBuildHouse()
        {
            HouseBuildEventTestSet testSet = new HouseBuildEventTestSet();
            HouseBuildEvent houseBuildEvent = testSet.houseBuildEvent;
            testSet.SetTiles(this.CreateRealEstatesWithTwoOwners().Cast<Tile>().ToList());
            testSet.mockedHouseBuildDecisionMaker.Setup(t => t.ChooseRealEstateToBuildHouse()).Returns(0);
            testSet.delegator.SetNextAction(houseBuildEvent.StartEvent);
            testSet.delegator.RunAction();
            testSet.delegator.RunAction();

            return testSet;
        }

        [TestMethod]
        public void StartEvent_Without_House_Buildable_RealEstates_And_HouseBuildEvent_Is_Skipped()
        {
            HouseBuildEventTestSet testSet = new HouseBuildEventTestSet();
            HouseBuildEvent houseBuildEvent = testSet.houseBuildEvent;
            testSet.SetTiles(this.CreateFreeRealEstates(3, "Red").Cast<Tile>().ToList());
            testSet.delegator.SetNextAction(houseBuildEvent.StartEvent);
            testSet.mockedMainEvent.Setup(t => t.AddNextAction(It.IsAny<Action>()));

            testSet.delegator.RunAction();
            testSet.mockedMainEvent.Verify(t => t.AddNextAction(It.IsAny<Action>()), Times.Once());
        }
        [TestMethod]
        public void StartEvent_With_Two_Builders_And_They_Build_Houses()
        {
            HouseBuildEventTestSet testSet = new HouseBuildEventTestSet();
            HouseBuildEvent houseBuildEvent = testSet.houseBuildEvent;
            testSet.SetTiles(this.CreateRealEstatesWithTwoOwners().Cast<Tile>().ToList());
            testSet.mockedHouseBuildDecisionMaker.Setup(t => t.ChooseRealEstateToBuildHouse()).Returns(0);
            testSet.delegator.SetNextAction(houseBuildEvent.StartEvent);
            List<string> expectedNextActionNames = new List<string>{
                "MakeCurrentBuilderDecision",
                "BuildHouse",
                "ChangeBuilder",
                "MakeCurrentBuilderDecision",
                "BuildHouse",
                "EndEvent"
            };

            for (int i = 0; i < 6; i++)
            {
                testSet.delegator.RunAction();
                Assert.AreEqual(testSet.delegator.NextActionName, expectedNextActionNames[i]);
            }   
        }
        [TestMethod]
        public void StartEvent_With_Two_Builders_And_They_Dont_Build()
        {
            HouseBuildEventTestSet testSet = new HouseBuildEventTestSet();
            HouseBuildEvent houseBuildEvent = testSet.houseBuildEvent;
            testSet.SetTiles(this.CreateRealEstatesWithTwoOwners().Cast<Tile>().ToList());
            testSet.mockedHouseBuildDecisionMaker.Setup(t => t.ChooseRealEstateToBuildHouse()).Returns(value:null);
            testSet.delegator.SetNextAction(houseBuildEvent.StartEvent);
            List<string> expectedNextActionNames = new List<string>{
                "MakeCurrentBuilderDecision",
                "ChangeBuilder",
                "MakeCurrentBuilderDecision",
                "EndEvent"
            };

            for (int i = 0; i < 4; i++)
            {
                testSet.delegator.RunAction();
                Assert.AreEqual(testSet.delegator.NextActionName, expectedNextActionNames[i]);
            }   
        }
        [TestMethod]
        public void BuildHouse_And_Call_BankHandler_And_PropertyManager()
        {
            HouseBuildEventTestSet testSet = this.CreateTestSetWhereNextActionIsBuildHouse();

            testSet.mockedBankHandler.Setup(t => t.DecreaseBalance(It.IsAny<int>(), It.IsAny<int>()));
            testSet.mockedPropertyManager.Setup(t => t.BuildHouse(It.IsAny<RealEstate>()));

            testSet.delegator.RunAction();
            Assert.AreEqual(testSet.delegator.LastActionName, "BuildHouse");
            testSet.mockedBankHandler.Verify(t => t.DecreaseBalance(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            testSet.mockedPropertyManager.Verify(t => t.BuildHouse(It.IsAny<RealEstate>()), Times.Once());
        }
    }
}