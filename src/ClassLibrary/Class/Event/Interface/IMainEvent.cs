public interface IMainEvent : IEvent
{
    public void CheckExtraTurn();

    public void EndEvent();
    public void StartEvent();
}