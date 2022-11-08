public class AuctionEvent
{
    private Delegator delegator;
    private IAuctionHandlerFunction auctionHandler;
    private EventFlow eventFlow;
    private Events? events;
    private DataCenter dataCenter;

    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount => this.AreInGame.Where(isInGame => isInGame == true).Count();
    private int initialPrice;
    private Action lastEvent;
    private AuctionDecisionMaker auctionDecisionMaker;

    public AuctionEvent
    (
        DataCenter dataCenter,
        AuctionHandler auctionHandler,
        EventFlow eventFlow,
        Delegator delegator,
        AuctionDecisionMaker auctionDecisionMaker
    )
    {
        this.auctionHandler = auctionHandler;
        this.delegator = delegator;
        this.eventFlow = eventFlow;
        this.dataCenter = dataCenter;
        this.auctionDecisionMaker = auctionDecisionMaker;
        this.lastEvent = this.StartAuction;
    }

    public List<int> Balances => this.dataCenter.Bank.Balances;

    public List<bool> AreInGame => this.dataCenter.InGame.AreInGame;

    public int CurrentPlayerNumber => this.dataCenter.EventFlow.CurrentPlayerNumber;
    public PropertyData? CurrentPropertyData => (PropertyData)this.dataCenter.CurrentTileData;

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
        int? winnerNumber = this.dataCenter.AuctionHandler.WinnerNumber;
        int? finalPrice = this.dataCenter.AuctionHandler.FinalPrice;

        this.eventFlow.RecommendedString = string.Format("Player {0} won the auction at {1}", 
                                            winnerNumber, finalPrice);
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
            }
        }

        if (this.lastEvent == this.EndAuction)
        {
            this.events!.MainEvent.AddNextEvent(this.events!.MainEvent.CheckExtraTurn);
        }
    }

    public void AddNextEvent(Action nextEvent)
    {
        this.lastEvent = nextEvent;
        this.delegator.SetNextEvent(nextEvent);
    }
}
