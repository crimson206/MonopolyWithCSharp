
public class DelegatorData : ICloneable
{
    private Delegator delegator;
    public DelegatorData(Delegator delegator)
    {
        this.delegator = delegator;
    }

    ///public int CurrentPlayerNumber => delegator.CurrentPlayerNumber;
    ///public string RecommendedString => delegator.RecommendedString;
    ///public int[] PlayerRollDiceResult => delegator.PlayerRollDiceResult;
    ///public bool PlayerBoolDecision => delegator.PlayerBoolDecision;

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        DelegatorData clone = (DelegatorData) this.MemberwiseClone();
        return clone;
    }
}
