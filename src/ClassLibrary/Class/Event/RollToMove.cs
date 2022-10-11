
public class RollToMove : Event
{
    public RollToMove(Delegator delegator) : base(delegator)
    {
        this.delegator= delegator;
        this.delegator.rollToMove = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;
    private void Start(DoubleSideEffectManager doubleSideEffectManager)
    {
        doubleSideEffectManager.ExtraTurns[playerNumber] = false;
        this.delegator!.playerRollDice = true;
        delegator.rollToMove = this.RolledPlayerDouble;
    }

    private void RolledPlayerDouble(DoubleSideEffectManager doubleSideEffectManager)
    {
        int[] rollDiceResult = this.delegator!.PlayerRollDiceResult;
        bool rolledPlayerDouble = rollDiceResult[0] == rollDiceResult[1];

        if (rolledPlayerDouble == true)
        {
            doubleSideEffectManager.DoubleCounts[playerNumber]++;
            delegator.rollToMove = this.RolledPlayerDoubleThreeTimes;
        }
        else
        {
            this.SetNextEvent(EventType.Move);
        }
    }

    private void RolledPlayerDoubleThreeTimes(DoubleSideEffectManager doubleSideEffectManager)
    {
        if ( doubleSideEffectManager.DoubleCounts[playerNumber] == 3)
        {
            this.SetNextEvent(EventType.GoToJail);
        }
        else
        {
            doubleSideEffectManager.ExtraTurns[playerNumber] = true;
            this.SetNextEvent(EventType.Move);
        }
    }
    protected override void SetNextEvent(EventType nextEvent)
    {
        this.delegator!.nextEvent = nextEvent;
        this.delegator.rollToMove = this.Start;
    }
}
