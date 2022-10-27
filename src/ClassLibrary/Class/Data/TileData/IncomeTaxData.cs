public class IncomeTaxData : TileData
{
    private IncomeTax incomeTax;

    public IncomeTaxData(Tile tile)
        : base(tile)
    {
        this.incomeTax = (IncomeTax)tile;
    }

    public int PercentageTax => this.incomeTax.PercentageTax;

    public int IncomeTax => this.incomeTax.Tax;
}
