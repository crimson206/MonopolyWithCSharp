public interface IPropertyData : ITileData
{
    public int? OwnerPlayerNumber { get; }

    public int Price { get; }

    public List<int> Rents { get; }

    public int CurrentRent { get; }

    public int Mortgage { get; }

    public bool IsMortgaged { get; }

    public bool IsMortgagible { get; }

    public bool IsTradable { get; }
    public bool IsSoldableWithAuction { get; }
}