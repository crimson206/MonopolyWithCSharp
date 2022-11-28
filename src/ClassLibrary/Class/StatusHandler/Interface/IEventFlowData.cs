public interface IEventFlowData
{
    public int CurrentPlayerNumber { get; }

    public string RecommendedString { get; }

    public bool BoolDecision { get; }

    public int[] RollDiceResult { get; }

    public bool AskedManualBoolDecision { get; }

    public int Turn { get; }
}
