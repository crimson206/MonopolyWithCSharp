public class Move : Event
{
    private Data data;
    private void Start(Delegator delegator, int playerNumber, Board board)
    {
        delegator.move = this.MovePlayer;
    }
    private void MovePlayer(Delegator delegator, int playerNumber, Board board)
    {
        int amountToMove = data.LastRollDiceResults[playerNumber].Sum();
        board.MovePlayerAroundBoard(playerNumber, amountToMove);
        delegator.move = this.PassedPlayerGo;
    }
    private void PassedPlayerGo(Delegator delegator, int playerNumber, Board board)
    {
        if (data.PlayerPassedGo[playerNumber])
        {
            SetNextEvent(delegator, EventType.ReceiveSalary);
        }
        else
        {
            SetNextEvent(delegator, EventType.LandOnTile);
        }
    }
    protected override void SetNextEvent(Delegator delegator, EventType nextEvent)
    {
        delegator.nextEvent = nextEvent;
        delegator.move = this.Start;
    }
}
