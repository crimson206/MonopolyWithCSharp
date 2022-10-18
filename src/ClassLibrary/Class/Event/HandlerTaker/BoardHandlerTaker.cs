
public class BoardHandlerTaker : IHandlerDistributorVisitor
{
    public BoardHandler? boardHandler;
    private HandlerDistrubutor handlerDistrubutor;
    public BoardHandlerTaker(HandlerDistrubutor handlerDistrubutor)
    {
        this.handlerDistrubutor = handlerDistrubutor;
        this.VisitHandlerDistributor();
    }
    public void SetBoardHandler(BoardHandler boardHandler)
    {
        this.boardHandler = boardHandler;
    }

    public void VisitHandlerDistributor()
    {
        this.handlerDistrubutor.AcceptBoardHandlerTaker(this);
    }
}
