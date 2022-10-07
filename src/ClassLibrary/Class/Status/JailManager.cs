
public class JailManager
{
    private Data data;
    private int jailFine = 60;
    private List<int> freeJailCards = new List<int> () {0, 0, 0, 0};
    private List<int> turnsInJail = new List<int>() {0, 0, 0, 0};
    public int JailFine { get => jailFine; }
    public List<int> TurnsInJail
    {
        get
        {
            return this.turnsInJail;
        }
        set
        {
            this.turnsInJail = value;
            this.data.UpdateJailManager(this);
        }
    }

    public List<int> FreeJailCards
    {
        get
        {
            return this.freeJailCards;
        }
        set
        {
            this.freeJailCards = value;
            this.data.UpdateJailManager(this);
        }
    }
}
