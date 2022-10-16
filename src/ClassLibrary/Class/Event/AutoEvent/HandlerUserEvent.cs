
public abstract class HandlerUserEvent : Event
{
    public HandlerUserEvent(Event previousEvent) : base(previousEvent)
    {
        this.VisitHandlerDistributor();
    }

    protected abstract void VisitHandlerDistributor();
}
