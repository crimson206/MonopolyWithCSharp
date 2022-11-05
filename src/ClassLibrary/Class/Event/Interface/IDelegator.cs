public interface IDelegator
{
    public void SetNextEvent(Action gameEvent);
    public void RunEvent();
    public void SwitchEvent(EventType fromEvent, EventType toEvent);
}
