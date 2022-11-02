public class EventStateMachine : IObserver
{
    protected EventState myEventState;
    protected EventStatesConnector eventStatesConnector;
    public EventStateMachine(EventStatesConnector eventStatesConnector)
    {
        this.eventStatesConnector = eventStatesConnector;
        this.eventStatesConnector.Register(this);
    }
    public void Update()
    {
        if (this.myEventState == eventStatesConnector.CurrentEventState)
        {
            TransitionFromIdle();
        }
    }
    protected virtual void TransitionFromIdle(){}
}
