public abstract class Event
{
    protected abstract void SetNextEvent(EventType nextEvent);
    protected Delegator? delegator;
    public Event(Delegator delegator)
    {
        this.delegator = delegator;
    }
}
