public class Events : IEvents
{
    private IMainEvent mainEvent;
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
        this.SetEvents();
    }

    public IMainEvent MainEvent => this.mainEvent;
    public AuctionEvent AuctionEvent => this.auctionEvent;
    public HouseBuildEvent HouseBuildEvent => this.houseBuildEvent;
    public TradeEvent TradeEvent => this.tradeEvent;

    private void SetEvents()
    {
        this.mainEvent.SetEvents(this);
        this.houseBuildEvent.SetEvents(this);
        this.auctionEvent.SetEvents(this);
        this.tradeEvent.SetEvents(this);
    }
}