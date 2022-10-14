public class Property : Tile , IPurchasable
{
    protected int? ownerPlayerNumber = null;
    protected int price;
    protected List<int> rents = new List<int>();
    protected List<Property> group = new List<Property>();
    protected int currentRent => CalCurrentRent();
    protected int mortgage;
    protected int password;
    protected bool isMortgaged;
    public Property(string name, int price, List<int> rents, int mortgageValue, int password) : base(name)
    {
        this.price = price;
        this.rents = rents;
        this.mortgage = mortgageValue;
        this.password = password;
    }

    public int? OwnerPlayerNumber { get=>ownerPlayerNumber; }
    public int Price { get => price; }
    public List<int> Rents { get => rents; }
    public int CurrentRent { get => currentRent; }
    public int Mortgage { get => mortgage; }
    public bool IsMortgaged { get => this.isMortgaged; }
    public void SetOnwerPlayerNumber(int password, int? playerNumber)
    {
        if ( password == this.password)
        {
            this.ownerPlayerNumber = playerNumber;
        }
        else
        {
            throw new Exception();
        }
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

    public virtual void SetIsMortgaged(int password, bool isMortgaged)
    {
        if ( password != this.password )
        {
            throw new Exception();
        }
        else
        {
            this.isMortgaged = isMortgaged;
        }
    }

    public void SetGroup(int password, List<Property> group)
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
}
