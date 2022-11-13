public class EconomyHandlers : IEconomyHandlers
{
    private AuctionHandler auctionHandler = new AuctionHandler();
    private TradeHandler tradeHandler = new TradeHandler();
    private HouseBuildHandler houseBuildHandler = new HouseBuildHandler();

    public AuctionHandler AuctionHandler => this.auctionHandler;

    public TradeHandler TradeHandler => this.tradeHandler;

    public HouseBuildHandler HouseBuildHandler => this.houseBuildHandler;
}
