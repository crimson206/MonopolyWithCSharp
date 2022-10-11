public class TryToEscapeJail : Event
{
    public TryToEscapeJail(Delegator delegator) : base(delegator)
    {
        this.delegator= delegator;
        this.delegator.tryToEscapeJail = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;
    public void Start(JailManager jailManager, Bank bank)
    {
        this.delegator!.boolDecisionType = BoolDecisionType.WantToUseJailFreeCard;
        delegator.tryToEscapeJail = this.WantPlayerUseJailFreeCard;
    }

    public void WantPlayerUseJailFreeCard(JailManager jailManager, Bank bank)
    {
        if (this.delegator!.PlayerBoolDecision)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(EventType.RollToMove);
        }
        else
        {
            delegator.boolDecisionType = BoolDecisionType.WantToPayJailFine;
            delegator.tryToEscapeJail = this.WantPlayerPayJailFine;
        }
    }

    public void WantPlayerPayJailFine(JailManager jailManager, Bank bank)
    {
        if (this.delegator!.PlayerBoolDecision)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            bank.Balances[playerNumber] -= jailManager.JailFine;
            this.SetNextEvent(EventType.RollToMove);
        }
        else
        {
            delegator.playerRollDice = true;
            delegator.tryToEscapeJail = this.RolledPlayerDouble;
        }
    }

    public void RolledPlayerDouble(JailManager jailManager, Bank bank)
    {
        int[] rollDiceResult = this.delegator!.PlayerRollDiceResult;

        if (rollDiceResult[0] == rollDiceResult[1])
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(EventType.EscapeJail);
        }
        else
        {
            delegator.tryToEscapeJail = this.StayedPlayerThreeTurns;
        }
    }

    public void StayedPlayerThreeTurns(JailManager jailManager, Bank bank)
    {
        if (jailManager.TurnsInJail[playerNumber] == 3)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(EventType.EscapeJail);
        }
        else
        {
            jailManager.TurnsInJail[playerNumber] += 1;
            this.SetNextEvent(EventType.EndTurn);
        }
    }

    protected override void SetNextEvent(EventType nextEvent)
    {
        this.delegator!.nextEvent = nextEvent;
        this.delegator.tryToEscapeJail = this.Start;
    }
}
