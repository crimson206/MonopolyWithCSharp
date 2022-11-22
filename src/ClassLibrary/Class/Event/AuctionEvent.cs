public class AuctionEvent : Event
{

    private IAuctionHandlerFunction auctionHandler;
    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount => this.AreInGame.Where(isInGame => isInGame == true).Count();
    private int initialPrice;
    private IAuctionDecisionMaker auctionDecisionMaker;
    private IBankHandler bankHandler;

    public AuctionEvent
    (StatusHandlers statusHandlers,
    ITileManager tileManager,
    IDataCenter dataCenter,
    AuctionHandler auctionHandler,
    Delegator delegator,
    DecisionMakers decisionMakers)
        :base
        (delegator,
        dataCenter,
        tileManager,
        statusHandlers)
    {
        this.bankHandler = statusHandlers.BankHandler;
        this.tileManager = tileManager;
        this.auctionHandler = auctionHandler;
        this.eventFlow = statusHandlers.EventFlow;
        this.auctionDecisionMaker = decisionMakers.AuctionDecisionMaker;
    }

    public List<int> ParticipantNumbers { get => this.participantPlayerNumbers; set => this.participantPlayerNumbers = value; }
    private List<int> Balances => this.dataCenter.Bank.Balances;

    private List<bool> AreInGame => this.dataCenter.InGame.AreInGame;
    private IPropertyData? CurrentPropertyData => (IPropertyData)this.dataCenter.CurrentTileData;
    public Property? PropertyToAuction { get; set; }

    public string RecommendedString { get; private set; } = string.Empty;

    public override void StartEvent()
    {
        this.eventFlow.RecommendedString = string.Format("An auction for {0} starts", this.CurrentPropertyData!.Name);
        this.CallNextEvent();
    }

    private void SetUpAuction()
    {
        this.auctionHandler.SetAuctionCondition(this.participantPlayerNumbers, initialPrice);

        this.eventFlow.RecommendedString = this.CreateParticipantsString() + " joined in the auction";

        this.CallNextEvent();
    }

    private void DecideInitialPrice()
    {
        int currentPlayersBalance = this.Balances[CurrentPlayerNumber];
        int currentPropertysPrice = this.CurrentPropertyData!.Price;

        if (currentPlayersBalance < currentPropertysPrice)
        {
            this.initialPrice = currentPlayersBalance;
            this.eventFlow.RecommendedString = "The initial price is the balance of the initiator";
        }
        else
        {
            this.initialPrice = currentPropertysPrice;
            this.eventFlow.RecommendedString = string.Format("The initial price is {0} ", this.CurrentPropertyData.Price);
        }

        this.CallNextEvent();
    }

    private void SuggestPriceInTurn()
    {
        int participantNumber = this.dataCenter.AuctionHandler.NextParticipantNumber;
        int suggestedPrice = this.auctionDecisionMaker.SuggestPrice(participantNumber);
        this.auctionHandler.SuggestNewPriceInTurn(suggestedPrice);

        this.eventFlow.RecommendedString = string.Format("Player {0} suggested {1}", participantNumber, suggestedPrice);

        this.CallNextEvent();
    }

    private void BuyWinnerProperty()
    {
        int winnerNumber = (int)this.dataCenter.AuctionHandler.WinnerNumber!;
        int finalPrice = (int)this.dataCenter.AuctionHandler.FinalPrice!;
        this.bankHandler.DecreaseBalance(winnerNumber, finalPrice);
        if (this.PropertyToAuction!.OwnerPlayerNumber is not null)
        {
            int previousOwner = (int)this.PropertyToAuction.OwnerPlayerNumber;
            this.bankHandler.IncreaseBalance(previousOwner, finalPrice);
        }

        this.tileManager.PropertyManager.ChangeOwner(this.PropertyToAuction, winnerNumber);

        this.eventFlow.RecommendedString = string.Format("Player {0} bought {1}", winnerNumber, this.PropertyToAuction.Name);
        this.CallNextEvent();
    }

    private string CreateParticipantsString()
    {
        string players = "Player ";
        
        foreach (var item in this.participantPlayerNumbers)
        {
            players += item.ToString() + ", ";
        }
        return players;
    }

    private string CreateParticipantNumbersString()
    {
        Dictionary<int, int> suggestedPrices = this.dataCenter.AuctionHandler.SuggestedPrices;
        return this.ConvertIntListToString(suggestedPrices.Values.ToList());
    }

    public override void EndEvent()
    {
        if (this.LastEvent == this.events!.MainEvent)
        {
            this.events!.MainEvent.AddNextAction(this.events!.MainEvent.CheckExtraTurn);
        }
        else if (this.LastEvent == this.events.SellItemEvent)
        {
            this.events!.SellItemEvent.AddNextAction(this.events!.SellItemEvent.CheckBalanceIsStillNegative);
        }
    }

    protected override void CallNextEvent()
    {
        if (this.lastAction == this.StartEvent)
        {
            this.AddNextAction(this.DecideInitialPrice);

            return;
        }

        if (this.lastAction == this.DecideInitialPrice)
        {
            this.AddNextAction(this.SetUpAuction);
            return;
        }

        if (this.lastAction == this.SetUpAuction)
        {
            this.AddNextAction(this.SuggestPriceInTurn);
            return;
        }

        if (this.lastAction == this.SuggestPriceInTurn)
        {
            if (this.dataCenter.AuctionHandler.IsAuctionOn)
            {
                this.AddNextAction(this.SuggestPriceInTurn);

                return;
            }
            else
            {
                this.AddNextAction(this.BuyWinnerProperty);
                return;
            }
        }

        if (this.lastAction == this.BuyWinnerProperty)
        {
            this.AddNextAction(this.EndEvent);
            return;
        }
    }

    private Property GetCurrentProperty()
    {
        int currentPosition = this.dataCenter.Board.PlayerPositions[this.CurrentPlayerNumber];
        return (Property)this.tileManager.Tiles[currentPosition];
    }
}
