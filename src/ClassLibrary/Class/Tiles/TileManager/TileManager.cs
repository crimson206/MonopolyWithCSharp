
public class TileManager
{
    private List<Tile> tiles;
    private List<Property> properties;
    private List<RealEstate> realEstates;
    private Random random = new Random();
    private MapTilesFactory mapTilesFactory = new MapTilesFactory();
    public Analyser Analyser;
    public PropertyManager propertyManager;


    public TileManager()
    {
        int newPassword = random.Next(0, (int) Math.Pow(10,6));
        this.tiles = this.CreateTiles(newPassword);
        
        List<Property> properties = this.FilterProperties(this.tiles);
        this.realEstates = this.FilterRealEstates(this.tiles);
        this.properties = properties;
        this.Analyser = new Analyser(properties, realEstates);
        this.propertyManager = new PropertyManager(newPassword);
        

    }
    public List<Tile> Tiles { get => new List<Tile> (this.tiles); }

    public List<Property> Properties { get => new List<Property> (this.properties); }
    public List<RealEstate> RealEstates { get => new List<RealEstate> (this.realEstates); }

    private List<Tile> CreateTiles(int password)
    {
        return this.mapTilesFactory.CreateRandomMapTiles(22, 4, 2, 3, 3, random, password);
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
