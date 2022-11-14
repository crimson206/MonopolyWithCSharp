public class AuctionEvent : Event
{

    private IAuctionHandlerFunction auctionHandler;
    private EventFlow eventFlow;
    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount => this.AreInGame.Where(isInGame => isInGame == true).Count();
    private int initialPrice;
    private IAuctionDecisionMaker auctionDecisionMaker;
    private BankHandler bankHandler;

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
        tileManager)
    {
        this.bankHandler = statusHandlers.BankHandler;
        this.tileManager = tileManager;
        this.auctionHandler = auctionHandler;
        this.eventFlow = statusHandlers.EventFlow;
        this.auctionDecisionMaker = decisionMakers.AuctionDecisionMaker;
    }

    private List<int> Balances => this.dataCenter.Bank.Balances;

    private List<bool> AreInGame => this.dataCenter.InGame.AreInGame;
    private IPropertyData? CurrentPropertyData => (IPropertyData)this.dataCenter.CurrentTileData;
    private Property CurrentProperty => this.GetCurrentProperty();

    public string RecommendedString { get; private set; } = string.Empty;

    public override void StartEvent()
    {
        this.eventFlow.RecommendedString = string.Format("An auction for {0} starts", this.CurrentPropertyData!.Name);
        this.CallNextEvent();
    }

    private void SetUpAuction()
    {
        this.SetParticipantPlayerNumbers();
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
        this.tileManager.PropertyManager.ChangeOwner(this.CurrentProperty, winnerNumber);

        this.eventFlow.RecommendedString = string.Format("Player {0} bought {1}", winnerNumber, this.CurrentPropertyData!.Name);
        this.CallNextEvent();
    }

    private void SetParticipantPlayerNumbers()
    {
        this.participantPlayerNumbers.Clear();
        int playerNumber = this.CurrentPlayerNumber;

        for (int i = 0; i < this.AreInGame.Count(); i++)
        {
            if (AreInGame[playerNumber] is true)
            {
                this.participantPlayerNumbers.Add(playerNumber);
            }
            playerNumber = (playerNumber + 1) % this.AreInGame.Count();
        }

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

    protected override void CallNextEvent()
    {
        if (this.lastEvent == this.StartEvent)
        {
            this.AddNextEvent(this.DecideInitialPrice);

            return;
        }

        if (this.lastEvent == this.DecideInitialPrice)
        {
            this.AddNextEvent(this.SetUpAuction);
            return;
        }

        if (this.lastEvent == this.SetUpAuction)
        {
            this.AddNextEvent(this.SuggestPriceInTurn);
            return;
        }

        if (this.lastEvent == this.SuggestPriceInTurn)
        {
            if (this.dataCenter.AuctionHandler.IsAuctionOn)
            {
                this.AddNextEvent(this.SuggestPriceInTurn);

                return;
            }
            else
            {
                this.AddNextEvent(this.BuyWinnerProperty);
                return;
            }
        }

        if (this.lastEvent == this.BuyWinnerProperty)
        {
            this.events!.MainEvent.AddNextEvent(this.events!.MainEvent.CheckExtraTurn);
            return;
        }
    }

    private Property GetCurrentProperty()
    {
        int currentPosition = this.dataCenter.Board.PlayerPositions[this.CurrentPlayerNumber];
        return (Property)this.tileManager.Tiles[currentPosition];
    }
}
