public interface IEconomyHandlers
{
    public AuctionHandler AuctionHandler { get; }

    public TradeHandler TradeHandler { get; }

    public HouseBuildHandler HouseBuildHandler { get; }

    public SellItemHandler SellItemHandler { get; }

    public DemortgageHandler DemortgageHandler { get; }
}