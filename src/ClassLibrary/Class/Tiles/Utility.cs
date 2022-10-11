public class Utility : Property
{
    public Utility(string name, int price, List<int> rents, int mortgageValue, int password) : base(name, price, rents, mortgageValue, password)
    {

    }

    private List<Utility> group = new List<Utility>();

    public void SetGroup(int password, List<Utility> utilities)
    {
        this.group = utilities;
    }

    protected override int CalCurrentRent()
    {
        if ( this.ownerPlayerNumber is not null)
        {
            int numRailRoadsWithSameOwner = group.Where(group => group.OwnerPlayerNumber == this.ownerPlayerNumber).ToList().Count();
            return this.rents[numRailRoadsWithSameOwner - 1];
        }
        else
        {
            return this.rents[0];
        }
    }
}
