public interface ITileManager
{
    public List<ITile> Tiles { get; }

    public List<IProperty> Properties { get; }
    public List<IRealEstate> RealEstates { get; }
    public List<ITileData> TileDatas { get; }
    public IPropertyManager PropertyManager { get; }
    public List<IPropertyData> GetPropertyDatasWithOwnerNumber(int? playerNumber);
}