    /// Developer Note
    ///  
    /// 1. Lines above Run() need to be refactored by creating an initializer 
    /// 2. All the objects are supposed to be independent on the UI System except for "ConnectConsolePrompt"
    /// 3. Since the codes can't interact with users themselves, 
    ///     it needs the connection to the system, so to run the game in the program, you need to design the "Prompter" as the connecter
    /// 4. To understand what is going on in "Run()", please go to the "Delegator"



public class Game
{

    private Delegator delegator = new Delegator();
    private BankHandler bank = new BankHandler();
    private TileManager tileManager = new TileManager();
    private DoubleSideEffectHandler doubleSideEffectManager = new DoubleSideEffectHandler();
    private JailHandler jailManager = new JailHandler();
    private BoardHandler boardData = new BoardHandler(40, 0);
    private DataCenter dataCenter;

    public Game()
    {
        BankData bankdata = new BankData(this.bank);
        BoardData boardData = new BoardData(this.boardData);
        DoubleSideEffectData doubleSideEffectData = new DoubleSideEffectData(this.doubleSideEffectManager);
        JailData jailData = new JailData(this.jailManager);
        this.dataCenter = new DataCenter(bankdata, boardData, doubleSideEffectData, jailData);
    }

    public DataCenter Data => (DataCenter) this.dataCenter.Clone();
    public void ConnectConsolePrompt(Prompter prompter)
    {
        this.delegator.ManualDecision = prompter.PromptBool;
    }

    public void Run()
    {
        delegator.RunEvent();
    }
}
