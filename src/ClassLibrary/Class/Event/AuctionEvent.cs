public class AuctionEvent : Event
{

    private IAuctionHandler auctionHandler;
    private List<int> participantNumbers = new List<int>();
    private IAuctionDecisionMaker auctionDecisionMaker;
    private IBankHandler bankHandler;

    public AuctionEvent
    (StatusHandlers statusHandlers,
    ITileManager tileManager,
    IDataCenter dataCenter,
    IAuctionHandler auctionHandler,
    Delegator delegator,
    IDecisionMakers decisionMakers)
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

    public List<int> ParticipantNumbers { get => this.participantNumbers; private set => this.participantNumbers = value; }
    private List<int> Balances => this.dataCenter.Bank.Balances;
    public bool FirstParticipantIsForcedToBid { get; private set; }

    public IPropertyData? PropertyToAuction { get; private set; }

    public int InitialPrice { get; private set; }

    public override void StartEvent()
    {
        this.eventFlow.RecommendedString = string.Format("An auction for {0} starts", this.PropertyToAuction!.Name);
        this.CallNextEvent();
    }

    public void SetUpAuction(List<int> participantNumbers, int initialPrice, IPropertyData propertyToAuction, bool firstParticipantIsForcedToBid, IEvent auctionCaller)
    {
        this.participantNumbers = participantNumbers;
        this.InitialPrice = initialPrice;
        this.PropertyToAuction = propertyToAuction;
        this.FirstParticipantIsForcedToBid = firstParticipantIsForcedToBid;
        this.LastEvent = auctionCaller;
    }

    private void SetUpAuctionHandler()
    {
        this.auctionHandler.SetAuctionCondition(this.participantNumbers, this.InitialPrice, this.PropertyToAuction!, this.FirstParticipantIsForcedToBid);

        this.eventFlow.RecommendedString = this.CreateParticipantsString() + " joined in the auction";

        this.CallNextEvent();
    }

    private void DecideInitialPrice()
    {

        if (this.FirstParticipantIsForcedToBid)
        {
            this.eventFlow.RecommendedString = string.Format("Player{0} was forced to bid {1}$", this.participantNumbers[0], this.InitialPrice);
        }
        else
        {
            this.eventFlow.RecommendedString = string.Format("The initial price is {0}$", this.InitialPrice);
        }

        this.CallNextEvent();
    }

    private void BidInTurn()
    {
        int biddedPrice = this.auctionDecisionMaker.Bid();
        int CurrentParticipantNumber = this.dataCenter.AuctionHandler.NextParticipantNumber;
        if (biddedPrice > this.Balances[CurrentParticipantNumber])
        {
            throw new Exception();
        }        

        this.auctionHandler.BidNewPriceInTurn(biddedPrice);

        this.eventFlow.RecommendedString = string.Format("Player{0} bidded {1}$", CurrentParticipantNumber, biddedPrice);

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
            int moneyToReceive = finalPrice;
            if (this.PropertyToAuction.IsMortgaged)
            {
                moneyToReceive = (this.PropertyToAuction.Mortgage >= moneyToReceive? 0 : moneyToReceive - this.PropertyToAuction.Mortgage);
                this.tileManager.PropertyManager.SetIsMortgaged(this.PropertyToAuction, false);
                
            }
            this.bankHandler.IncreaseBalance(previousOwner, moneyToReceive);
        }

        this.tileManager.PropertyManager.ChangeOwner(this.PropertyToAuction, winnerNumber);

        this.eventFlow.RecommendedString = string.Format("Player{0} bought {1}", winnerNumber, this.PropertyToAuction.Name);
        this.CallNextEvent();
    }

    private void EndAuctionWithoutAnyBid()
    {
        this.eventFlow.RecommendedString = "No one bidded";

        this.CallNextEvent();
    }

    private string CreateParticipantsString()
    {
        string players = "Player ";
        int count = this.participantNumbers.Count();
        for (int i = 0; i < count; i++)
        {
            players += this.participantNumbers[i];
            if (i != count -1)
            {
                players += ", ";
            }
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
        this.eventFlow.RecommendedString = "This auction event is over";

        this.CallNextEvent();
    }

    protected override void CallNextEvent()
    {
        if (this.lastAction == this.StartEvent)
        {
            this.AddNextAction(this.SetUpAuctionHandler);

            return;
        }

        if (this.lastAction == this.SetUpAuctionHandler)
        {
            this.AddNextAction(this.DecideInitialPrice);
            return;
        }

        if (this.lastAction == this.DecideInitialPrice)
        {
            if(this.auctionHandler.IsAuctionOn)
            {
                this.AddNextAction(this.BidInTurn);
            }
            else
            {
                this.AddNextAction(this.BuyWinnerProperty);
            }

            return;
        }

        if (this.lastAction == this.BidInTurn)
        {
            if (this.dataCenter.AuctionHandler.IsAuctionOn)
            {
                this.AddNextAction(this.BidInTurn);
            }
            else
            {
                if (this.dataCenter.AuctionHandler.WinnerNumber is null)
                {
                    this.AddNextAction(this.EndAuctionWithoutAnyBid);
                }
                else
                {
                    this.AddNextAction(this.BuyWinnerProperty);
                }
            }

            return;
        }

        if (this.lastAction == this.EndAuctionWithoutAnyBid)
        {
            this.AddNextAction(this.EndEvent);

            return;
        }

        if (this.lastAction == this.BuyWinnerProperty)
        {
            this.AddNextAction(this.EndEvent);

            return;
        }

        if (this.lastAction == this.EndEvent)
        {
            if (this.LastEvent == this.events!.MainEvent)
            {
                this.events!.MainEvent.AddNextAction(this.events!.MainEvent.CheckExtraTurn);
            }
            else if (this.LastEvent == this.events.SellItemEvent)
            {
                this.events!.SellItemEvent.AddNextAction(this.events!.SellItemEvent.RenewSellItemEventFromAuction);
            }

            return;
        }
    }
}
