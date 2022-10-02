public class Tile
{
    private int position;
    private string name = "Tile";

    public Tile(int position, string name)
    {
        this.position = position;
        this.name = name;
    }

    public int Position { get => position; }
    public string Name { get => name; }
}
