
public class Game
{

    private Delegator delegator = new Delegator();
    private BankHandler bankHandler = new BankHandler();
    private TileManager tileManager = new TileManager();
    private DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
    private JailHandler jailManager = new JailHandler();
    private BoardHandler boardHandler = new BoardHandler();
    private DataCenter dataCenter;

    public Game()
    {
        this.dataCenter = this.GenerateDataCenter();
    }

    public DataCenter Data => (DataCenter)this.dataCenter.Clone();

    public void Run()
    {
        delegator.RunEvent();
    }

    private DataCenter GenerateDataCenter()
    {
        BankData bankdata = new BankData(this.bankHandler);
        BoardData boardData = new BoardData(this.boardHandler);
        DoubleSideEffectData doubleSideEffectData = new DoubleSideEffectData(this.doubleSideEffectHandler);
        JailHandlerData jailData = new JailHandlerData(this.jailManager);
        DelegatorData delegatorData = new DelegatorData(this.delegator);

        return new DataCenter(bankdata, boardData, doubleSideEffectData, jailData, delegatorData, this.tileManager);
    }

    private void SetBoardInfo()
    {
        this.boardHandler.SetInfo(this.dataCenter.TileDatas);
    }
}
