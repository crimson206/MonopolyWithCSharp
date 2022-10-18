public class CheckBankrupt : Event
{
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;

    public CheckBankrupt(Event previousEvent) : base(previousEvent)
    {
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
    }

    public void IfTrue_ResetAndAddEvent()
    {
        if (this.CopyConditionBool(this.bankHandler.Balances[this.playerNumber] < 0))
        {
            throw new NotImplementedException();
        }
    }
}
