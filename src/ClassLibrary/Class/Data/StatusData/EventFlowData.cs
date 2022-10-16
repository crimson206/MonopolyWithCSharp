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
    public int[] RollDiceResult => new int[] {this.eventFlow.RoolDiceResult[0], this.eventFlow.RoolDiceResult[1]};
    public bool AskedEventManualBoolDecision => this.eventFlow.AskedManualBoolDecision;
    public bool ChangedNoticeably => this.eventFlow.ChangedNoticeably;

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        EventFlowData  clone = (EventFlowData ) this.MemberwiseClone();
        return clone;
    }
}