public class Delegator : IDelegator
{
    private DelEvent? nextEvent;
    private List<IResponseToSwitchEvent> events = new List<IResponseToSwitchEvent>();

    public delegate void DelEvent();

    public void SetNextEvent(Action gameEvent)
    {
        DelEvent receivedEvent = new DelEvent(gameEvent);
        this.nextEvent = receivedEvent;
    }

    public void Attach(IResponseToSwitchEvent gameEvent)
    {
        this.events.Add(gameEvent);
    }

    public void SwitchEvent(EventType fromEvent, EventType toEvent)
    {
        foreach (var gameEvent in this.events)
        {
            gameEvent.ResponseToSwitchEvent(fromEvent, toEvent);
        }
    }

    public void RunEvent()
    {
        this.nextEvent!();
    }
}
