public class SellItemDecisionMaker : DecisionMaker, ISellItemDecisionMaker
{    
    private ISellItemHandlerData sellItemHandlerData;

    public SellItemDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
        this.sellItemHandlerData = dataCenter.SellItemHandler;
    }

    public Dictionary<SellingType, int> MakeDecisionOnItemToSell()
    {
        Dictionary<SellingType, int> itemToSell = new Dictionary<SellingType, int> ();

        if (this.sellItemHandlerData.MortgagibleProperties.Count() != 0)
        {
            itemToSell.Add(SellingType.Mortgage_A_Property, 0);

        }
        else if (this.sellItemHandlerData.AuctionableProperties.Count() != 0)
        {
            itemToSell.Add(SellingType.Auction_A_Property, 0);
        }
        else if (this.sellItemHandlerData.HouseDistructableRealEstates.Count() != 0)
        {
            itemToSell.Add(SellingType.Distruct_A_House, 0);
        }
        else
        {
            itemToSell.Add(SellingType.None, 0);
        }

        return itemToSell;
    }
}