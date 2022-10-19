

public class Delegator
{
    private List<DelEvent> nextEvents = new List<DelEvent>();

    public DelEvent test;

    public void ResetAndAddEvent(Action gameEvent)
    {
        nextEvents.Clear();
        DelEvent newEvent = new DelEvent(gameEvent);
        nextEvents.Add(newEvent);
    }
    public void SetNextEvent(Action gameEvent)
    {
        DelEvent receivedEvent = new DelEvent(gameEvent);
        this.test = receivedEvent;
    }


    public delegate void DelEvent();

    /// it runs a function of events
    public void RunEvent()
    {
        this.test();
        var DebuggingBreakPoint = 0;
    }
}