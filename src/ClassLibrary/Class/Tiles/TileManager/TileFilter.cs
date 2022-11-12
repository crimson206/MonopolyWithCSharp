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
            List<RealEstate> colorGroup = this.FilterRealEstateByColor(color, realEstates);
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

    private List<Property> UpriseToProperties(List<Property> property)
    {
        return property;
    }

    public Dictionary<int, List<IPropertyData>> ConvertPropertiesToOwnedPropertyDatasDictionary(List<Property> properties)
    {
        Dictionary<int, List<IPropertyData>> ownedProperties = new Dictionary<int, List<IPropertyData>>();
        foreach (var property in properties)
        {
            if (property.OwnerPlayerNumber is not null)
            {
                int playerNumber = (int)property.OwnerPlayerNumber;
                IPropertyData propertyData = (IPropertyData)property;

                if (ownedProperties.Keys.Contains(playerNumber) is false)
                {
                    ownedProperties.Add(playerNumber, new List<IPropertyData>());
                }
 
                ownedProperties[playerNumber].Add(propertyData);
            }
        }

        return ownedProperties;
    }

    public List<IPropertyData> FilterTradablePropertyDatas (List<IPropertyData> propertyDatas)
    {
        List<IPropertyData> tradablePropertyDatas = propertyDatas.
                                                    Where(propertyData => propertyData.IsTradable is true).
                                                    ToList();
        return tradablePropertyDatas;
    }
}
