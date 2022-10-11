public class Delegator
{
    private int currentPlayerNumber;
    private bool playerBoolDecision;
    private int[] rollDiceResult = new int[2];
    public int CurrentPlayerNumber { get => this.currentPlayerNumber; set => this.currentPlayerNumber = value;}
    public bool PlayerBoolDecision { get => playerBoolDecision; set => playerBoolDecision = value; }
    public int[] PlayerRollDiceResult { get => rollDiceResult; set => this.rollDiceResult = value; }
    public delegate void DelTryToEscapeJail(JailManager jailManager, Bank bank);
    public DelTryToEscapeJail? tryToEscapeJail;
    public delegate void RollToMove(DoubleSideEffectManager doubleSideEffectManager);
    public RollToMove? rollToMove;
    public delegate void Move(Board board);
    public Move? move;
    public delegate void ReceiveSalary(Bank bank);
    public ReceiveSalary? receiveSalary;
    public delegate void LandOnTile(Board board, TileManager tileManager);
    public LandOnTile? landOnTile;
    public delegate void LandOnProperty( Bank bank, Board board,  TileManager tileManager);
    public LandOnProperty? landOnProperty;

    public bool playerRollDice;
    public EventType nextEvent;
    public BoolDecisionType? boolDecisionType;
}
