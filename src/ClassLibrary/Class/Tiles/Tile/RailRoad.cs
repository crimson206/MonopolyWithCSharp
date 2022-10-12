
public class RailRoad : Property
{
    public RailRoad(string name, int price, List<int> rents, int mortgageValue, int password) : base(name, price, rents, mortgageValue, password)
    {

    }

    private List<RailRoad> group = new List<RailRoad>();

    public void SetGroup(int password, List<RailRoad> railroads)
    {
        this.group = railroads;
    }

    protected override int CalCurrentRent()
    {
        if ( this.ownerPlayerNumber is not null)
        {
            int numRailRoadsWithSameOwner = group.Where(group => group.OwnerPlayerNumber == this.ownerPlayerNumber).ToList().Count();
            return this.rents[numRailRoadsWithSameOwner-1];
        }
        else
        {
            return this.rents[0];
        }
    }

}
