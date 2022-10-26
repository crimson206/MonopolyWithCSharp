public class GroupSetter
{
    private TileFilter tileFilter = new TileFilter();

    public void SetGroups(List<Tile> tiles)
    {
        List<RealEstate> realEstates = this.tileFilter.FilterRealEstates(tiles);
        Dictionary<string, List<RealEstate>> colorGroups = this.tileFilter.DivideRealEstatesByColor(realEstates);
        List<Property> railRoads = this.tileFilter.FilterRailRoads(tiles).Cast<Property>().ToList();
        List<Property> utilities = this.tileFilter.FilterUtilities(tiles).Cast<Property>().ToList();

        foreach (var colorGroup in colorGroups.Values)
        {
            this.SetGroup(colorGroup.Cast<Property>().ToList());
        }

        this.SetGroup(railRoads);
        this.SetGroup(utilities);
    }

    private void SetGroup(List<Property> group)
    {
        foreach (var property in group)
        {
            property.SetGroup(group);
        }
    }
}
