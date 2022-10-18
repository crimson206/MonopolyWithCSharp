public class JailData : TileData
{
    protected Jail jail;

    public JailData(Tile tile) : base(tile)
    {
        this.jail = (Jail) tile;
    }

    public int Salary => this.jail.JailFine;
}
