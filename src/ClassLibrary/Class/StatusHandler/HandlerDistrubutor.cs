
public class HandlerDistrubutor
{
    private BoardHandler boardHandler;
    private BankHandler bankHandler;
    private TileManager tileManager;
    private DoubleSideEffectHandler doubleSideEffectHandler;
    private JailHandler jailHandler;

    public HandlerDistrubutor(BoardHandler boardHandler, BankHandler bankHandler, TileManager tileManager, DoubleSideEffectHandler doubleSideEffectHandler, JailHandler jailHandler)
    {
        this.boardHandler = boardHandler;
        this.bankHandler = bankHandler;
        this.tileManager = tileManager;
        this.doubleSideEffectHandler = doubleSideEffectHandler;
        this.jailHandler = jailHandler;
    }

    public void AcceptBoardHandlerTaker(BoardHandlerTaker boardEvent)
    {
        boardEvent.SetBoardHandler(this.boardHandler);
    }

    public void AcceptBankHandlerTaker(BankHandlerTaker bankEvent)
    {
        bankEvent.SetBankHandler(this.bankHandler);
    }

    public void AcceptTileManagerTaker(TileManagerTaker tileEvent)
    {
        tileEvent.SetTileManager(this.tileManager);
    }

    public void AcceptDoubleSideEffectHandlerTaker(DoubleSideEffectHandlerTaker tileEvent)
    {
        tileEvent.SetDoubleSideEffectHandler(this.doubleSideEffectHandler);
    }

    public void AcceptJailHandlerTaker(JailHandlerTaker jailEvent)
    {
        jailEvent.SetJailHandler(this.jailHandler);
    }
}
