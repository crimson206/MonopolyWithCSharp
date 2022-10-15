public class Property : Tile , IPurchasable
{
    protected int? ownerPlayerNumber = null;
    protected int price;
    protected List<int> rents = new List<int>();
    protected List<Property> group = new List<Property>();
    protected int currentRent => CalCurrentRent();
    protected int mortgage;
    
    protected bool isMortgaged;
    public Property(string name, int price, List<int> rents, int mortgageValue) : base(name)
    {
        this.price = price;
        this.rents = rents;
        this.mortgage = mortgageValue;
         ;
    }

    public int? OwnerPlayerNumber { get=>ownerPlayerNumber; }
    public int Price { get => price; }
    public List<int> Rents { get => rents; }
    public int CurrentRent { get => currentRent; }
    public int Mortgage { get => mortgage; }
    public bool IsMortgaged { get => this.isMortgaged; }
    public void SetOnwerPlayerNumber(int? playerNumber)
    {

        this.ownerPlayerNumber = playerNumber;
 
    }

    protected virtual int CalCurrentRent()
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

    public virtual void SetIsMortgaged(bool isMortgaged)
    {

        this.isMortgaged = isMortgaged;

    }

    public void SetGroup(List<Property> group)
    {

        this.group = group;

    }
}
