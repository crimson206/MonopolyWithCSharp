public class Tile : ICloneable
{
    private string name = "Tile";

    public Tile(string name)
    {
        this.name = name;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public string Name { get => name; }
}
