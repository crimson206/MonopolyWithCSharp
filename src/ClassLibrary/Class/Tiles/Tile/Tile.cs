public class Tile : ITile, ITileData
{
    protected string name = "Tile";

    public Tile(string name)
    {
        this.name = name;
    }

    public string Name { get => name; }
}
