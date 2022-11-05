public class AuctionEvent : IResponseToSwitchEvent
{
    private IDelegator delegator;
    private AuctionHandler auctionHandler = new AuctionHandler();
    private IAuctionHandlerFunction auctionHandlerFunction => this.auctionHandler;
    private IAuctionHandlerData auctionHandlerData => this.auctionHandler;
    private DataCenter dataCenter;
    private EventFlow eventFlow;

    private int auctionRoundCount = 0;

    private List<int> balances => dataCenter.Bank.Balances;
    private List<bool> areInGame => dataCenter.InGame.AreInGame;
    private int currentPlayerNumber => dataCenter.EventFlow.CurrentPlayerNumber;
    private PropertyData currentPropertyData => this.GetCurrentPropertyData();
    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount = 0;
    private IAuctionDecisionMaker auctionDecisionMaker = new AuctionDecisionMaker();
    private int initialPrice;
    private int currentRoundPlayerNumber => this.participantPlayerNumbers[auctionRoundCount%participantCount];
    private string stringCurrentRoundPlayer => string.Format("Player{0}", this.currentRoundPlayerNumber);
    private Action? lastEvent;

    public AuctionEvent
    (
        DataCenter dataCenter,
        EventFlow eventFlow,
        IDelegator delegator
    )
    {
        this.dataCenter = dataCenter;
        this.delegator = delegator;
        this.eventFlow = eventFlow;
    }

    public IAuctionHandlerData AuctionHandlerData => this.auctionHandlerData;

    public string RecommendedString { get; private set; } = string.Empty;

    private void SwitchEvent(EventType fromEvent, EventType toEvent)
    {
        this.delegator.SwitchEvent(fromEvent, toEvent);
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
        this.eventFlow.RecommendedString = string.Format("An auction for {0} starts", this.currentPropertyData.Name);
        this.CallNextEvent();
    }

    private void SetUpAuction()
    {
        this.participantCount = this.areInGame.Where(isInGame => isInGame == true).Count();
        this.auctionHandlerFunction.SetAuctionCondition(this.participantPlayerNumbers, initialPrice);
        this.SetParticipantPlayerNumbers();
        
        this.eventFlow.RecommendedString = this.CreateParticipantsString() + " joined in the auction";

        this.CallNextEvent();
    }

    private void DecideInitialPrice()
    {
        int currentPlayersBalance = this.balances[currentPlayerNumber];
        int currentPropertysPrice = this.currentPropertyData.Price;

        if (currentPlayersBalance < currentPropertysPrice)
        {
            this.initialPrice = currentPlayersBalance;
            this.eventFlow.RecommendedString = "The initial price is the balance of the initiator";
        }
        else
        {
            this.initialPrice = currentPropertysPrice;
            this.eventFlow.RecommendedString = string.Format("The initial price is {0} ", this.currentPropertyData.Price);
        }

        this.CallNextEvent();
    }

    private void SuggestPriceInTurn()
    {
        int participantNumber = this.auctionHandlerData.NextParticipantNumber;
        int suggestedPrice = this.auctionDecisionMaker.SuggestPrice(participantNumber);
        this.auctionHandler.SuggestNewPriceInTurn(suggestedPrice);

        this.eventFlow.RecommendedString = string.Format("Player {0} suggested {1}", participantNumber, suggestedPrice);

        this.CallNextEvent();
    }

    private void EndAuction()
    {
        this.eventFlow.RecommendedString = string.Format("Player {0} won the auction at {1}", 
                                            this.auctionHandlerData.WinnerNumber, this.auctionHandlerData.FinalPrice);
    }

    private void SetParticipantPlayerNumbers()
    {
        int index = 0;
        foreach (var participant in this.areInGame)
        {
            if(areInGame[index])
            {
                this.participantPlayerNumbers.Add(index);
            }
            index++;
        }
    }

    private PropertyData GetCurrentPropertyData()
    {
        int positionOfCurrentPlayer = this.dataCenter.Board.PlayerPositions[this.currentRoundPlayerNumber];
        PropertyData currentPropertyData = (PropertyData)this.dataCenter.TileDatas[positionOfCurrentPlayer];

        return currentPropertyData;
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
        Dictionary<int, int> suggestedPrices = this.auctionHandlerData.SuggestedPrices;
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
            if (this.auctionHandlerData.IsAuctionOn)
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
}
