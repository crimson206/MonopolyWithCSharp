public class TradeEvent : Event
{
    private EventFlow eventFlow;
    private BankHandler bankHandler;
    private ITradeHandlerFunction tradeHandler;
    private List<bool> AreInGame => this.dataCenter.InGame.AreInGame;
    private List<int>? participantPlayerNumbers = new List<int>();
    private ITradeDecisionMaker tradeDecisionMaker;
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
        : base(delegator,
            dataCenter,
            tileManager)
    {
        this.bankHandler = statusHandlers.BankHandler;
        this.eventFlow = statusHandlers.EventFlow;
        this.tileManager = tileManager;
        this.tradeHandler = economyHandlers.TradeHandler;
        this.tradeHandlerData = dataCenter.TradeHandler;
        this.tradeDecisionMaker = decisionMakers.TradeDecisionMaker;
    }

    private int currentTradeOwner => (int)this.tradeHandlerData.CurrentTradeOwner!;
    private int currentTradeTarget => (int)this.tradeHandlerData.CurrentTradeTarget!;
    private int additionalMoneyFromTradeOwner => this.tradeHandlerData.MoneyOwnerWillingToAddOnTrade;
    private List<Property> properties => this.tileManager.Properties;

    public void SetEvents(IEvents events)
    {
        this.events = events;
    }
    public override void StartEvent()
    {
        this.SetParticipantPlayerNumbers();

        this.tradeHandler
            .SetTrade(this.participantPlayerNumbers!,
                    this.properties);

        if (this.tradeHandlerData.IsThereTradableProperties)
        {
            this.eventFlow
                .RecommendedString =
                    CreateParticipantsString() +
                    " will have trade chances in turn";
        }

        this.CallNextEvent();
    }

    public void HasNoTradeTarget()
    {
        this.eventFlow.RecommendedString =
            string.Format("Player{0} has no selectable trade target",
                        this.currentTradeOwner);
        
        this.CallNextEvent();
    }

    public void SelectTradeTarget()
    {

        int tradeTarget = this.tradeHandlerData
                        .SelectableTargetNumbers
                            [this.tradeDecisionMaker
                            .SelectTradeTarget()];

        this.tradeHandler.SetTradeTarget(tradeTarget);

        this.eventFlow.RecommendedString =
            string.Format("Player{0} choose Player{1} as the trade target",
                        this.currentTradeOwner,
                        this.currentTradeTarget);

        this.CallNextEvent();
    }

    private void SuggestTradeOwnerTradeCondition()
    {
        IPropertyData? propertyToGet;
        IPropertyData? propertyToGive;

        List<IPropertyData> tradablePropertiesOfTradeTarget = this.tradeHandlerData.TradablePropertiesOfTradeTarget;
        List<IPropertyData> tradablePropertiesOfTradeOwner = this.tradeHandlerData.TradablePropertiesOfTradeOwner;

        if (tradablePropertiesOfTradeTarget.Count() != 0)
        {
            int? decision = this.tradeDecisionMaker.SelectPropertyToGet();
            propertyToGet = (decision is null? null : tradablePropertiesOfTradeTarget[(int)decision]);
        }
        else
        {
            propertyToGet = null;
        }

        if (tradablePropertiesOfTradeOwner.Count() != 0)
        {
            int? decision = this.tradeDecisionMaker.SelectPropertyToGive();
            propertyToGive = (decision is null? null : tradablePropertiesOfTradeOwner[(int)decision]);
        }
        else
        {
            propertyToGive = null;
        }

        int addtionalMoney =
            this.tradeDecisionMaker
                .DecideAdditionalMoney();

        this.tradeHandler
            .SuggestTradeConditions
            (propertyToGet,
            propertyToGive,
            addtionalMoney);

        string stringPropertyToGet = (propertyToGet is null? "null" :propertyToGet.Name);
        string stringPropertyToGive = (propertyToGive is null? "null" :propertyToGive.Name);

        this.eventFlow
            .RecommendedString =
                string.Format("Conditions : {0}, {1}, {2}",
                            stringPropertyToGet,
                            stringPropertyToGive,
                            addtionalMoney);

        this.CallNextEvent();
    }

    private void MakeTradeTargetDecisionOnTradeAgreement()
    {
        bool agreedTradeTargetWithTradeCondition = this.tradeDecisionMaker
                        .MakeTradeTargetDecisionOnTradeAgreement();
        
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
                            this.currentTradeTarget);
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

        if (this.tradeHandlerData.SelectableTargetNumbers.Count() != 0)
        {
        this.eventFlow
            .RecommendedString =
                string.Format(
                            "Player{0} can select a trade target",
                            this.currentTradeOwner);
        }

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

    protected override void CallNextEvent()
    {
        if (this.lastEvent == this.StartEvent)
        {
            if (this.tradeHandlerData.IsThereTradableProperties)
            {
                if (this.tradeHandlerData.SelectableTargetNumbers.Count() == 0)
                {
                    this.AddNextEvent(this.HasNoTradeTarget);
                }
                else
                {
                    this.AddNextEvent(this.SelectTradeTarget);
                }
            }
            else
            {
                this.events!.HouseBuildEvent.AddNextEvent(this.events!
                                .HouseBuildEvent
                                .StartEvent);
            }
            return;
        }

        if( this.lastEvent == this.HasNoTradeTarget)
        {
            if (this.tradeHandlerData.IsLastParticipant)
            {
                this.AddNextEvent(this.EndTrade);
            }
            else
            {
                this.AddNextEvent(this.ChangeTradeOwner);
            }

            return;
        }

        if (this.lastEvent == this.SelectTradeTarget)
        {
            this.AddNextEvent(this.SuggestTradeOwnerTradeCondition);

            return;
        }

        if (this.lastEvent == this.SuggestTradeOwnerTradeCondition)
        {
            this.AddNextEvent(this.MakeTradeTargetDecisionOnTradeAgreement);

            return;
        }

        if (this.lastEvent == this.MakeTradeTargetDecisionOnTradeAgreement)
        {
            if (this.tradeHandlerData.IsTradeAgreed is true)
            {
                this.AddNextEvent(this.DoTrade);

                return;
            }
            else
            {
                if (this.tradeHandlerData.IsLastParticipant)
                {
                    this.AddNextEvent(this.EndTrade);
                }
                else
                {
                    this.AddNextEvent(this.ChangeTradeOwner);
                }
                
                return;
            }
        }

        if (this.lastEvent == this.DoTrade)
        {
            if (this.tradeHandlerData.IsLastParticipant)
            {
                this.AddNextEvent(this.EndTrade);
            }
            else
            {
                this.AddNextEvent(this.ChangeTradeOwner);
            }

            return;
        }

        if (this.lastEvent == this.ChangeTradeOwner)
        {
            if (this.tradeHandlerData.SelectableTargetNumbers.Count() != 0)
            {
                this.AddNextEvent(this.SelectTradeTarget);

            }
            else
            {
                this.AddNextEvent(this.HasNoTradeTarget);
            }

            return;
        }

        if (this.lastEvent == this.EndTrade)
        {
            this.AddNextEvent(this.events!
                            .HouseBuildEvent
                            .StartEvent);

            return;
        }
    }
}
