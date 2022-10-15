using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        Visualizer visualizer = new Visualizer(game.Data);
        visualizer.Setup(11, 11, 13, 4);
        ConsoleInteractor prompter = new ConsoleInteractor(visualizer);
        game.ConnectConsoleInteractor(prompter);

        
        while (true)
        {
            visualizer.UpdateLogging();
            visualizer.Visualize();
            Console.ReadKey();
            
            game.Run();
        }
    }

}
