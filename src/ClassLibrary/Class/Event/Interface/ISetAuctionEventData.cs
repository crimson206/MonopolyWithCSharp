public interface ISetAuctionEventData
{
    public IAuctionHandlerData AuctionHandlerData { set; }
    public List<int> Balances { set; }
    public int CurrentPlayerNumber { set; }
    public PropertyData CurrentPropertyData { set; }
    public int PlayerIntDecision { set; }
    public List<bool> AreInGame { set; }
}
