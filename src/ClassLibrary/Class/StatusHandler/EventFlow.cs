public class EventFlow
{
    public int CurrentPlayerNumber { get; set; }

    public string RecommendedString { get; set; } = string.Empty;

    public bool BoolDecision { get; set; }

    public int[] RollDiceResult { get; set; } = new int [2];

    public bool AskedManualBoolDecision { get; set; }
}
