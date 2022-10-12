public abstract class Event
{


    protected Delegator? delegator;
    protected EventStorage eventStorage;
    public Event(EventStorage eventStorage, Delegator delegator)
    {
        this.eventStorage = eventStorage;
        this.delegator = delegator;
    }
    public abstract void Start();
    protected abstract void SetNextEvent(Event gameEvent);
}
