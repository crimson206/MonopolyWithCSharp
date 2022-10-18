public class CheckRolledDouble3Times : Event
{
    private DoubleSideEffectHandler doubleSideEffectHandler => DoubleSideEffectHandlerTaker.doubleSideEffectHandler!;
    private DoubleSideEffectHandlerTaker DoubleSideEffectHandlerTaker;

    public CheckRolledDouble3Times(Event previousEvent) : base(previousEvent)
    {
        this.DoubleSideEffectHandlerTaker = new DoubleSideEffectHandlerTaker(this.handlerDistrubutor);
    }
    
    public void IfTrue_ResetAndAddEvent()
    {
        if (this.CopyConditionBool(this.doubleSideEffectHandler.DoubleCounts[this.playerNumber] == 3))
        {
            this.delegator.ResetAndAddFollowingEvent = this.events.HasJailPenalty.ResetAndAddEvent;
        }
    }
}
