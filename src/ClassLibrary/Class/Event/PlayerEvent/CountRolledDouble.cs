public class CountRolledDouble : Event
{

    private DoubleSideEffectHandler doubleSideEffectHandler => DoubleSideEffectHandlerTaker.doubleSideEffectHandler!;
    private DoubleSideEffectHandlerTaker DoubleSideEffectHandlerTaker;
       
    public CountRolledDouble(Event previousEvent) : base(previousEvent)
    {
        this.DoubleSideEffectHandlerTaker = new DoubleSideEffectHandlerTaker(this.handlerDistrubutor);
    }

    public void Pass()
    {
        bool RolledDouble = this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];
        if (CopyConditionBool(RolledDouble))
        {
            this.doubleSideEffectHandler.SetExtraTurn(this.playerNumber, true);
            this.doubleSideEffectHandler.CountDouble(this.playerNumber);
        }
    }
}
