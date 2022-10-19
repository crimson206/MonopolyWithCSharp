
public class Game
{

    private Delegator delegator;
    private BankHandler bankHandler;
    private TileManager tileManager;
    private DoubleSideEffectHandler doubleSideEffectHandler;
    private JailHandler jailHandler;
    private BoardHandler boardHandler;
    private EventFlow eventFlow;
    private DataCenter dataCenter;
    public BoolCopier boolCopier;

    private TestEvent testEvent;
    private TestEvent2 testEvent2;

    public Game()
    {
        this.bankHandler = new BankHandler();
        this.tileManager = new TileManager();
        this.boolCopier = new BoolCopier();
        this.doubleSideEffectHandler = new DoubleSideEffectHandler();
        this.delegator = new Delegator();
        this.eventFlow = new EventFlow();
        this.jailHandler = new JailHandler();
        this.boardHandler = new BoardHandler();

        this.dataCenter = this.GenerateDataCenter();

        this.SetBoardInfo();

        this.testEvent2 = this.GetTestEvent2();

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
        JailHandlerData jailData = new JailHandlerData(this.jailHandler);
        EventFlowData eventFlowData = new EventFlowData(this.eventFlow);

        return new DataCenter(bankdata, boardData, doubleSideEffectData, jailData, this.tileManager, eventFlowData);
    }

    private void SetBoardInfo()
    {
        this.boardHandler.SetInfo(this.dataCenter.TileDatas);
    }


    public TestEvent GetTestEvent1()
    {
        return new TestEvent(this.bankHandler, this.boardHandler, this.doubleSideEffectHandler, this.tileManager, this.jailHandler, this.eventFlow, this.delegator);
    }

    public TestEvent2 GetTestEvent2()
    {

        return new TestEvent2(this.bankHandler, this.boardHandler, this.doubleSideEffectHandler, this.tileManager, this.jailHandler, this.eventFlow, this.delegator);

    }
}
