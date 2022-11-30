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
        int termBetweenVisualizazions = 1;
        int termIndex = 0;

        while (game.Data.EventFlow.GameIsOver is false)
        {
            if (oldRecommendedString != game.Data.EventFlow.RecommendedString)
            {
                termIndex++;
                visualizer.UpdateLogging();
                oldRecommendedString = game.Data.EventFlow.RecommendedString;

                if (termIndex % termBetweenVisualizazions == 0)
                {
                    visualizer.Visualize();
                    var readKey = Console.ReadKey();

                    if ( char.IsDigit(readKey.KeyChar))
                    {
                        int newTerm = int.Parse(readKey.KeyChar.ToString()!);
                        if (newTerm != 0)
                        {
                            termBetweenVisualizazions = newTerm;
                        }
                    }
                }
            }

            game.Run();
        }
    }
}
