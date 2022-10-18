
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
    private HandlerDistrubutor handlerDistributor;
    public BoolCopier boolCopier;

    public StartTurn debugger;

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
        this.handlerDistributor = this.GenerateHandlerDistributor();

        this.SetBoardInfo();

        this.debugger = this.GenerateEvent();

        this.delegator.ResetAndAddFollowingEvent = this.debugger.ResetAndAddEvent;

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

    private HandlerDistrubutor GenerateHandlerDistributor()
    {
        return new HandlerDistrubutor(this.boardHandler, this.bankHandler, this.tileManager, this.doubleSideEffectHandler, this.jailHandler);
    }

    private void SetBoardInfo()
    {
        this.boardHandler.SetInfo(this.dataCenter.TileDatas);
    }

    public StartTurn GenerateEvent()
    {
        return new StartTurn(this.delegator, this.boolCopier, this.eventFlow, this.handlerDistributor);
    }

}
