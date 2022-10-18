
public class LuxuryTaxData : TileData
{
    protected LuxuryTax luxuryTax;

    public LuxuryTaxData(Tile tile) : base(tile)
    {
        this.luxuryTax = (LuxuryTax) tile;
    }

    public int Tax => this.luxuryTax.Tax;
}
