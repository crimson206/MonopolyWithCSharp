
public class RealEstate : Property
{
    private string color;
    private int numOfHouses;

    public RealEstate(string name, int price, List<int> rents, int mortgageValue, string color) : base(name, price, rents, mortgageValue)
    {
        this.color = color;
    }

    public string Color { get=>color; }
    public int NumOfHouses { get=>numOfHouses; set=> numOfHouses = value; }
}
