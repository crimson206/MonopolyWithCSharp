public class EconomyHandlers : IEconomyHandlers
{
    private AuctionHandler auctionHandler = new AuctionHandler();
    private TradeHandler tradeHandler = new TradeHandler();

    public AuctionHandler AuctionHandler => this.auctionHandler;

    public TradeHandler TradeHandler => this.tradeHandler;
}
