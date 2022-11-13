public abstract class Event
{
    protected Action lastEvent;
    protected IEvents? events;
    protected Delegator delegator;
    protected IDataCenter dataCenter;

    public Event(Delegator delegator, IDataCenter dataCenter)
    {
        this.delegator = delegator;
        this.dataCenter = dataCenter;

        this.lastEvent = this.StartEvent;
    }

    public void SetEvents(Events events)
    {
        this.events = events;
    }
    protected abstract void CallNextEvent();
    public abstract void StartEvent();

    public void AddNextEvent(Action nextEvent)
    {
        this.lastEvent = nextEvent;

        this.delegator
            .SetNextEvent(nextEvent);
    }
}