
public class RealEstate : Property
{
    private string color;
    private int numOfHouses;
    private bool buildable => this.IsBuildable();
    private bool distructable => this.IsDistructable();
    private List<RealEstate> group = new List<RealEstate>();

    public RealEstate(string name, int price, List<int> rents, int mortgageValue, string color, int password) : base(name, price, rents, mortgageValue, password)
    {
        this.color = color;
    }

    public string Color { get=>color; }
    public int NumOfHouses { get=>numOfHouses; }
    public bool IsMortgaged { get=> isMortgaged; }
    public bool Buildable { get => this.buildable; }
    public bool Distructable { get => this.distructable; }

    public void BuildHouse(int password)
    {
        if (this.buildable is true)
        {
            this.numOfHouses++;
            if (numOfHouses > 5)
            {
                throw new Exception();
            }
        }
        else
        {
            throw new Exception();
        }

    }

    public void DistructHouse(int password)
    {
        this.numOfHouses--;
    }

    protected override int CalCurrentRent()
    {
        if (!this.isMortgaged)
        {
            if (this.ownerPlayerNumber is not null)
            {
                if (group.All(realEstate => realEstate.OwnerPlayerNumber == this.ownerPlayerNumber))
                {
                    return Rents[numOfHouses+1];
                }
                return Rents[0];
            }
            else
            {
                return Rents[0];
            }
        }
        else
        {
            return 0;
        }

    }

    private bool IsBuildable()
    {
        if (this.ownerPlayerNumber is not null && this.isMortgaged is false)
        {    
            if (group.All(realEstate => realEstate.OwnerPlayerNumber == this.ownerPlayerNumber))
            {
                if (group.Any(realEstate => realEstate.NumOfHouses < this.numOfHouses))
                {
                    return false;
                }
                else
                {
                    if (numOfHouses > 5)
                    {
                        return false;
                    }
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

    private bool IsDistructable()
    {
        if (group.Any(realEstate => realEstate.NumOfHouses > this.numOfHouses))
        {
            return false;
        }
        else
        {
            if ( numOfHouses < 1)
            {
                return false;
            }
            return true;
        }
    }

    public void SetGroup(int password, List<RealEstate> colorGroup)
    {
        this.group = group;
        if( password != this.password)
        {
            throw new Exception();
        }
        if( group.Any(realEstate => realEstate.Color != this.Color))
        {
            throw new Exception();
        }
    }

    public void Reset()
    {
        this.ownerPlayerNumber = null;
        this.numOfHouses = 0;
    }

    public override void SetIsMortgaged( int password, bool isMortgaged)
    {

        if (isMortgaged is true && this.numOfHouses != 0)
        {
            throw new Exception();
        }
        else
        {
            this.isMortgaged = isMortgaged;
        }

    }

    private bool CheckIsMortgagible()
    {
        if (this.ownerPlayerNumber is not null && this.numOfHouses != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
