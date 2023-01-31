public class HouseBuildEvent : Event
{
    private List<int> participantPlayerNumbers = new List<int>();
    private HouseBuildHandler houseBuildHandler;
    private IHouseBuildHandlerData houseBuildHandlerData;
    private IHouseBuildDecisionMaker houseBuildDecisionMaker;
    private IBankHandler bankHandler;

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
        tileManager,
        statusHandlers)
    {
        this.eventFlow = statusHandlers.EventFlow;
        this.houseBuildHandler = economyHandlers.HouseBuildHandler;
        this.houseBuildHandlerData = dataCenter.HouseBuildHandler;
        this.bankHandler = statusHandlers.BankHandler;
        this.houseBuildDecisionMaker = decisionMakers.HouseBuildDecisionMaker;
    }

    private int? CurrentHouseBuilder => this.houseBuildHandler.CurrentHouseBuilder;
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
                                                    "Player{0} can build a house",
                                                    (int)this.houseBuildHandlerData.CurrentHouseBuilder!);
        }

        this.CallNextAction();
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
            this.houseBuildHandler.SetRealEstateToBuildHouse((int)decision);

            this.eventFlow.RecommendedString = string.Format(
                "Player{0} will build a house at {1}",
                this.CurrentHouseBuilder,
                this.RealEstateToBuildHouse.Name
            );
        }

        this.CallNextAction();
    }

    private void BuildHouse()
    {
        this.propertyManager.BuildHouse(this.RealEstateToBuildHouse);

        this.bankHandler.DecreaseBalance((int)this.CurrentHouseBuilder!,
                                        this.RealEstateToBuildHouse.BuildingCost);

        this.eventFlow.RecommendedString = "A house was built";

        this.CallNextAction();
    }

    private void ChangeBuilder()
    {
        this.houseBuildHandler.ChangeHouseBuilder();

        this.eventFlow.RecommendedString = string.Format(
                                                "Player{0} can build a house",
                                                (int)this.houseBuildHandlerData.CurrentHouseBuilder!);

        this.CallNextAction();
    }

    public override void EndEvent()
    {
        if (this.CurrentHouseBuilder is not null)
        {
            this.eventFlow.RecommendedString = "This house build event is over";
        }

        this.CallNextAction();
    }


    protected override void CallNextAction()
    {
        if (this.lastAction == this.StartEvent)
        {
            if (this.houseBuildHandlerData.AreAnyBuildable is true)
            {
                this.AddNextAction(this.MakeCurrentBuilderDecision);
            }
            else
            {
                this.AddNextAction(this.EndEvent);
            }

            return;
        }

        if (this.lastAction == this.MakeCurrentBuilderDecision)
        {
            if (this.houseBuildHandlerData.RealEstateToBuildHouse is not null)
            {
                this.AddNextAction(this.BuildHouse);
            }
            else
            {
                if (this.houseBuildHandlerData.IsLastBuilder)
                {
                    this.AddNextAction(this.EndEvent);
                }
                else
                {
                    this.AddNextAction(this.ChangeBuilder);
                }
            }

            return;
        }

        if (this.lastAction == this.BuildHouse)
        {
            if (this.houseBuildHandlerData.IsLastBuilder is true)
            {
                this.AddNextAction(this.EndEvent);
            }
            else
            {
                this.AddNextAction(this.ChangeBuilder);
            }

            return;
        }

        if (this.lastAction == this.ChangeBuilder)
        {
            this.AddNextAction(this.MakeCurrentBuilderDecision);

            return;
        }

        if (this.lastAction == this.EndEvent)
        {
            this.events!.UnmortgageEvent.AddNextAction(this.events!.UnmortgageEvent.StartEvent);

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
