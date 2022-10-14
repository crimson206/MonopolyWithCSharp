public abstract class Event
{
    protected Delegator delegator;
    protected EventStorage events;
    public Event(EventStorage eventStorage, Delegator delegator)
    {
        this.events = eventStorage;
        this.delegator = delegator;
    }
    public abstract void Start();
    protected abstract void SetNextEvent(Event gameEvent);
}
