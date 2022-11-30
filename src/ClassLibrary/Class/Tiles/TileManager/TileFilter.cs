public class TileFilter
{
    public List<IProperty> FilterProperties(List<Tile> tiles)
    {
        var query = from tile in tiles where tile is IProperty select tile as IProperty;
        return query.ToList();
    }

    public List<IRealEstate> FilterRealEstates(List<ITile> tiles)
    {
        var query = from tile in tiles where tile is IRealEstate select tile as IRealEstate;
        return query.ToList();
    }

    public List<IRailRoad> FilterRailRoads(List<ITile> tiles)
    {
        var query = from tile in tiles where tile is IRailRoad select tile as IRailRoad;
        return query.ToList();
    }

    public List<IUtility> FilterUtilities(List<ITile> tiles)
    {
        var query = from tile in tiles where tile is IUtility select tile as IUtility;
        return query.ToList();
    }

    public Dictionary<string, List<IRealEstate>> DivideRealEstatesByColor(List<IRealEstate> realEstates)
    {
        Dictionary<string, List<IRealEstate>> colorGroups = new Dictionary<string, List<IRealEstate>>();
        List<string> colors = this.ExtractColorsFromRealEstates(realEstates);

        foreach (var color in colors)
        {
            List<IRealEstate> colorGroup = this.FilterRealEstateByColor(color, realEstates);
            colorGroups.Add(color, colorGroup);
        }

        return colorGroups;
    }

    private List<string> ExtractColorsFromRealEstates(List<IRealEstate> realEstates)
    {
        var query = from realEstate in realEstates select realEstate.Color;
        List<string> colors = query.Distinct().ToList();
        return colors;
    }

    private List<IRealEstate> FilterRealEstateByColor(string color, List<IRealEstate> realEstates)
    {
        List<IRealEstate> colorGroup = realEstates.Where(realEstate => realEstate.Color == color).ToList();
        return colorGroup;
    }

    private List<IProperty> UpriseToProperties(List<IProperty> property)
    {
        return property;
    }

    public Dictionary<int, List<IPropertyData>> ConvertPropertiesToOwnedPropertyDatasDictionary(List<int> ownerNumbers, List<IProperty> properties)
    {
        Dictionary<int, List<IPropertyData>> ownedProperties = new Dictionary<int, List<IPropertyData>>();
        
        foreach (var ownerNumber in ownerNumbers)
        {
            ownedProperties.Add(ownerNumber, new List<IPropertyData>());

            foreach (var property in properties)
            {
                if (property.OwnerPlayerNumber == ownerNumber)
                {
                    ownedProperties[ownerNumber].Add(property);
                }
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
