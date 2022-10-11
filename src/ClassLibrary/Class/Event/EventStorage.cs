
public class EventStorage
{
    public LandOnProperty landOnProperty;
    public LandOnTile landOnTile;
    public Move move;
    public ReceiveSalary receiveSalary;
    public RollToMove rollToMove;
    public TryToEscapeJail tryToEscapeJail;
    public EventStorage
    (
        Delegator delegator, Bank bank, Board board, TileManager tileManager,
        JailManager jailManager, DoubleSideEffectManager doubleSideEffectManager
    )
    {
        this.landOnProperty = new LandOnProperty(this, delegator, bank, board, tileManager);
        this.landOnTile = new LandOnTile(this, delegator, board, tileManager);
        this.move = new Move(this, delegator, board);
        this.receiveSalary = new ReceiveSalary(this, delegator, bank);
        this.tryToEscapeJail = new TryToEscapeJail(this, delegator, jailManager, bank);
        this.rollToMove = new RollToMove(this, delegator, doubleSideEffectManager);
    }
}
