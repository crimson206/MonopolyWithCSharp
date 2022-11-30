public class DemortgageEvent : Event
{
    private List<int> participantPlayerNumbers = new List<int>();
    private DemortgageHandler demortgageHandler;
    private IDemortgageHandlerData demortgageHandlerData;
    private IDemortgageDecisionMaker demortgageDecisionMaker;
    private IBankHandler bankHandler;

    public DemortgageEvent
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
        this.demortgageHandler = economyHandlers.DemortgageHandler;
        this.demortgageHandlerData = dataCenter.DemortgageHandler;
        this.bankHandler = statusHandlers.BankHandler;
        this.demortgageDecisionMaker = decisionMakers.DemortgageDecisionMaker;
    }

    private int? CurrentPlayerToDemortgage => this.demortgageHandler.CurrentPlayerToDemortgage;
    private Property PropertyToDemortgage => (Property)this.demortgageHandler.PropertyToDeMortgage!;

    public override void StartEvent()
    {
        List<int> balances = this.dataCenter.Bank.Balances;
        List<ITileData> tileDatas = this.dataCenter.TileDatas;
        List<IPropertyData> propertyDatas = this.FilterPropertyDatas(tileDatas);

        this.demortgageHandler
            .SetDeMortgageHandler
                (balances,
                propertyDatas);

        if (this.demortgageHandlerData.AreAnyDemortgagible is true)
        {
            this.eventFlow.RecommendedString = "Players can de-mortgage a property";
        }

        this.CallNextEvent();
    }

    private void MakeCurrentPlayerDecision()
    {
        int? decision = this.demortgageDecisionMaker
                            .MakeDecionOnPropertyToDemortgage();

        if (decision is null)
        {
            this.eventFlow.RecommendedString = "Player" +
                                                this.CurrentPlayerToDemortgage +
                                                " didn't de-mortgage any property"; 
        }
        else
        {
            this.demortgageHandler.SetRealEstateToBuildHouse((int)decision);

            this.eventFlow.RecommendedString = string.Format(
                "Player{0} will de-mortgage {1}",
                this.CurrentPlayerToDemortgage,
                this.PropertyToDemortgage.Name
            );
        }

        this.CallNextEvent();
    }

    private void Demortgage()
    {
        this.propertyManager.SetIsMortgaged(this.PropertyToDemortgage, false);
        int costToDemortgage = (int)(1.1 * this.PropertyToDemortgage.Mortgage);

        this.bankHandler.DecreaseBalance((int)this.CurrentPlayerToDemortgage!, 
                                        costToDemortgage);

        this.eventFlow.RecommendedString = "The property is now not mortgaged";

        this.CallNextEvent();
    }

    private void ChangePlayer()
    {
        this.demortgageHandler.ChangePlayerToDeMortgage();

        this.eventFlow.RecommendedString = string.Format(
            "Player{0} can de-mortgage a property",
            (int)this.demortgageHandlerData.CurrentPlayerToDemortgage!);

        this.CallNextEvent();
    }

    public override void EndEvent()
    {
        if ((this.CurrentPlayerToDemortgage is not null))
        {
            this.eventFlow.RecommendedString = "This de-mortgage event is over";
        }

        this.CallNextEvent();
    }


    protected override void CallNextEvent()
    {
        if (this.lastAction == this.StartEvent)
        {
            if (this.demortgageHandlerData.AreAnyDemortgagible is true)
            {
                this.AddNextAction(this.MakeCurrentPlayerDecision);
            }
            else
            {
                this.AddNextAction(this.EndEvent);
            }

            return;
        }

        if (this.lastAction == this.MakeCurrentPlayerDecision)
        {
            if (this.demortgageHandlerData.PropertyToDeMortgage is not null)
            {
                this.AddNextAction(this.Demortgage);
            }
            else
            {
                if (this.demortgageHandlerData.IsLastPlayer)
                {
                    this.AddNextAction(this.EndEvent);
                }
                else
                {
                    this.AddNextAction(this.ChangePlayer);
                }
            }

            return;
        }

        if (this.lastAction == this.Demortgage)
        {
            if (this.demortgageHandlerData.IsLastPlayer is true)
            {
                this.AddNextAction(this.EndEvent);
            }
            else
            {
                this.AddNextAction(this.ChangePlayer);
            }

            return;
        }

        if (this.lastAction == this.ChangePlayer)
        {
            this.AddNextAction(this.MakeCurrentPlayerDecision);

            return;
        }

        if (this.lastAction == this.EndEvent)
        {
           this.events!.MainEvent.AddNextAction(this.events!.MainEvent.EndEvent);

    	    return;
        }
    }

    private List<IPropertyData> FilterPropertyDatas(List<ITileData> tileDatas)
    {
        List<IPropertyData> propertyDatas = 
            (from tileData in tileDatas
            where tileData is IPropertyData
            select tileData as IPropertyData).ToList();
        return propertyDatas;
    }
}
