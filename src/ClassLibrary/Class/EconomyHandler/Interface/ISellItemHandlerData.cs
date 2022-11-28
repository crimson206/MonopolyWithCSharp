public interface ISellItemHandlerData
{
    public List<IPropertyData> MortgagibleProperties { get; }
    public List<IPropertyData> HouseDistructableRealEstates { get; }
    public List<IPropertyData> AuctionableProperties { get; }
    public IPropertyData? PropertyToAuction { get; }
    public IPropertyData? RealEstateToDistructHouse { get; }
    public IPropertyData? PropertyToMortgage { get; }
    public SellingType? sellingType { get; }

}