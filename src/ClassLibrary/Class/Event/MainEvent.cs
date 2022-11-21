public class MainEvent : Event, IMainEvent
{
    private IBankHandler bankHandler;
    private BoardHandler boardHandler;
    private IDice dice;
    private DoubleSideEffectHandler doubleSideEffectHandler;
    private JailHandler jailHandler;
    private Random random;
    private IDecisionMakers decisionMakers;
    private bool isDoubleSideEffectOn = true;
    private Tile currentTile => this.GetCurrentTile();
    private bool rolledDouble => this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];

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
        this.eventFlow = statusHandlers.EventFlow;
        this.tileManager = tileManager;
        this.decisionMakers = decisionMakers;
        this.delegator = delegator;

        this.delegator.SetNextAction(this.StartEvent);
        this.lastEvent = this.StartEvent;

        this.dice = dice;
        this.random = random;
    }

    private int PlayerNumber => this.eventFlow.CurrentPlayerNumber;

    private string stringPlayer => String.Format("Player{0}", this.PlayerNumber);

    protected override void CallNextEvent()
    {
        if (this.lastEvent == this.StartEvent)
        {
            if (this.jailHandler.TurnsInJailCounts[this.PlayerNumber] > 0)
            {
                this.AddNextAction(this.MakeDecisionOnUsageOfJailFreeCard);
            }
            else
            {
                this.AddNextAction(this.RollDice);
            }
            return;
        }

        if (this.lastEvent == this.MakeDecisionOnUsageOfJailFreeCard)
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

        if (this.lastEvent == this.MakeDecisionOnPaymentOfJailFine)
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

        if (this.lastEvent == this.EscapeJail)
        {
            this.AddNextAction(this.RollDice);
            return;
        }

        if (this.lastEvent == this.RollDice)
        {

            if (this.lastEvent == this.MakeDecisionOnPaymentOfJailFine || this.lastEvent == this.MakeDecisionOnUsageOfJailFreeCard)
            {
                if (this.rolledDouble)
                {
                    this.AddNextAction(this.IsReleasedFromJail);
                }
                else
                {
                    if (this.jailHandler.TurnsInJailCounts[this.PlayerNumber] == 3)
                    {
                        this.AddNextAction(this.PayJailFine);
                    }
                    else
                    {
                        this.AddNextAction(this.StayInJail);
                    }
                }
            }
            else
            {
                if (this.isDoubleSideEffectOn && this.doubleSideEffectHandler.DoubleCounts[this.PlayerNumber] == 3)
                {
                    this.AddNextAction(this.HasJailPenalty);
                }
                else
                {
                this.AddNextAction(this.MoveByRollDiceResultTotal);
                }
            }

            return;
        }

        if (this.lastEvent == this.MoveByRollDiceResultTotal)
        {
            if (this.boardHandler.PlayerPassedGo[this.PlayerNumber])
            {
                this.AddNextAction(this.ReceiveSalary);
            }
            else
            {
                this.AddNextAction(this.LandOnTile);
            }

            return;
        }

        if (this.lastEvent == this.ReceiveSalary)
        {
            this.AddNextAction(this.LandOnTile);
            return;
        }

        if (this.lastEvent == this.LandOnTile)
        {
            if (this.currentTile is Property)
            {
                Property currentProperty = (Property)this.currentTile;

                if (currentProperty.OwnerPlayerNumber == this.PlayerNumber)
                {
                    this.AddNextAction(this.CheckExtraTurn);
                }
                else if (currentProperty.OwnerPlayerNumber == null)
                {
                    bool canBuyProperty = this.bankHandler.Balances[this.PlayerNumber] >= currentProperty.Price;

                    if (canBuyProperty)
                    {
                        this.AddNextAction(this.MakeDecisionOnPurchaseOfProperty);
                    }
                }
                else
                {
                    this.AddNextAction(this.PayRent);
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

        if (this.lastEvent == this.CheckExtraTurn)
        {
            if ( this.doubleSideEffectHandler.ExtraTurns[this.PlayerNumber])
            {
                this.AddNextAction(this.RollDice);
            }
            else
            {
                this.events!.TradeEvent.AddNextAction(this.events.TradeEvent.StartEvent);
            }

            return;
        }

        if (this.lastEvent == this.PayJailFine)
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

        if (this.lastEvent == this.MakeDecisionOnPurchaseOfProperty)
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
        if (this.lastEvent == this.HasJailPenalty)
        {
            this.events!.TradeEvent.AddNextAction(this.events.TradeEvent.StartEvent);

            return;
        }

        if (this.lastEvent == this.PurchaseProperty)
        {
            this.AddNextAction(this.CheckExtraTurn);
            return;
        }

        if (this.lastEvent == this.DontPurchaseProperty)
        {
            this.events!.AuctionEvent.AddNextAction(this.events!.AuctionEvent.StartEvent);
            return;
        }

        if (this.lastEvent == this.EndTurn)
        {
            this.AddNextAction(this.StartEvent);
            return;
        }

        if (this.lastEvent == this.PayTax)
        {
            this.AddNextAction(this.CheckExtraTurn);
            return;
        }

        if (this.lastEvent == this.PayRent)
        {
            this.AddNextAction(this.CheckExtraTurn);
            return;
        }

        throw new Exception("It seems that you didn't set a next event of an event. Check the last event and check if there is the next event setting for the last event in CallNextEvent()");
    }

    private void RollDice()
    {
        /// reset value before setting a new value
        this.doubleSideEffectHandler.SetExtraTurn(this.PlayerNumber, false);

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
        this.bankHandler.DecreaseBalance(this.PlayerNumber, 60);
        this.jailHandler.ResetTurnInJail(this.PlayerNumber);
        this.eventFlow.RecommendedString = this.stringPlayer + " paid the jail fine";

        this.CallNextEvent();
    }

    private void UseJailFreeCard()
    {
        this.jailHandler.RemoveAJailFreeCard(this.PlayerNumber);
        this.jailHandler.ResetTurnInJail(this.PlayerNumber);
        this.eventFlow.RecommendedString = this.stringPlayer + " paid the jail fine";

        this.CallNextEvent();
    }

    public void EscapeJail()
    {
        this.jailHandler.ResetTurnInJail(this.PlayerNumber);
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
        if (this.jailHandler.JailFreeCardCounts[this.PlayerNumber] > 0)
        {
            this.isDoubleSideEffectOn = true;
            this.eventFlow.BoolDecision = this.decisionMakers.JailFreeCardUsageDecisionMaker.MakeDecisionOnUsage(this.PlayerNumber);
        }

        this.CallNextEvent();
    }

    public void MakeDecisionOnPaymentOfJailFine()
    {
        if (this.bankHandler.Balances[this.PlayerNumber] >= 60)
        {
            this.isDoubleSideEffectOn = true;
            this.eventFlow.BoolDecision = this.decisionMakers.
            JailFinePaymentDecisionMaker.
            MakeDecisionOnPayment(this.PlayerNumber);
        }

        this.CallNextEvent();
    }

    public void BeReleasedIfAlreadyStayed3TurnsInJail()
    {
        if (this.jailHandler.TurnsInJailCounts[this.PlayerNumber] == 3)
        {
            this.jailHandler.ResetTurnInJail(this.PlayerNumber);
            this.bankHandler.DecreaseBalance(this.PlayerNumber, 60);

            this.eventFlow.RecommendedString = this.stringPlayer + " is released after staying 3 turns in jail";

            this.delegator.SetNextAction(this.MoveByRollDiceResultTotal);
        }
    }

    public void StayInJail()
    {
        this.jailHandler.CountTurnInJail(this.PlayerNumber);
        this.eventFlow.RecommendedString = this.stringPlayer + " stays one more turn in jail";

        this.CallNextEvent();
    }

    public void IsReleasedFromJail()
    {
        this.jailHandler.ResetTurnInJail(this.PlayerNumber);
        this.isDoubleSideEffectOn = false;

        if (this.lastEvent == this.RollDice)
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " is released from jail becasue he/she rolled double";
        }

        if (this.lastEvent == this.PayJailFine)
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " is released from jail";
        }
    }

    public void HasJailPenalty()
    {
        if (this.lastEvent == this.RollDice)
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " rolled double 3 times in a row. It is so suspicious. "
                                            + this.stringPlayer + " is moved to the jail";
        }

        if (this.lastEvent == this.LandOnTile)
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " is moved to the jail";
        }

        this.boardHandler.Teleport(this.PlayerNumber, this.GetJailPosition());
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

        this.boardHandler.MovePlayerAroundBoard(this.PlayerNumber, rollDiceResultTotal);
        this.CallNextEvent();
    }

    public void PayRent()
    {
        Property property = (Property)this.currentTile;
        int propertyOwner = (int)property.OwnerPlayerNumber!;

        int rentOfProperty = property.CurrentRent;
        this.bankHandler.TransferBalanceFromTo(this.PlayerNumber, propertyOwner, rentOfProperty);
        this.eventFlow.RecommendedString = this.stringPlayer + " paid a rent to the owner of the property";

        this.CallNextEvent();
    }

    public void CheckExtraTurn()
    {
        if (this.isDoubleSideEffectOn && this.doubleSideEffectHandler.ExtraTurns[this.PlayerNumber])
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " has an extra turn due to rolling double";
        }

        this.CallNextEvent();
    }

    public void PayTax()
    {
        TaxTile taxTile = (TaxTile)this.currentTile;
        int tax = taxTile.Tax;

        this.bankHandler.DecreaseBalance(this.PlayerNumber, tax);
        this.eventFlow.RecommendedString = this.stringPlayer + " paid the tax";

        this.CallNextEvent();
    }

    public void MakeDecisionOnPurchaseOfProperty()
    {
        Property property = (Property)this.currentTile;

        this.eventFlow.BoolDecision = this.decisionMakers.
        PropertyPurchaseDecisionMaker.MakeDecisionOnPurchase(this.PlayerNumber);

        this.CallNextEvent();
    }

    public void PurchaseProperty()
    {
        Property property = (Property)this.currentTile;

        this.tileManager.PropertyManager.ChangeOwner(property, this.PlayerNumber);
        this.bankHandler.DecreaseBalance(this.PlayerNumber, property.Price);
        this.eventFlow.RecommendedString = this.stringPlayer + " bought the property";

        this.CallNextEvent();
    }

    public void DontPurchaseProperty()
    {
        this.eventFlow.RecommendedString = this.stringPlayer + " didn't buy the property";

        this.CallNextEvent();
    }

    public void EndTurn()
    {
        this.ResetDoubleSideEffect();

        int playerCountInGame = this.bankHandler.Balances.Where(balance => balance >= 0).Count();

        if (playerCountInGame == 1)
        {
            this.eventFlow.RecommendedString = "Congratulations!! " + this.stringPlayer + " won the game!";
        }
        else if (playerCountInGame < 1)
        {
            throw new Exception();
        }
        else
        {
            this.eventFlow.RecommendedString = this.stringPlayer + " ended this turn";
            this.eventFlow.CurrentPlayerNumber = this.CalculateNextPlayer();
        }

        this.CallNextEvent();
    }

    public void ReceiveSalary()
    {
        this.eventFlow.RecommendedString = this.stringPlayer + " passed go and received the salary";
        this.bankHandler.IncreaseBalance(this.PlayerNumber, 200);

        this.CallNextEvent();
    }

    private void ResetDoubleSideEffect()
    {
        this.doubleSideEffectHandler.ResetDoubleCount(this.PlayerNumber);
        this.doubleSideEffectHandler.SetExtraTurn(this.PlayerNumber, false);
        this.isDoubleSideEffectOn = true;
    }

    private int GetJailPosition()
    {
        Tile jail = this.tileManager.Tiles.Where(tile => tile is Jail).ToList()[0];
        return this.tileManager.Tiles.IndexOf(jail);
    }

    private string ConvertRollDiceResultToString()
    {
        return string.Join(", ", from dieValue in this.eventFlow.RollDiceResult select dieValue.ToString());
    }

    private int CalculateNextPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            int candidate = (this.PlayerNumber + 1 + i) % 4;
            if (this.bankHandler.Balances[candidate] >= 0)
            {
                return candidate;
            }
        }

        throw new Exception();
    }

    private Tile GetCurrentTile()
    {
        int playerPosition = this.boardHandler.PlayerPositions[this.PlayerNumber];
        return this.tileManager.Tiles[playerPosition];
    }

    private void UpdateNextDoubleSideEffect()
    {
        if (this.rolledDouble)
        {
            this.doubleSideEffectHandler.CountDouble(this.PlayerNumber);
            this.doubleSideEffectHandler.SetExtraTurn(this.PlayerNumber, true);
        }
    }
}
