public class Property : Tile
{
    protected int? ownerPlayerNumber = null;
    protected int price;
    protected List<int> rents = new List<int>();
    protected int currentRent;
    protected int mortgage;
    protected int password;
    
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
    public void SetCurrentRent(int password, int rent)
    {
        if ( password == this.password)
        {
            this.currentRent = rent;
        }
    }
    public void SetOnwerPlayerNumber(int password, int playerNumber)
    {
        if ( password == this.password)
        {
            this.ownerPlayerNumber = playerNumber;
        }
    }
}
