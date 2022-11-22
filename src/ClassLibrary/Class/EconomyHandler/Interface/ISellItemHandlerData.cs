public interface ISellItemHandlerData
{
    public List<IPropertyData> MortgagibleProperties { get; }
    public List<IPropertyData> RealEstatesWithDistructableHouse { get; }
    public List<IPropertyData> SoldableItemWithAuction { get; }
    public IPropertyData? PropertyToAuction { get; }
    public IPropertyData? RealEstateToDistructHouse { get; }
    public IPropertyData? PropertyToMortgage { get; }
    public SellingType? SellingOption { get; }

}