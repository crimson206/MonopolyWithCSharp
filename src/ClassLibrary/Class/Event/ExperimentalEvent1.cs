public class ExperimentalEvent1
{
    private IBankHandler bankHandler;
    private IBoardHandler boardHandler;
    private IDoubleSideEffectHandler doubleSideEffectHandler;
    private ITileManager tileManager;
    private IJailHandler jailHandler;
    private EventFlow eventFlow;
    private IDelegator delegator;
    private Random random = new Random();

    public ExperimentalEvent1
    (
        IBankHandler bankHandler,
        IBoardHandler boardHandler,
        IDoubleSideEffectHandler doubleSideEffectHandler,
        ITileManager tileManager,
        IJailHandler jailHandler,
        EventFlow eventFlow,
        IDelegator delegator
    )
    {
        this.bankHandler = bankHandler;
        this.boardHandler = boardHandler;
        this.tileManager = tileManager;
        this.jailHandler = jailHandler;
        this.eventFlow = eventFlow;
        this.delegator = delegator;
        this.doubleSideEffectHandler = doubleSideEffectHandler;
    }

    private int playerNumber => this.eventFlow.CurrentPlayerNumber;
    private Tile currentTile => this.GetCurrentTile();
    private string stringPlayer => String.Format("Player {0}", this.playerNumber);

    public void DecideTurnType()
    {
        if (this.jailHandler.TurnsInJailCounts[this.playerNumber] > 0)
        {
            this.delegator.SetNextEvent(this.EscapeJailIfCan);
        }
        else
        {
            this.delegator.SetNextEvent(this.RollDiceAndMoveIfCan);
        }
    }

    public void EscapeJailIfCan()
    {
        this.eventFlow.RecommentedString = this.stringPlayer + " starts a turn in jail";

        if (this.jailHandler.JailFreeCardCounts[this.playerNumber] > 0)
        {
            this.delegator.SetNextEvent(this.UseJailFreeCardAndEscapeIfWant);
        }
        if (this.bankHandler.Balances[this.playerNumber] >= this.bankHandler.JailFine)
        {
            this.delegator.SetNextEvent(this.PayJailFineAndEscapeIfWant);
        }

        this.delegator.SetNextEvent(this.RollDice);
        this.delegator.SetNextEvent(this.EscapeJailIfRolledDouble);
        this.delegator.SetNextEvent(this.BeReleasedIfAlreadyStayed3TurnsInJail);
        this.delegator.SetNextEvent(this.StayInJail);
    }

    public void BeReleasedIfAlreadyStayed3TurnsInJail()
    {
        if (this.jailHandler.TurnsInJailCounts[this.playerNumber] == 3)
        {
            this.jailHandler.ResetTurnInJail(this.playerNumber);
            this.bankHandler.DecreaseBalance(this.playerNumber, this.bankHandler.JailFine);

            this.eventFlow.RecommentedString = this.stringPlayer + " is released after staying 3 turns in jail";

            this.delegator.ResetAndAddEvent(this.MoveByRollDiceResultTotal);
        }
    }

    public void StayInJail()
    {
        this.jailHandler.CountTurnInJail(this.playerNumber);
        this.eventFlow.RecommentedString = this.stringPlayer + " stays one more turn in jail";

        this.delegator.SetNextEvent(this.EndTurn);
    }

    public void EscapeJailIfRolledDouble()
    {
        bool rolledDouble = this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];
        if ( rolledDouble )
        {
            this.delegator.ResetAndAddEvent(this.MoveByRollDiceResultTotal);
        }
    }

    public void PayJailFineAndEscapeIfWant()
    {
        if (this.bankHandler.Balances[this.playerNumber] >= 2 * this.bankHandler.JailFine)
        {
            this.eventFlow.RecommentedString = this.stringPlayer + "paid the jail fine";

            this.delegator.ResetAndAddEvent(this.RollDiceAndMoveIfCan);
        }
    }
    public void UseJailFreeCardAndEscapeIfWant()
    {
        /// need to make decision maker for the decision bools

        if (this.jailHandler.JailFreeCardCounts[this.playerNumber] > 0)
        {
            this.eventFlow.RecommentedString = this.stringPlayer + "used a jail free card";

            this.delegator.ResetAndAddEvent(this.RollDiceAndMoveIfCan);
        }
    }

    public void RollDiceAndMoveIfCan()
    {
        this.delegator.ResetAndAddEvent(this.RollDice);
        this.delegator.SetNextEvent(this.UpdateNextDoubleSideEffect);
        this.delegator.SetNextEvent(this.GoToJailIfRolledDouble3Times);
        this.delegator.SetNextEvent(this.MoveByRollDiceResultTotal);
    }

    public void GoToJailIfRolledDouble3Times()
    {
        if (this.doubleSideEffectHandler.DoubleCounts[playerNumber] == 3)
        {
            this.eventFlow.RecommentedString = this.stringPlayer + " rolled double 3 times. It is so suspicious...";

            this.boardHandler.Teleport(this.playerNumber, GetJailPosition());
            this.delegator.ResetAndAddEvent(this.HasJailPenalty);
        }
    }

    public void HasJailPenalty()
    {
        this.eventFlow.RecommentedString = this.stringPlayer + " goes to jail and has the jail penalty there";

        this.boardHandler.Teleport(this.playerNumber, GetJailPosition());
        this.delegator.SetNextEvent(this.EndTurn);
    }

    public void HasLandEvent()
    {

        this.eventFlow.RecommentedString = this.stringPlayer + String.Format(" landed on {0}", currentTile.Name);
    
        if (this.currentTile is Property)
        {
            this.delegator.SetNextEvent(this.HasPropertyLandEvent);
        }
        else if ((this.currentTile is TaxTile))
        {
            this.delegator.SetNextEvent(this.PayTax);
        }
        else if ( currentTile is GoToJail)
        {
            this.delegator.SetNextEvent(this.HasJailPenalty);
        }
        else
        {
            this.delegator.SetNextEvent(this.HasExtraTurnOrEndTurn);
        }
    }

    public void MoveByRollDiceResultTotal()
    {
        int rollDiceResultTotal = this.eventFlow.RollDiceResult.Sum();
        this.eventFlow.RecommentedString = this.stringPlayer + String.Format(" moved {0} steps", rollDiceResultTotal);

        this.boardHandler.MovePlayerAroundBoard(this.playerNumber, rollDiceResultTotal);
        
        this.delegator.SetNextEvent(this.ReceiveSalaryIfPassedGo);
        this.delegator.SetNextEvent(this.HasLandEvent);
    }

    public void UpdateNextDoubleSideEffect()
    {
        bool rolledDouble = this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];
        if (rolledDouble)
        {
            this.doubleSideEffectHandler.CountDouble(this.playerNumber);
            this.doubleSideEffectHandler.SetExtraTurn(this.playerNumber, true);
        }
    }

    public void HasPropertyLandEvent()
    {
        Property currentProperty = (Property) currentTile;

        if (currentProperty.OwnerPlayerNumber == this.playerNumber)
        {

        }
        else if (currentProperty.OwnerPlayerNumber == null)
        {
            bool canBuyProperty = this.bankHandler.Balances[this.playerNumber] >= currentProperty.Price;

            if ( canBuyProperty )
            {
                this.delegator.SetNextEvent(this.BuyPropertyIfWant);
            }
        }
        else
        {
            this.delegator.SetNextEvent(this.PayRent);
        }

        this.delegator.SetNextEvent(this.HasExtraTurnOrEndTurn);
    }

    public void PayRent()
    {
        Property property = (Property)this.currentTile;
        int propertyOwner = (int) property.OwnerPlayerNumber!;
        
        int rentOfProperty = property.CurrentRent;
        this.bankHandler.TransferBalanceFromTo(this.playerNumber, propertyOwner, rentOfProperty);
        this.eventFlow.RecommentedString = this.stringPlayer + " paid a rent to the owner of the property";
    }

    public void HasExtraTurnOrEndTurn()
    {
        if (this.doubleSideEffectHandler.ExtraTurns[this.playerNumber])
        {
            this.doubleSideEffectHandler.SetExtraTurn(this.playerNumber, false);
            this.eventFlow.RecommentedString = this.stringPlayer + " rolled double last time and has an extra turn";

            this.delegator.SetNextEvent(this.RollDiceAndMoveIfCan);
        }
        else
        {
            this.delegator.SetNextEvent(this.EndTurn);
        }
    }

    public void PayTax()
    {
        TaxTile taxTile = (TaxTile) this.currentTile;
        int tax = taxTile.Tax;

        this.bankHandler.DecreaseBalance(this.playerNumber, tax);

        this.delegator.SetNextEvent(this.HasExtraTurnOrEndTurn);
    }

    public void BuyPropertyIfWant()
    {
        Property property = (Property)this.currentTile;

        if ( this.bankHandler.Balances[this.playerNumber] > property.Price)
        {
        this.tileManager.propertyManager.ChangeOwner(property, this.playerNumber);
        this.bankHandler.DecreaseBalance(this.playerNumber, property.Price);
        this.eventFlow.RecommentedString = this.stringPlayer + " bought the property";
        }
    }

    public void EndTurn()
    {
        this.ResetDoubleSideEffect();

        int playerCountInGame = this.bankHandler.Balances.Where(balance => balance >= 0).Count();

        if (playerCountInGame == 1)
        {
            this.eventFlow.RecommentedString = "Congratulations!! " + this.stringPlayer + " won the game!";
        }
        else if(playerCountInGame < 1)
        {
            throw new Exception();
        }
        else
        {
            this.eventFlow.RecommentedString = this.stringPlayer + " ended this turn";
            this.eventFlow.CurrentPlayerNumber = this.CalculateNextPlayer();
        }

        this.delegator.SetNextEvent(this.DecideTurnType);
    }

    public void ReceiveSalaryIfPassedGo()
    {
        this.eventFlow.RecommentedString = this.stringPlayer + " passed go and received the salary";
        this.bankHandler.IncreaseBalance(this.playerNumber, this.bankHandler.Salary);
    }

    private void ResetDoubleSideEffect()
    {
        this.doubleSideEffectHandler.ResetDoubleCount(this.playerNumber);
        this.doubleSideEffectHandler.SetExtraTurn(this.playerNumber, false);
    }

    private int GetJailPosition()
    {
        Tile jail = this.tileManager.Tiles.Where(tile => tile is Jail).ToList()[0];
        return this.tileManager.Tiles.IndexOf(jail);
    }

    public void RollDice()
    {
        this.eventFlow.RollDiceResult = Dice.Roll(random);
        this.eventFlow.RecommentedString = this.stringPlayer + " rolled " + this.ConvertRollDiceResultToString();
    }

    private string ConvertRollDiceResultToString()
    {
        return String.Join(", ", from dieValue in this.eventFlow.RollDiceResult select dieValue.ToString());
    }

    private int CalculateNextPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            int candidate = (this.playerNumber + 1 + i) % 4;
            if ( this.bankHandler.Balances[candidate] >= 0)
            {
                return candidate;
            }
        }

        throw new Exception();
    }

    private Tile GetCurrentTile()
    {
        int playerPosition = this.boardHandler.PlayerPositions[this.playerNumber];
        return this.tileManager.Tiles[playerPosition];
    }

}
