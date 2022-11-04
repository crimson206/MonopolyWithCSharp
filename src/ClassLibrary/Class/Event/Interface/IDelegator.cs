public interface IDelegator
{
    public abstract void SetNextEvent(Action gameEvent);
    public abstract void RunEvent();
}
