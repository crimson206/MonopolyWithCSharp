public class SellItemEvent : Event
{
    private IBankHandler bankHandler;
    private ISellItemHandler sellItemHandler;
    private ISellItemHandlerData sellItemHandlerData;
    private int playerToSellItmes;
    private ISellItemDecisionMaker sellItemDecisionMaker;
    private IEvent? nextEvent;

    public SellItemEvent
    (Delegator delegator,
    IDataCenter dataCenter,
    IStatusHandlers statusHandlers,
    ITileManager tileManager,
    IEconomyHandlers economyHandlers,
    IDecisionMakers decisionMakers)
        :base
        (delegator,
        dataCenter,
        tileManager,
        statusHandlers)
    {
        this.eventFlow = statusHandlers.EventFlow;
        this.bankHandler = statusHandlers.BankHandler;
        this.sellItemHandler = economyHandlers.SellItemHandler;
        this.sellItemHandlerData = dataCenter.SellItemHandler;
        this.sellItemDecisionMaker = decisionMakers.SellItemDecisionMaker;
        this.propertyManager = this.tileManager.PropertyManager;
    }

    public int PlayerToSellItmes { get => this.playerToSellItmes; set => this.playerToSellItmes = value; }

    private List<bool> AreInGame => this.dataCenter.InGame.AreInGame;
    public override void StartEvent()
    {
        int playerNumber = this.PlayerToSellItmes;
        List<IPropertyData> propertyDatasOnwedByThePlayer = this.tileManager.GetPropertyDatasWithOwnerNumber(playerNumber);

        this.sellItemHandler.SetPlayerToSellItems(playerNumber, propertyDatasOnwedByThePlayer);

        this.eventFlow.RecommendedString = string.Format("Player{0} needs to sell items", this.playerToSellItmes);

        this.CallNextEvent();
    }

    private void MakeDecisionOnItemToSell()
    {
        Dictionary<SellingType, int> itemToSell = this.sellItemDecisionMaker.MakeDecisionOnItemToSell();
        string method;

        switch (itemToSell.Keys.ElementAt(0))
        {
            case SellingType.MortgageProperty:
                int indexOfPropertyToMortgage = itemToSell.Values.ElementAt(0);
                this.sellItemHandler.SetPropertyToMortgage(indexOfPropertyToMortgage);
                method = "moartgage";
                break;

            case SellingType.DistructHouse:
                int indexOfRealEstateToDistructHouse = itemToSell.Values.ElementAt(0);
                this.sellItemHandler.SetRealEstateToBuildHouse(indexOfRealEstateToDistructHouse);
                method = "distruct a house of";
                break;

            case SellingType.AuctionProperty:
                int indexOfPropertyToAuction = itemToSell.Values.ElementAt(0);
                this.sellItemHandler.SetPropertyToAuction(indexOfPropertyToAuction);
                method = "auction";
                break;

            default:
                throw new Exception();
        }
        this.eventFlow.RecommendedString = string.Format("Player {0}decided to {1} a property",
                                                this.playerToSellItmes,
                                                method);

        this.CallNextEvent();
    }

    private void MortgageProperty()
    {
        IPropertyData propertyToMortgage = this.sellItemHandlerData.PropertyToMortgage!;

        int balanceToIncrease = propertyToMortgage.Mortgage;

        this.propertyManager.SetIsMortgaged(propertyToMortgage, true);
        this.bankHandler.IncreaseBalance(this.playerToSellItmes, balanceToIncrease);

        this.eventFlow.RecommendedString = string.Format("Player{0} mortgated {1}",
                                                this.playerToSellItmes,
                                                propertyToMortgage.Name);

        this.CallNextEvent();
    }

    private void DistructHouse()
    {
            RealEstate realEstateToDistructHouse = (RealEstate) this.sellItemHandlerData.RealEstateToDistructHouse!;

            int balanceToIncrease = realEstateToDistructHouse.BuildingCost;

            this.propertyManager.DistructHouse(realEstateToDistructHouse);
            this.bankHandler.IncreaseBalance(this.playerToSellItmes, balanceToIncrease);

            this.eventFlow.RecommendedString = string.Format("Player{0} mortgated {1}",
                                                    this.playerToSellItmes,
                                                    realEstateToDistructHouse.Name);
    }

    private void PassAuctionCondition()
    {
        List<int> auctionParticipantNumbers = this.CreateAuctionParticipantPlayerNumbers();

        this.events!.AuctionEvent.ParticipantNumbers = auctionParticipantNumbers;
        this.nextEvent = this.events.AuctionEvent;
    }

    public void CheckBalanceIsStillNegative()
    {
        if (this.bankHandler.Balances[this.playerToSellItmes] < 0 )
        {
            this.eventFlow.RecommendedString = string.Format("Player{0} needs to pay more money back", this.playerToSellItmes);
        }

        this.CallNextEvent();
    }

    public override void EndEvent()
    {
        if (this.nextEvent == this.events!.AuctionEvent)
        {
            this.events.AuctionEvent.LastEvent = this;
            this.events.AuctionEvent.AddNextAction(this.events.AuctionEvent.StartEvent);
        }
        else if (this.nextEvent == this.events!.MainEvent)
        {
            this.events.MainEvent.LastEvent = this;
            this.events.MainEvent.AddNextAction(this.events.MainEvent.EndEvent);
        }
    }


    protected override void CallNextEvent()
    {
        if (this.lastAction == this.StartEvent)
        {
            this.AddNextAction(this.MakeDecisionOnItemToSell);
            return;
        }

        if (this.lastAction == this.MakeDecisionOnItemToSell)
        {
            SellingType? sellingType = this.sellItemHandlerData.SellingOption;
            switch (sellingType)
            {
                case SellingType.MortgageProperty:
                    this.AddNextAction(this.MortgageProperty);
                    break;
                case SellingType.DistructHouse:
                    this.AddNextAction(this.DistructHouse);
                    break;
                case SellingType.AuctionProperty:
                    this.AddNextAction(this.PassAuctionCondition);
                    break;
                default:
                    this.nextEvent = this.events!.MainEvent;
                    this.AddNextAction(this.EndEvent);
                    break;
            }

            return;
        }

        if (this.lastAction == this.MortgageProperty
            || this.lastAction == this.DistructHouse)
        {
            this.AddNextAction(this.CheckBalanceIsStillNegative);

            return;
        }
    }


    private List<int> CreateAuctionParticipantPlayerNumbers()
    {
        List<int> participantNumbers = new List<int>();
        int playerNumber = this.CurrentPlayerNumber;

        for (int i = 0; i < this.AreInGame.Count(); i++)
        {
            if (AreInGame[playerNumber] is true)
            {
                participantNumbers.Add(playerNumber);
            }
            playerNumber = (playerNumber + 1) % this.AreInGame.Count();
        }

        participantNumbers.Remove(this.playerToSellItmes);

        return participantNumbers;
    }
}