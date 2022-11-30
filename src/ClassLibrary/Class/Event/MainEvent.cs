public class MainEvent : Event, IMainEvent
{
    private IBankHandler bankHandler;
    private BoardHandler boardHandler;
    private InGameHandler inGameHandler;
    private IDice dice;
    private DoubleSideEffectHandler doubleSideEffectHandler;
    private JailHandler jailHandler;
    private Random random;
    private IDecisionMakers decisionMakers;
    private bool isDoubleSideEffectOn = true;
    private ITile currentTile => this.GetCurrentTile();
    private bool rolledDouble => this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];
    private Action? previousActionOfLastAction;
    private bool timeToTrade = false;
    private int idleCount;

    public MainEvent
    (StatusHandlers statusHandlers,
    ITileManager tileManager,
    IDecisionMakers decisionMakers,
    IDataCenter dataCenter,
    Delegator delegator,
    IDice dice,
    Random random)
        :base
        (delegator,
        dataCenter,
        tileManager,
        statusHandlers)
    {
        this.bankHandler = statusHandlers.BankHandler;
        this.boardHandler = statusHandlers.BoardHandler;
        this.jailHandler = statusHandlers.JailHandler;
        this.doubleSideEffectHandler = statusHandlers.DoubleSideEffectHandler;
        this.inGameHandler = statusHandlers.InGameHandler;
        this.eventFlow = statusHandlers.EventFlow;
        this.tileManager = tileManager;
        this.decisionMakers = decisionMakers;
        this.delegator = delegator;

        this.delegator.SetNextAction(this.StartEvent);
        this.lastAction = this.StartEvent;

        this.dice = dice;
        this.random = random;
    }

    private int CurrentPlayerNumber => this.eventFlow.CurrentPlayerNumber;

    private string stringPlayer => String.Format("Player{0}", this.CurrentPlayerNumber);

    protected override void CallNextEvent()
    {
        if (this.lastAction == this.StartEvent)
        {
            if (this.jailHandler.TurnsInJailCounts[this.CurrentPlayerNumber] > 0)
            {
                this.AddNextAction(this.MakeDecisionOnUsageOfJailFreeCard);
            }
            else
            {
                this.AddNextAction(this.RollDice);
            }
            return;
        }

        if (this.lastAction == this.MakeDecisionOnUsageOfJailFreeCard)
        {
            if (this.eventFlow.BoolDecision is true)
            {
                this.AddNextAction(this.UseJailFreeCard);
            }
            else
            {
                this.AddNextAction(this.MakeDecisionOnPaymentOfJailFine);
            }
            return;
        }

        if (this.lastAction == this.MakeDecisionOnPaymentOfJailFine)
        {
            if (this.eventFlow.BoolDecision is true)
            {
                this.AddNextAction(this.PayJailFine);
            }
            else
            {
                this.AddNextAction(this.RollDice);
            }
            return;
        }

        if (this.lastAction == this.EscapeJail)
        {
            this.AddNextAction(this.RollDice);
            return;
        }

        if (this.lastAction == this.RollDice)
        {

            if (this.previousActionOfLastAction == this.MakeDecisionOnPaymentOfJailFine || this.previousActionOfLastAction == this.MakeDecisionOnUsageOfJailFreeCard)
            {
                if (this.rolledDouble)
                {
                    this.AddNextAction(this.IsReleasedFromJail);
                }
                else
                {
                    if (this.jailHandler.TurnsInJailCounts[this.CurrentPlayerNumber] == 3)
                    {
                        this.AddNextAction(this.PayJailFine);
                    }
                    else
                    {
                        this.AddNextAction(this.StayInJail);
                    }
                }
            }
            else if (this.previousActionOfLastAction == this.CheckExtraTurn
                    || this.previousActionOfLastAction == this.StartEvent
                    || this.previousActionOfLastAction == this.EscapeJail)
            {
                if (this.isDoubleSideEffectOn && this.doubleSideEffectHandler.DoubleCounts[this.CurrentPlayerNumber] == 3)
                {
                    this.AddNextAction(this.HasJailPenalty);
                }
                else
                {
                    this.AddNextAction(this.MoveByRollDiceResultTotal);
                }
            }
            else if (this.previousActionOfLastAction == this.LandOnTile)
            {
                this.AddNextAction(this.PayRent);
            }

            return;
        }

        if (this.lastAction == this.IsReleasedFromJail)
        {
            this.AddNextAction(this.MoveByRollDiceResultTotal);

            return;
        }

        if (this.lastAction == this.MoveByRollDiceResultTotal)
        {
            if (this.boardHandler.PlayerPassedGo[this.CurrentPlayerNumber])
            {
                this.AddNextAction(this.ReceiveSalary);
            }
            else
            {
                this.AddNextAction(this.LandOnTile);
            }

            return;
        }

        if (this.lastAction == this.ReceiveSalary)
        {
            this.AddNextAction(this.LandOnTile);
            return;
        }

        if (this.lastAction == this.LandOnTile)
        {
            if (this.currentTile is Property)
            {
                Property currentProperty = (Property)this.currentTile;

                if (currentProperty.OwnerPlayerNumber == this.CurrentPlayerNumber)
                {
                    this.AddNextAction(this.CheckExtraTurn);
                }
                else if (currentProperty.OwnerPlayerNumber == null)
                {
                    bool canBuyProperty = this.bankHandler.Balances[this.CurrentPlayerNumber] >= currentProperty.Price;

                    if (canBuyProperty)
                    {
                        this.AddNextAction(this.MakeDecisionOnPurchaseOfProperty);
                    }
                    else
                    {
                        this.AddNextAction(this.CheckExtraTurn);
                    }
                }
                else
                {
                    if (currentProperty is not Utility)
                    {
                        this.AddNextAction(this.PayRent);
                    }
                    else
                    {
                        this.AddNextAction(this.RollDice);
                    }
                }
            }
            else if (this.currentTile is TaxTile)
            {
                this.AddNextAction(this.PayTax);
            }
            else if (this.currentTile is GoToJail)
            {
                this.AddNextAction(this.HasJailPenalty);
            }
            else
            {
                this.AddNextAction(this.CheckExtraTurn);
            }

            return;
        }

        if (this.lastAction == this.CheckExtraTurn)
        {
            if (this.doubleSideEffectHandler.ExtraTurns[this.CurrentPlayerNumber])
            {
                this.AddNextAction(this.RollDice);
            }
            else if (this.bankHandler.Balances[this.CurrentPlayerNumber] < 0)
            {
                this.events!.SellItemEvent.PlayerToSellItmes = this.CurrentPlayerNumber;
                this.events!.SellItemEvent.AddNextAction(this.events!.SellItemEvent.StartEvent);
            }
            else
            {
                this.AddNextAction(this.EndEvent);
            }

            return;
        }

        if (this.lastAction == this.StayInJail)
        {
            this.AddNextAction(this.EndEvent);

            return;
        }

        if (this.lastAction == this.PayJailFine)
        {
            if (this.isDoubleSideEffectOn)
            {
                this.AddNextAction(this.EscapeJail);
            }
            else
            {
                this.AddNextAction(this.IsReleasedFromJail);
            }

            return;
        }

        if (this.lastAction == this.MakeDecisionOnPurchaseOfProperty)
        {
            if (this.eventFlow.BoolDecision)
            {
                this.AddNextAction(this.PurchaseProperty);
            }
            else
            {
                this.AddNextAction(this.DontPurchaseProperty);
            }

            return;
        }
        if (this.lastAction == this.HasJailPenalty)
        {
            this.AddNextAction(this.EndEvent);

            return;
        }

        if (this.lastAction == this.PurchaseProperty)
        {
            this.AddNextAction(this.CheckExtraTurn);
            return;
        }

        if (this.lastAction == this.DontPurchaseProperty)
        {
            this.AddNextAction(this.PassAuctionCondition);
            return;
        }

        if (this.lastAction == this.PassAuctionCondition)
        {
            this.events!.AuctionEvent.AddNextAction(this.events!.AuctionEvent.StartEvent);
            return;
        }

        if (this.lastAction == this.EndEvent)
        {
            if (this.inGameHandler.AreInGame.Where(isInGame => isInGame == true).Count() == 1)
            {
                this.AddNextAction(this.GameIsOver);
            }
            else if (this.timeToTrade)
            {
                this.timeToTrade = false;
                this.events!.TradeEvent.AddNextAction(this.events.TradeEvent.StartEvent);
            }
            else
            {
                this.events!.HouseBuildEvent.AddNextAction(this.events.HouseBuildEvent.StartEvent);
            }
            return;
        }

        if (this.lastAction == this.PayTax)
        {
            this.AddNextAction(this.CheckExtraTurn);
            return;
        }

        if (this.lastAction == this.PayRent)
        {
            this.AddNextAction(this.CheckExtraTurn);
            return;
        }

        if (this.lastAction == this.GameIsOver)
        {
            this.AddNextAction(this.Idle);

            return;
        }

        if (this.lastAction == this.Idle)
        {
            this.AddNextAction(this.Idle);

            return;
        }

        throw new Exception("It seems that you didn't set a next event of an event. Check the last event and check if there is the next event setting for the last event in CallNextEvent()");
    }

    private void RollDice()
    {
        /// reset value before setting a new value
        this.doubleSideEffectHandler.SetExtraTurn(this.CurrentPlayerNumber, false);

        this.eventFlow.RollDiceResult = this.dice.Roll(random);

        if (this.isDoubleSideEffectOn)
        {
            this.UpdateNextDoubleSideEffect();
        }

        this.eventFlow.RecommendedString = this.stringPlayer + " rolled " + this.ConvertRollDiceResultToString();

        this.CallNextEvent();
    }

    private void PayJailFine()
    {
        this.bankHandler.DecreaseBalance(this.CurrentPlayerNumber, 60);
        this.jailHandler.ResetTurnInJail(this.CurrentPlayerNumber);
        this.eventFlow.RecommendedString = this.stringPlayer + " paid the jail fine";

        this.CallNextEvent();
    }

    private void PassAuctionCondition()
    {
        List<int> participantNumber = this.CreateAuctionParticipantPlayerNumbers();
        IPropertyData propertyToAuction = (IPropertyData)this.GetCurrentTile();
        int priceOfPropertyToAuction = propertyToAuction.Price;
        int initialPrice = 0;

        initialPrice = ComparePriceAndMinBalanceOfPlayerInGameAndReturnSmallerOne(priceOfPropertyToAuction);
        List<int> balancesOfPlayersInGame = new List<int>();

        this.events!.AuctionEvent.SetUpAuction(this.CreateAuctionParticipantPlayerNumbers(), initialPrice, (Property)this.GetCurrentTile(), true, this);

        this.CallNextEvent();
    }

    private void UseJailFreeCard()
    {
        this.jailHandler.RemoveAJailFreeCard(this.CurrentPlayerNumber);
        this.jailHandler.ResetTurnInJail(this.CurrentPlayerNumber);
        this.eventFlow.RecommendedString = this.stringPlayer + " used the jail fine";

        this.CallNextEvent();
    }

    public void EscapeJail()
    {
        this.jailHandler.ResetTurnInJail(this.CurrentPlayerNumber);
        this.eventFlow.RecommendedString = this.stringPlayer + " escaped the jail";
        this.CallNextEvent();
    }

    public override void StartEvent()
    {
        this.eventFlow.RecommendedString = this.stringPlayer + " start a turn";

        this.CallNextEvent();
    }

    public void MakeDecisionOnUsageOfJailFreeCard()
    {
        if (this.jailHandler.JailFreeCardCounts[this.CurrentPlayerNumber] > 0)
        {
            this.isDoubleSideEffectOn = true;
            this.eventFlow.BoolDecision = this.decisionMakers.JailFreeCardUsageDecisionMaker.MakeDecisionOnUsage(this.CurrentPlayerNumber);
        }
        else
        {
            this.eventFlow.BoolDecision = false;
        }

        this.CallNextEvent();
    }

    public void MakeDecisionOnPaymentOfJailFine()
    {
        if (this.bankHandler.Balances[this.CurrentPlayerNumber] >= 60)
        {
            this.isDoubleSideEffectOn = true;
            this.eventFlow.BoolDecision = this.decisionMakers.
            JailFinePaymentDecisionMaker.
            MakeDecisionOnPayment(this.CurrentPlayerNumber);
        }

        this.CallNextEvent();
    }

    public void BeReleasedIfAlreadyStayed3TurnsInJail()
    {
        if (this.jailHandler.TurnsInJailCounts[this.CurrentPlayerNumber] == 3)
        {
            this.jailHandler.ResetTurnInJail(this.CurrentPlayerNumber);
            this.bankHandler.DecreaseBalance(this.CurrentPlayerNumber, 60);

            this.eventFlow.RecommendedString = this.stringPlayer + " is released after staying 3 turns in jail";

            this.delegator.SetNextAction(this.MoveByRollDiceResultTotal);
        }
    }

    public void StayInJail()
    {
        this.jailHandler.CountTurnInJail(this.CurrentPlayerNumber);
        this.eventFlow.RecommendedString = this.stringPlayer + " stays one more turn in jail";

        this.CallNextEvent();
    }

    public void IsReleasedFromJail()
    {
        this.jailHandler.ResetTurnInJail(this.CurrentPlayerNumber);
        this.isDoubleSideEffectOn = false;

        if (this.lastAction == this.RollDice)
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " is released from jail becasue he/she rolled double";
        }
        else if (this.lastAction == this.PayJailFine)
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " is released from jail";
        }

        this.CallNextEvent();
    }

    public void HasJailPenalty()
    {
        if (this.delegator.LastActionName == "RollDice")
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " rolled double 3 times in a row. It is so suspicious. "
                                            + this.stringPlayer + " is moved to the jail";
        }

        if (this.delegator.LastActionName == "LandOnTile")
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " is moved to the jail";
        }

        this.boardHandler.Teleport(this.CurrentPlayerNumber, this.GetJailPosition());
        this.jailHandler.CountTurnInJail(this.CurrentPlayerNumber);
        this.CallNextEvent();
    }

    public void LandOnTile()
    {
        this.eventFlow.RecommendedString = this.stringPlayer + string.Format(" landed on {0}", this.currentTile.Name);
        this.CallNextEvent();
    }

    public void MoveByRollDiceResultTotal()
    {
        int rollDiceResultTotal = this.eventFlow.RollDiceResult.Sum();
        this.eventFlow.RecommendedString = this.stringPlayer + string.Format(" moved {0} steps", rollDiceResultTotal);

        this.boardHandler.MovePlayerAroundBoard(this.CurrentPlayerNumber, rollDiceResultTotal);
        this.CallNextEvent();
    }

    public void PayRent()
    {
        Property property = (Property)this.currentTile;
        int propertyOwner = (int)property.OwnerPlayerNumber!;
        int rentOfProperty = property.CurrentRent;

        if (property is Utility)
        {
            rentOfProperty = rentOfProperty * this.eventFlow.RollDiceResult.Sum();
        }

        this.bankHandler.TransferBalanceFromTo(this.CurrentPlayerNumber, propertyOwner, rentOfProperty);
        this.eventFlow.RecommendedString = this.stringPlayer + string.Format(" paid {0}$ to Player{1}", rentOfProperty, propertyOwner);

        this.CallNextEvent();
    }

    public void CheckExtraTurn()
    {
        if (this.isDoubleSideEffectOn && this.doubleSideEffectHandler.ExtraTurns[this.CurrentPlayerNumber])
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " has an extra turn";
        }

        this.CallNextEvent();
    }

    public void PayTax()
    {
        TaxTile taxTile = (TaxTile)this.currentTile;
        int tax = taxTile.Tax;

        this.bankHandler.DecreaseBalance(this.CurrentPlayerNumber, tax);
        this.eventFlow.RecommendedString = this.stringPlayer + " paid the tax";

        this.CallNextEvent();
    }

    public void MakeDecisionOnPurchaseOfProperty()
    {
        Property property = (Property)this.currentTile;

        this.eventFlow.BoolDecision = this.decisionMakers.
        PropertyPurchaseDecisionMaker.MakeDecisionOnPurchase();

        this.CallNextEvent();
    }

    public void PurchaseProperty()
    {
        IProperty property = (IProperty)this.currentTile;

        this.tileManager.PropertyManager.ChangeOwner(property, this.CurrentPlayerNumber);
        this.bankHandler.DecreaseBalance(this.CurrentPlayerNumber, property.Price);
        this.eventFlow.RecommendedString = this.stringPlayer + " bought the property";

        this.CallNextEvent();
    }

    public void DontPurchaseProperty()
    {
        this.eventFlow.RecommendedString = this.stringPlayer + " didn't buy the property";

        this.CallNextEvent();
    }

    public void Idle()
    {
        this.idleCount++;
        this.eventFlow.RecommendedString = string.Format("Game is idle...{0}", this.idleCount);

        this.CallNextEvent();
    }

    public override void EndEvent()
    {
        this.ResetDoubleSideEffect();

        for (int i = 0; i < this.bankHandler.Balances.Count(); i++)
        {
            if (bankHandler.Balances[i] < 0 && this.inGameHandler.AreInGame[i] is true)
            {
                this.inGameHandler.SetIsInGame(i, false);
                this.ResetPropertiesOfPlayerNumber(i);
                this.eventFlow.RecommendedString = string.Format("Player{0} is bankrupt", i);
            }
        }

        int playerInGameCount = this.inGameHandler.AreInGame.Where(isInGame => isInGame == true).Count();

        if (playerInGameCount == 1)
        {
            this.eventFlow.RecommendedString = "Congratulations!! " + this.stringPlayer + " won the game!";
        }
        else if (playerInGameCount < 1)
        {
            throw new Exception();
        }
        else
        {
            this.eventFlow.CurrentPlayerNumber = this.CalculateNextPlayer();
        }

        this.CallNextEvent();
    }

    private void ReceiveSalary()
    {
        this.eventFlow.RecommendedString = this.stringPlayer + " passed go and received the salary";
        this.bankHandler.IncreaseBalance(this.CurrentPlayerNumber, 200);

        this.CallNextEvent();
    }

    private void GameIsOver()
    {
        this.eventFlow.GameIsOver = true;
        this.eventFlow.RecommendedString = "Game Is Over";

        this.CallNextEvent();
    }

    private void ResetDoubleSideEffect()
    {
        this.doubleSideEffectHandler.ResetDoubleCount(this.CurrentPlayerNumber);
        this.doubleSideEffectHandler.SetExtraTurn(this.CurrentPlayerNumber, false);
        this.isDoubleSideEffectOn = true;
    }

    private int GetJailPosition()
    {
        ITile jail = this.tileManager.Tiles.Where(tile => tile is Jail).ToList()[0];
        return this.tileManager.Tiles.IndexOf(jail);
    }

    private string ConvertRollDiceResultToString()
    {
        return string.Join(", ", from dieValue in this.eventFlow.RollDiceResult select dieValue.ToString());
    }

    private int CalculateNextPlayer()
    {
        int previousPlayerNumber = this.CurrentPlayerNumber;

        for (int i = 0; i < 4; i++)
        {
            int candidate = (previousPlayerNumber + 1 + i) % 4;
            if (this.inGameHandler.AreInGame[candidate])
            {
                int newPlayerNumber = candidate;
                if (newPlayerNumber < previousPlayerNumber)
                {
                    this.eventFlow.Turn++;
                    this.timeToTrade = true;
                }

                return newPlayerNumber;
            }
        }

        throw new Exception();
    }

    private ITile GetCurrentTile()
    {
        int playerPosition = this.boardHandler.PlayerPositions[this.CurrentPlayerNumber];
        return this.tileManager.Tiles[playerPosition];
    }

    private void UpdateNextDoubleSideEffect()
    {
        if (this.rolledDouble)
        {
            this.doubleSideEffectHandler.CountDouble(this.CurrentPlayerNumber);
            this.doubleSideEffectHandler.SetExtraTurn(this.CurrentPlayerNumber, true);
        }
    }

    private List<int> CreateAuctionParticipantPlayerNumbers()
    {
        List<int> auctionParticipantNumbers = new List<int>();
        int playerNumber = this.CurrentPlayerNumber;
        List<bool> areInGame = this.inGameHandler.AreInGame;

        for (int i = 0; i < this.inGameHandler.AreInGame.Count(); i++)
        {
            if (areInGame[playerNumber] is true)
            {
                auctionParticipantNumbers.Add(playerNumber);
            }
            playerNumber = (playerNumber + 1) % areInGame.Count();
        }

        return auctionParticipantNumbers;
    }

    public override void AddNextAction(Action nextAction)
    {
        this.previousActionOfLastAction = this.lastAction;
        this.lastAction = nextAction;

        this.delegator
            .SetNextAction(nextAction);
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

    private int ComparePriceAndMinBalanceOfPlayerInGameAndReturnSmallerOne(int price)
    {
        List<int> balances = this.dataCenter.Bank.Balances;
        List<bool> areInGame = this.dataCenter.InGame.AreInGame;
        int smallerInt = 0;
        int count = areInGame.Count();

        List<int> balancesOfPlayersInGame = new List<int>();

        for (int i = 0; i < count; i++)
        {
            if (areInGame[i] is true)
            {
                balancesOfPlayersInGame.Add(balances[i]);
            }
        }

        smallerInt = (price < balancesOfPlayersInGame.Min()? price : balancesOfPlayersInGame.Min());

        return smallerInt;
    }
}
