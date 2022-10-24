public class EventStateMachine
{
    private bool? startedTurnInJail;
    List<IEvent> gameEvents = new List<IEvent>();
    public enum EventState
    {
        StartingTurn,
        CheckingImmediateJailEscape,
        RollingDiceToMove,
        RollingDiceInJail,
        HavingLandEvent,
        Auctioning,
        CheckingExtraTurn,
        EndingTurn,
        Building,
        Trading,
    }
    public enum Condition
    {
        IsInJail,
        DecidedToEscapeJail,
        RolledDoubleInJail,
        RejectedPropertyPurchase,
        HasExtraTurn,
        RolledDouble3TimeInRow,
        IsEventDone,
        IsGameOver
    }

    private bool? currentConditionBoolValue;
    private EventState currentState = EventState.StartingTurn;

    public void UpdateEventBool(bool  a)
    {

    }

    public EventState CurrentState
    {
        get { return this.currentState; }
    }

    public bool CurrentConditionBoolValue
    {
        set => this.currentConditionBoolValue = value;
    }

    public void Attach(IEvent gameEvent)
    {
        gameEvents.Add(gameEvent);
    }

    public void Notify()
    {
        foreach (var gameEvent in this.gameEvents)
        {
            gameEvent.Update();
        }
    }

    private void SetNextEventStateAfterStartingTurn(bool startedTurnInJail)
    {

    }

}