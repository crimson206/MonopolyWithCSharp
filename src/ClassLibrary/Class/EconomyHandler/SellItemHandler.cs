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
    public List<IPropertyData> HouseDistructableRealEstates => this.GetRealEstatesWithDistructableHouse();
    public List<IPropertyData> AuctionableProperties => this.GetSoldableItemsWithAuction();
    public IPropertyData? PropertyToAuction { get; private set; }
    public IPropertyData? RealEstateToDistructHouse { get; private set; }
    public IPropertyData? PropertyToMortgage { get; private set; }
    public SellingType? sellingType { get; private set; }

    public void SetSellingOption(Dictionary<SellingType, int> itemToSell)
    {
        this.sellingType = itemToSell.Keys.ElementAt(0);
        int indexOfItemToSell = itemToSell.Values.ElementAt(0);

        switch (sellingType)
        {
            case SellingType.Auction_A_Property:
                this.PropertyToAuction = this.AuctionableProperties[indexOfItemToSell];
                break;
            case SellingType.Mortgage_A_Property:
                this.PropertyToMortgage = this.MortgagibleProperties[indexOfItemToSell];
                break;
            case SellingType.Distruct_A_House:
                this.RealEstateToDistructHouse = this.HouseDistructableRealEstates[indexOfItemToSell];
                break;
            case SellingType.None:
                break;
            default:
                throw new Exception();
        }
    }

    public void ResetPropertyToChange()
    {
        this.PropertyToAuction = null;
        this.PropertyToMortgage = null;
        this.RealEstateToDistructHouse = null;
        this.sellingType = null;
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
        List<IRealEstateData> realEstates = this.backUpPropertyData.Where(property => property is IRealEstateData).Cast<IRealEstateData>().ToList();

        List<IPropertyData> realEstatesWithDistructableHouse =
            (from realEstate in realEstates
            where realEstate.OwnerPlayerNumber == this.playerToSellItem
            && realEstate.IsHouseDistructable
            select realEstate).Cast<IPropertyData>().ToList();

        return realEstatesWithDistructableHouse;                    
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
