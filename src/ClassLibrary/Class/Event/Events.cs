public class Events : IEvents
{
    private IMainEvent mainEvent;
    private AuctionEvent auctionEvent;
    private HouseBuildEvent houseBuildEvent;
    private TradeEvent tradeEvent;
    private SellItemEvent sellItemEvent;
    private DemortgageEvent demortgageEvent;

    public Events(
        MainEvent mainEvent,
        AuctionEvent auctionEvent,
        HouseBuildEvent houseBuildEvent,
        TradeEvent tradeEvent,
        SellItemEvent sellItemEvent,
        DemortgageEvent demortgageEvent)
    {
        this.mainEvent = mainEvent;
        this.auctionEvent = auctionEvent;
        this.houseBuildEvent = houseBuildEvent;
        this.tradeEvent = tradeEvent;
        this.sellItemEvent = sellItemEvent;
        this.demortgageEvent = demortgageEvent;
        this.SetEvents();
    }

    public IMainEvent MainEvent => this.mainEvent;
    public AuctionEvent AuctionEvent => this.auctionEvent;
    public HouseBuildEvent HouseBuildEvent => this.houseBuildEvent;
    public TradeEvent TradeEvent => this.tradeEvent;
    public SellItemEvent SellItemEvent => this.sellItemEvent;
    public DemortgageEvent DemortgageEvent => this.demortgageEvent;

    private void SetEvents()
    {
        this.mainEvent.SetEvents(this);
        this.houseBuildEvent.SetEvents(this);
        this.auctionEvent.SetEvents(this);
        this.tradeEvent.SetEvents(this);
        this.sellItemEvent.SetEvents(this);
        this.demortgageEvent.SetEvents(this);
    }
}