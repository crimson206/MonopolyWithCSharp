using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        TileManager tilesManager = new TileManager();
        MapTilesFactory mapTilesFactory = new MapTilesFactory();
        Random random = new Random();

        Board board = new Board(tilesManager.Tiles.Count(), 0);

        Visualizer visualizer = new Visualizer(board, tilesManager);

        visualizer.Setup(11, 11, 13, 4);

        Delegator delegator = new Delegator();
    
        List<Player> players = new List<Player> {new Player(), new Player(),new Player(),new Player()};

        DoubleSideEffectManager doubleSideEffectManager = new DoubleSideEffectManager();

        JailManager jailManager = new JailManager();
        Bank bank = new Bank();

        EventStorage eventStorage = new EventStorage(delegator, bank, board, tilesManager, jailManager, doubleSideEffectManager);
        

        delegator.nextEvent = eventStorage.tryToEscapeJail.Start;
        delegator.CurrentPlayerNumber = 0;
        int i = 0;
        visualizer.Visualize();
        while (true)
        {
            int playerNumber = delegator.CurrentPlayerNumber;
            

            if (delegator.boolDecisionType != null)
            {
                delegator.boolDecision = false;
            }

            if (delegator.playerRollDice is true)
            {
                delegator.RollDiceResult = Dice.Roll(random);
            }


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



