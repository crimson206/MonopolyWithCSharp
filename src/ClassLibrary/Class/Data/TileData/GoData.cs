public class GoData : TileData
{
    private Go go;

    public GoData(Tile tile)
        : base(tile)
    {
        this.go = (Go)tile;
    }

    public int Salary => this.go.Salary;
}
