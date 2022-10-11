
public class DoubleSideEffectManager
{
    private List<int> doubleCounts = new List<int> { 0, 0, 0, 0 };
    private List<bool> extraTurns = new List<bool> { false, false, false, false };

    public List<bool> ExtraTurn { get => this.extraTurns; }

    public List<int> DoubleCounts{ get => new List<int> (this.doubleCounts); set => this.doubleCounts = value; }

    public List<bool> ExtraTurns{ get => new List<bool> (this.extraTurns); set => this.extraTurns = value; }

}
