public class SellItemEvent : Event
{
    private IBankHandler bankHandler;
    private ISellItemHandler sellItemHandler;
    private ISellItemHandlerData sellItemHandlerData;
    private int playerToSellItems;
    private ISellItemDecisionMaker sellItemDecisionMaker;
    private IEvent? nextEvent;
    private List<int> auctionParticipants = new List<int>();

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

    public int PlayerToSellItmes { get => this.playerToSellItems; set => this.playerToSellItems = value; }

    private List<bool> AreInGame => this.dataCenter.InGame.AreInGame;
    public override void StartEvent()
    {
        int playerNumber = this.PlayerToSellItmes;
        List<IPropertyData> propertyDatasOnwedByThePlayer = this.tileManager.GetPropertyDatasWithOwnerNumber(playerNumber);

        this.sellItemHandler.SetPlayerToSellItems(playerNumber, propertyDatasOnwedByThePlayer);

        this.eventFlow.RecommendedString = string.Format("Player{0} needs to sell items", this.playerToSellItems);

        this.CallNextEvent();
    }

    private void MakeDecisionOnItemToSell()
    {
        Dictionary<SellingType, int> decisionOnItemToSell = this.sellItemDecisionMaker.MakeDecisionOnItemToSell();
        
        SellingType sellingType = decisionOnItemToSell.Keys.ElementAt(0);
        int indexOfItemToSell = decisionOnItemToSell.Values.ElementAt(0);

        this.sellItemHandler.SetSellingOption(decisionOnItemToSell);
        string[] decisionStrings = sellingType.ToString().Split("_");
        string decisionString = string.Empty;

        foreach (var _string in decisionStrings)
        {
            decisionString += _string + " ";
        }

        if (decisionOnItemToSell.Keys.ElementAt(0) is not SellingType.None)
        {
            this.eventFlow.RecommendedString = string.Format("Player{0} decided to {1}",
                                                    this.playerToSellItems,
                                                    decisionString);
        }

        this.CallNextEvent();
    }

    private void MortgageProperty()
    {
        IPropertyData propertyToMortgage = this.sellItemHandlerData.PropertyToMortgage!;

        int balanceToIncrease = propertyToMortgage.Mortgage;

        this.propertyManager.SetIsMortgaged(propertyToMortgage, true);
        this.bankHandler.IncreaseBalance(this.playerToSellItems, balanceToIncrease);

        this.eventFlow.RecommendedString = string.Format("Player{0} mortgated {1}",
                                                this.playerToSellItems,
                                                propertyToMortgage.Name);

        this.CallNextEvent();
    }

    private void DistructHouse()
    {
            RealEstate realEstateToDistructHouse = (RealEstate) this.sellItemHandlerData.RealEstateToDistructHouse!;

            int balanceToIncrease = realEstateToDistructHouse.BuildingCost / 2;

            this.propertyManager.DistructHouse(realEstateToDistructHouse);
            this.bankHandler.IncreaseBalance(this.playerToSellItems, balanceToIncrease);

            this.eventFlow.RecommendedString = string.Format("Player{0} distructed a house of {1}",
                                                    this.playerToSellItems,
                                                    realEstateToDistructHouse.Name);

            this.CallNextEvent();
    }

    private void SellPropertyToBankBecauseNoOneCanAuction()
    {
        int moneyToRecieve = 0;
        IPropertyData property = this.sellItemHandler.PropertyToAuction!;

        if (property.IsMortgaged)
        {
            moneyToRecieve = property.Price - property.Mortgage;
        }
        else
        {
            moneyToRecieve = property.Price;
        }

        this.bankHandler.IncreaseBalance(this.playerToSellItems, moneyToRecieve);
        this.propertyManager.SetIsMortgaged(property, false);
        this.propertyManager.ChangeOwner(property, null);

        this.eventFlow.RecommendedString = "The bank bought it because no one has enough money";

        this.CallNextEvent();
    }

    private void SellPropertyToTheOnlyPlayerWithEnoughMoney()
    {
        int moneyToRecieve = 0;
        IPropertyData property = this.sellItemHandler.PropertyToAuction!;

        if (property.IsMortgaged)
        {
            moneyToRecieve = property.Price - property.Mortgage;
        }
        else
        {
            moneyToRecieve = property.Price;
        }

        int playerToBuyProperty = 0;

        for (int i = 0; i < 4; i++)
        {
            if (this.bankHandler.Balances[i] >= moneyToRecieve)
            {
                playerToBuyProperty = i;
            }
        }

        this.bankHandler.TransferBalanceFromTo(playerToBuyProperty, this.playerToSellItems, moneyToRecieve);
        this.propertyManager.ChangeOwner(property, playerToBuyProperty);

        this.eventFlow.RecommendedString = "The only player with enough money bought it";

        this.CallNextEvent();
    }


    private void PassAuctionCondition()
    {

        int initialPrice = 0;
        IPropertyData propertyToAuction = this.sellItemHandler.PropertyToAuction!;
        int price = propertyToAuction.Price;
        int mortgage = propertyToAuction.Mortgage;

        if (propertyToAuction.IsMortgaged is false)
        {
            initialPrice = price;
        }
        else
        {
            initialPrice = price - mortgage;
        }

        this.auctionParticipants = this.CreateAuctionParticipantPlayerNumbers(initialPrice);

        this.events!.AuctionEvent.PropertyToAuction = propertyToAuction;
        this.events!.AuctionEvent.InitialPrice = initialPrice;
        this.events!.AuctionEvent.ParticipantNumbers = this.auctionParticipants;

        this.eventFlow.RecommendedString = string.Format("Player{0} wants to auction {1}", this.playerToSellItems, propertyToAuction);


        this.CallNextEvent();
    }

    private void CallAuctionEvent()
    {
        this.PassAuctionCondition();
        this.events!.AuctionEvent.LastEvent = this;
        this.events.AuctionEvent.AddNextAction(this.events.AuctionEvent.StartEvent);
    }

    public void CheckBalanceIsStillNegative()
    {
        if (this.bankHandler.Balances[this.playerToSellItems] < 0)
        {
            this.eventFlow.RecommendedString = string.Format("Player{0} needs to pay more money back", this.playerToSellItems);
        }

        this.CallNextEvent();
    }

    public override void EndEvent()
    {
        if (this.bankHandler.Balances[this.playerToSellItems] < 0)
        {
            this.eventFlow.RecommendedString = string.Format("Player{0} couldn't bail out", this.playerToSellItems);
        }
        else
        {
            this.eventFlow.RecommendedString = string.Format("Player{0} bailed out", this.playerToSellItems);
        }

        this.CallNextEvent();
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
            

            SellingType? sellingType = this.sellItemHandlerData.sellingType;
            switch (sellingType)
            {
                case SellingType.Mortgage_A_Property:
                    this.AddNextAction(this.MortgageProperty);
                    break;
                case SellingType.Distruct_A_House:
                    this.AddNextAction(this.DistructHouse);
                    break;
                case SellingType.Auction_A_Property:
                    this.AddNextAction(this.PassAuctionCondition);
                    break;
                default:
                    this.AddNextAction(this.EndEvent);
                    break;
            }

            return;
        }

        if (this.lastAction == this.PassAuctionCondition)
        {
            if (this.auctionParticipants.Count() >= 2)
            {
                this.AddNextAction(this.CallAuctionEvent);
            }
            else if (this.auctionParticipants.Count() == 1)
            {
                this.AddNextAction(this.SellPropertyToTheOnlyPlayerWithEnoughMoney);
            }
            else
            {
                this.AddNextAction(this.SellPropertyToBankBecauseNoOneCanAuction);
            }

            return;
        }

        if (this.lastAction == this.MortgageProperty
            || this.lastAction == this.DistructHouse
            || this.lastAction == this.CallAuctionEvent
            || this.lastAction == this.SellPropertyToBankBecauseNoOneCanAuction
            || this.lastAction == this.SellPropertyToTheOnlyPlayerWithEnoughMoney)
        {
            this.AddNextAction(this.CheckBalanceIsStillNegative);

            return;
        }

        if (this.lastAction == this.EndEvent)
        {
            this.events!.HouseBuildEvent.AddNextAction(this.events.HouseBuildEvent.StartEvent);

            return;
        }

        if (this.lastAction == this.CheckBalanceIsStillNegative)
        {
            if (this.bankHandler.Balances[this.playerToSellItems] < 0)
            {
                this.AddNextAction(this.MakeDecisionOnItemToSell);
            }
            else
            {
                this.AddNextAction(this.EndEvent);
            }

            return;
        }
    }


    private List<int> CreateAuctionParticipantPlayerNumbers(int initialPrice)
    {
        List<int> participantNumbers = new List<int>();
        int playerNumber = this.CurrentPlayerNumber;

        for (int i = 0; i < this.AreInGame.Count(); i++)
        {
            if (AreInGame[playerNumber] is true
            && this.bankHandler.Balances[playerNumber] >= initialPrice)
            {
                participantNumbers.Add(playerNumber);
            }
            playerNumber = (playerNumber + 1) % this.AreInGame.Count();
        }

        return participantNumbers;
    }
}
