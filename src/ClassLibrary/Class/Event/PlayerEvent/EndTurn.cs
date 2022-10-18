public class EndTurn : Event
{
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;

    public EndTurn(Event previousEvent) : base(previousEvent)
    {
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
    }
    
    public void ResetAndAddEvent()
    {
        int playerCountInGame = this.bankHandler.Balances.Where(balance => balance >= 0).Count();

        if ( playerCountInGame == 1)
        {
            this.eventFlow.RecommentedString = "Congratulations!! " + this.stringPlayer + " won the game!";
        }
        else if( playerCountInGame < 1)
        {
            throw new Exception();
        }
        else
        {
            this.eventFlow.RecommentedString = this.stringPlayer + " ended this turn";
            this.eventFlow.CurrentPlayerNumber = this.CalculateNextPlayer();

            this.delegator.ResetAndAddFollowingEvent = this.events.StartTurn.ResetAndAddEvent;
        }
    }

    private int CalculateNextPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            int candidate = (this.playerNumber + 1 + i) % 4;
            if ( this.bankHandler.Balances[candidate] >= 0)
            {
                return candidate;
            }
        }

        throw new Exception();
    }
}
