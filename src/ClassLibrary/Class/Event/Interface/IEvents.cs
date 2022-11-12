public interface IEvents
{
    public MainEvent MainEvent { get; }
    public AuctionEvent AuctionEvent { get; }
    public HouseBuildEvent HouseBuildEvent { get; }
    public TradeEvent TradeEvent { get; }
}