public class TileManager : ITileManager
{
    private List<Tile> tiles;
    private List<Property> properties;
    private List<RealEstate> realEstates;
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
        this.Analyser = new Analyser(properties, realEstates);
        this.propertyManager = new PropertyManager();
        this.tileDatas = this.mapTilesFactory.ExtractTileDataSet(this.tiles);
    }

    public List<Tile> Tiles { get => new List<Tile>(this.tiles); }

    public List<Property> Properties { get => new List<Property>(this.properties); }
    public List<RealEstate> RealEstates { get => new List<RealEstate>(this.realEstates); }
    public List<ITileData> TileDatas { get => new List<ITileData>(this.tileDatas); }
    public IPropertyManager PropertyManager { get => this.propertyManager; }

    private List<IPropertyData> player0sProperties => this.GetPropertyDatasWithOwnerNumber(0);
    private List<IPropertyData> player1sProperties => this.GetPropertyDatasWithOwnerNumber(1);
    private List<IPropertyData> player2sProperties => this.GetPropertyDatasWithOwnerNumber(2);
    private List<IPropertyData> player3sProperties => this.GetPropertyDatasWithOwnerNumber(3);
    public List<IPropertyData> GetPropertyDatasWithOwnerNumber(int? playerNumber)
    {
        List<IPropertyData> propertiesOnwedByThePlayer = this.tileDatas
                                                            .Cast<IPropertyData>()
                                                            .Where(property => property.OwnerPlayerNumber == playerNumber)
                                                            .ToList();
                            
        return propertiesOnwedByThePlayer;
    }


    private List<Tile> CreateTiles()
    {
        return this.mapTilesFactory.CreateRandomMapTiles(22, 4, 2, 3, 3, true, 0, 10, 20, 30);
    }

    private List<Tile> CreateSmallerTiles()
    {
        return this.mapTilesFactory.CreateRandomMapTiles(22, 2, 2, 0, 0, true, 0, 9, 16, 27);
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
