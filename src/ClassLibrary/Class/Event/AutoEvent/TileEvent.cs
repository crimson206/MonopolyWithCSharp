
public class TileEvent : Event
{
    private Tile currentTile => this.GetCurrentTile();
    private TileManager tileManager;

    public TileEvent(Event previousEvent) : base(previousEvent)
    {
        
    }

    public void LandOnTile()
    {
        this.eventFlow.RecommentedString = this.stringPlayer + " landed on " + this.currentTile.Name;

        if ( this.currentTile is Property )
        {
            this.nextEvent += this.DealWithProperty;
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

    }

    public void SetTileManager(TileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    private void VisitHandlerDistributor(HandlerDistrubutor handlerDistrubutor)
    {
        handlerDistrubutor.AcceptTileEvent(this);
    }

    private Tile GetCurrentTile()
    {
        int playerPosition = this.boardData.PlayerPositions[this.playerNumber];
        return this.tileManager.Tiles[playerPosition];
    }

}
