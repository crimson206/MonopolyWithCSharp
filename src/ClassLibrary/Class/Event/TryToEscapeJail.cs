public class TryToEscapeJail : Event
{
    private JailManager jailManager;
    private Bank bank;
    private DecisionMakerStorage decisionMakers;
    private TryToExcapeJailStrings recommendedStringFor = new TryToExcapeJailStrings();
    private Random random = new Random();
    private int playerNumber => this.delegator.CurrentPlayerNumber;
    private int amountFreeJailCard => this.jailManager.FreeJailCards[playerNumber];
    private int turnsInJail => this.jailManager.TurnsInJail[playerNumber];

    public TryToEscapeJail(EventStorage eventStorage, Delegator delegator, JailManager jailManager, Bank bank, DecisionMakerStorage decisionMakerStorage) : base(eventStorage, delegator)
    {
        this.decisionMakers = decisionMakerStorage;
        this.jailManager = jailManager;
        this.bank = bank;
    }

    public override void Start()
    {
        this.delegator.RecommendedString = this.recommendedStringFor.Start(playerNumber);

        this.delegator.nextEvent = this.CanPlayerUseJailCard;
    }

    public void CanPlayerUseJailCard()
    {

        if (jailManager.FreeJailCards[playerNumber] != 0)
        {
            this.delegator.decisionMaking = this.decisionMakers.wantToUseJailFreeCard.MakeDecision;

            this.delegator.RecommendedString = this.recommendedStringFor.CanPlayerUseJailFreeCard(playerNumber, amountFreeJailCard);
            
            this.delegator.nextEvent = this.WantPlayerUseJailFreeCard;
        }
        else 
        { 
            this.delegator.nextEvent = this.CanPlayerPayJailFine;
        }
    }

    private void WantPlayerUseJailFreeCard()
    {
        if (this.delegator.BoolDecision == true)
        {
            jailManager.TurnsInJail[playerNumber] = 0;

            this.SetNextEvent(events.rollToMove);
        }
        else
        {
            this.delegator.nextEvent = this.CanPlayerPayJailFine;
        }

    }

    private void CanPlayerPayJailFine()
    {
        if (bank.Balances[playerNumber] >= bank.JailFine)
        {
            this.delegator.decisionMaking = this.decisionMakers.wantToPayJailFine.MakeDecision;

            this.delegator.RecommendedString = this.recommendedStringFor.CanPlayerPayJailFine(playerNumber);

            delegator.nextEvent = this.WantPlayerPayJailFine;
        }
        else
        {
            this.delegator.nextEvent = this.RollPlayerDiceToEscape;
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
        else
        {
            delegator.nextEvent = this.RollPlayerDiceToEscape;
        }

    }

    private void RollPlayerDiceToEscape()
    {
        int[] rollDiceResult = Dice.Roll(random);
        this.delegator.RollDiceResult = rollDiceResult;

        this.delegator.RecommendedString = this.recommendedStringFor.RollPlayerDiceToEscape(playerNumber,rollDiceResult);

        delegator.nextEvent = this.RolledPlayerDouble;
    }

    private void RolledPlayerDouble()
    {
        int[] rollDiceResult = this.delegator.RollDiceResult;

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

            this.delegator.RecommendedString = this.recommendedStringFor.StaypedPlayerThreeTurnsInJailTrue(playerNumber);

            this.SetNextEvent(events.escapeJail);
        }
        else
        {
            jailManager.TurnsInJail[playerNumber] += 1;

            this.delegator.RecommendedString = this.recommendedStringFor.StaypedPlayerThreeTurnsInJailFalse(playerNumber);

            this.SetNextEvent(events.endTurn);
        }
    }

    protected override void SetNextEvent(Event gameEvent)
    {
        this.delegator.nextEvent = gameEvent.Start;
    }

}
