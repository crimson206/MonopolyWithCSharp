
public class RollToMove : Event
{
    private Data data;

    private void Start(Delegator delegator, int playerNumber, DoubleSideEffectManager doubleSideEffectManager)
    {
        doubleSideEffectManager.SetExtraTurn(playerNumber, false);
        delegator.playerRollDice = true;
        delegator.rollToMove = this.RolledPlayerDouble;
    }

    private void RolledPlayerDouble(Delegator delegator, int playerNumber, DoubleSideEffectManager doubleSideEffectManager)
    {
        int[] rollDiceResult = data.LastRollDiceResults[playerNumber];
        bool rolledPlayerDouble = rollDiceResult[0] == rollDiceResult[1];

        if (rolledPlayerDouble == true)
        {
            doubleSideEffectManager.CountDouble(playerNumber);
            delegator.rollToMove = this.RolledPlayerDoubleThreeTimes;
        }
        else
        {
            this.SetNextEvent(delegator, EventType.Move);
        }
    }

    private void RolledPlayerDoubleThreeTimes(Delegator delegator, int playerNumber, DoubleSideEffectManager doubleSideEffectManager)
    {
        if ( data.CountDoubles[playerNumber] == 3)
        {
            this.SetNextEvent(delegator, EventType.GoToJail);
        }
        else
        {
            doubleSideEffectManager.SetExtraTurn(playerNumber, true);
            this.SetNextEvent(delegator, EventType.Move);
        }
    }
    protected override void SetNextEvent(Delegator delegator, EventType nextEvent)
    {
        delegator.nextEvent = nextEvent;
        delegator.rollToMove = this.Start;
    }
}
