public class MainEvent : IResponseToSwitchEvent
{
    private BankHandler bankHandler;
    private BoardHandler boardHandler;
    private DoubleSideEffectHandler doubleSideEffectHandler;
    private TileManager tileManager;
    private JailHandler jailHandler;
    private EventFlow eventFlow;
    private Delegator delegator;
    private BoolCopier boolCopier = new BoolCopier();
    private Random random = new Random();
    private DecisionMakerDummy decisionMaker = new DecisionMakerDummy();
    private bool isDoubleSideEffectOn = true;
    private bool isAbleToMove = true;
    private Tile currentTile => this.GetCurrentTile();
    private bool rolledDouble => this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];
    private bool boughtProperty;
    private List<IResponseToSwitchEvent> eventGroup = new List<IResponseToSwitchEvent>();

    public MainEvent
    (BankHandler bankHandler,
    BoardHandler boardHandler,
    DoubleSideEffectHandler doubleSideEffectHandler,
    TileManager tileManager,
    JailHandler jailHandler,
    EventFlow eventFlow,
    Delegator delegator)
    {
        this.bankHandler = bankHandler;
        this.boardHandler = boardHandler;
        this.tileManager = tileManager;
        this.jailHandler = jailHandler;
        this.eventFlow = eventFlow;
        this.delegator = delegator;
        this.doubleSideEffectHandler = doubleSideEffectHandler;
        this.delegator.SetNextEvent(this.StartTurn);
        this.lastEvent = this.StartTurn;
    }

    private int PlayerNumber => this.eventFlow.CurrentPlayerNumber;

    private string stringPlayer => String.Format("Player {0}", this.PlayerNumber);

    private Action lastEvent;

    public void ResponseToSwitchEvent(EventType fromEvent, EventType toEvent)
    {
        if (toEvent != EventType.MainEvent)
        {
            return;
        }

        if (fromEvent != EventType.AuctionEvent)
        {
            this.CallNextEvent();
        }
    }

    private void SwitchEvent(EventType fromEvent, EventType toEvent)
    {
        foreach (var gameEvent in this.eventGroup)
        {
            gameEvent.ResponseToSwitchEvent(fromEvent, toEvent);
        }
    }

    private void CallNextEvent()
    {
        if (this.lastEvent == this.StartTurn)
        {
            if (this.jailHandler.TurnsInJailCounts[this.PlayerNumber] > 0)
            {
                this.AddNextEvent(this.MakeDecisionOnUsageOfJailFreeCard);
            }
            else
            {
                this.AddNextEvent(this.RollDice);
            }
            return;
        }

        if (this.lastEvent == this.MakeDecisionOnUsageOfJailFreeCard)
        {
            if (this.eventFlow.BoolDecision is true)
            {
                this.AddNextEvent(this.UseJailFreeCard);
            }
            else
            {
                this.AddNextEvent(this.MakeDecisionOnPaymentOfJailFine);
            }
            return;
        }

        if (this.lastEvent == this.MakeDecisionOnPaymentOfJailFine)
        {
            if (this.eventFlow.BoolDecision is true)
            {
                this.AddNextEvent(this.PayJailFine);
            }
            else
            {
                this.AddNextEvent(this.RollDice);
            }
            return;
        }

        if (this.lastEvent == this.EscapeJail)
        {
            this.AddNextEvent(this.RollDice);
            return;
        }

        if (this.lastEvent == this.RollDice)
        {
            bool rolledDouble = this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];

            if (this.lastEvent == this.MakeDecisionOnPaymentOfJailFine || this.lastEvent == this.MakeDecisionOnUsageOfJailFreeCard)
            {
                if (rolledDouble)
                {
                    this.AddNextEvent(this.IsReleasedFromJail);
                }
                else
                {
                    if (this.jailHandler.TurnsInJailCounts[this.PlayerNumber] == 3)
                    {
                        this.AddNextEvent(this.PayJailFine);
                    }
                    else
                    {
                        this.AddNextEvent(this.StayInJail);
                    }
                }
            }
            else
            {
                if (this.isDoubleSideEffectOn && this.doubleSideEffectHandler.DoubleCounts[this.PlayerNumber] == 3)
                {
                    this.AddNextEvent(this.HasJailPenalty);
                }
                else
                {
                this.AddNextEvent(this.MoveByRollDiceResultTotal);
                }
            }

            return;
        }

        if (this.lastEvent == this.MoveByRollDiceResultTotal)
        {
            if (this.boardHandler.PlayerPassedGo[this.PlayerNumber])
            {
                this.AddNextEvent(this.ReceiveSalary);
            }
            else
            {
                this.AddNextEvent(this.LandOnTile);
            }

            return;
        }

        if (this.lastEvent == this.ReceiveSalary)
        {
            this.AddNextEvent(this.LandOnTile);
            return;
        }

        if (this.lastEvent == this.LandOnTile)
        {
            if (this.currentTile is Property)
            {
                Property currentProperty = (Property)this.currentTile;

                if (currentProperty.OwnerPlayerNumber == this.PlayerNumber)
                {
                    this.AddNextEvent(this.CheckExtraTurn);
                }
                else if (currentProperty.OwnerPlayerNumber == null)
                {
                    bool canBuyProperty = this.bankHandler.Balances[this.PlayerNumber] >= currentProperty.Price;

                    if (canBuyProperty)
                    {
                        this.AddNextEvent(this.MakeDecisionOnPurchaseOfProperty);
                    }
                }
                else
                {
                    this.AddNextEvent(this.PayRent);
                }
            }
            else if (this.currentTile is TaxTile)
            {
                this.AddNextEvent(this.PayTax);
            }
            else if (this.currentTile is GoToJail)
            {
                this.AddNextEvent(this.HasJailPenalty);
            }
            else
            {
                this.AddNextEvent(this.CheckExtraTurn);
            }

            return;
        }

        if (this.lastEvent == this.CheckExtraTurn)
        {
            if ( this.doubleSideEffectHandler.ExtraTurns[this.PlayerNumber])
            {
                this.AddNextEvent(this.RollDice);
            }
            else
            {
                this.AddNextEvent(this.EndTurn);
            }

            return;
        }

        if (this.lastEvent == this.PayJailFine)
        {
            if (this.isDoubleSideEffectOn)
            {
                this.AddNextEvent(this.EscapeJail);
            }
            else
            {
                this.AddNextEvent(this.IsReleasedFromJail);
            }

            return;
        }

        if (this.lastEvent == this.MakeDecisionOnPurchaseOfProperty)
        {
            if (this.eventFlow.BoolDecision)
            {
                this.AddNextEvent(this.PurchaseProperty);
            }
            else
            {
                this.AddNextEvent(this.DontPurchaseProperty);
            }

            return;
        }

        if (this.lastEvent == this.HasJailPenalty)
        {
            this.AddNextEvent(this.EndTurn);

            return;
        }

        if (this.lastEvent == this.PurchaseProperty)
        {
            this.AddNextEvent(this.CheckExtraTurn);
            return;
        }

        if (this.lastEvent == this.DontPurchaseProperty)
        {
            this.SwitchEvent(EventType.MainEvent, EventType.AuctionEvent);
            return;
        }

        if (this.lastEvent == this.EndTurn)
        {
            this.AddNextEvent(this.StartTurn);
            return;
        }

        if (this.lastEvent == this.PayTax)
        {
            this.AddNextEvent(this.CheckExtraTurn);
            return;
        }

        if (this.lastEvent == this.PayRent)
        {
            this.AddNextEvent(this.CheckExtraTurn);
            return;
        }

        throw new Exception("It seems that you didn't set a next event of an event. Check the last event and check if there is the next event setting for the last event in CallNextEvent()");
    }

    private void RollDice()
    {
        /// reset value before setting a new value
        this.doubleSideEffectHandler.SetExtraTurn(this.PlayerNumber, false);

        this.eventFlow.RollDiceResult = Dice.Roll(random);

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

    public void StartTurn()
    {
        this.eventFlow.RecommendedString = this.stringPlayer + " start a turn";

        this.CallNextEvent();
    }

    public void MakeDecisionOnUsageOfJailFreeCard()
    {
        if (this.jailHandler.JailFreeCardCounts[this.PlayerNumber] > 0)
        {
            this.isDoubleSideEffectOn = true;
            this.eventFlow.BoolDecision = this.decisionMaker.MakeDecision();
        }

        this.CallNextEvent();
    }

    public void MakeDecisionOnPaymentOfJailFine()
    {
        if (this.bankHandler.Balances[this.PlayerNumber] >= 60)
        {
            this.isDoubleSideEffectOn = true;
            this.eventFlow.BoolDecision = this.decisionMaker.MakeDecision();
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

            this.delegator.SetNextEvent(this.MoveByRollDiceResultTotal);
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

        this.eventFlow.BoolDecision = this.decisionMaker.MakeDecision();

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

    private void AddNextEvent(Action nextEvent)
    {
        this.lastEvent = nextEvent;
        this.delegator.SetNextEvent(nextEvent);
    }

    private void UpdateNextDoubleSideEffect()
    {
        bool rolledDouble = this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];
        if (rolledDouble)
        {
            this.doubleSideEffectHandler.CountDouble(this.PlayerNumber);
            this.doubleSideEffectHandler.SetExtraTurn(this.PlayerNumber, true);
        }
    }
}
