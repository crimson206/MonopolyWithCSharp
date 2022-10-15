
public class DataCenter : ICloneable
{
    private BankData bank;
    private BoardData board;
    private DoubleSideEffectData doubleSideEffect;
    private JailData jail;

    public DataCenter(BankData bankData, BoardData boardData, DoubleSideEffectData doubleSideEffectData, JailData jailData)
    {
        this.bank = bankData;
        this.board = boardData;
        this.doubleSideEffect = doubleSideEffectData;
        this.jail = jailData;
    }

    public BankData Bank => (BankData) this.bank.Clone();
    public BoardData Board => (BoardData) this.board.Clone();
    public DoubleSideEffectData DoubleSideEffect => (DoubleSideEffectData) this.doubleSideEffect.Clone();
    public JailData Jail => (JailData) this.jail.Clone();

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        DataCenter clone = (DataCenter) this.MemberwiseClone();
        return clone;
    }
}
