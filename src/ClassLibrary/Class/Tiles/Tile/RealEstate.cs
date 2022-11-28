
public class RealEstate : Property, IRealEstate
{
    private string color;
    private int houseCount;
    private int buildingCost;
    private List<IRealEstate> colorGroup => this.group.Cast<IRealEstate>().ToList();
    private RealEstateCalculator calculator;
    public RealEstate(string name, int price, int buildingCost, List<int> rents, int mortgageValue, string color) : base(name, price, rents, mortgageValue)
    {
        this.buildingCost = buildingCost;
        this.color = color;
        this.calculator = new RealEstateCalculator(this);
    }

    public string Color { get=>color; }
    public int HouseCount { get=>houseCount; }
    public int BuildingCost { get => this.buildingCost; }
    public bool IsHouseBuildable => this.calculator.IsHouseBuildable();
    public bool IsHouseDistructable => this.calculator.IsHouseDistructable();
    protected override bool CheckMortgagible() => this.calculator.IsMortgagible();
    public void BuildHouse()
    {
        if (this.IsHouseBuildable is true)
        {
            if (houseCount == 5)
            {
                throw new Exception();
            }
            else    
            {
                this.houseCount++;
            }
        }
        else
        {
            throw new Exception();
        }
    }
    public void DistructHouse()
    {
        
        if (this.IsHouseDistructable == false)
        {
            throw new Exception();
        }
        else
        {
            this.houseCount--;
        }
    }
    public override void SetIsMortgaged(bool isMortgaged)
    {
        if (isMortgaged is true && this.houseCount != 0)
        {
            throw new Exception();
        }
        else
        {
            this.isMortgaged = isMortgaged;
        }
    }

    public void AcceptCalculator(RealEstateCalculator realEstateCalculator)
    {
        realEstateCalculator.ColorGroup = this.colorGroup;
    }

    protected override int CalCurrentRent()
    {

        if (this.ownerPlayerNumber is not null)
        {
            if (this.isMortgaged)
            {
                return 0;
            }
            else
            {
                if (colorGroup.All(realEstate => realEstate.OwnerPlayerNumber == this.ownerPlayerNumber))
                {
                    return Rents[houseCount+1];
                }
                return Rents[0];
            }
        }
        else
        {
            return Rents[0];
        }
    }

    protected override bool CheckTradable()
    {
        if (this.colorGroup.Any(member => member.HouseCount > 0) || this.isMortgaged || this.ownerPlayerNumber is null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected override bool CheckSoldableWithAuction()
    {
        if (this.colorGroup.Any(member => member.HouseCount > 0) || this.ownerPlayerNumber is null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
