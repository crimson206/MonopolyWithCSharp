
public class EventFlow
{
    public int CurrentPlayerNumber;
    public string RecommentedString = String.Empty;
    public bool BoolDecision;
    public int[] RoolDiceResult = new int [2];
    public bool AskedManualBoolDecision;
    public bool ChangedNoticeably;

}

public class EventFlowData
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
}