public interface IRealEstateData : IPropertyData
{
    public string Color { get; }
    public int HouseCount { get; }
    public int BuildingCost { get; }
    public bool IsHouseBuildable { get; }
    public bool IsHouseDistructable { get; }
}