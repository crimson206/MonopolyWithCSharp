
public class RollToMove : Event
{
    private DoubleSideEffectManager doubleSideEffectManager;
    public RollToMove(EventStorage eventStorage, Delegator delegator, DoubleSideEffectManager doubleSideEffectManager) : base(eventStorage, delegator)
    {
        this.doubleSideEffectManager = doubleSideEffectManager;
        this.delegator= delegator;
        this.delegator.nextEvent = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;
    public override void Start()
    {
        doubleSideEffectManager.ExtraTurns[playerNumber] = false;
        delegator.nextEvent = this.RolledPlayerDouble;
    }

    private void RolledPlayerDouble()
    {
        int[] rollDiceResult = this.delegator!.RollDiceResult;
        bool rolledPlayerDouble = rollDiceResult[0] == rollDiceResult[1];

        if (rolledPlayerDouble == true)
        {
            doubleSideEffectManager.DoubleCounts[playerNumber]++;
            delegator.nextEvent = this.RolledPlayerDoubleThreeTimes;
        }
        else
        {
            ///this.SetNextEvent(EventType.Move);
        }
    }

    private void RolledPlayerDoubleThreeTimes()
    {
        if ( doubleSideEffectManager.DoubleCounts[playerNumber] == 3)
        {
            ///this.SetNextEvent(EventType.GoToJail);
        }
        else
        {
            doubleSideEffectManager.ExtraTurns[playerNumber] = true;
            ///this.SetNextEvent(EventType.Move);
        }
    }
    protected override void SetNextEvent(Event gameEvent)
    {
        this.delegator!.nextEvent = gameEvent.Start;
    }
}
