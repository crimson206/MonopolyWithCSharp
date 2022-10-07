
public class DoubleSideEffectManager
{
    private Data data;
    private List<int> doubleCounts = new List<int> { 0, 0, 0, 0 };
    private List<bool> extraTurns = new List<bool> { false, false, false, false };

    public List<int> DoubleCounts { get => this.doubleCounts; }
    public List<bool> ExtraTurn { get => this.extraTurns; }

    public void CountDouble(int playerNumber)
    {
        this.doubleCounts[playerNumber] += 1;
        this.data.UpdateDoubleSideEffectManager(this);
    }

    public void SetExtraTurn( int playerNumber, bool extraTurn)
    {
        this.extraTurns[playerNumber] = extraTurn;
        this.data.UpdateDoubleSideEffectManager(this);
    }

}
