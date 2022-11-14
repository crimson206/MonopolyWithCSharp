public class HouseBuildEvent : Event
{
    private EventFlow eventFlow;
    private List<int> participantPlayerNumbers = new List<int>();
    private HouseBuildHandler houseBuildHandler;
    private IHouseBuildHandlerData houseBuildHandlerData;
    private IHouseBuildDecisionMaker houseBuildDecisionMaker;
    private BankHandler bankHandler;

    public HouseBuildEvent
    (Delegator delegator,
    IDataCenter dataCenter,
    IStatusHandlers statusHandlers,
    ITileManager tileManager,
    IEconomyHandlers economyHandlers,
    IDecisionMakers decisionMakers)
        :base
        (delegator,
        dataCenter,
        tileManager)
    {
        this.eventFlow = statusHandlers.EventFlow;
        this.houseBuildHandler = economyHandlers.HouseBuildHandler;
        this.houseBuildHandlerData = dataCenter.HouseBuildHandler;
        this.bankHandler = statusHandlers.BankHandler;
        this.houseBuildDecisionMaker = decisionMakers.HouseBuildDecisionMaker;
    }

    private int CurrentHouseBuilder => (int)this.houseBuildHandler.CurrentHouseBuilder!;
    private RealEstate RealEstateToBuildHouse => (RealEstate)this.houseBuildHandler.RealEstateToBuildHouse!;

    public override void StartEvent()
    {
        List<int> balances = this.dataCenter.Bank.Balances;
        List<ITileData> tileDatas = this.dataCenter.TileDatas;
        List<IRealEstateData> realEstateDatas = this.FilterTileDatasToRealEstateDatas(tileDatas);

        this.houseBuildHandler
            .SetHouseBuildHandler
                (balances,
                realEstateDatas);

        if (this.houseBuildHandlerData.AreAnyBuildable is true)
        {
            this.eventFlow.RecommendedString = string.Format(
                "Player{0} can build houses",
                (int)this.houseBuildHandlerData.CurrentHouseBuilder!
            );
        }

        this.CallNextEvent();
    }

    private void MakeCurrentBuilderDecision()
    {
        int? decision = this.houseBuildDecisionMaker
                            .ChooseRealEstateToBuildHouse();

        if (decision is null)
        {
            this.eventFlow.RecommendedString = "Player" +
                                                this.CurrentHouseBuilder +
                                                " didn't build a house"; 
        }
        else
        {
            IRealEstateData realEstateToBuildHouse = this.houseBuildHandler.HouseBuildableRealEstatesOfCurrentBuilder[(int)decision];
            this.houseBuildHandler.SetRealEstateToBuildHouse(realEstateToBuildHouse);

            this.eventFlow.RecommendedString = string.Format(
                "Player{0} will build a house at {}",
                this.CurrentHouseBuilder,
                this.RealEstateToBuildHouse
            );
        }

        this.CallNextEvent();
    }

    private void BuildHouse()
    {
        this.propertyManager.BuildHouse(this.RealEstateToBuildHouse);

        this.bankHandler.DecreaseBalance(this.CurrentHouseBuilder,
                                        this.RealEstateToBuildHouse.BuildingCost);

        this.eventFlow.RecommendedString = "A house was built";

        this.CallNextEvent();
    }

    private void ChangeBuilder()
    {
        this.houseBuildHandler.ChangeHouseBuilder();

        if (this.houseBuildHandlerData.AreAnyBuildable is true)
        {
            this.eventFlow.RecommendedString = string.Format(
                "Player{0} can build houses",
                (int)this.houseBuildHandlerData.CurrentHouseBuilder!
            );
        }

        this.CallNextEvent();
    }

    private void EndEvent()
    {
        this.eventFlow.RecommendedString = "This house build event is over";

        this.CallNextEvent();
    }


    protected override void CallNextEvent()
    {
        if (this.lastEvent == this.StartEvent)
        {
            if (this.houseBuildHandlerData.AreAnyBuildable is true)
            {
                this.AddNextEvent(this.MakeCurrentBuilderDecision);
            }
            else
            {
                this.events!.MainEvent.AddNextEvent(this.events!.MainEvent.EndTurn);
            }

            return;
        }

        if (this.lastEvent == this.MakeCurrentBuilderDecision)
        {
            if (this.houseBuildHandlerData.RealEstateToBuildHouse is not null)
            {
                this.AddNextEvent(this.BuildHouse);
            }
            else
            {
                if (this.houseBuildHandlerData.IsLastBuilder)
                {
                    this.AddNextEvent(this.EndEvent);
                }
                else
                {
                    this.AddNextEvent(this.ChangeBuilder);
                }
            }

            return;
        }

        if (this.lastEvent == this.ChangeBuilder)
        {
            this.AddNextEvent(this.MakeCurrentBuilderDecision);

            return;
        }

        if (this.lastEvent == this.EndEvent)
        {
            this.events!.MainEvent.AddNextEvent(this.events!.MainEvent.EndTurn);

            return;
        }
    }

    private List<IRealEstateData> FilterTileDatasToRealEstateDatas(List<ITileData> tileDatas)
    {
        List<IRealEstateData> realEstateDatas = 
            (from tileData in tileDatas
            where tileData is IRealEstateData
            select tileData as IRealEstateData).ToList();
        return realEstateDatas;
    }
}
