public class CheckAndDoExtraTurn : Event
{
    private DoubleSideEffectHandler doubleSideEffectHandler => doubleSideEffectHandlerTaker.doubleSideEffectHandler!;
    private DoubleSideEffectHandlerTaker doubleSideEffectHandlerTaker;

    public CheckAndDoExtraTurn(Event previousEvent) : base(previousEvent)
    {
        this.doubleSideEffectHandlerTaker = new DoubleSideEffectHandlerTaker(this.handlerDistrubutor);
    }

    public void ResetAndAddEvent()
    {
        bool extraTurn = this.doubleSideEffectHandler.ExtraTurns[this.playerNumber];
        if (CopyConditionBool(extraTurn is false))
        {
            this.delegator.ResetAndAddFollowingEvent = this.events.EndTurn.ResetAndAddEvent;
        }
        else
        {
            this.doubleSideEffectHandler.SetExtraTurn(this.playerNumber, false);
            this.eventFlow.RecommentedString = this.stringPlayer + "rolled double last time, so roll one more time";

            this.delegator.ResetAndAddFollowingEvent = this.events.RollDice.Pass;
            this.delegator.AddFollowingEvent = this.events.CountRolledDouble.Pass;
            this.delegator.AddFollowingEvent = this.events.CheckRolledDouble3Times.IfTrue_ResetAndAddEvent;
            this.delegator.AddFollowingEvent = this.events.MoveByRollDiceTotal.Pass;
            this.delegator.AddFollowingEvent = this.events.LandOnTile.ResetAndAddEvent;
        }
    }
}
