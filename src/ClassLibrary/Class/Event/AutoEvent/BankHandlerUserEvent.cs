
public class BankHandlerUserEvent : HandlerUserEvent
{
    private BankHandler? bankHandler;
    private int salary => bankHandler!.Salary;
    public BankHandlerUserEvent(Event previousEvent) : base(previousEvent)
    {
    }

    public void ReceiveSalary()
    {
        this.bankHandler!.GiveSalary(playerNumber);
        this.eventFlow.RecommentedString = this.stringPlayer + " received the salary";
    }


    public void SetBankHandler(BankHandler bankHandler)
    {
        this.bankHandler = bankHandler;
    }

    protected override void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptBankHandlerUserEvent(this);
    }

}
