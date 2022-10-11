public class Delegator
{
    private int currentPlayerNumber;
    private bool playerBoolDecision;
    private int[] rollDiceResult = new int[2];
    public int CurrentPlayerNumber { get => this.currentPlayerNumber; set => this.currentPlayerNumber = value;}
    public bool PlayerBoolDecision { get => playerBoolDecision; set => playerBoolDecision = value; }
    public int[] PlayerRollDiceResult { get => rollDiceResult; set => this.rollDiceResult = value; }






    public bool playerRollDice;

    public BoolDecisionType? boolDecisionType;
    public delegate void DelEvent();
    public DelEvent nextEvent;

    public void SetNextEvent(Event gameEvent)
    {
        this.nextEvent = gameEvent.Start;
    }
}
