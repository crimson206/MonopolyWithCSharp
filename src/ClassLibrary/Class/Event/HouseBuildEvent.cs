public class HouseBuildEvent
{

    private Action lastEvent;
    private IEvents? events;
    private Delegator delegator;
    private EventFlow eventFlow;

    public HouseBuildEvent(Delegator delegator, IStatusHandlers statusHandlers)
    {
        this.delegator = delegator;
        this.lastEvent = this.StartBuildingHouse;
        this.eventFlow = statusHandlers.EventFlow;
    }

    public void StartBuildingHouse()
    {
        this.eventFlow.RecommendedString = "The house build event was called";
        this.CallNextEvent();
    }

    public void SetEvents(IEvents events)
    {
        this.events = events;
    }

    public void CallNextEvent()
    {
        if (this.lastEvent == this.StartBuildingHouse)
        {
            this.events!.MainEvent.AddNextEvent(this.events!.MainEvent.EndTurn);
        }
    }

    public void AddNextEvent(Action nextEvent)
    {
        this.lastEvent = nextEvent;

        this.delegator
            .SetNextEvent(nextEvent);
    }

}