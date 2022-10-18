public class HasJailPenalty : Event
{
    private BoardHandler boardHandler => boardHandlerTaker.boardHandler!;
    private BoardHandlerTaker boardHandlerTaker;
    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;
    private TileManager tileManager => tileManagerTaker.tileManager!;
    private TileManagerTaker tileManagerTaker;

    public HasJailPenalty(Event previousEvent) : base(previousEvent)
    {
        this.boardHandlerTaker = new BoardHandlerTaker(this.handlerDistrubutor);
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
        this.tileManagerTaker = new TileManagerTaker(this.handlerDistrubutor);
    }

    public void ResetAndAddEvent()
    {
            int jailPosition = (from tile in this.tileManager.Tiles where tile is Jail select tileManager.Tiles.IndexOf(tile)).ToList()[0];
            this.jailHandler.CountTurnInJail(this.playerNumber);
            this.boardHandler.Teleport(this.playerNumber, jailPosition);
            this.eventFlow.RecommentedString = this.stringPlayer + " will have the jail penalty from the next turn";

            this.delegator.ResetAndAddFollowingEvent = this.events.EndTurn.ResetAndAddEvent;
    }
}
