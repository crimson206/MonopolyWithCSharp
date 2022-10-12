public class Utility : Property
{
    public Utility(string name, int price, List<int> rents, int mortgageValue, int password) : base(name, price, rents, mortgageValue, password)
    {

    }


    public override void SetGroup(int password, List<Property> group)
    {

        if( password != this.password)
        {
            throw new Exception();
        }
        else
        {
            this.group = group;
        }
        
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
