//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        Visualizer visualizer = new Visualizer(game.Data);
        visualizer.Setup(10, 8, 13, 3);
        ConsoleInteractor prompter = new ConsoleInteractor(visualizer);
        string oldRecommendedString = String.Empty;

        while (true)
        {
            if (oldRecommendedString != game.Data.EventFlow.RecommentedString)
            {
                visualizer.UpdateLogging();
                oldRecommendedString = game.Data.EventFlow.RecommentedString;
                Console.ReadKey();
            }

            visualizer.VisualizeSmallMap();

            game.Run();
        }
    }
}
