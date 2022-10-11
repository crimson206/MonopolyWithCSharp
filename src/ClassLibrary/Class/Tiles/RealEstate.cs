
public class RealEstate : Property
{
    private string color;
    private int numOfHouses;
    private bool buildable => this.IsBuildable();
    private bool distructable => this.IsDistructable();
    private int buildingCost;
    private List<RealEstate> group = new List<RealEstate>();

    public RealEstate(string name, int price, int buildingCost, List<int> rents, int mortgageValue, string color, int password) : base(name, price, rents, mortgageValue, password)
    {
        this.buildingCost = buildingCost;
        this.color = color;
    }

    public string Color { get=>color; }
    public int NumOfHouses { get=>numOfHouses; }
    public bool Buildable { get => this.buildable; }
    public bool Distructable { get => this.distructable; }
    public int BuildingCost { get => this.buildingCost; }

    public void BuildHouse(int password)
    {
        if (this.buildable is true)
        {
            if (numOfHouses == 5)
            {
                throw new Exception();
            }
            else    
            {
                this.numOfHouses++;
            }
        }
        else
        {
            throw new Exception();
        }
    }

    public void DistructHouse(int password)
    {
        
        if (this.distructable == false)
        {
            throw new Exception();
        }
        else
        {
            this.numOfHouses--;
        }
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

    private bool IsBuildable()
    {
        if (this.ownerPlayerNumber is not null && this.isMortgaged is false)
        {    
            if (group.All(realEstate => realEstate.OwnerPlayerNumber == this.ownerPlayerNumber))
            {
                if (group.Any(realEstate => realEstate.NumOfHouses < this.numOfHouses) || numOfHouses > 5)
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



    public void SetGroup(int password, List<RealEstate> colorGroup)
    {
        
        if( password != this.password)
        {
            throw new Exception();
        }
        else if( group.Any(realEstate => realEstate.Color != this.Color))
        {
            throw new Exception();
        }
        else
        {
            this.group = colorGroup;
        }
    }

    protected override int CalCurrentRent()
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
