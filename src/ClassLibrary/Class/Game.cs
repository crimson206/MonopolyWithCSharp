    /// Developer Note
    ///  
    /// 1. Lines above Run() need to be refactored by creating an initializer 
    /// 2. All the objects are supposed to be independent on the UI System except for "ConnectConsolePrompt"
    /// 3. Since the codes can't interact with users themselves, 
    ///     it needs the connection to the system, so to run the game in the program, you need to design the "Prompter" as the connecter
    /// 4. To understand what is going on in "Run()", please go to the "Delegator"


    /// Understanding "nextEvent"! (this note starts from "Delegator.cs")
    /// 10. In the loop of "Run()", delegator will make a decision, and run the next event again.
    /// 11. Go to "TryToEscapeJail.cs"


public class Game
{



    public Delegator delegator = new Delegator();
    public BankHandler bank = new BankHandler();
    public TileManager tileManager = new TileManager();
    public DoubleSideEffectHandler doubleSideEffectManager = new DoubleSideEffectHandler();
    public JailHandler jailManager = new JailHandler();
    
    /// make basic set extractor for where is go, the size of tile, jail fine and so on.
    public BoardHandler board = new BoardHandler(40, 0);
    public DecisionMakerStorage decisionMakerStorage;
    public EventStorage events;

    public Game()
    {
        this.decisionMakerStorage = new DecisionMakerStorage(delegator);
        this.events = new EventStorage(delegator, bank, board, tileManager, jailManager, doubleSideEffectManager, this.decisionMakerStorage);
    }

    public void ConnectConsolePrompt(Prompter prompter)
    {
        this.delegator.manualDecision = prompter.PromptBool;
    }

    public void Run()
    {
        while (true)
        {
            delegator.ResetRecommendedString();

            if (delegator.IsThereDecisionMaking)
            {
                delegator.MakeDecision();
            }
            else
            {
                delegator.RunEvent();
            }
        }

    }



}
