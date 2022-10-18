
public class WantPayJailFine : Event
{
    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;
    public WantPayJailFine(Event previousEvent) : base(previousEvent)
    {
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
    }

    public void IfTrue_ResetAndAddEvent()
    {
        if ( this.CopydecisionBool( this.bankHandler.Balances[playerNumber] > 3 * this.bankHandler.JailFine ))
        {
            this.bankHandler.DecreaseBalance(this.playerNumber, this.bankHandler.JailFine);
            this.jailHandler.ResetTurnInJail(this.playerNumber);

            this.eventFlow.RecommentedString = this.stringPlayer + " paid the jail fine.";

            this.delegator.ResetAndAddFollowingEvent = this.events.RollDice.Pass;
            this.delegator.AddFollowingEvent = this.events.CountRolledDouble.Pass;
            this.delegator.AddFollowingEvent = this.events.MoveByRollDiceTotal.Pass;
            this.delegator.AddFollowingEvent = this.events.PassedGoToReceiveSalary.Pass;
            this.delegator.AddFollowingEvent = this.events.LandOnTile.ResetAndAddEvent;
        }
    }

}
