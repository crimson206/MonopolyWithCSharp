public class Delegator
{
    private DelEvent? nextEvent;

    public delegate void DelEvent();

    public void SetNextEvent(Action gameEvent)
    {
        DelEvent receivedEvent = new DelEvent(gameEvent);
        this.nextEvent = receivedEvent;
    }

    public void RunEvent()
    {
        this.nextEvent!();
    }
}
