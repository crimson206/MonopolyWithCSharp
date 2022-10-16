
public class EventStoragy
{
    public Event previousEvent;
    public EventStoragy(Event previousEvent)
    {
        this.previousEvent = previousEvent;
    }
    public BankHandlerUserEvent BankEvent => new BankHandlerUserEvent(this.previousEvent);
    public BoardHandlerUserEvent BoardHandlerUser => new BoardHandlerUserEvent(this.previousEvent);
    public IndependentEvent IndependentEvent => new IndependentEvent(this.previousEvent);
    public TileManagerUserEvent TileManagerUserEvent => new TileManagerUserEvent(this.previousEvent);
    public DoubleSideEffectUserEvent DoubleSideEffectUser => new DoubleSideEffectUserEvent(this.previousEvent);

}