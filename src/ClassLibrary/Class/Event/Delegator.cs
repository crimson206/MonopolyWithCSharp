public class Delegator
{
    private DelEvent? currentEvent;
    private DelEvent? nextEvent;
    private string nextEventName = string.Empty;


    public delegate void DelEvent();
    public DelEvent? NextEvent => this.nextEvent;
    public string NextEventName => this.nextEventName;

    public void SetNextEvent(Action gameEvent)
    {
        this.nextEvent = new DelEvent(gameEvent);
        this.nextEventName = gameEvent.Method.Name;
    }

    public void RunEvent()
    {
        this.currentEvent = this.nextEvent;
        this.nextEvent = null;
        this.nextEventName = string.Empty;
        this.currentEvent!();
    }
}
