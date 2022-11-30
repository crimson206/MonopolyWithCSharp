public class TradeEvent : Event
{
    private IBankHandler bankHandler;
    private ITradeHandler tradeHandler;
    private List<int>? participantPlayerNumbers = new List<int>();
    private ITradeDecisionMaker tradeDecisionMaker;
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
            tileManager,
            statusHandlers)
    {
        this.bankHandler = statusHandlers.BankHandler;
        this.eventFlow = statusHandlers.EventFlow;
        this.tileManager = tileManager;
        this.tradeHandler = economyHandlers.TradeHandler;
        this.tradeHandlerData = dataCenter.TradeHandler;
        this.tradeDecisionMaker = decisionMakers.TradeDecisionMaker;
    }
    
    private List<bool> AreInGame => this.dataCenter.InGame.AreInGame;
    private int CurrentTradeOwner => (int)this.tradeHandlerData.CurrentTradeOwner!;
    private int CurrentTradeTarget => (int)this.tradeHandlerData.CurrentTradeTarget!;
    private int AdditionalMoneyFromTradeOwner => this.tradeHandlerData.MoneyOwnerWillingToAddOnTrade;
    private List<IProperty> Properties => this.tileManager.Properties;
    private List<int> Balances => this.dataCenter.Bank.Balances;
    public override void StartEvent()
    {
        this.SetParticipantPlayerNumbers();

        this.tradeHandler
            .SetTrade(this.participantPlayerNumbers!,
                    this.Properties);

        if (this.tradeHandlerData.IsThereTradableProperties)
        {
            this.eventFlow
                .RecommendedString =
                    string.Format(
                            "Player{0} can select a trade target",
                            this.CurrentTradeOwner);
        }

        this.CallNextEvent();
    }

    public void HasNoTradeTarget()
    {
        this.eventFlow.RecommendedString =
            string.Format("Player{0} has no selectable trade target",
                        this.CurrentTradeOwner);
        
        this.CallNextEvent();
    }

    public void SelectTradeTarget()
    {
        int? selectedTradeTarget = this.tradeDecisionMaker
                                        .SelectTradeTarget();

        if (selectedTradeTarget is not null)
        {
            this.tradeHandler.SetTradeTarget((int)selectedTradeTarget);

            this.eventFlow.RecommendedString =
                string.Format("Player{0} chose Player{1} as the trade target",
                            this.CurrentTradeOwner,
                            this.CurrentTradeTarget);
        }
        else
        {
            this.eventFlow.RecommendedString =
                string.Format("Player{0} skipped this trade turn", this.CurrentTradeOwner);
        }

        this.CallNextEvent();
    }

    private void SelectTradeOwnerPropertyToGet()
    {
        int? decision = this.tradeDecisionMaker.SelectPropertyToGet();

        if (decision is not null)
        {
            this.tradeHandler.SetPropertyTradeOwnerWantsFromTarget((int)decision);
            this.eventFlow.RecommendedString =
                string.Format("Player{0} wants {1}", this.tradeHandler.CurrentTradeOwner, this.tradeHandler.PropertyTradeOwnerToGet!.Name);

        }
        
        this.CallNextEvent();
    }

    private void SelectTradeOwnerPropertyToGive()
    {
        int? decision = this.tradeDecisionMaker.SelectPropertyToGive();

        if (decision is not null)
        {
            this.tradeHandler.SetPropertyTradeOwnerIsWillingToGive((int)decision);
            this.eventFlow.RecommendedString =
                string.Format("Player{0} is willing to give {1}", this.tradeHandler.CurrentTradeOwner, this.tradeHandler.PropertyTradeOwnerToGive!.Name);
        }
        
        this.CallNextEvent();
    }

    private void SetTradeOwnerAddicionalMoney()
    {
        int decision = this.tradeDecisionMaker.DecideAdditionalMoney();

        if (decision < 0)
        {
            if (- decision > this.Balances[this.CurrentTradeTarget])
            {
                throw new Exception();
            }

            this.tradeHandler.SetAdditionalMoneyTradeOwnerIsWillingToAdd(decision);
            this.eventFlow.RecommendedString =
                string.Format("Player{0} want to receive {1}$", this.tradeHandler.CurrentTradeOwner, - decision);

        }
        else if (decision > 0)
        {
            if (decision > this.Balances[this.CurrentTradeOwner])
            {
                throw new Exception();
            }

            this.tradeHandler.SetAdditionalMoneyTradeOwnerIsWillingToAdd(decision);
            this.eventFlow.RecommendedString =
                string.Format("Player{0} is willing to give {1}$", this.tradeHandler.CurrentTradeOwner, decision);
        }
        
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
                                this.CurrentTradeTarget);
        }
        else
        {
            this.eventFlow
                .RecommendedString =
                    string.Format("Player{0} disagreed with the condition",
                                this.CurrentTradeTarget);
        }

        this.CallNextEvent();
    }

    private void DoTrade()
    {
        if (this.tradeHandlerData.PropertyTradeOwnerToGet is not null)
        {
            IProperty propertyTradeOwnerToGet = (IProperty)
                                            this.tradeHandlerData
                                            .PropertyTradeOwnerToGet;

            this.propertyManager
                .ChangeOwner(propertyTradeOwnerToGet,
                            this.CurrentTradeOwner);
        }

        if (this.tradeHandlerData.PropertyTradeOwnerToGive is not null)
        {
            IProperty propertyTradeOwnerToGive = (IProperty)
                                            this.tradeHandlerData
                                            .PropertyTradeOwnerToGive;

            this.propertyManager
                .ChangeOwner(propertyTradeOwnerToGive,
                            this.CurrentTradeTarget);
        }

        int addtionalMoney = this.tradeHandlerData
                                .MoneyOwnerWillingToAddOnTrade;

        if (this.AdditionalMoneyFromTradeOwner >= 0)
        {
            this.bankHandler
                .TransferBalanceFromTo(this.CurrentTradeOwner,
                                    this.CurrentTradeTarget,
                                    this.AdditionalMoneyFromTradeOwner);
        }
        else
        {
            this.bankHandler
                .TransferBalanceFromTo(this.CurrentTradeTarget,
                                    this.CurrentTradeOwner,
                                    - this.AdditionalMoneyFromTradeOwner);
        }



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
                            this.CurrentTradeOwner);
        }

        this.CallNextEvent();
    }

    public override void EndEvent()
    {
        this.eventFlow
            .RecommendedString =
                "All player used their trade chances";

        this.CallNextEvent();
    }

    private void SetParticipantPlayerNumbers()
    {
        this.participantPlayerNumbers!.Clear();
        int playerNumber = this.eventFlow.CurrentPlayerNumber;

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
        int playersCount = this.participantPlayerNumbers!.Count();
        for (int i = 0; i < playersCount; i++)
        {
            players += i.ToString();

            if (i != playersCount-1)
            {
                players += ", ";
            }
        }

        return players;
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
        if (this.lastAction == this.StartEvent)
        {
            if (this.tradeHandlerData.IsThereTradableProperties)
            {
                if (this.tradeHandlerData.SelectableTargetNumbers.Count() == 0)
                {
                    this.AddNextAction(this.HasNoTradeTarget);
                }
                else
                {
                    this.AddNextAction(this.SelectTradeTarget);
                }
            }
            else
            {
                this.AddNextAction(this.EndEvent);
            }
            return;
        }

        if( this.lastAction == this.HasNoTradeTarget)
        {
            if (this.tradeHandlerData.IsLastParticipant)
            {
                this.AddNextAction(this.EndEvent);
            }
            else
            {
                this.AddNextAction(this.ChangeTradeOwner);
            }

            return;
        }

        if (this.lastAction == this.SelectTradeTarget)
        {
            if (this.tradeHandlerData.CurrentTradeTarget is null)
            {
                if (this.tradeHandlerData.IsLastParticipant)
                {
                    this.AddNextAction(this.EndEvent);
                }
                else
                {
                    this.AddNextAction(this.ChangeTradeOwner);
                }
            }
            else
            {
                this.AddNextAction(this.SelectTradeOwnerPropertyToGet);
            }

            return;
        }

        if (this.lastAction == this.SelectTradeOwnerPropertyToGet)
        {
            this.AddNextAction(this.SelectTradeOwnerPropertyToGive);

            return;
        }

        if (this.lastAction == this.SelectTradeOwnerPropertyToGive)
        {
            this.AddNextAction(this.SetTradeOwnerAddicionalMoney);

            return;
        }

        if (this.lastAction == this.SetTradeOwnerAddicionalMoney)
        {
            this.AddNextAction(this.MakeTradeTargetDecisionOnTradeAgreement);

            return;
        }

        if (this.lastAction == this.MakeTradeTargetDecisionOnTradeAgreement)
        {
            if (this.tradeHandlerData.IsTradeAgreed is true)
            {
                this.AddNextAction(this.DoTrade);
            }
            else
            {
                if (this.tradeHandlerData.IsLastParticipant)
                {
                    this.AddNextAction(this.EndEvent);
                }
                else
                {
                    this.AddNextAction(this.ChangeTradeOwner);
                }
            }

            return;
        }

        if (this.lastAction == this.DoTrade)
        {
            if (this.tradeHandlerData.IsLastParticipant)
            {
                this.AddNextAction(this.EndEvent);
            }
            else
            {
                this.AddNextAction(this.ChangeTradeOwner);
            }

            return;
        }

        if (this.lastAction == this.ChangeTradeOwner)
        {
            if (this.tradeHandlerData.SelectableTargetNumbers.Count() != 0)
            {
                this.AddNextAction(this.SelectTradeTarget);

            }
            else
            {
                this.AddNextAction(this.HasNoTradeTarget);
            }

            return;
        }

        if (this.lastAction == this.EndEvent)
        {
            this.events!.HouseBuildEvent.AddNextAction(this.events!
                            .HouseBuildEvent
                            .StartEvent);

            return;
        }
    }
}
