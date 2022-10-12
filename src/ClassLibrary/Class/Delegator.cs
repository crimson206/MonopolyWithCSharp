/// There are main events such as RollToMove, Move, TryToEscapeJail and so on.
/// Think about how to make decisions
/// Think about how to implement sub events like Chance or CommunityChest
/// It passes the currentPlayerNumber
/// It can delegate the decision makers as it does for the events
/// Each decision maker has players' setting, and pass the decision result to this delegator
/// For the purpose, some events will need the DecisionMakerStorage
/// If many of events need the storage, then just spread them

public class Delegator
{
    private int currentPlayerNumber;
    private bool? boolDecision;
    private int[] rollDiceResult = new int[2];
    public int CurrentPlayerNumber { get => this.currentPlayerNumber; set => this.currentPlayerNumber = value;}
    public bool? BoolDecision { get => boolDecision; set => boolDecision = value; }
    public int[] RollDiceResult { get => rollDiceResult; set => this.rollDiceResult = value; }

    public delegate void DelDecisionMaker();
    public DelDecisionMaker? makeDecision;
    public delegate void DelEvent();
    public DelEvent? nextEvent;

    public void RunEvent()
    {
        if (this.makeDecision != null)
        {
            this.makeDecision();
        }
        else
        {
            this.nextEvent!();
        }
    }
}
