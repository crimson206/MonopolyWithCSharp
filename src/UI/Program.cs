using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        Visualizer visualizer = new Visualizer(game.Data);
        visualizer.Setup(11, 11, 13, 4);
        ConsoleInteractor prompter = new ConsoleInteractor(visualizer);
    string oldRecommendedString = String.Empty;
        
        while (true)
        {
            if ( oldRecommendedString != game.Data.EventFlow.RecommentedString )
            {
                visualizer.UpdateLogging();
                oldRecommendedString = game.Data.EventFlow.RecommentedString;
                Console.ReadKey();
            }

            visualizer.Visualize();

            
            game.Run();


        }
    }

}
