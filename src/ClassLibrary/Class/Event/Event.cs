public abstract class Event : IResponseToSwitchEvent, IVisitor, IGroupable
{
    protected List<IResponseToSwitchEvent> eventGroup = new List<IResponseToSwitchEvent>();

    protected void SwitchEvent(EventType fromEvent, EventType toEvent)
    {

    }

    public void ResponseToSwitchEvent(EventType fromEvent, EventType toEvent)
    {
        if (toEvent is EventType.AuctionEvent)
        {

        }
    }
    public void Visit(IElement element)
    {
    }

    public void SetGroup(List<IGroupable> group)
    {
        foreach (var member in group)
        {
            IResponseToSwitchEvent gameEvent = (IResponseToSwitchEvent)member; 
            this.eventGroup.Add(gameEvent);
        }
    }
}