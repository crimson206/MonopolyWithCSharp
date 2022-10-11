public class TryToEscapeJail : Event
{
    private JailManager jailManager;
    private Bank bank;
    public TryToEscapeJail(EventStorage eventStorage, Delegator delegator, JailManager jailManager, Bank bank) : base(eventStorage, delegator)
    {
        this.eventStorage = eventStorage;
        this.jailManager = jailManager;
        this.bank = bank;
        this.delegator= delegator;
        this.delegator.nextEvent = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;
    public override void Start()
    {
        this.delegator!.boolDecisionType = BoolDecisionType.WantToUseJailFreeCard;
        delegator.nextEvent = this.WantPlayerUseJailFreeCard;

    }

    public void WantPlayerUseJailFreeCard()
    {
        if (this.delegator!.PlayerBoolDecision)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            ///this.SetNextEvent(EventType.RollToMove);
        }
        else
        {
            delegator.boolDecisionType = BoolDecisionType.WantToPayJailFine;
            delegator.nextEvent = this.WantPlayerPayJailFine;
        }
    }

    public void WantPlayerPayJailFine()
    {
        if (this.delegator!.PlayerBoolDecision)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            bank.Balances[playerNumber] -= jailManager.JailFine;
            ///this.SetNextEvent(EventType.RollToMove);
        }
        else
        {
            delegator.playerRollDice = true;
            delegator.nextEvent = this.RolledPlayerDouble;
        }
    }

    public void RolledPlayerDouble()
    {
        int[] rollDiceResult = this.delegator!.PlayerRollDiceResult;

        if (rollDiceResult[0] == rollDiceResult[1])
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            ///this.SetNextEvent(EventType.EscapeJail);
        }
        else
        {
            delegator.nextEvent = this.StayedPlayerThreeTurns;
        }
    }

    public void StayedPlayerThreeTurns()
    {
        if (jailManager.TurnsInJail[playerNumber] == 3)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            ///this.SetNextEvent(EventType.EscapeJail);
        }
        else
        {
            jailManager.TurnsInJail[playerNumber] += 1;
            ///this.SetNextEvent(EventType.EndTurn);
        }
    }

    protected void SetNextEvent(Event gameEvent)
    {

        this.delegator.nextEvent = gameEvent.Start;
    }
}
