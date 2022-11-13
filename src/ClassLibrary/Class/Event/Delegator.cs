public class Delegator
{
    private DelEvent? currentEvent;
    private DelEvent? nextEvent;
    private string nextEventName = string.Empty;
    private string lastEventName = string.Empty;


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
        if (this.nextEvent is null)
        {
            throw new Exception();
        }

        this.currentEvent = this.nextEvent;
        this.nextEvent = null;
        this.lastEventName = this.nextEventName;
        this.nextEventName = string.Empty;
        this.currentEvent!();
    }
}
