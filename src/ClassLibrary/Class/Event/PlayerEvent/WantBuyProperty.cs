public class WantBuyProperty : Event
{

    private Tile currentTile => this.GetCurrentTile();

    private TileManager tileManager => tileManagerTaker.tileManager!;
    private TileManagerTaker tileManagerTaker;
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;
    private BoardHandler boardHandler => boardHandlerTaker.boardHandler!;
    private BoardHandlerTaker boardHandlerTaker;

    public WantBuyProperty(Event previousEvent) : base(previousEvent)
    {
        this.tileManagerTaker = new TileManagerTaker(this.handlerDistrubutor);
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
        this.boardHandlerTaker = new BoardHandlerTaker(this.handlerDistrubutor);
    }

    public void ResetAndAddEvent()
    {
        Property currentProperty = (Property) currentTile;

        if (this.CopydecisionBool( this.bankHandler.Balances[this.playerNumber] >= 2 * currentProperty.Price))
        {
            this.tileManager.propertyManager.ChangeOwner(currentProperty, this.playerNumber);
            this.bankHandler.DecreaseBalance(this.playerNumber, currentProperty.Price);
            this.eventFlow.RecommentedString = this.stringPlayer + " bought the property";

            this.delegator.ResetAndAddFollowingEvent = this.events.CheckAndDoExtraTurn.ResetAndAddEvent;
        }
        else
        {
            this.eventFlow.RecommentedString = this.stringPlayer + " didn't buy the property";

            this.delegator.ResetAndAddFollowingEvent = this.events.CheckAndDoExtraTurn.ResetAndAddEvent;
        }
    }
    public Tile GetCurrentTile()
    {
        int playerPosition = this.boardHandler.PlayerPositions[this.playerNumber];
        return this.tileManager.Tiles[playerPosition];
    }
}
