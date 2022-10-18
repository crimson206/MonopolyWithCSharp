
public class DoubleSideEffectHandlerTaker : IHandlerDistributorVisitor
{
    public DoubleSideEffectHandler? doubleSideEffectHandler;
    private HandlerDistrubutor handlerDistrubutor;
    public DoubleSideEffectHandlerTaker(HandlerDistrubutor handlerDistrubutor)
    {
        this.handlerDistrubutor = handlerDistrubutor;
        this.VisitHandlerDistributor();
    }
    public void SetDoubleSideEffectHandler(DoubleSideEffectHandler doubleSideEffectHandler)
    {
        this.doubleSideEffectHandler = doubleSideEffectHandler;
    }

    public void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptDoubleSideEffectHandlerTaker(this);
    }
}
