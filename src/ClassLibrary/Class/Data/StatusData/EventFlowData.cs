public class EventFlowData : ICloneable
{
    private EventFlow eventFlow;

    public EventFlowData(EventFlow eventFlow)
    {
        this.eventFlow = eventFlow;
    }

    public int CurrentPlayerNumber => this.eventFlow.CurrentPlayerNumber;

    public string RecommentedString => this.eventFlow.RecommentedString;

    public bool BoolDecision => this.eventFlow.BoolDecision;

    public int[] RollDiceResult => new int[] { this.eventFlow.RollDiceResult[0], this.eventFlow.RollDiceResult[1] };

    public bool AskedEventManualBoolDecision => this.eventFlow.AskedManualBoolDecision;

    public bool ChangedNoticeably => this.eventFlow.ChangedNoticeably;

    public object Clone()
    {
        EventFlowData clone = (EventFlowData)this.MemberwiseClone();
        return clone;
    }
}