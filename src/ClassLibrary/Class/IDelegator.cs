public interface IDelegator
{
    public void ResetAndAddEvent(Action gameEvent);
    public void SetNextEvent(Action gameEvent);
    public delegate void DelEvent();
    public void RunEvent();
}
