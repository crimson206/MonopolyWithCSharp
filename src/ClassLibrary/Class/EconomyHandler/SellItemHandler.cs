public class SellItemHandler
{
    private List<IPropertyData> backUpPropertyData = new List<IPropertyData>();
    private int playerToSellItem;

    public void SetPlayerToSellItems(int playerNumber, List<IPropertyData> properties)
    {
        this.playerToSellItem = playerNumber;
        this.backUpPropertyData = properties;
    }

    public List<IPropertyData> MortgagibleItems => this.GetMortgagibleItems();
    public List<IPropertyData> RealEstatesWithDistructableHouse => this.GetRealEstatesWithDistructableHouse();
    public List<IPropertyData> SoldableItemWithAuction => this.GetSoldableItemsWithAuction();

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