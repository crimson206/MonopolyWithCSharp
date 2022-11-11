public class AuctionEvent
{
    private Delegator delegator;
    private IAuctionHandlerFunction auctionHandler;
    private EventFlow eventFlow;
    private Events? events;
    private IDataCenter dataCenter;

    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount => this.AreInGame.Where(isInGame => isInGame == true).Count();
    private int initialPrice;
    private Action lastEvent;
    private IAuctionDecisionMaker auctionDecisionMaker;
    private BankHandler bankHandler;
    private ITileManager tileManager;

    public AuctionEvent
    (
        StatusHandlers statusHandlers,
        ITileManager tileManager,
        IDataCenter dataCenter,
        AuctionHandler auctionHandler,
        Delegator delegator,
        DecisionMakers decisionMakers
    )
    {
        this.bankHandler = statusHandlers.BankHandler;
        this.tileManager = tileManager;
        this.auctionHandler = auctionHandler;
        this.delegator = delegator;
        this.eventFlow = statusHandlers.EventFlow;
        this.dataCenter = dataCenter;
        this.auctionDecisionMaker = decisionMakers.AuctionDecisionMaker;
        this.lastEvent = this.StartAuction;
    }

    private List<int> Balances => this.dataCenter.Bank.Balances;

    private List<bool> AreInGame => this.dataCenter.InGame.AreInGame;

    private int CurrentPlayerNumber => this.dataCenter.EventFlow.CurrentPlayerNumber;
    private IPropertyData? CurrentPropertyData => (IPropertyData)this.dataCenter.CurrentTileData;
    private Property CurrentProperty => this.GetCurrentProperty();

    public string RecommendedString { get; private set; } = string.Empty;

    public void SetEvents(Events events)
    {
        this.events = events;
    }

    public void StartAuction()
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

    private void EndAuction()
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
        Dictionary<int, int> suggestedPrices = this.dataCenter.AuctionHandler.SuggestedPrices;
        return this.ConvertIntListToString(suggestedPrices.Values.ToList());
    }

    private void CallNextEvent()
    {
        if (this.lastEvent == this.StartAuction)
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
                this.AddNextEvent(this.EndAuction);
                return;
            }
        }

        if (this.lastEvent == this.EndAuction)
        {
            this.events!.MainEvent.AddNextEvent(this.events!.MainEvent.CheckExtraTurn);
            return;
        }
    }

    public void AddNextEvent(Action nextEvent)
    {
        this.lastEvent = nextEvent;
        this.delegator.SetNextEvent(nextEvent);
    }

    public Property GetCurrentProperty()
    {
        int currentPosition = this.dataCenter.Board.PlayerPositions[this.CurrentPlayerNumber];
        return (Property)this.tileManager.Tiles[currentPosition];
    }
}
