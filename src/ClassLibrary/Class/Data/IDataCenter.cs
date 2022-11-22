public interface IDataCenter
{
    public IBankHandlerData Bank { get; }

    public IBoardHandlerData Board { get; }

    public IDoubleSideEffectHandlerData DoubleSideEffect { get; }

    public IJailHandlerData Jail { get; }
    public IInGameHandlerData InGame { get; }
    public IEventFlowData EventFlow { get; }

    public List<ITileData> TileDatas { get; }
    public IAuctionHandlerData AuctionHandler { get; }
    public ITradeHandlerData TradeHandler { get; }
    public ITileData CurrentTileData { get; }
    public IHouseBuildHandlerData HouseBuildHandler { get; }
    public ISellItemHandlerData SellItemHandler { get; }
}
