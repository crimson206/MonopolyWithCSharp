
public class RealEstate : Property
{
    private string color;
    private int numOfHouses;
    private bool buildable;

    public RealEstate(string name, int price, List<int> rents, int mortgageValue, string color, int password) : base(name, price, rents, mortgageValue, password)
    {
        this.color = color;
    }

    public string Color { get=>color; }
    public int NumOfHouses { get=>numOfHouses; set=> numOfHouses = value; }

    public bool Buildable { get => this.buildable; }
    public void SetBuidable ( int password, bool buildable)
    {
        this.buildable = buildable;
    }

    public void SetNumOfHouses(int password, int numOfHouses)
    {
        this.numOfHouses = numOfHouses;
    }

    public delegate void ShareOwnerChange(int playerNumber);
    public event ShareOwnerChange ownerChanged;
}
