using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        Visualizer visualizer = new Visualizer(game.Data.Board, new TileManager(), game.delega)
        visualizer.Setup(11, 11, 13, 4);
        Prompter prompter = new Prompter(visualizer);
        game.ConnectConsolePrompt(prompter);

        
        while (true)
        {
            visualizer.UpdateLogging();
            visualizer.Visualize();
            Console.ReadKey();
            
            game.Run();
        }
    }

}
