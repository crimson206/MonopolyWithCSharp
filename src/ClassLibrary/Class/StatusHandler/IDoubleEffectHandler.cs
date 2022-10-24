
public interface IDoubleSideEffectHandler
{
    public List<int> DoubleCounts { get; }
    public List<bool> ExtraTurns { get; }
    public void CountDouble(int playerNumber);

    public void ResetDoubleCount(int playerNumber);

    public void SetExtraTurn(int playerNumber, bool extraTurn);

}
