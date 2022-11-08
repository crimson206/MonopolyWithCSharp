public class Delegator
{
    private DelEvent? reservedEvent;
    private DelEvent? nextEvent;
    private List<IVisitor> events = new List<IVisitor>();

    public delegate void DelEvent();

    public void SetNextEvent(Action gameEvent)
    {
        DelEvent receivedEvent = new DelEvent(gameEvent);
        this.reservedEvent = receivedEvent;
    }

    public void RunEvent()
    {
        this.nextEvent = this.reservedEvent;
        this.reservedEvent = null;
        this.nextEvent!();
    }
}
