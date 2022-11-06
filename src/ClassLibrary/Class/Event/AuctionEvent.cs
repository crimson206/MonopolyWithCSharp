public class AuctionEvent : IResponseToSwitchEvent, ISetAuctionEventData, IVisitor
{
    private IDelegator delegator;
    private IAuctionHandlerFunction auctionHandler;
    private EventFlow eventFlow;
    private List<IResponseToSwitchEvent> eventGroup = new List<IResponseToSwitchEvent>();
    private int auctionRoundCount = 0;

    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount = 0;
    private int initialPrice;
    private int currentRoundPlayerNumber => this.participantPlayerNumbers[auctionRoundCount%participantCount];
    private string stringCurrentRoundPlayer => string.Format("Player{0}", this.currentRoundPlayerNumber);
    private Action? lastEvent;

    public AuctionEvent
    (
        IAuctionHandlerFunction auctionHandler,
        EventFlow eventFlow,
        IDelegator delegator
    )
    {
        this.auctionHandler = auctionHandler;
        this.delegator = delegator;
        this.eventFlow = eventFlow;
    }

    public List<int> Balances { private get; set; } = new List<int>();

    public List<bool> AreInGame { private get; set; } = new List<bool>();

    public int CurrentPlayerNumber { private get; set; }
    public PropertyData? CurrentPropertyData { private get; set; }

    public int PlayerIntDecision { private get; set; }

    public IAuctionHandlerData? AuctionHandlerData { private get; set; }

    public string RecommendedString { get; private set; } = string.Empty;

    public void Visit(IElement element)
    {
        DataCenter dataCenter = (DataCenter)element;

    }

    private void SwitchEvent(EventType fromEvent, EventType toEvent)
    {
        foreach (var gameEvent in this.eventGroup)
        {
            gameEvent.ResponseToSwitchEvent(fromEvent, toEvent);
        }
    }

    public void ResponseToSwitchEvent(EventType fromEvent, EventType toEvent)
    {
        if (toEvent is EventType.AuctionEvent)
        {
            this.delegator.SetNextEvent(this.StartAuction);
        }
    }

    private void StartAuction()
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
        int participantNumber = this.AuctionHandlerData!.NextParticipantNumber;
        int suggestedPrice = this.PlayerIntDecision;
        this.auctionHandler.SuggestNewPriceInTurn(suggestedPrice);

        this.eventFlow.RecommendedString = string.Format("Player {0} suggested {1}", participantNumber, suggestedPrice);

        this.CallNextEvent();
    }

    private void EndAuction()
    {
        this.eventFlow.RecommendedString = string.Format("Player {0} won the auction at {1}", 
                                            this.AuctionHandlerData!.WinnerNumber, this.AuctionHandlerData.FinalPrice);
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
        Dictionary<int, int> suggestedPrices = this.AuctionHandlerData!.SuggestedPrices;
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
            if (this.AuctionHandlerData!.IsAuctionOn)
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
            this.SwitchEvent(EventType.AuctionEvent, EventType.MainEvent);
        }
    }

    private void AddNextEvent(Action nextEvent)
    {
        this.lastEvent = nextEvent;
        this.delegator.SetNextEvent(nextEvent);
    }

    public void UpdataData(IDataCenter dataCenter)
    {
        this.AuctionHandlerData = dataCenter.AuctionHandler;
        this.Balances = dataCenter.Bank.Balances;
        this.CurrentPlayerNumber = dataCenter.EventFlow.CurrentPlayerNumber;
        this.AreInGame = dataCenter.InGame.AreInGame;
        this.CurrentPropertyData = (PropertyData)dataCenter.CurrentTileData;
    }
}
