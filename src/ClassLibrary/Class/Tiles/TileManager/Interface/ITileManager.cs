public interface ITileManager
{
    public List<Tile> Tiles { get; }

    public List<Property> Properties { get; }
    public List<RealEstate> RealEstates { get; }
    public List<ITileData> TileDatas { get; }
    public IPropertyManager PropertyManager { get; }
    public List<IPropertyData> GetPropertyDatasWithOwnerNumber(int? playerNumber);
}