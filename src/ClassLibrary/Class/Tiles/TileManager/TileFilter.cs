
public class TileFilter
{
    public List<Property> FilterProperties(List<Tile> tiles)
    {
        var query = from tile in tiles where tile is Property select tile as Property;
        return query.ToList();
    }

    public List<RealEstate> FilterRealEstates(List<Tile> tiles)
    {
        var query = from tile in tiles where tile is RealEstate select tile as RealEstate;
        return query.ToList();
    }
    
    public List<RailRoad> FilterRailRoads(List<Tile> tiles)
    {
        var query = from tile in tiles where tile is RailRoad select tile as RailRoad;
        return query.ToList();
    }

    public List<Utility> FilterUtilities(List<Tile> tiles)
    {
        var query = from tile in tiles where tile is Utility select tile as Utility;
        return query.ToList();
    }

    public Dictionary<string, List<RealEstate>> DivideRealEstatesByColor(List<RealEstate> realEstates)
    {
        Dictionary<string, List<RealEstate>> colorGroups = new Dictionary<string, List<RealEstate>>();
        List<string> colors = this.ExtractColorsFromRealEstates(realEstates);

        foreach (var color in colors)
        {
            List<RealEstate> colorGroup = FilterRealEstateByColor(color, realEstates);
            colorGroups.Add(color, colorGroup);
        }

        return colorGroups;
    }

    private List<string> ExtractColorsFromRealEstates(List<RealEstate> realEstates)
    {
        var query = from realEstate in realEstates select realEstate.Color;
        List<string> colors = query.Distinct().ToList();
        return colors;
    }

    private List<RealEstate> FilterRealEstateByColor(string color, List<RealEstate> realEstates)
    {
        List<RealEstate> colorGroup = realEstates.Where(realEstate => realEstate.Color == color).ToList();
        return colorGroup;
    }

    public List<Property> UpriseToProperties(List<Property> property)
    {
        return property;
    }
}
