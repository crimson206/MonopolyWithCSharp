public class EventStatesConnector : IRegister
{
    public EventState CurrentEventState 
    {
        get
        {
            return this.CurrentEventState;
        }
        set
        {
            this.CurrentEventState = value;
            this.Notify();
        }
    }
    private List<IObserver> EventStateMachines = new List<IObserver> ();
    public void Register(IObserver eventStateMachine)
    {
        EventStateMachines.Add(eventStateMachine);
    }
    private void Notify()
    {
        foreach (var eventStateMachine in EventStateMachines)
        {
            eventStateMachine.Update();
        }
    }
}