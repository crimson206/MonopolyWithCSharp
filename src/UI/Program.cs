using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        Visualizer visualizer = new Visualizer(game.board, game.tileManager, game.delegator);
        visualizer.Setup(11, 11, 13, 4);
        Prompter prompter = new Prompter(visualizer);
        game.ConnectConsolePrompt(prompter);

        game.delegator.nextEvent = game.events.tryToEscapeJail.Start;
        
        while (true)
        {
            visualizer.Visualize();
            game.Run();
        }
    }

}



