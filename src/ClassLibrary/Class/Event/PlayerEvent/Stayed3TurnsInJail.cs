public class Stayed3TurnsInJail : Event
{
    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;
    public Stayed3TurnsInJail(Event previousEvent) : base(previousEvent)
    {
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
    }

    public void IfTrue_ResetAndAddEvent()
    {
        if (CopyConditionBool( this.jailHandler.TurnsInJailCounts[playerNumber] > 2 ))
        {
            this.bankHandler.DecreaseBalance(this.playerNumber, this.bankHandler.JailFine);
            this.jailHandler.ResetTurnInJail(this.playerNumber);

            this.eventFlow.RecommentedString = this.stringPlayer + " already stayed 3 turns in jail. Pay the jail fine and escape";

            this.delegator.ResetAndAddFollowingEvent = this.events.MoveByRollDiceTotal.Pass;
            this.delegator.AddFollowingEvent = this.events.PassedGoToReceiveSalary.Pass;
            this.delegator.AddFollowingEvent = this.events.LandOnTile.ResetAndAddEvent;
        }
    }
}
