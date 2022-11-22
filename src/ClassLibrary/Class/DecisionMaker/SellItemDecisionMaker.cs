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
            itemToSell.Add(SellingType.MortgageProperty, 0);

        }
        else if (this.sellItemHandlerData.RealEstatesWithDistructableHouse.Count() != 0)
        {
            itemToSell.Add(SellingType.DistructHouse, 0);

        }
        else
        {
            itemToSell.Add(SellingType.AuctionProperty, 0);

        }

        return itemToSell;
    }
}