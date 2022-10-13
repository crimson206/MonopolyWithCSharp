
public class EscapeJail : Event
{

    public EscapeJail(EventStorage eventStorage, Delegator delegator) : base(eventStorage, delegator)
    {
        this.delegator.nextEvent = this.Start;
    }
    public override void Start()
    {
        throw new NotImplementedException();
    }
    protected override void SetNextEvent(Event gameEvent)
    {
        throw new NotImplementedException();
    }
}
