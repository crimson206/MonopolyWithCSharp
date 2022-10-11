
public class RailRoad : Property
{
    public RailRoad(string name, int price, List<int> rents, int mortgageValue, int password) : base(name, price, rents, mortgageValue, password)
    {

    }

    private List<Property> group = new List<Property>();

    public void SetGroup(List<Property> railroads)
    {
        this.group = railroads;
    }

    private int CalCurrentRent()
    {
        if ( this.ownerPlayerNumber is not null )
        {
            int numRailRoadsWithSameOwner = group.Where(group => group.OwnerPlayerNumber == this.ownerPlayerNumber).ToList().Count();
        }
        
    }

}
