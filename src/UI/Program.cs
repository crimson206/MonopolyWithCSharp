//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
internal class Program
{
    private static void Main(string[] args)
    {

        bool isBoardSmall = true;

        Game game = new Game(isBoardSmall);
        Visualizer visualizer = new Visualizer(game.Data, isBoardSmall);

        ConsoleInteractor prompter = new ConsoleInteractor(visualizer);
        string oldRecommendedString = string.Empty;

        while (true)
        {
            if (oldRecommendedString != game.Data.EventFlow.RecommendedString)
            {
                visualizer.UpdateLogging();
                oldRecommendedString = game.Data.EventFlow.RecommendedString;
                Console.ReadKey();
            }

            visualizer.Visualize();

            game.Run();
        }
    }
}
