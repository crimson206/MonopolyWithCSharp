/// Now it is getting bigger to be given to the events
/// The events only need the features of the property manager and the information of tiles
/// Maybe the board can store the information of tiles
/// Only the property manager needs to share the  with properties.
/// Make tiles with the tile factory and give the tiles to the board.
/// Board can deal with simple events "PayRent" "Pass" without the property manager.
/// Property manager will be called when they need to by the property.


public class TileManager
{
    private List<Tile> tiles;
    private List<Property> properties;
    private List<RealEstate> realEstates;
    private Random random = new Random();
    public Analyser Analyser;
    public PropertyManager propertyManager;


    public TileManager()
    {
        List<Property> properties = this.FilterProperties(this.tiles);
        this.realEstates = this.FilterRealEstates(this.tiles);
        this.properties = properties;
        this.Analyser = new Analyser(properties, realEstates);
        this.propertyManager = new PropertyManager();
    }



    public List<Tile> Tiles { get => new List<Tile> (this.tiles); }

    public List<Property> Properties { get => new List<Property> (this.properties); }
    public List<RealEstate> RealEstates { get => new List<RealEstate> (this.realEstates); }

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
