public class Jail : Tile
{    
    private int jailFine;

    public Jail(string name, int jailFine) : base(name)
    {
        this.jailFine = jailFine;
    }

    public int JailFine { get=>jailFine; }
}
