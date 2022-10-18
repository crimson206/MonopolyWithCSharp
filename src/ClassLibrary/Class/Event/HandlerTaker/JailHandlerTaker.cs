
public class JailHandlerTaker : IHandlerDistributorVisitor
{
    public JailHandler? jailHandler;
    private HandlerDistrubutor handlerDistrubutor;
    public JailHandlerTaker(HandlerDistrubutor handlerDistrubutor)
    {
        this.handlerDistrubutor = handlerDistrubutor;
        this.VisitHandlerDistributor();
    }
    public void SetJailHandler(JailHandler jailHandler)
    {
        this.jailHandler = jailHandler;
    }

    public void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptJailHandlerTaker(this);
    }
}
