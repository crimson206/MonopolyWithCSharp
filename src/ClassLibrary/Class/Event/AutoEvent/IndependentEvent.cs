public class IndependentEvent : Event
{

    private string stringRollDiceResult => this.ConvertRollDiceResultToString();
    public IndependentEvent(DataCenter dataCenter, Delegator delegator, BoolCopier boolCopier, EventFlow eventFlow, HandlerDistrubutor handlerDistrubutor)
    :base (dataCenter, delegator, boolCopier, eventFlow, handlerDistrubutor)
    {}
    public IndependentEvent(Event previousEvent) : base(previousEvent)
    {
    }

    public void StartTurn()
    {
        if (this.jailData.TurnsInJailCounts[playerNumber] != 0)
        {
            throw new NotImplementedException();
        }
        else
        {
            this.newEvent = this.events.DoubleSideEffectUser.RollDiceWithCountingDouble;
        }
    }

    public void RollDice()
    {
        this.eventFlow.RoolDiceResult = Dice.Roll(this.random);
        this.eventFlow.RecommentedString = this.stringPlayer + " rolled " + this.stringRollDiceResult;
    }

    private string ConvertRollDiceResultToString()
    {
        return String.Join(", ", (from dieValue in this.eventFlow.RoolDiceResult select dieValue.ToString()));
    }

}
