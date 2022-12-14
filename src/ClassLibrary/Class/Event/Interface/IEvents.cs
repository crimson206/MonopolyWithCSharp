public interface IEvents
{
    public IMainEvent MainEvent { get; }
    public AuctionEvent AuctionEvent { get; }
    public HouseBuildEvent HouseBuildEvent { get; }
    public TradeEvent TradeEvent { get; }
    public SellItemEvent SellItemEvent { get; }
    public UnmortgageEvent UnmortgageEvent { get; }
}