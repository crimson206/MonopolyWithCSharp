public class Property : Tile , IPurchasable
{
    protected int? ownerPlayerNumber = null;
    protected int price;
    protected List<int> rents = new List<int>();
    protected List<Property> group = new List<Property>();
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
    public int CurrentRent => CalCurrentRent();
    public int Mortgage { get => mortgage; }
    public bool IsMortgaged { get => this.isMortgaged; }
    public bool IsMortgagible => this.CheckMortgagible();
    public void SetOnwerPlayerNumber(int? playerNumber)
    {

        this.ownerPlayerNumber = playerNumber;
 
    }

    protected virtual int CalCurrentRent()
    {
        if ( this.ownerPlayerNumber is not null)
        {
            if ( this.isMortgaged )
            {
                return 0;
            }
            else
            {
                int numRailRoadsWithSameOwner = group.Where(group => group.OwnerPlayerNumber == this.ownerPlayerNumber).ToList().Count();
                return this.rents[numRailRoadsWithSameOwner-1];
            }
        }
        else
        {
            return this.rents[0];
        }
    }

    protected virtual bool CheckMortgagible()
    {
        if (this.ownerPlayerNumber != null && this.isMortgaged is false)
        {
            return true;
        }
        else
        {
            return false;
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
