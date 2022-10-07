
public class TaxTile : Tile
{
    private int tax;
    public TaxTile(string name, int tax) : base(name)
    {
        this.tax = tax;
    }

    public int Tax { get => this.tax; }
}
