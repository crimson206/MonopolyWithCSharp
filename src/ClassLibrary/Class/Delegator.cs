public class Delegator
{
    public delegate void DelTryToEscapeJail(Delegator delegator, int playerNumber, JailManager jailManager, Bank bank);
    public DelTryToEscapeJail? tryToEscapeJail;
    public delegate void RollToMove(Delegator delegator, int playerNumber, DoubleSideEffectManager doubleSideEffectManager);
    public RollToMove? rollToMove;
    public delegate void Move(Delegator delegator, int playerNumber, Board board);
    public Move? move;
    public delegate void ReceiveSalary(Delegator delegator, int playerNumber, Bank bank);
    public ReceiveSalary? receiveSalary;
    public delegate void LandOnTile(Delegator delegator, int playerNumber);
    public LandOnTile? landOnTile;
    public delegate void LandOnProperty(Delegator delegator, int playerNumber, Bank bank, TileManager tileManager);
    public LandOnProperty? landOnProperty;

    public bool playerRollDice;
    public EventType nextEvent;
    public BoolDecisionType? playerBoolDecision;
}
