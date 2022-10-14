
public class EventStorage
{
    public LandOnProperty landOnProperty;
    public LandOnTile landOnTile;
    public Move move;
    public ReceiveSalary receiveSalary;
    public RollToMove rollToMove;
    public TryToEscapeJail tryToEscapeJail;
    public EscapeJail escapeJail;
    public EndTurn endTurn;
    public EventStorage
    (
        Delegator delegator, BankHandler bank, BoardHandler board, TileManager tileManager,
        JailHandler jailManager, DoubleSideEffectHandler doubleSideEffectManager, DecisionMakerStorage decisionMakerStorage
    )
    {
        this.landOnProperty = new LandOnProperty(this, delegator, bank, board, tileManager);
        this.landOnTile = new LandOnTile(this, delegator, board, tileManager);
        this.move = new Move(this, delegator, board);
        this.receiveSalary = new ReceiveSalary(this, delegator, bank);
        this.tryToEscapeJail = new TryToEscapeJail(this, delegator, jailManager, bank, decisionMakerStorage);
        this.rollToMove = new RollToMove(this, delegator, doubleSideEffectManager);
        this.escapeJail = new EscapeJail(this, delegator);
        this.endTurn = new EndTurn(this, delegator);
    }
}
