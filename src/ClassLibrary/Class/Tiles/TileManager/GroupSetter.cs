public class GroupSetter
{
    private TileFilter tileFilter = new TileFilter();

    public void SetGroups(List<ITile> tiles)
    {
        List<IRealEstate> realEstates = this.tileFilter.FilterRealEstates(tiles);
        Dictionary<string, List<IRealEstate>> colorGroups = this.tileFilter.DivideRealEstatesByColor(realEstates);
        List<IProperty> railRoads = this.tileFilter.FilterRailRoads(tiles).Cast<IProperty>().ToList();
        List<IProperty> utilities = this.tileFilter.FilterUtilities(tiles).Cast<IProperty>().ToList();

        foreach (var colorGroup in colorGroups.Values)
        {
            this.SetGroup(colorGroup.Cast<IProperty>().ToList());
        }

        this.SetGroup(railRoads);
        this.SetGroup(utilities);
    }

    private void SetGroup(List<IProperty> group)
    {
        foreach (var property in group)
        {
            property.SetGroup(group);
        }
    }
}
