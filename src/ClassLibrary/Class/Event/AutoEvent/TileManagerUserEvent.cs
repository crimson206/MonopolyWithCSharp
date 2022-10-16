
public class TileManagerUserEvent : HandlerUserEvent
{
    private Tile currentTile;
    private TileManager? tileManager;

    public TileManagerUserEvent(Event previousEvent) : base(previousEvent)
    {
        this.currentTile = this.GetCurrentTile();
    }

    public void LandOnTile()
    {
        this.eventFlow.RecommentedString = this.stringPlayer + " landed on " + this.currentTile.Name;

        if ( this.currentTile is Property )
        {
            this.newEvent = this.DealWithProperty;
        }
        else if ( currentTile is EventTile )
        {
            throw new NotImplementedException();
        }
        else if ( currentTile is GoToJail)
        {
            throw new NotImplementedException();
        }
        else if ( currentTile is TaxTile )
        {
            throw new NotImplementedException();
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public void DealWithProperty()
    {
        Property currentProperty = (Property) this.currentTile;

        throw new NotImplementedException();

    }

    public void SetTileManager(TileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    protected override void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptTileHandlerUserEvent(this);
    }

    private Tile GetCurrentTile()
    {
        int playerPosition = this.boardData.PlayerPositions[this.playerNumber];
        return this.tileManager!.Tiles[playerPosition];
    }

}
