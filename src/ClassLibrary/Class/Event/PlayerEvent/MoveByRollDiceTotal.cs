public class MoveByRollDiceTotal : Event
{
    private BoardHandler boardHandler => boardHandlerTaker.boardHandler!;
    private BoardHandlerTaker boardHandlerTaker;
    public MoveByRollDiceTotal(Event previousEvent) : base(previousEvent)
    {
        this.boardHandlerTaker = new BoardHandlerTaker(this.handlerDistrubutor);
    }

    public void Pass()
    {
        int rollDiceResultTotal = this.eventFlow.RollDiceResult.Sum();

        this.boardHandler.MovePlayerAroundBoard(this.playerNumber, rollDiceResultTotal);
        this.eventFlow.RecommentedString = stringPlayer + " moved " + rollDiceResultTotal.ToString() + " steps";
    }

}
