public class Events
{
    private MainEvent mainEvent;
    private AuctionEvent auctionEvent;
    private HouseBuildEvent houseBuildEvent;
    private TradeEvent tradeEvent;

    public Events(
        MainEvent mainEvent,
        AuctionEvent auctionEvent,
        HouseBuildEvent houseBuildEvent,
        TradeEvent tradeEvent)
    {
        this.mainEvent = mainEvent;
        this.auctionEvent = auctionEvent;
        this.houseBuildEvent = houseBuildEvent;
        this.tradeEvent = tradeEvent;
    }

    public MainEvent MainEvent => this.mainEvent;
    public AuctionEvent AuctionEvent => this.auctionEvent;
    public HouseBuildEvent HouseBuildEvent => this.houseBuildEvent;
    public TradeEvent TradeEvent => this.tradeEvent;
}