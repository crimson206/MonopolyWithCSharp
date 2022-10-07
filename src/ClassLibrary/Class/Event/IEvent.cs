public abstract class Event
{
    protected abstract void SetNextEvent(Delegator delegator, EventType nextEvent);
}
