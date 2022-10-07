using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        TileManager tilesManager = new TileManager();
        MapTilesFactory mapTilesFactory = new MapTilesFactory();
        Random random = new Random();
        tilesManager.SetTiles(mapTilesFactory.CreateRandomMapTiles(22,4,2,3,3, random));

        Data data = new Data();
        data.UpdateTiles(tilesManager);

        Visualizer visualizer = new Visualizer(data);

        visualizer.Setup(11, 11, 13, 4);

        Delegator delegator = new Delegator();
    
        List<Player> players = new List<Player> {new Player(data), new Player(data),new Player(data),new Player(data)};

        TryToEscapeJail tryToEscapeJail = new TryToEscapeJail(data);

        JailManager jailManager = new JailManager();
        Bank bank = new Bank();

        delegator.nextEvent = EventType.TryToEscapeJail;
        delegator.tryToEscapeJail = tryToEscapeJail.Start;

        int i = 0;
        visualizer.Visualize();
        while (delegator.nextEvent == EventType.TryToEscapeJail)
        {
            int playerNumber = 0;
            

            if (delegator.playerBoolDecision != null)
            {
                players[playerNumber].WantToUseJailFreeCard();
            }

            if (delegator.playerRollDice is true)
            {
                players[playerNumber].RollDice(random);
            }

            delegator.tryToEscapeJail(delegator, playerNumber, jailManager, bank);

            visualizer.loggingDrawer.UpdateLogging(i.ToString());
            i++;
            visualizer.Visualize();
            Console.CursorLeft = 40;
            Console.CursorTop = 0;
            Console.ReadKey();
        }

    }

}



