public class BankHandlerTaker : IHandlerDistributorVisitor
{
    public BankHandler? bankHandler;
    private HandlerDistrubutor handlerDistrubutor;
    public BankHandlerTaker(HandlerDistrubutor handlerDistrubutor)
    {
        this.handlerDistrubutor = handlerDistrubutor;
        this.VisitHandlerDistributor();
    }
    public void SetBankHandler(BankHandler bankHandler)
    {
        this.bankHandler = bankHandler;
    }

    public void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptBankHandlerTaker(this);
    }
}
