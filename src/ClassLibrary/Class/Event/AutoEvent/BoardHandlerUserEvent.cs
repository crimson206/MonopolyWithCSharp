
public class BoardHandlerUserEvent : HandlerUserEvent
{
    private BoardHandler? boardHandler;
    public BoardHandlerUserEvent(Event previousEvent) : base(previousEvent)
    {
    }

    public void Move()
    {
        int movementAmount = this.eventFlow.RoolDiceResult.Sum();
        this.boardHandler!.MovePlayerAroundBoard(this.playerNumber, movementAmount);

        this.eventFlow.RecommentedString = this.stringPlayer + " moved " + movementAmount.ToString() + " steps.";

        if (this.CopyConditionBool(this.boardData.PlayerPassedGo[playerNumber]))
        {
            this.newEvent = this.events.BankEvent.ReceiveSalary;
        }
        this.newEvent = this.events.TileManagerUserEvent.LandOnTile;
    }

    public void GoToJailDoubleSideEffect()
    {
        throw new NotImplementedException();
    }

    public void SetBoardHandler(BoardHandler boardHandler)
    {
        this.boardHandler = boardHandler;
    }

    protected override void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptBoardHandlerUserEvent(this);
    }
}
