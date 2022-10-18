public class DataCenter : ICloneable
{
    private BankData bank;
    private BoardData board;
    private DoubleSideEffectData doubleSideEffect;
    private JailHandlerData jail;
    private List<TileData> tileDatas;
    private EventFlowData eventFlowData;

    public DataCenter(
        BankData bankData,
        BoardData boardData,
        DoubleSideEffectData doubleSideEffectData,
        JailHandlerData jailData,
        TileManager tileManager,
        EventFlowData eventFlowData)
    {
        this.bank = bankData;
        this.board = boardData;
        this.doubleSideEffect = doubleSideEffectData;
        this.jail = jailData;
        this.tileDatas = tileManager.tileDatas;
        this.eventFlowData = eventFlowData;
    }

    public BankData Bank => (BankData)this.bank.Clone();

    public BoardData Board => (BoardData)this.board.Clone();

    public DoubleSideEffectData DoubleSideEffect => (DoubleSideEffectData)this.doubleSideEffect.Clone();

    public JailHandlerData Jail => (JailHandlerData)this.jail.Clone();

    public EventFlowData EventFlow => (EventFlowData)this.eventFlowData.Clone();

    public List<TileData> TileDatas => new List<TileData>(this.tileDatas);

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        DataCenter clone = (DataCenter)this.MemberwiseClone();
        return clone;
    }
}
