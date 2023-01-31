public class Tile : ITile
{
    protected string name = "Tile";

    public Tile(string name)
    {
        this.name = name;
    }

    public string Name { get => name; }
}
