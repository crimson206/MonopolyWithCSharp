public interface IEvent
{
    public void SetEvents(IEvents events);
    public void AddNextAction(Action nextAction);
}