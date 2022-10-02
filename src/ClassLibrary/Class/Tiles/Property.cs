public class Property : Tile
{
    private int? ownerID = null;
    private int price;
    private List<int> rents = new List<int>();
    private int mortgage;
    
    public Property(int position, string name, int price, List<int> rents, int mortgageValue) : base(position, name)
    {
        this.price = price;
        this.rents = rents;
        this.mortgage = mortgageValue;
    }

    public int? OwnerID { get=>ownerID; set=>ownerID = value; }
    public int Price { get => price; }
    public List<int> Rents { get => rents; }
    public int Mortgage { get => mortgage; }
}
