public class DataCenter : IDataCenter
{
    private IBankHandlerData bank;
    private IBoardHandlerData board;
    private IDoubleSideEffectHandlerData doubleSideEffect;
    private IJailHandlerData jail;
    private IInGameHandlerData inGame;
    private List<ITileData> tileDatas;
    private IAuctionHandlerData auctionHandler;
    private ITradeHandlerData tradeHandler;
    private IEventFlowData eventFlow;
    private IHouseBuildHandlerData houseBuildHandler;
    public ISellItemHandlerData sellItemHandler;
    public IDemortgageHandlerData demortgageHandler;

    public DataCenter(
        IStatusHandlers statusHandlers,
        IEconomyHandlers economyHandlers,
        ITileManager tileManager
        )
    {
        this.bank = statusHandlers.BankHandler;
        this.board = statusHandlers.BoardHandler;
        this.doubleSideEffect = statusHandlers.DoubleSideEffectHandler;
        this.jail = statusHandlers.JailHandler;
        this.inGame = statusHandlers.InGameHandler;
        this.eventFlow = statusHandlers.EventFlow;

        this.tileDatas = tileManager.TileDatas;
        this.auctionHandler = economyHandlers.AuctionHandler;
        this.tradeHandler = economyHandlers.TradeHandler;
        this.houseBuildHandler = economyHandlers.HouseBuildHandler;
        this.sellItemHandler = economyHandlers.SellItemHandler;
        this.demortgageHandler = economyHandlers.DemortgageHandler;
    }

    public IBankHandlerData Bank => this.bank;

    public IBoardHandlerData Board => this.board;

    public IDoubleSideEffectHandlerData DoubleSideEffect => this.doubleSideEffect;

    public IJailHandlerData Jail => this.jail;
    public IInGameHandlerData InGame => this.inGame;
    public IEventFlowData EventFlow => this.eventFlow;
    public IAuctionHandlerData AuctionHandler => this.auctionHandler;
    public ITradeHandlerData TradeHandler => this.tradeHandler;
    public IHouseBuildHandlerData HouseBuildHandler => this.houseBuildHandler;
    public ISellItemHandlerData SellItemHandler => this.sellItemHandler;
    public IDemortgageHandlerData DemortgageHandler => this.demortgageHandler;
    public List<ITileData> TileDatas => new List<ITileData>(this.tileDatas);
    public ITileData CurrentTileData => this.GetCurrentTileData();
    private ITileData GetCurrentTileData()
    {
        int currentPlayerNumber = this.eventFlow.CurrentPlayerNumber;
        int currentPlayerPosition = this.board.PlayerPositions[currentPlayerNumber];
        ITileData currentTile = this.tileDatas[currentPlayerPosition];
        return currentTile;
    }
}
