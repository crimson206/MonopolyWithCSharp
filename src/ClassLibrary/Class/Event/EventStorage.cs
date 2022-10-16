
public class EventStoragy
{
    public Event previousEvent;
    public EventStoragy(Event previousEvent)
    {
        this.previousEvent = previousEvent;
    }
    public BankEvent BankEvent => new BankEvent(this.previousEvent);
    public BoardEvent BoardEvent => new BoardEvent(this.previousEvent);
    public IndependentEvent IndependentEvent => new IndependentEvent(this.previousEvent);
    public TileEvent TileEvent => new TileEvent(this.previousEvent);

}