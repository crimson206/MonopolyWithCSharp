
public class LandOnTile : Event
{
    private int playerPosition;
    private Tile? currentTile;
    private Board board;
    private TileManager tileManager;
    public LandOnTile(EventStorage eventStorage, Delegator delegator, Board board, TileManager tileManager) : base(eventStorage, delegator)
    {
        this.eventStorage = eventStorage;
        this.board = board;
        this.tileManager = tileManager;
        this.delegator= delegator;
        delegator.nextEvent = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;

    public override void Start()
    {
        this.delegator!.nextEvent = this.CheckTile;
        this.playerPosition = board.PlayerPositions[playerNumber];
        this.currentTile = tileManager.Tiles[playerPosition];
    }
    private void CheckTile()
    {
        
        if (this.currentTile is Property)
        {

            ///SetNextEvent(EventType.LandOnProperty);
        }
        else if (this.currentTile is Chance)
        {

            ///SetNextEvent(EventType.LandOnCardTile);
        }
        else if (this.currentTile is GoToJail)
        {

            ///SetNextEvent(EventType.GoToJail);        
        }
        else if (this.currentTile is TaxTile)
        {

            ///SetNextEvent(EventType.PayTax);
        }
        else
        {
            ///SetNextEvent(EventType.CheckExtraTurn);
        }
    }

    protected override void SetNextEvent(Event gameEvent)
    {

        this.delegator.nextEvent = gameEvent.Start;
    }

}
