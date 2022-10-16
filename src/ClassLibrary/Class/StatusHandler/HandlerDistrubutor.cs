
public class HandlerDistrubutor
{
    private BoardHandler boardHandler;
    private BankHandler bankHandler;
    private TileManager tileManager;
    private DoubleSideEffectHandler doubleSideEffectHandler;

    public HandlerDistrubutor(BoardHandler boardHandler, BankHandler bankHandler, TileManager tileManager, DoubleSideEffectHandler doubleSideEffectHandler)
    {
        this.boardHandler = boardHandler;
        this.bankHandler = bankHandler;
        this.tileManager = tileManager;
        this.doubleSideEffectHandler = doubleSideEffectHandler;
    }

    public void AcceptBoardHandlerUserEvent(BoardHandlerUserEvent boardEvent)
    {
        boardEvent.SetBoardHandler(this.boardHandler);
    }

    public void AcceptBankHandlerUserEvent(BankHandlerUserEvent bankEvent)
    {
        bankEvent.SetBankHandler(this.bankHandler);
    }

    public void AcceptTileHandlerUserEvent(TileManagerUserEvent tileEvent)
    {
        tileEvent.SetTileManager(this.tileManager);
    }

    public void AcceptDoubleSideEffectEvent(DoubleSideEffectUserEvent tileEvent)
    {
        tileEvent.SetDoubleSideEffectHandler(this.doubleSideEffectHandler);
    }

}
