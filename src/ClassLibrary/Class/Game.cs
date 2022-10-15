    /// Developer Note
    ///  
    /// 1. Lines above Run() need to be refactored by creating an initializer 
    /// 2. All the objects are supposed to be independent on the UI System except for "ConnectConsolePrompt"
    /// 3. Since the codes can't interact with users themselves, 
    ///     it needs the connection to the system, so to run the game in the program, you need to design the "Prompter" as the connecter
    /// 4. To understand what is going on in "Run()", please go to the "Delegator"



public class Game
{

    public Delegator delegator = new Delegator();
    public BankHandler bank = new BankHandler();
    public TileManager tileManager = new TileManager();
    public DoubleSideEffectHandler doubleSideEffectManager = new DoubleSideEffectHandler();
    public JailHandler jailManager = new JailHandler();
    
    /// make basic set extractor for where is go, the size of tile, jail fine and so on.
    public BoardHandler board = new BoardHandler(40, 0);



    public void ConnectConsolePrompt(Prompter prompter)
    {
        this.delegator.ManualDecision = prompter.PromptBool;
    }



    public void Run()
    {
        delegator.RunEvent();
    }
}
