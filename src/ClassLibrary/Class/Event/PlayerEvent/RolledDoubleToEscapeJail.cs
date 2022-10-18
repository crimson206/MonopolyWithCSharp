public class RolledDoubleToEscapeJail : Event
{
    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;

    public RolledDoubleToEscapeJail(Event previousEvent) : base(previousEvent)
    {
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
    }

    public void IfTrue_ResetAndAddEvent()
    {
        bool RolledDouble = this.eventFlow.RollDiceResult[0] == this.eventFlow.RollDiceResult[1];
        if ( this.CopyConditionBool( RolledDouble))
        {
            this.jailHandler.ResetTurnInJail(this.playerNumber);
            this.eventFlow.RecommentedString = stringPlayer + "rolled double, and escape the jail";

            this.delegator.ResetAndAddFollowingEvent = this.events.MoveByRollDiceTotal.Pass;
            this.delegator.AddFollowingEvent = this.events.PassedGoToReceiveSalary.Pass;
            this.delegator.AddFollowingEvent = this.events.LandOnTile.ResetAndAddEvent;
        }

    }
}
