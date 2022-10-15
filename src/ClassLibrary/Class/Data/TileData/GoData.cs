
public class GoData : TileData
{
    protected Go go;

    public GoData(Tile tile) : base(tile)
    {
        this.go = (Go) tile;
    }

    public int Salary => this.go.Salary;
}
