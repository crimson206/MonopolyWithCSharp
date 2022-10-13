
public class Game
{
    public Delegator delegator = new Delegator();
    public Bank bank = new Bank();
    public TileManager tileManager = new TileManager();
    public DoubleSideEffectManager doubleSideEffectManager = new DoubleSideEffectManager();
    public JailManager jailManager = new JailManager();
    
    /// make basic set extractor for where is go, the size of tile, jail fine and so on.
    public Board board = new Board(40, 0);
    public DecisionMakerStorage decisionMakerStorage;
    public EventStorage events;

    public Game()
    {
        this.decisionMakerStorage = new DecisionMakerStorage(delegator);
        this.events = new EventStorage(delegator, bank, board, tileManager, jailManager, doubleSideEffectManager, this.decisionMakerStorage);
    }

    public void Run()
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

    public void ConnectConsolePrompt(Prompter prompter)
    {
        this.delegator.manualDecision = prompter.PromptBool;
    }

}
