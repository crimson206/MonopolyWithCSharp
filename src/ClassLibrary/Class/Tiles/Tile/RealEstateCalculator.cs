public class RealEstateCalculator
{
    private RealEstate realEstate;
    private int? ownerPlayerNumber => this.realEstate.OwnerPlayerNumber;
    private bool isMortgaged => this.realEstate.IsMortgaged;
    private int houseCount => this.realEstate.HouseCount;
    private List<RealEstate> colorGroup;
    public RealEstateCalculator(RealEstate realEstate, List<RealEstate> colorGroup)
    {
        this.realEstate = realEstate;
        this.colorGroup = colorGroup;
    }

    public bool IsHouseBuildable()
    {

        if (this.ownerPlayerNumber is not null && this.isMortgaged is false)
        {    
            if (colorGroup.All(realEstate => realEstate.OwnerPlayerNumber == this.ownerPlayerNumber))
            {
                if (colorGroup.Any(realEstate => realEstate.HouseCount < this.houseCount) || houseCount > 5)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool IsHouseDistructable()
    {
        if (colorGroup.Any(realEstate => realEstate.HouseCount > this.houseCount))
        {
            return false;
        }
        else
        {
            if ( houseCount < 1)
            {
                return false;
            }
            return true;
        }
    }

    public bool IsMortgagible()
    {
        if (this.ownerPlayerNumber is not null && this.houseCount != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
