
public class DoubleSideEffectHandler
{
    private List<int> doubleCounts = new List<int> { 0, 0, 0, 0 };
    private List<bool> extraTurns = new List<bool> { false, false, false, false };

    public List<int> DoubleCounts { get => new List<int>(this.doubleCounts); private set => this.doubleCounts = value; }
    public List<bool> ExtraTurns { get => new List<bool>(this.extraTurns); private set => this.ExtraTurns = value; }

    public void CountDouble(int playerNumber)
    {
        this.doubleCounts[playerNumber]++;
    }

    public void ResetDoubleCount(int playerNumber)
    {
        this.doubleCounts[playerNumber] = 0;
    }

    public void SetExtraTurn(int playerNumber, bool extraTurn)
    {
        this.extraTurns[playerNumber] = extraTurn;
    }

}
