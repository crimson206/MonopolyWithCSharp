public class DataCenter : ICloneable
{
    private IBankHandlerData bank;
    private IBoardHandlerData board;
    private IDoubleSideEffectHandlerData doubleSideEffect;
    private IJailHandlerData jail;
    private IInGameHandlerData inGame;
    private List<TileData> tileDatas;
    private EventFlowData eventFlowData;

    public DataCenter(
        IBankHandlerData bankData,
        IBoardHandlerData boardData,
        IDoubleSideEffectHandlerData doubleSideEffectData,
        IJailHandlerData jailData,
        IInGameHandlerData inGameData,
        TileManager tileManager,
        EventFlowData eventFlowData
        )
    {
        this.bank = bankData;
        this.board = boardData;
        this.doubleSideEffect = doubleSideEffectData;
        this.jail = jailData;
        this.tileDatas = tileManager.TileDatas;
        this.inGame = inGameData;
        this.eventFlowData = eventFlowData;
    }

    public IBankHandlerData Bank => this.bank;

    public IBoardHandlerData Board => this.board;

    public IDoubleSideEffectHandlerData DoubleSideEffect => this.doubleSideEffect;

    public IJailHandlerData Jail => this.jail;
    public IInGameHandlerData InGame => this.inGame;
    public EventFlowData EventFlow => (EventFlowData)this.eventFlowData.Clone();

    public List<TileData> TileDatas => new List<TileData>(this.tileDatas);

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        DataCenter clone = (DataCenter)this.MemberwiseClone();
        return clone;
    }
}
