
public class TaxTile : Tile
{
    private int tax;
    public TaxTile(int position, string name, int tax) : base(position, name)
    {
        this.tax = tax;
    }

    public int Tax { get => this.tax; }
}
