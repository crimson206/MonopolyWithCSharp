public class AuctionEvent
{
    private Delegator delegator;
    private IAuctionHandlerFunction auctionHandler;
    private EventFlow eventFlow;
    private Events? events;
    private DataCenter dataCenter;
    private int auctionRoundCount = 0;

    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount = 0;
    private int initialPrice;
    private int currentRoundPlayerNumber => this.participantPlayerNumbers[auctionRoundCount%participantCount];
    private string stringCurrentRoundPlayer => string.Format("Player{0}", this.currentRoundPlayerNumber);
    private Action? lastEvent;
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
        this.participantCount = this.AreInGame.Where(isInGame => isInGame == true).Count();
        this.auctionHandler.SetAuctionCondition(this.participantPlayerNumbers, initialPrice);
        this.SetParticipantPlayerNumbers();
        
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
    }

    private void SetParticipantPlayerNumbers()
    {
        int index = 0;
        foreach (var participant in this.AreInGame)
        {
            if(AreInGame[index])
            {
                this.participantPlayerNumbers.Add(index);
            }
            index++;
        }
    }

    private string CreateParticipantsString()
    {
        string players = "Player ";
        
        foreach (var item in this.participantPlayerNumbers)
        {
            players += item.ToString() + ", ";
        }

        players.Remove(-2, 2);

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
            this.delegator.SetNextEvent(this.DecideInitialPrice);

            return;
        }

        if (this.lastEvent == this.DecideInitialPrice)
        {
            this.delegator.SetNextEvent(this.SetUpAuction);

            return;
        }

        if (this.lastEvent == this.SuggestPriceInTurn)
        {
            if (this.dataCenter.AuctionHandler.IsAuctionOn)
            {
                this.delegator.SetNextEvent(this.SuggestPriceInTurn);

                return;
            }
            else
            {
                this.delegator.SetNextEvent(this.EndAuction);
            }
        }

        if (this.lastEvent == this.EndAuction)
        {
            this.delegator.SetNextEvent(this.events!.MainEvent.CheckExtraTurn);
        }
    }

    private void AddNextEvent(Action nextEvent)
    {
        this.lastEvent = nextEvent;
        this.delegator.SetNextEvent(nextEvent);
    }

}
