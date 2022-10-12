
public class Examples
{
    public void run()
    {
        TileManager tilesManager = new TileManager();
        MapTilesFactory mapTilesFactory = new MapTilesFactory();
        Random random = new Random();

        Board board = new Board(tilesManager.Tiles.Count(), 0);

        Visualizer visualizer = new Visualizer(board, tilesManager);

        visualizer.Setup(11, 11, 13, 4);

        Delegator delegator = new Delegator();
        PromptDrawer prompter = new PromptDrawer();
    
        List<Player> players = new List<Player> {new Player(), new Player(),new Player(),new Player()};

        DoubleSideEffectManager doubleSideEffectManager = new DoubleSideEffectManager();
        DecisionMakerStorage decisionMakerStorage = new DecisionMakerStorage(prompter, delegator);

        JailManager jailManager = new JailManager();
        Bank bank = new Bank();

        EventStorage eventStorage = new EventStorage(delegator, bank, board, tilesManager, jailManager, doubleSideEffectManager, decisionMakerStorage);
        

        delegator.nextEvent = eventStorage.tryToEscapeJail.Start;
        delegator.CurrentPlayerNumber = 0;
        int i = 0;
        visualizer.Visualize();
        while (true)
        {
            int playerNumber = delegator.CurrentPlayerNumber;
            

            delegator.nextEvent();


            




            visualizer.loggingDrawer.UpdateLogging(i.ToString());
            i++;
            visualizer.Visualize();
            Console.CursorLeft = 40;
            Console.CursorTop = 0;
            Console.ReadKey();
        }
    }
}
