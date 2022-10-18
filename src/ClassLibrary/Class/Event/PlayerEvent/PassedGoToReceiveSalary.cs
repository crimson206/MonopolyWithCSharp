public class PassedGoToReceiveSalary : Event
{
    private BoardHandler boardHandler => boardHandlerTaker.boardHandler!;
    private BoardHandlerTaker boardHandlerTaker;
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;
    public PassedGoToReceiveSalary(Event previousEvent) : base(previousEvent)
    {
        this.boardHandlerTaker = new BoardHandlerTaker(this.handlerDistrubutor);
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
    }

    public void Pass()
    {
        bool passedGo = this.boardHandler.PlayerPassedGo[this.playerNumber];
        if ( this.CopyConditionBool(passedGo))
        {
            this.bankHandler.IncreaseBalance(playerNumber, this.bankHandler.Salary);
            this.eventFlow.RecommentedString = this.stringPlayer + " received the salary";
        }
    }
}
