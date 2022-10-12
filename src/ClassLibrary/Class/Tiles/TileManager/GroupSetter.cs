
public class GroupSetter
{
    private TileFilter tileFilter = new TileFilter();

    public void SetGroups(int password, List<Tile> tiles)
    {
        List<RealEstate> realEstates = tileFilter.FilterRealEstates(tiles);
        Dictionary<string, List<RealEstate>> colorGroups = tileFilter.DivideRealEstatesByColor(realEstates);
        List<Property> railRoads = tileFilter.FilterRailRoads(tiles).Cast<Property>().ToList();
        List<Property> utilities = tileFilter.FilterUtilities(tiles).Cast<Property>().ToList();

        foreach (var colorGroup in colorGroups.Values)
        {
            SetGroup(password, colorGroup.Cast<Property>().ToList());
        }

        SetGroup(password, railRoads);
        SetGroup(password, utilities);
    }

    private void SetGroup(int password, List<Property> group)
    {
        foreach (var property in group)
        {
            property.SetGroup(password, group);
        }
    }

}
