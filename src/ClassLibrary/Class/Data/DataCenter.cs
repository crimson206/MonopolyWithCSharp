
public class DataCenter : ICloneable
{
    private BankData bank;
    private BoardData board;
    private DoubleSideEffectData doubleSideEffect;
    private JailHandlerData jail;
    private DelegatorData delegator;
    private List<TileData> tileDataSet;

    public DataCenter(BankData bankData, BoardData boardData, DoubleSideEffectData doubleSideEffectData, JailHandlerData jailData, DelegatorData delegatorData, TileManager tileManager)
    {
        this.bank = bankData;
        this.board = boardData;
        this.doubleSideEffect = doubleSideEffectData;
        this.jail = jailData;
        this.tileDataSet = tileManager.tileDataSet;
        this.delegator = delegatorData;
    }

    public BankData Bank => (BankData) this.bank.Clone();
    public BoardData Board => (BoardData) this.board.Clone();
    public DoubleSideEffectData DoubleSideEffect => (DoubleSideEffectData) this.doubleSideEffect.Clone();
    public JailHandlerData Jail => (JailHandlerData) this.jail.Clone();
    public DelegatorData Delegator => (DelegatorData) this.delegator.Clone();
    public List<TileData> TileDataSet => new List<TileData>(this.tileDataSet);

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        DataCenter clone = (DataCenter) this.MemberwiseClone();
        return clone;
    }
}
