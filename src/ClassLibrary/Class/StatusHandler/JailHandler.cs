
public class JailHandler
{
    private List<int> freeJailCardCounts = new List<int> () {0, 0, 0, 0};
    private List<int> turnsInJailCounts = new List<int>() {0, 0, 0, 0};
    public List<int> TurnsInJailCounts { get => new List<int>(this.turnsInJailCounts); private set => this.turnsInJailCounts = value; }
    public List<int> JailFreeCardCounts { get => new List<int>(this.freeJailCardCounts); private set => this.freeJailCardCounts = value; }

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
    public void RemoveAJailFreeCard(int playerNumber)
    {
        if (this.freeJailCardCounts[playerNumber] <= 0)
        {
            throw new Exception();
        }
        else
        {
            this.freeJailCardCounts[playerNumber]--;
        }
    }
}
