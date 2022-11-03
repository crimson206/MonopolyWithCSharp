public interface IDataForAuctionEvent
{
    public List<int> Balances { get; }
    public int CurrentPlayerNumber { get; }
    public List<bool> AreInGame { get; }
    public IPropertyData CurrentPropertyData { get; }
}
