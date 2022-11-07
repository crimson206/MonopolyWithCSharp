public class Events
{
    private MainEvent mainEvent;
    private AuctionEvent auctionEvent;

    public Events(MainEvent mainEvent, AuctionEvent auctionEvent)
    {
        this.mainEvent = mainEvent;
        this.auctionEvent = auctionEvent;
    }

    public MainEvent MainEvent => this.mainEvent;
    public AuctionEvent AuctionEvent => this.auctionEvent;
}