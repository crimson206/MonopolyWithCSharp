public class TryToEscapeJail : Event
{
    private Data data;
    public TryToEscapeJail(Data data)
    {
        this.data = data;
    }
    public void Start(Delegator delegator, int playerNumber, JailManager jailManager, Bank bank)
    {
        delegator.playerBoolDecision = BoolDecisionType.WantToUseJailFreeCard;
        delegator.tryToEscapeJail = this.WantPlayerUseJailFreeCard;
    }

    public void WantPlayerUseJailFreeCard(Delegator delegator, int playerNumber, JailManager jailManager, Bank bank)
    {
        if (data.LastBoolDecisions[playerNumber])
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            data.UpdateJailManager(jailManager);
            this.SetNextEvent(delegator, EventType.RollToMove);
        }
        else
        {
            delegator.playerBoolDecision = BoolDecisionType.WantToPayJailFine;
            delegator.tryToEscapeJail = this.WantPlayerPayJailFine;
        }
    }

    public void WantPlayerPayJailFine(Delegator delegator, int playerNumber, JailManager jailManager, Bank bank)
    {
        if (data.LastBoolDecisions[playerNumber])
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            bank.Balances[playerNumber] -= jailManager.JailFine;
            this.SetNextEvent(delegator, EventType.RollToMove);
        }
        else
        {
            delegator.playerRollDice = true;
            delegator.tryToEscapeJail = this.RolledPlayerDouble;
        }
    }

    public void RolledPlayerDouble(Delegator delegator, int playerNumber, JailManager jailManager, Bank bank)
    {
        int[] rollDiceResult = data.LastRollDiceResults[playerNumber];

        if (rollDiceResult[0] == rollDiceResult[1])
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(delegator, EventType.EscapeJail);
        }
        else
        {
            delegator.tryToEscapeJail = this.StayedPlayerThreeTurns;
        }
    }

    public void StayedPlayerThreeTurns(Delegator delegator, int playerNumber, JailManager jailManager, Bank bank)
    {
        if (jailManager.TurnsInJail[playerNumber] == 3)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(delegator, EventType.EscapeJail);
        }
        else
        {
            jailManager.TurnsInJail[playerNumber] += 1;
            this.SetNextEvent(delegator, EventType.EndTurn);
        }
    }

    protected override void SetNextEvent(Delegator delegator, EventType nextEvent)
    {
        delegator.nextEvent = nextEvent;
        delegator.tryToEscapeJail = this.Start;
    }
}
