
public class LandOnTile : Event
{
    private int playerPosition;
    private Tile? currentTile;
    public LandOnTile(Delegator delegator) : base(delegator)
    {
        this.delegator= delegator;
        delegator.landOnTile = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;

    private void Start(Board board, TileManager tileManager)
    {
        this.delegator!.landOnTile = this.CheckTile;
        this.playerPosition = board.PlayerPositions[playerNumber];
        this.currentTile = tileManager.Tiles[playerPosition];
    }
    private void CheckTile(Board board, TileManager tileManager)
    {
        
        if (this.currentTile is Property)
        {

            SetNextEvent(EventType.LandOnProperty);
        }
        else if (this.currentTile is Chance)
        {

            SetNextEvent(EventType.LandOnCardTile);
        }
        else if (this.currentTile is GoToJail)
        {

            SetNextEvent(EventType.GoToJail);        
        }
        else if (this.currentTile is TaxTile)
        {

            SetNextEvent(EventType.PayTax);
        }
        else
        {
            SetNextEvent(EventType.CheckExtraTurn);
        }
    }

    protected override void SetNextEvent(EventType nextEvent)
    {
        this.delegator!.nextEvent = nextEvent;
        this.delegator.landOnTile = this.Start;
    }

}
