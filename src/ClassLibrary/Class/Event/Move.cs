public class Move : Event
{
    private Board board;

    public Move(EventStorage eventStorage, Delegator delegator, Board board) : base(eventStorage, delegator)
    {
        this.eventStorage = eventStorage;
        this.board = board;
        this.delegator= delegator;
        this.delegator.nextEvent = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;
    public override void Start()
    {
        this.delegator!.nextEvent = this.MovePlayer;

    }
    private void MovePlayer()
    {
        int amountToMove = this.delegator!.PlayerRollDiceResult.Sum();
        board.MovePlayerAroundBoard(playerNumber, amountToMove);
        delegator.nextEvent = this.PassedPlayerGo;
    }
    private void PassedPlayerGo()
    {
        if (board.PlayerPassedGo[playerNumber])
        {
            ///SetNextEvent(EventType.ReceiveSalary);
        }
        else
        {
            ///SetNextEvent(EventType.LandOnTile);
        }
    }
    protected void SetNextEvent(Event gameEvent)
    {

        this.delegator.nextEvent = gameEvent.Start;
    }
}
