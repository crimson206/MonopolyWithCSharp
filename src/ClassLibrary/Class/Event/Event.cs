public abstract class Event
{
    public abstract void Start();
    protected Delegator? delegator;
    protected EventStorage eventStorage;
    public Event(EventStorage eventStorage, Delegator delegator)
    {
        this.eventStorage = eventStorage;
        this.delegator = delegator;
    }
}
