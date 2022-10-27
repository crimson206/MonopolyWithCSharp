public class RealEstateData : PropertyData
{
    private RealEstate realEstate;

    public RealEstateData(Property property)
        : base(property)
    {
        this.realEstate = (RealEstate)property;
    }

    public string Color => this.realEstate.Color;

    public int HouseCount => this.realEstate.HouseCount;

    public bool Buildable => this.realEstate.IsHouseBuildable;

    public bool Distructable => this.realEstate.IsHouseDistructable;

    public int BuildingCost => this.realEstate.BuildingCost;
}
