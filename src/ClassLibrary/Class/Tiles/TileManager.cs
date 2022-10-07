
public class TileManager
{
    private List<Tile> tiles = new List<Tile> ();
    private List<RealEstate> realEstates = new List<RealEstate> ();
    private List<RailRoad> railRoads = new List<RailRoad> ();
    private List<Utility> utilities = new List<Utility> ();

    public void ChangePropertyOwner(int tileNum, int playerNum)
    {
        Property property = (Property) tiles[tileNum];
        property.OwnerPlayerNumber = playerNum;
    }

    public void SetTiles(List<Tile> tiles)
    {
        this.tiles = tiles;
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

    public List<Tile> GetTiles()
    {
        List<Tile> Tiles = new List<Tile>();
        foreach (var tile in this.tiles)
        {
            Tiles.Add((Tile)tile.Clone());
        }
        return Tiles;
    }

    public List<RealEstate> GetRealEstates()
    {
        List<RealEstate> RealEstates = new List<RealEstate>();
        foreach (var realEstate in this.realEstates)
        {
            RealEstates.Add((RealEstate)realEstate.Clone());
        }
        return RealEstates;
    }

    public List<RailRoad> GetRailRoads()
    {
        List<RailRoad> RailRoads = new List<RailRoad>();
        foreach (var RailRoad in this.railRoads)
        {
            RailRoads.Add((RailRoad)RailRoad.Clone());
        }
        return RailRoads;
    }
}
