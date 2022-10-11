
public class JailManager
{
    private int jailFine = 60;
    private List<int> freeJailCards = new List<int> () {0, 0, 0, 0};
    private List<int> turnsInJail = new List<int>() {0, 0, 0, 0};
    public int JailFine { get => jailFine; }
    public List<int> TurnsInJail{ get => new List<int> (this.turnsInJail); set => this.turnsInJail = value; }
    public List<int> FreeJailCards{ get => new List<int> (this.freeJailCards); set => this.freeJailCards = value; }
}
