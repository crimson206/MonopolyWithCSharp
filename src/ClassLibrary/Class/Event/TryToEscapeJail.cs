public class TryToEscapeJail : Event
{
    private JailManager jailManager;
    private Bank bank;
    private DecisionMakerStorage decisionMakers;
    private Random random = new Random();
    private int playerNumber => this.delegator.CurrentPlayerNumber;
    public TryToEscapeJail(EventStorage eventStorage, Delegator delegator, JailManager jailManager, Bank bank, DecisionMakerStorage decisionMakerStorage) : base(eventStorage, delegator)
    {
        this.decisionMakers = decisionMakerStorage;
        this.jailManager = jailManager;
        this.bank = bank;
    }

    public override void Start()
    {
        if (jailManager.FreeJailCards[playerNumber] != 0)
        {
            this.delegator.makeDecision = decisionMakers.wantToUseJailFreeCard.MakeDecision;
            this.delegator.nextEvent = this.WantPlayerUseJailFreeCard;
        }
        else
        {
            this.delegator.nextEvent = this.WantPlayerPayJailFine;
        }

    }

    private void WantPlayerUseJailFreeCard()
    {
        if (this.delegator.BoolDecision == true)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(events.rollToMove);
        }
        else if (this.delegator.BoolDecision == false)
        {
            this.delegator.makeDecision = decisionMakers.wantToPayJailFine.MakeDecision;
            delegator.nextEvent = this.WantPlayerPayJailFine;
        }
        else
        {
            throw new Exception();
        }
    }

    private void WantPlayerPayJailFine()
    {
        if (this.delegator.BoolDecision == true)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            bank.DecreaseBalance(playerNumber, jailManager.JailFine);
            this.SetNextEvent(events.rollToMove);
        }
        else if (this.delegator.BoolDecision == true)
        {
            delegator.nextEvent = this.RolledPlayerDouble;
        }
        else
        {
            throw new Exception();
        }
    }

    private void RolledPlayerDouble()
    {
        int[] rollDiceResult = Dice.Roll(random);
        this.delegator.RollDiceResult = rollDiceResult;

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

    private void StayedPlayerThreeTurns()
    {
        if (jailManager.TurnsInJail[playerNumber] == 3)
        {
            jailManager.TurnsInJail[playerNumber] = 0;
            this.SetNextEvent(events.escapeJail);
        }
        else
        {
            jailManager.TurnsInJail[playerNumber] += 1;
            this.SetNextEvent(events.endTurn);
        }
    }

    protected override void SetNextEvent(Event gameEvent)
    {
        this.delegator.nextEvent = gameEvent.Start;
    }
}
