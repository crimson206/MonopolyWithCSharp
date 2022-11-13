public class HouseBuildEvent : Event
{
    private EventFlow eventFlow;

    public HouseBuildEvent
    (Delegator delegator,
    IDataCenter dataCenter,
    IStatusHandlers statusHandlers)
        :base
        (delegator,
        dataCenter)
    {
        this.eventFlow = statusHandlers.EventFlow;
    }

    public override void StartEvent()
    {
        this.eventFlow.RecommendedString = "The house build event was called";
        this.CallNextEvent();
    }

    protected override void CallNextEvent()
    {
        if (this.lastEvent == this.StartEvent)
        {
            this.events!.MainEvent.AddNextEvent(this.events!.MainEvent.EndTurn);
        }
    }

}