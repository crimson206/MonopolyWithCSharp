
public class BoardEvent : Event
{
    private BoardHandler? boardHandler;
    public BoardEvent(Event previousEvent) : base(previousEvent)
    {
        
    }

    public void Move()
    {
        int movementAmount = this.eventFlow.RoolDiceResult.Sum();
        this.VisitHandlerDistributor(this.handlerDistrubutor);
        this.eventFlow.RecommentedString = this.stringPlayer + " moved " + movementAmount.ToString() + " steps.";
        this.boardHandler!.MovePlayerAroundBoard(this.playerNumber, movementAmount);
    }

    public void SetBoardHandler(BoardHandler boardHandler)
    {
        this.boardHandler = boardHandler;
    }

    private void VisitHandlerDistributor(HandlerDistrubutor handlerDistrubutor)
    {
        handlerDistrubutor.AcceptBoardEvent(this);
    }
}
