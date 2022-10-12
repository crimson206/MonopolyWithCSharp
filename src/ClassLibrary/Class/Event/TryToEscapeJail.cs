public class TryToEscapeJail : Event
{
    private JailManager jailManager;
    private Bank bank;
    private EventStorage events => this.eventStorage;
    private DecisionMakerStorage decisionMakers;
    private Random random = new Random();
    public TryToEscapeJail(EventStorage eventStorage, Delegator delegator, JailManager jailManager, Bank bank) : base(eventStorage, delegator)
    {
        this.jailManager = jailManager;
        this.bank = bank;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;
    public override void Start()
    {
        this.delegator!.makeDecision = decisionMakers.wantToUseJailFreeCard.MakeDecision;
        delegator.nextEvent = this.WantPlayerUseJailFreeCard;
    }

    public void WantPlayerUseJailFreeCard()
    {
        if (this.delegator!.BoolDecision == true)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(events.rollToMove);
        }
        else if (this.delegator!.BoolDecision == false)
        {
            ///this.delegator!.makeDecision = decisionMakers.wantToPayJailFine;
            delegator.nextEvent = this.WantPlayerPayJailFine;
        }
        else
        {
            throw new Exception();
        }
    }

    public void WantPlayerPayJailFine()
    {
        if (this.delegator!.BoolDecision == true)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            bank.Balances[playerNumber] -= jailManager.JailFine;
            ///this.SetNextEvent(EventType.RollToMove);
        }
        else if (this.delegator!.BoolDecision == true)
        {
            ///delegator.playerRollDice = true;
            delegator.nextEvent = this.RolledPlayerDouble;
        }
        else
        {
            throw new Exception();
        }
    }

    public void RolledPlayerDouble()
    {
        int[] rollDiceResult = Dice.Roll(random);
        this.delegator!.RollDiceResult = rollDiceResult;

        if (rollDiceResult[0] == rollDiceResult[1])
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(events.escapeJail);
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

    protected override void SetNextEvent(Event gameEvent)
    {
        this.delegator!.nextEvent = gameEvent.Start;
    }
}
