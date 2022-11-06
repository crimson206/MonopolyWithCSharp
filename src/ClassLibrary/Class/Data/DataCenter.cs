public class DataCenter : ICloneable, IDataCenter
{
    private IBankHandlerData bank;
    private IBoardHandlerData board;
    private IDoubleSideEffectHandlerData doubleSideEffect;
    private IJailHandlerData jail;
    private IInGameHandlerData inGame;
    private List<TileData> tileDatas;
    private IAuctionHandlerData auctionHandler;
    private EventFlowData eventFlowData;

    public DataCenter(
        IBankHandlerData bankData,
        IBoardHandlerData boardData,
        IDoubleSideEffectHandlerData doubleSideEffectData,
        IJailHandlerData jailData,
        IInGameHandlerData inGameData,
        IAuctionHandlerData auctionHandlerData,
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
        this.auctionHandler = auctionHandlerData;
        this.eventFlowData = eventFlowData;
    }

    public IBankHandlerData Bank => this.bank;

    public IBoardHandlerData Board => this.board;

    public IDoubleSideEffectHandlerData DoubleSideEffect => this.doubleSideEffect;

    public IJailHandlerData Jail => this.jail;
    public IInGameHandlerData InGame => this.inGame;
    public EventFlowData EventFlow => (EventFlowData)this.eventFlowData.Clone();
    public IAuctionHandlerData AuctionHandler => this.auctionHandler;
    public List<TileData> TileDatas => new List<TileData>(this.tileDatas);
    public TileData CurrentTileData => this.GetCurrentTileData();
    private TileData GetCurrentTileData()
    {
        int currentPlayerNumber = this.eventFlowData.CurrentPlayerNumber;
        int currentPlayerPosition = this.board.PlayerPositions[currentPlayerNumber];
        TileData currentTile = this.tileDatas[currentPlayerPosition];
        return currentTile;
    }
    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        DataCenter clone = (DataCenter)this.MemberwiseClone();
        return clone;
    }
}
