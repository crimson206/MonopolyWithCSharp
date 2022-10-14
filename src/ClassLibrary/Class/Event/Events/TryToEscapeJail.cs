    /// Understanding "nextEvent"! (this note starts from "Delegator.cs")
    /// 2. Welcome :) Are you from "Delegator.cs"?
    /// 3. Functions of Event Classed basically have 4 parts. See "CanPlayerUseJailCard"
    ///     - do something
    ///     - recommend string to delegator
    ///     - ask to make a decision
    ///     - set the next event
    /// 4. At the "set the next event" stage, another function of this class is assigned to the delegator.
    ///     For example, "CanPlayerUseJailCard()" was assigned to the delegator by "Start()"
    /// 5. Go to "Delegator.cs"
    /// 8. "CanPlayerUseJailCard()" 
    ///     - recommends delegator some string to be displayed
    ///     - tells delegator that we need a decision making
    ///     - set the next event of "WantPlayerUseJailFreeCard"
    /// 9. Go to "Game.cs"
    /// 12. As the Run() of Game keeps being called, the function to be called moves down.
    /// 13. In the end, it assign a "Start()" function of another event class to the "nextEvent".

public class TryToEscapeJail : Event
{
    private JailHandler jailManager;
    private BankHandler bank;
    private DecisionMakerStorage decisionMakers;
    private TryToExcapeJailStrings recommendedStringFor = new TryToExcapeJailStrings();
    private Random random = new Random();
    private int playerNumber => this.delegator.CurrentPlayerNumber;
    private int amountFreeJailCard => this.jailManager.FreeJailCards[playerNumber];
    private int turnsInJail => this.jailManager.TurnsInJail[playerNumber];

    public TryToEscapeJail(EventStorage eventStorage, Delegator delegator, JailHandler jailManager, BankHandler bank, DecisionMakerStorage decisionMakerStorage) : base(eventStorage, delegator)
    {
        this.decisionMakers = decisionMakerStorage;
        this.jailManager = jailManager;
        this.bank = bank;
    }

    public override void Start()
    {
        this.delegator.RecommendedString = this.recommendedStringFor.Start(playerNumber);

        this.delegator.NextEvent = this.CanPlayerUseJailCard;
    }

    private void CanPlayerUseJailCard()
    {

        if (jailManager.FreeJailCards[playerNumber] != 0)
        {
            /// Do something


            /// Recommend string to delegator
            this.delegator.RecommendedString = this.recommendedStringFor.CanPlayerUseJailFreeCard(playerNumber, amountFreeJailCard);

            /// Ask to make a decision
            this.delegator.DecisionMaking = this.decisionMakers.wantToUseJailFreeCard.MakeDecision;
            
            /// Set next event
            this.delegator.NextEvent = this.WantPlayerUseJailFreeCard;
        }
        else 
        { 
            this.delegator.NextEvent = this.CanPlayerPayJailFine;
        }
    }

    private void WantPlayerUseJailFreeCard()
    {
        if (this.delegator.BoolDecision == true)
        {
            jailManager.TurnsInJail[playerNumber] = 0;

            delegator.RecommendedString = this.recommendedStringFor.WantPlayerUseJailFreeCardTrue(playerNumber);

            this.SetNextEvent(events.rollToMove);
        }
        else
        {
            delegator.RecommendedString = this.recommendedStringFor.WantPlayerUseJailFreeCardFalse(playerNumber);

            this.delegator.NextEvent = this.CanPlayerPayJailFine;
        }

    }

    private void CanPlayerPayJailFine()
    {
        if (bank.Balances[playerNumber] >= bank.JailFine)
        {
            this.delegator.RecommendedString = this.recommendedStringFor.CanPlayerPayJailFine(playerNumber);

            this.delegator.DecisionMaking = this.decisionMakers.wantToPayJailFine.MakeDecision;

            delegator.NextEvent = this.WantPlayerPayJailFine;
        }
        else
        {
            this.delegator.NextEvent = this.RollPlayerDiceToEscape;
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
            delegator.NextEvent = this.RollPlayerDiceToEscape;
        }

    }

    private void RollPlayerDiceToEscape()
    {
        int[] rollDiceResult = Dice.Roll(random);
        this.delegator.RollDiceResult = rollDiceResult;

        this.delegator.RecommendedString = this.recommendedStringFor.RollPlayerDiceToEscape(playerNumber,rollDiceResult);

        delegator.NextEvent = this.RolledPlayerDouble;
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
            delegator.NextEvent = this.StayedPlayerThreeTurns;
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
        this.delegator.NextEvent = gameEvent.Start;
    }

}
