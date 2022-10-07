public class Property : Tile
{
    private int? ownerPlayerNumber = null;
    private int price;
    private List<int> rents = new List<int>();
    public int CurrentRent => CalCurrentRent();
    private int mortgage;
    
    public Property(string name, int price, List<int> rents, int mortgageValue) : base(name)
    {
        this.price = price;
        this.rents = rents;
        this.mortgage = mortgageValue;
    }

    public int? OwnerPlayerNumber { get=>ownerPlayerNumber; set=>ownerPlayerNumber = value; }
    public int Price { get => price; }
    public List<int> Rents { get => rents; }
    public int Mortgage { get => mortgage; }
    private int CalCurrentRent()
    {
        return 1;
    }
}
