public class Jail : Tile
{    
    private int jailFine;

    public Jail(int position, string name, int jailFine) : base(position, name)
    {
        this.jailFine = jailFine;
    }

    public int JailFine { get=>jailFine; }
}
