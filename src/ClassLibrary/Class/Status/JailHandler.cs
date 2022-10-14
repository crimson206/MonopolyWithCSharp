
public class JailHandler
{
    private int jailFine = 60;
    private List<int> freeJailCards = new List<int> () {0, 0, 0, 0};
    private List<int> turnsInJail = new List<int>() {0, 0, 0, 0};
    public int JailFine { get => jailFine; }
    public List<int> TurnsInJail{ get => new List<int> (this.turnsInJail); set => this.turnsInJail = value; }
    public List<int> FreeJailCards{ get => new List<int> (this.freeJailCards); set => this.freeJailCards = value; }

    public void CountTurnInJail(int playerNumber)
    {
        this.turnsInJail[playerNumber]++;
    }

    public void ResetTurnInJail(int playerNumber)
    {
        this.turnsInJail[playerNumber] = 0;
    }

    public void AddFreeJailCard(int playerNumber)
    {
        this.freeJailCards[playerNumber]++;
    }
    public void RemoveAFreeJailCard(int playerNumber)
    {
        if (freeJailCards[playerNumber] <= 0)
        {
            throw new Exception();
        }
        else
        {
            this.freeJailCards[playerNumber]--;
        }
    }
}
