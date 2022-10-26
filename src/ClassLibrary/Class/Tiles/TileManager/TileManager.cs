public class TileManager
{
    private List<Tile> tiles;
    private List<Property> properties;
    private List<RealEstate> realEstates;
    private Random random = new Random();
    private MapTilesFactory mapTilesFactory = new MapTilesFactory();
    public Analyser Analyser;
    private PropertyManager propertyManager;
    private List<TileData> tileDatas;

    /// <summary>
    /// normal tiles size = 40, smaller tiles size = 32
    /// </summary>
    /// <param name="isTileSamll"></param>
    public TileManager(bool isTileSamll)
    {
        if (isTileSamll)
        {
            this.tiles = this.CreateSmallerTiles();
        }
        else
        {
            this.tiles = this.CreateTiles();
        }

        this.realEstates = this.FilterRealEstates(this.tiles);
        this.properties = this.FilterProperties(this.tiles);
        this.Analyser = new Analyser(properties, realEstates);
        this.propertyManager = new PropertyManager();
        this.tileDatas = this.mapTilesFactory.ExtractTileDataSet(this.tiles);
    }

    public List<Tile> Tiles { get => new List<Tile>(this.tiles); }

    public List<Property> Properties { get => new List<Property>(this.properties); }
    public List<RealEstate> RealEstates { get => new List<RealEstate>(this.realEstates); }
    public List<TileData> TileDatas { get => new List<TileData>(this.tileDatas); }
    public PropertyManager PropertyManager { get => this.PropertyManager; }
    private List<Tile> CreateTiles()
    {
        return this.mapTilesFactory.CreateRandomMapTiles(22, 4, 2, 3, 3);
    }

    private List<Tile> CreateSmallerTiles()
    {
        return this.mapTilesFactory.CreateRandomMapTiles(20, 2, 2, 1, 1);
    }

    private List<Property> FilterProperties(List<Tile> tiles)
    {
        var query = from tile in this.tiles where tile is Property select tile as Property;
        return query.ToList();
    }

    private List<RealEstate> FilterRealEstates(List<Tile> tiles)
    {
        var query = from tile in this.tiles where tile is RealEstate select tile as RealEstate;
        return query.ToList();
    }

    private List<RailRoad> FilterRailRoads(List<Tile> tiles)
    {
        var query = from tile in this.tiles where tile is RailRoad select tile as RailRoad;
        return query.ToList();
    }

    private List<Utility> FilterUtilities(List<Tile> tiles)
    {
        var query = from tile in this.tiles where tile is Utility select tile as Utility;
        return query.ToList();
    }
}
