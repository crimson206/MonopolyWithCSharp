
public class BankEvent : Event
{
    private BankHandler? bankHandler;
    private int salary => bankHandler!.Salary;
    public BankEvent(Event previousEvent) : base(previousEvent)
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

    private void VisitHandlerDistributor(HandlerDistrubutor handlerDistrubutor)
    {
        handlerDistrubutor.AcceptBankEvent(this);
    }

}
