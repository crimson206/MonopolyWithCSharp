public class TradeEvent
{
    private Delegator delegator;
    private EventFlow eventFlow;
    private Events? events;
    private IDataCenter dataCenter;
    private BankHandler bankHandler;
    private ITileManager tileManager;
    private ITradeHandlerFunction tradeHandler;
    private List<bool> AreInGame => this.dataCenter.InGame.AreInGame;
    private int CurrentPlayerNumber => this.dataCenter.EventFlow.CurrentPlayerNumber;
    private Action lastEvent;
    private List<int>? participantPlayerNumbers = new List<int>();
    private TradeDecisionMaker tradeDecisionMaker = new TradeDecisionMaker();
    private PropertyManager propertyManager = new PropertyManager();
    private TileFilter tileFilter = new TileFilter();
    private ITradeHandlerData tradeHandlerData;

    public TradeEvent
    (
        IStatusHandlers statusHandlers,
        ITileManager tileManager,
        IDataCenter dataCenter,
        IEconomyHandlers economyHandlers,
        Delegator delegator,
        IDecisionMakers decisionMakers
    )
    {
        this.dataCenter = dataCenter;
        this.bankHandler = statusHandlers.BankHandler;
        this.eventFlow = statusHandlers.EventFlow;
        this.tileManager = tileManager;
        this.delegator = delegator;
        this.tradeHandler = economyHandlers.TradeHandler;
        this.tradeHandlerData = dataCenter.TradeHandler;
        this.lastEvent = this.StartTrade;
    }

    private int currentTradeOwner => (int)this.tradeHandlerData.CurrentTradeOwner!;
    private int currentTradeTarget => (int)this.tradeHandlerData.CurrentTradeTarget!;
    private int additionalMoneyFromTradeOwner => this.tradeHandlerData.MoneyOwnerWillingToAddOnTrade;
    private List<Property> properties => this.tileManager.Properties;

    public void SetEvents(Events events)
    {
        this.events = events;
    }
    public void StartTrade()
    {
        this.SetParticipantPlayerNumbers();

        this.tradeHandler
            .SetTrade(this.participantPlayerNumbers!,
                    this.properties);

        this.eventFlow
            .RecommendedString =
                CreateParticipantsString() +
                " will have trade chances in turn";
            
        this.CallNextEvent();
    }

    public void SelectTradeTarget()
    {

        int tradeTarget = this.tradeDecisionMaker.SelectTradeTarget(this.currentTradeOwner);

        this.eventFlow.RecommendedString =
            string.Format("Player{0} choose Player{1} as the trade target",
                        this.currentTradeOwner,
                        this.currentTradeTarget);

        this.CallNextEvent();
    }

    private void SuggestTradeOwnerTradeCondition()
    {
        IPropertyData propertyToGet =
            this.tradeDecisionMaker
                .SelectPropertyToGet
                (this.currentTradeOwner);

        IPropertyData propertyToGive =
            this.tradeDecisionMaker
                .SelectPropertyToGive
                (this.currentTradeOwner);

        int addtionalMoney =
            this.tradeDecisionMaker
                .DecideAdditionalMoney
                (this.currentTradeOwner);

        this.tradeHandler
            .SuggestTradeConditions
            (propertyToGet,
            propertyToGive,
            addtionalMoney);

        this.eventFlow
            .RecommendedString =
                string.Format("Conditions : {0}, {1}, {2}",
                            propertyToGet.Name,
                            propertyToGive.Name,
                            addtionalMoney);

        this.CallNextEvent();
    }

    private void MakeTradeTargetDecionOnTradeAgreement()
    {
        bool agreedTradeTargetWithTradeCondition = this.tradeDecisionMaker
                        .MakeTradeTargetDecisionOnTradeAgreement
                        (this.currentTradeTarget);
        
        this.tradeHandler
            .SetIsTradeAgreed
            (agreedTradeTargetWithTradeCondition);

        if (agreedTradeTargetWithTradeCondition)
        {
            this.eventFlow
                .RecommendedString =
                    string.Format("Player{0} agreed with the condition",
                                this.currentTradeTarget);
        }
        else
        {
            this.eventFlow
                .RecommendedString =
                    string.Format("Player{0} disagreed with the condition",
                                this.currentTradeTarget);
        }

        this.CallNextEvent();
    }

    private void DoTrade()
    {
        if (this.tradeHandlerData.PropertyTradeOwnerToGet is not null)
        {
            Property propertyTradeOwnerToGet = (Property)
                                            this.tradeHandlerData
                                            .PropertyTradeOwnerToGet;

            this.propertyManager
                .ChangeOwner(propertyTradeOwnerToGet,
                            this.currentTradeOwner);
        }

        if (this.tradeHandlerData.PropertyTradeOwnerToGive is not null)
        {
            Property propertyTradeOwnerToGive = (Property)
                                            this.tradeHandlerData
                                            .PropertyTradeOwnerToGive;

            this.propertyManager
                .ChangeOwner(propertyTradeOwnerToGive,
                            this.currentTradeOwner);
        }

        int addtionalMoney = this.tradeHandlerData
                                .MoneyOwnerWillingToAddOnTrade;

        this.bankHandler
            .TransferBalanceFromTo(this.currentTradeOwner,
                                this.currentTradeTarget,
                                this.additionalMoneyFromTradeOwner);

        this.eventFlow
            .RecommendedString =
                "Trade items were exchanged";

        this.CallNextEvent();
    }

    private void ChangeTradeOwner()
    {
        this.tradeHandler
            .ChangeTradeOwner();

        this.eventFlow
            .RecommendedString =
                string.Format(
                            "Player{0} can select a trade target",
                            this.currentTradeOwner);

        this.CallNextEvent();
    }

    private void EndTrade()
    {
        this.eventFlow
            .RecommendedString =
                "All player used their trade chances";

        this.CallNextEvent();
    }

    private void SetParticipantPlayerNumbers()
    {
        this.participantPlayerNumbers!.Clear();
        int playerNumber = this.CurrentPlayerNumber;

        for (int i = 0; i < this.AreInGame.Count(); i++)
        {
            if (AreInGame[playerNumber] is true)
            {
                this.participantPlayerNumbers
                    .Add
                    (playerNumber);
            }

            playerNumber = (playerNumber + 1)
                        % this.AreInGame.Count();
        }
    }

    private string CreateParticipantsString()
    {
        string players = "Player ";
        
        foreach (var item in this.participantPlayerNumbers!)
        {
            players += item.ToString() + ", ";
        }
        return players;
    }

    private string ConvertIntListToString(List<int> intList)
    {
        string converted = string.Empty;

        foreach (var item in intList)
        {
            converted += item.ToString() + ", ";
        }

        converted.Remove(-2, 2);

        return converted;
    }

    private string CreateParticipantNumbersString()
    {
        Dictionary<int, int> suggestedPrices = this.dataCenter
                                                .AuctionHandler
                                                .SuggestedPrices;

        return this.ConvertIntListToString(suggestedPrices.Values.ToList());
    }

    private void CallNextEvent()
    {
        if (this.lastEvent == this.StartTrade)
        {
            this.AddNextEvent(this.SelectTradeTarget);

            return;
        }

        if (this.lastEvent == this.SelectTradeTarget)
        {
            this.AddNextEvent(this.SuggestTradeOwnerTradeCondition);

            return;
        }

        if (this.lastEvent == this.SuggestTradeOwnerTradeCondition)
        {
            this.AddNextEvent(this.MakeTradeTargetDecionOnTradeAgreement);

            return;
        }

        if (this.lastEvent == this.MakeTradeTargetDecionOnTradeAgreement)
        {
            if (this.tradeHandlerData.IsTradeAgreed is true)
            {
                this.AddNextEvent(this.DoTrade);

                return;
            }
            else
            {
                if (this.tradeHandlerData.IsTimeToCloseTrade)
                {
                    this.AddNextEvent(this.EndTrade);

                    return;
                }
                else
                {
                    this.AddNextEvent(this.ChangeTradeOwner);

                    return;
                }
            }


        }

        if (this.lastEvent == this.EndTrade)
        {
            this.AddNextEvent(this.events!
                            .HouseBuildEvent
                            .StartBuidlingHouse);

            return;
        }
    }

    public void AddNextEvent(Action nextEvent)
    {
        this.lastEvent = nextEvent;

        this.delegator
            .SetNextEvent(nextEvent);
    }
}
