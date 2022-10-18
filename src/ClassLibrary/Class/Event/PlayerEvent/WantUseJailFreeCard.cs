public class WantUseJailFreeCard : Event
{
    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;
    
    public WantUseJailFreeCard(Event previousEvent) : base(previousEvent)
    {
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);

    }

    public void IfTrue_ResetAndAddEvent()
    {
        if ( this.CopydecisionBool( true ))
        {
            this.jailHandler.RemoveAJailFreeCard(this.playerNumber);
            this.jailHandler.ResetTurnInJail(this.playerNumber);

            this.eventFlow.RecommentedString = this.stringPlayer + " used a jail free card.";

            this.delegator.ResetAndAddFollowingEvent = this.events.RollDice.Pass;
            this.delegator.AddFollowingEvent = this.events.CountRolledDouble.Pass;
            this.delegator.AddFollowingEvent = this.events.MoveByRollDiceTotal.Pass;
            this.delegator.AddFollowingEvent = this.events.LandOnTile.ResetAndAddEvent;
        }
    }

}
