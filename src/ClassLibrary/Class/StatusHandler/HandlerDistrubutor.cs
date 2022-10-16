
public class HandlerDistrubutor
{
    private BoardHandler boardHandler;
    private BankHandler bankHandler;
    private TileManager tileManager;

    public void AcceptBoardEvent(BoardEvent boardEvent)
    {
        boardEvent.SetBoardHandler(this.boardHandler);
    }

    public void AcceptBankEvent(BankEvent bankEvent)
    {
        bankEvent.SetBankHandler(this.bankHandler);
    }

    public void AcceptTileEvent(TileEvent tileEvent)
    {
        tileEvent.SetTileManager(this.tileManager);
    }

}
