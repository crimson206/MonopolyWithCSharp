public class Move : Event
{
    private BoardHandler board;

    public Move(EventStorage eventStorage, Delegator delegator, BoardHandler board) : base(eventStorage, delegator)
    {
        this.events = eventStorage;
        this.board = board;
        this.delegator= delegator;
        this.delegator.NextEvent = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;
    public override void Start()
    {
        this.delegator!.NextEvent = this.MovePlayer;

    }
    private void MovePlayer()
    {
        int amountToMove = this.delegator!.RollDiceResult.Sum();
        board.MovePlayerAroundBoard(playerNumber, amountToMove);
        delegator.NextEvent = this.PassedPlayerGo;
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
    protected override void SetNextEvent(Event gameEvent)
    {

        this.delegator.NextEvent = gameEvent.Start;
    }
}
