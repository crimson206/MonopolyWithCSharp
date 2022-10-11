
public class TileManager
{
    private List<Tile> tiles = new List<Tile> ();
    private List<RealEstate> realEstates = new List<RealEstate> ();
    private List<RailRoad> railRoads = new List<RailRoad> ();
    private List<Utility> utilities = new List<Utility> ();
    private Random random = new Random();
    private MapTilesFactory mapTilesFactory = new MapTilesFactory();
    private int password;
    public void ChangePropertyOwner(int tileNum, int playerNum)
    {
        Property property = (Property) tiles[tileNum];
        property.SetOnwerPlayerNumber(this.password, playerNum);
    }

    public void SetTiles()
    {
        this.password = random.Next(0, (int) Math.Pow(10,6));
        this.tiles = this.mapTilesFactory.CreateRandomMapTiles(22, 4, 2, 3, 3, random, password);
        SetRealEstates();
        SetRailRoads();
    }

    private void SetRealEstates()
    {
        var query = from tile in this.tiles where tile is RealEstate select tile as RealEstate;
        this.realEstates = query.ToList();
    }

    private void SetRailRoads()
    {
        var query = from tile in this.tiles where tile is RailRoad select tile as RailRoad;
        this.railRoads = query.ToList();
    }

    private void SetUtilities()
    {
        var query = from tile in this.tiles where tile is Utility select tile as Utility;
        this.utilities = query.ToList();
    }

    public List<Tile> Tiles { get => new List<Tile> (this.tiles); }

    public List<RealEstate> RealEstates { get => new List<RealEstate> (this.realEstates); }
    public List<RailRoad> RailRoads { get => new List<RailRoad> (this.railRoads); }


}
