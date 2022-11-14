public interface IHouseBuildHandlerData
{
    public bool? AreAnyBuildable { get; }
    public List<int>? ParticipantPlayerNumbers { get; }
    public bool IsLastBuilder { get; }
    public Dictionary<int, List<IRealEstateData>> HouseBuildableRealEstatesOfOwners { get; }
    public IRealEstateData? RealEstateToBuildHouse { get; }
    public int? CurrentHouseBuilder { get; }
}