
public class TileManagerTaker : IHandlerDistributorVisitor
{
    public TileManager? tileManager;
    private HandlerDistrubutor handlerDistrubutor;
    public TileManagerTaker(HandlerDistrubutor handlerDistrubutor)
    {
        this.handlerDistrubutor = handlerDistrubutor;
        this.VisitHandlerDistributor();
    }
    public void SetTileManager(TileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    public void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptTileManagerTaker(this);
    }
}
