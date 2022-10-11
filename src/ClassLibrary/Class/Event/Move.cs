public class Move : Event
{
    public Move(Delegator delegator) : base(delegator)
    {
        this.delegator= delegator;
        this.delegator.move = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;
    private void Start(Board board)
    {
        this.delegator!.move = this.MovePlayer;

    }
    private void MovePlayer(Board board)
    {
        int amountToMove = this.delegator!.PlayerRollDiceResult.Sum();
        board.MovePlayerAroundBoard(playerNumber, amountToMove);
        delegator.move = this.PassedPlayerGo;
    }
    private void PassedPlayerGo(Board board)
    {
        if (board.PlayerPassedGo[playerNumber])
        {
            SetNextEvent(EventType.ReceiveSalary);
        }
        else
        {
            SetNextEvent(EventType.LandOnTile);
        }
    }
    protected override void SetNextEvent(EventType nextEvent)
    {
        this.delegator!.nextEvent = nextEvent;
        this.delegator.move = this.Start;
    }
}
