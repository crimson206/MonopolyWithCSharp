
public class RealEstateData
{
    private RealEstate realEstate;

    public RealEstateData(RealEstate realEstate)
    {
        this.realEstate = realEstate;
    }

    public string Color => this.realEstate.Color;
    public int NumOfHouses => this.realEstate.HouseCount;
    public bool Buildable => this.realEstate.IsHouseBuildable;
    public bool Distructable => this.realEstate.IsHouseDistructable;
    public int BuildingCost => this.realEstate.BuildingCost;

}
