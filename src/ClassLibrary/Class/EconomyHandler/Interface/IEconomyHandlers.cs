public interface IEconomyHandlers
{
    public AuctionHandler AuctionHandler { get; }

    public TradeHandler TradeHandler { get; }

public HouseBuildHandler HouseBuildHandler { get; }
}