public abstract class PropertyData : TileData
{
    private Property property;

    public PropertyData(Tile tile)
        : base(tile)
    {
        this.property = (Property)tile;
    }

    public int? OwnerPlayerNumber => this.property.OwnerPlayerNumber;

    public int Price => this.property.Price;

    public List<int> Rents => this.property.Rents;

    public int CurrentRent => this.property.CurrentRent;

    public int Mortgage => this.property.Mortgage;

    public bool IsMortgaged => this.property.IsMortgaged;

    public bool IsMortgagible => this.property.IsMortgagible;
}
