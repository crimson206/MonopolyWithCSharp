public class UnmortgageEvent : Event
{
    private List<int> participantPlayerNumbers = new List<int>();
    private UnmortgageHandler unmortgageHandler;
    private IUnmortgageHandlerData unmortgageHandlerData;
    private IUnmortgageDecisionMaker unmortgageDecisionMaker;
    private IBankHandler bankHandler;

    public UnmortgageEvent
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
        this.unmortgageHandler = economyHandlers.UnmortgageHandler;
        this.unmortgageHandlerData = dataCenter.UnmortgageHandler;
        this.bankHandler = statusHandlers.BankHandler;
        this.unmortgageDecisionMaker = decisionMakers.DemortgageDecisionMaker;
    }

    private int? CurrentPlayerToDemortgage => this.unmortgageHandler.CurrentPlayerToDemortgage;
    private Property PropertyToDemortgage => (Property)this.unmortgageHandler.PropertyToDeMortgage!;

    public override void StartEvent()
    {
        List<int> balances = this.dataCenter.Bank.Balances;
        List<ITileData> tileDatas = this.dataCenter.TileDatas;
        List<IPropertyData> propertyDatas = this.FilterPropertyDatas(tileDatas);

        this.unmortgageHandler
            .SetDeMortgageHandler
                (balances,
                propertyDatas);

        if (this.unmortgageHandlerData.AreAnyDemortgagible is true)
        {
            this.eventFlow.RecommendedString = "Players can unmortgage a property";
        }

        this.CallNextAction();
    }

    private void MakeCurrentPlayerDecision()
    {
        int? decision = this.unmortgageDecisionMaker
                            .MakeDecionOnPropertyToDemortgage();

        if (decision is null)
        {
            this.eventFlow.RecommendedString = "Player" +
                                                this.CurrentPlayerToDemortgage +
                                                " didn't unmortgage any property"; 
        }
        else
        {
            this.unmortgageHandler.SetRealEstateToBuildHouse((int)decision);

            this.eventFlow.RecommendedString = string.Format(
                "Player{0} will unmortgage {1}",
                this.CurrentPlayerToDemortgage,
                this.PropertyToDemortgage.Name
            );
        }

        this.CallNextAction();
    }

    private void Demortgage()
    {
        this.propertyManager.SetIsMortgaged(this.PropertyToDemortgage, false);
        int costToDemortgage = this.PropertyToDemortgage.Mortgage;

        this.bankHandler.DecreaseBalance((int)this.CurrentPlayerToDemortgage!, 
                                        costToDemortgage);

        this.eventFlow.RecommendedString = "The property is unmortgaged";

        this.CallNextAction();
    }

    private void ChangePlayer()
    {
        this.unmortgageHandler.ChangePlayerToDeMortgage();

        this.eventFlow.RecommendedString = string.Format(
            "Player{0} can unmortgage a property",
            (int)this.unmortgageHandlerData.CurrentPlayerToDemortgage!);

        this.CallNextAction();
    }

    public override void EndEvent()
    {
        if ((this.CurrentPlayerToDemortgage is not null))
        {
            this.eventFlow.RecommendedString = "This unmortgage event is over";
        }

        this.CallNextAction();
    }


    protected override void CallNextAction()
    {
        if (this.lastAction == this.StartEvent)
        {
            if (this.unmortgageHandlerData.AreAnyDemortgagible is true)
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
            if (this.unmortgageHandlerData.PropertyToDeMortgage is not null)
            {
                this.AddNextAction(this.Demortgage);
            }
            else
            {
                if (this.unmortgageHandlerData.IsLastPlayer)
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
            if (this.unmortgageHandlerData.IsLastPlayer is true)
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
           this.events!.MainEvent.AddNextAction(this.events!.MainEvent.StartEvent);

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
