
public class DoubleSideEffectUserEvent : HandlerUserEvent
{
    
    private string stringRollDiceResult => this.ConvertRollDiceResultToString();
    private DoubleSideEffectHandler? doubleSideEffectHandler;
    public DoubleSideEffectUserEvent(Event previousEvent) : base(previousEvent)
    {
    }


    public void RollDiceWithCountingDouble()
    {
        int[] rollDiceResult = Dice.Roll(this.random);
        this.eventFlow.RoolDiceResult = rollDiceResult;
        this.eventFlow.RecommentedString = this.stringPlayer + " rolled " + this.stringRollDiceResult;
        if (rollDiceResult[0] == rollDiceResult[1])
        {
            this.doubleSideEffectHandler!.CountDouble(this.playerNumber);
        }

        if (this.doubleSideEffectData.DoubleCounts[playerNumber] > 3)
        {
            this.newEvent = this.events.BoardHandlerUser.GoToJailDoubleSideEffect;
        }
        else
        {
            this.newEvent = this.events.BoardHandlerUser.Move;
        }

    }

    public void SetDoubleSideEffectHandler(DoubleSideEffectHandler doubleSideEffectHandler)
    {
        this.doubleSideEffectHandler = doubleSideEffectHandler;
    }

    protected override void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptDoubleSideEffectEvent(this);
    }

    private string ConvertRollDiceResultToString()
    {
        return String.Join(", ", (from dieValue in this.eventFlow.RoolDiceResult select dieValue.ToString()));
    }
}
