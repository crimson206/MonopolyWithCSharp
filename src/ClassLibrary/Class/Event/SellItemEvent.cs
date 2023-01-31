public class SellItemEvent : Event
{
    private IBankHandler bankHandler;
    private ISellItemHandler sellItemHandler;
    private ISellItemHandlerData sellItemHandlerData;
    private int playerToSellItems;
    private ISellItemDecisionMaker sellItemDecisionMaker;
    private IEvent? nextEvent;
    private List<int> auctionParticipants = new List<int>();
    private InGameHandler inGameHandler;

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
        this.inGameHandler = statusHandlers.InGameHandler;
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

        this.CallNextAction();
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
                                                    decisionString.ToLower());
        }

        this.CallNextAction();
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

        this.CallNextAction();
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

            this.CallNextAction();
    }

    public void RenewSellItemEventFromAuction()
    {
        this.CallNextAction();
    }


    private void PassAuctionCondition()
    {
        IPropertyData propertyToAuction = this.sellItemHandler.PropertyToAuction!;
        int price = propertyToAuction.Price;
        this.auctionParticipants = this.CreateAuctionParticipantPlayerNumbers();
        List<int> balancesOfAuctionParticipants = this.CreateBalancesOfAuctionParticipants();
        int minBalance = balancesOfAuctionParticipants.Min();

        int initialPrice = (price < minBalance? price : minBalance);

        this.events!.AuctionEvent.SetUpAuction(this.auctionParticipants, initialPrice, propertyToAuction, true, this);

        this.eventFlow.RecommendedString = string.Format("Player{0} wants to auction {1}", this.playerToSellItems, propertyToAuction.Name);

        this.CallNextAction();
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

        this.CallNextAction();
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

        this.CallNextAction();
    }


    protected override void CallNextAction()
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

            this.AddNextAction(this.CallAuctionEvent);

            return;
        }

        if (this.lastAction == this.MortgageProperty
            || this.lastAction == this.DistructHouse
            || this.lastAction == this.CallAuctionEvent)
        {
            this.AddNextAction(this.CheckBalanceIsStillNegative);

            return;
        }

        if (this.lastAction == this.RenewSellItemEventFromAuction)
        {

            this.AddNextAction(this.CheckBalanceIsStillNegative);

            return;
        }

        if (this.lastAction == this.EndEvent)
        {
            this.events!.MainEvent.AddNextAction(this.events.MainEvent.EndEvent);

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

    private List<int> CreateAuctionParticipantPlayerNumbers()
    {
        List<int> participantNumbers = new List<int>();
        int playerNumber = this.eventFlow.CurrentPlayerNumber;

        for (int i = 0; i < this.AreInGame.Count(); i++)
        {
            if (AreInGame[playerNumber] is true
            && playerNumber != this.playerToSellItems)
            {
                participantNumbers.Add(playerNumber);
            }
            playerNumber = (playerNumber + 1) % this.AreInGame.Count();
        }

        return participantNumbers;
    }

    private void SetOwnerNumberOfPropertiesNull(List<IProperty> properties)
    {
        foreach (var property in properties)
        {
            this.propertyManager.ChangeOwner(property, null);
        }
    }

    private void SetIsMortgagedOfPropertiesFalse(List<IProperty> properties)
    {
        foreach (var property in properties)
        {
            this.propertyManager.SetIsMortgaged(property, false);
        }
    }

    private void DistructAllHouses(List<IRealEstate> realEstates)
    {
        do
        {
            foreach (var realEstate in realEstates)
            {
                if (realEstate.IsHouseDistructable)
                {
                    this.propertyManager.DistructHouse(realEstate);
                }
            }
        } while (realEstates.Where(realEstate => realEstate.IsHouseDistructable).Count() != 0);
    }

    private void ResetPropertiesOfPlayerNumber(int playerNumber)
    {
        List<IProperty> properties = this.tileManager.Tiles.Where(tile => tile is IProperty).Cast<IProperty>().ToList();
        List<IProperty> propertiesOfPlayer = properties.Where(property => property.OwnerPlayerNumber == playerNumber).ToList();
        List<IRealEstate> realEstatesOfPlayer = propertiesOfPlayer.Where(property => property is IRealEstate).Cast<IRealEstate>().ToList();

        this.SetIsMortgagedOfPropertiesFalse(propertiesOfPlayer);
        this.DistructAllHouses(realEstatesOfPlayer);
        this.SetOwnerNumberOfPropertiesNull(propertiesOfPlayer);
    }

    private List<int> CreateBalancesOfAuctionParticipants()
    {
        List<int> balancesOfAuctionParticipants = new List<int>();

        foreach (var participant in this.auctionParticipants)
        {
            balancesOfAuctionParticipants.Add(this.dataCenter.Bank.Balances[participant]);
        }

        return balancesOfAuctionParticipants;
    }

}
