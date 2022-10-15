
public class JailHandler
{
    private int jailFine = 60;
    private List<int> freeJailCardCounts = new List<int> () {0, 0, 0, 0};
    private List<int> turnsInJailCounts = new List<int>() {0, 0, 0, 0};
    public int JailFine { get => jailFine; }
    public List<int> TurnsInJailCounts{ get => new List<int> (this.turnsInJailCounts); set => this.turnsInJailCounts = value; }
    public List<int> FreeJailCardCounts{ get => new List<int> (this.freeJailCardCounts); set => this.freeJailCardCounts = value; }

    public void CountTurnInJail(int playerNumber)
    {
        this.turnsInJailCounts[playerNumber]++;
    }

    public void ResetTurnInJail(int playerNumber)
    {
        this.turnsInJailCounts[playerNumber] = 0;
    }

    public void AddFreeJailCard(int playerNumber)
    {
        this.freeJailCardCounts[playerNumber]++;
    }
    public void RemoveAFreeJailCard(int playerNumber)
    {
        if (freeJailCardCounts[playerNumber] <= 0)
        {
            throw new Exception();
        }
        else
        {
            this.freeJailCardCounts[playerNumber]--;
        }
    }
}
