public class LuxuryTaxData : TileData
{
    private LuxuryTax luxuryTax;

    public LuxuryTaxData(Tile tile)
        : base(tile)
    {
        this.luxuryTax = (LuxuryTax)tile;
    }

    public int Tax => this.luxuryTax.Tax;
}
