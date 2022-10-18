public class StartTurn : Event
{

    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;

    public StartTurn(Event previousEvent) : base(previousEvent)
    {
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
    }
    
    public StartTurn(Delegator delegator, BoolCopier boolCopier, EventFlow eventFlow, HandlerDistrubutor handlerDistrubutor)
    :base (delegator, boolCopier, eventFlow, handlerDistrubutor)
    {
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
    }


    public void ResetAndAddEvent()
    {

        /// has jail turn
        if (this.CopyConditionBool(this.jailHandler.TurnsInJailCounts[this.playerNumber] != 0) )
        {
            this.eventFlow.RecommentedString = this.stringPlayer + " starts a turn in jail";

            if (this.CopyConditionBool(this.jailHandler.JailFreeCardCounts[this.playerNumber] > 0))
            {

                this.delegator.ResetAndAddFollowingEvent = this.events.WantUseJailFreeCard.IfTrue_ResetAndAddEvent;

            }

            if (this.CopyConditionBool(this.bankHandler.Balances[this.playerNumber] >= this.bankHandler.JailFine))
            {
                this.delegator.ResetAndAddFollowingEvent = this.events.WantPayJailFine.IfTrue_ResetAndAddEvent;
            }
            else
            {
                this.delegator.ResetAndAddFollowingEvent = this.events.RollDice.Pass;
                this.delegator.AddFollowingEvent = this.events.IfRolledDouble_EscapeJail.IfTrue_ResetAndAddEvent;
                this.delegator.AddFollowingEvent = this.events.Stayed3TurnsInJail.IfTrue_ResetAndAddEvent;
                this.delegator.AddFollowingEvent = this.events.StayInJail.Pass;
                this.delegator.AddFollowingEvent = this.events.EndTurn.ResetAndAddEvent;
            }

        }

        /// has normal turn
        else
        {
            this.eventFlow.RecommentedString = this.stringPlayer + " starts a turn";

            this.delegator.ResetAndAddFollowingEvent = this.events.RollDice.Pass;
            /// turn on extra turn here as well
            /// Make event IfRolledDouble3Times_GoToJail.IfTrue_ResetAndAddEvent; but not need here
            this.delegator.AddFollowingEvent = this.events.CountRolledDouble.Pass;
            this.delegator.AddFollowingEvent = this.events.MoveByRollDiceTotal.Pass;
            this.delegator.AddFollowingEvent = this.events.PassedGoToReceiveSalary.Pass;
            this.delegator.AddFollowingEvent = this.events.LandOnTile.ResetAndAddEvent;
        }

    }

}
