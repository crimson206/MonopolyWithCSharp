public class TileData
{
    private Tile tile;

    public TileData(Tile tile)
    {
        this.tile = tile;
    }

    public string Name => this.tile.Name;
}
