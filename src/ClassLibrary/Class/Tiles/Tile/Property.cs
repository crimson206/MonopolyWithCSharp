public class Property : Tile, IPurchasable, IPropertyData
{
    protected int? ownerPlayerNumber = null;
    protected int price;
    protected List<int> rents = new List<int>();
    protected List<IProperty> group = new List<IProperty>();
    protected int mortgage;
    protected bool isMortgaged;
    public Property(string name, int price, List<int> rents, int mortgageValue) : base(name)
    {
        this.price = price;
        this.rents = rents;
        this.mortgage = mortgageValue;
    }

    public int? OwnerPlayerNumber { get => this.ownerPlayerNumber; }

    public int Price { get => this.price; }

    public List<int> Rents { get => this.rents; }

    public int CurrentRent => this.CalCurrentRent();

    public int Mortgage { get => this.mortgage; }

    public bool IsMortgaged { get => this.isMortgaged; }

    public bool IsMortgagible => this.CheckMortgagible();

    public bool IsTradable => this.CheckTradable();
    public bool IsSoldableWithAuction => this.CheckSoldableWithAuction();
    public List<IProperty> Group { get => this.group; }

    public void SetOwnerPlayerNumber(int? playerNumber)
    {

        this.ownerPlayerNumber = playerNumber;
 
    }

    public virtual void SetIsMortgaged(bool isMortgaged)
    {
        this.isMortgaged = isMortgaged;
    }

    public void SetGroup(List<IProperty> group)
    {
        this.group = group;
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
                int numRailRoadsWithSameOwner = this.group.Where(group => group.OwnerPlayerNumber == this.ownerPlayerNumber).ToList().Count();
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

    protected virtual bool CheckTradable()
    {
        if (this.ownerPlayerNumber is null || this.isMortgaged)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected virtual bool CheckSoldableWithAuction()
    {
        if (this.OwnerPlayerNumber is not null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
