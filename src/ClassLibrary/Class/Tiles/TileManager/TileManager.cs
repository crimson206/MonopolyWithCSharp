public class TileManager : ITileManager
{
    private List<ITile> tiles;
    private List<IProperty> properties;
    private List<IRealEstate> realEstates;
    private Random random = new Random();
    private MapTilesFactory mapTilesFactory = new MapTilesFactory();
    public Analyser Analyser;
    private IPropertyManager propertyManager;
    private List<ITileData> tileDatas;


    /// <summary>
    /// normal tiles size = 40, smaller tiles size = 32
    /// </summary>
    /// <param name="isBoardSmall"></param>
    public TileManager(bool isBoardSmall)
    {
        if (isBoardSmall)
        {
            this.tiles = this.CreateSmallerTiles();
        }
        else
        {
            this.tiles = this.CreateTiles();
        }

        this.realEstates = this.FilterRealEstates(this.tiles);
        this.properties = this.FilterProperties(this.tiles);
        this.propertyManager = new PropertyManager();
        this.tileDatas = this.mapTilesFactory.ExtractTileDataSet(this.tiles);
    }

    public List<ITile> Tiles { get => new List<ITile>(this.tiles); }

    public List<IProperty> Properties { get => new List<IProperty>(this.properties); }
    public List<IRealEstate> RealEstates { get => new List<IRealEstate>(this.realEstates); }
    public List<ITileData> TileDatas { get => new List<ITileData>(this.tileDatas); }
    public IPropertyManager PropertyManager { get => this.propertyManager; }

    public List<IPropertyData> GetPropertyDatasWithOwnerNumber(int? playerNumber)
    {
        List<IPropertyData> properties = this.tileDatas.Where(tile => tile is IPropertyData).Cast<IPropertyData>().ToList();
        List<IPropertyData> propertiesOnwedByThePlayer = properties
                                                        .Where(property => property.OwnerPlayerNumber == playerNumber)
                                                        .ToList();
                            
        return propertiesOnwedByThePlayer;
    }


    private List<ITile> CreateTiles()
    {
        return this.mapTilesFactory.CreateRandomMapTiles(22, 4, 2, 3, 3, true, 0, 10, 20, 30);
    }

    private List<ITile> CreateSmallerTiles()
    {
        return this.mapTilesFactory.CreateRandomMapTiles(22, 2, 2, 0, 0, true, 0, 9, 16, 27);
    }

    private List<IProperty> FilterProperties(List<ITile> tiles)
    {
        var query = from tile in this.tiles where tile is IProperty select tile as IProperty;
        return query.ToList();
    }

    private List<IRealEstate> FilterRealEstates(List<ITile> tiles)
    {
        var query = from tile in this.tiles where tile is IRealEstate select tile as IRealEstate;
        return query.ToList();
    }

    private List<IRailRoad> FilterRailRoads(List<ITile> tiles)
    {
        var query = from tile in this.tiles where tile is IRailRoad select tile as IRailRoad;
        return query.ToList();
    }

    private List<IUtility> FilterUtilities(List<ITile> tiles)
    {
        var query = from tile in this.tiles where tile is IUtility select tile as IUtility;
        return query.ToList();
    }
}
