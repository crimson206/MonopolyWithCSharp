public interface IDataCenter
{
    public IBankHandlerData Bank { get; }

    public IBoardHandlerData Board { get; }

    public IDoubleSideEffectHandlerData DoubleSideEffect { get; }

    public IJailHandlerData Jail { get; }
    public IInGameHandlerData InGame { get; }
    public EventFlowData EventFlow { get; }

    public List<TileData> TileDatas { get; }
    public IAuctionHandlerData AuctionHandler { get; }
    public TileData CurrentTileData{ get; }
}