public class SellItemHandler : ISellItemHandler
{
    private List<IPropertyData> backUpPropertyData = new List<IPropertyData>();
    private int playerToSellItem;

    public void SetPlayerToSellItems(int playerNumber, List<IPropertyData> properties)
    {
        this.playerToSellItem = playerNumber;
        this.backUpPropertyData = properties;
    }

    public List<IPropertyData> MortgagibleProperties => this.GetMortgagibleItems();
    public List<IPropertyData> RealEstatesWithDistructableHouse => this.GetRealEstatesWithDistructableHouse();
    public List<IPropertyData> SoldableItemWithAuction => this.GetSoldableItemsWithAuction();
    public IPropertyData? PropertyToAuction { get; private set; }
    public IPropertyData? RealEstateToDistructHouse { get; private set; }
    public IPropertyData? PropertyToMortgage { get; private set; }
    public SellingType? SellingOption { get; private set; }

    public void SetPropertyToAuction(int index)
    {
        this.PropertyToAuction = this.SoldableItemWithAuction[index];
        this.SellingOption = SellingType.AuctionProperty;
    }

    public void SetRealEstateToBuildHouse(int index)
    {
        this.RealEstateToDistructHouse =  this.RealEstatesWithDistructableHouse[index];
        this.SellingOption = SellingType.DistructHouse;
    }

    public void SetPropertyToMortgage(int index)
    {
        this.PropertyToMortgage = this.MortgagibleProperties[index];
        this.SellingOption = SellingType.AuctionProperty;
    }

    public void ResetPropertyToChange()
    {
        this.PropertyToAuction = null;
        this.PropertyToMortgage = null;
        this.RealEstateToDistructHouse = null;
        this.SellingOption = null;
    }

    private List<IPropertyData> GetSoldableItemsWithAuction()
    {
        List<IPropertyData> soldableItemsWithAuction =
            (from property in this.backUpPropertyData
            where property.OwnerPlayerNumber == this.playerToSellItem
            && property.IsSoldableWithAuction
            select property).ToList();

        return soldableItemsWithAuction;                    
    }

    private List<IPropertyData> GetRealEstatesWithDistructableHouse()
    {

        List<IPropertyData> soldableItemsWithAuction =
            (from realEstate in this.backUpPropertyData.Cast<IRealEstateData>()
            where realEstate.OwnerPlayerNumber == this.playerToSellItem
            && realEstate.IsHouseDistructable
            select realEstate).Cast<IPropertyData>().ToList();

        return soldableItemsWithAuction;                    
    }

    private List<IPropertyData> GetMortgagibleItems()
    {
        List<IPropertyData> mortgagibleItems =
            (from property in this.backUpPropertyData
            where property.OwnerPlayerNumber == this.playerToSellItem
            && property.IsMortgagible
            select property).ToList();

        return mortgagibleItems;   
    }
}