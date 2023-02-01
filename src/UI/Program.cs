//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
internal class Program
{
    private static void Main(string[] args)
    {
        do
        {
            Console.WriteLine("Do you want the statistics mode? Press Y, otherwise N");
            var readKey = Console.ReadKey();
            
            if(readKey.KeyChar == 'y'
            || readKey.KeyChar == 'Y')
            {
                Console.WriteLine("/nStatistics is working. Please wait a minite");
                
                for (int i = 0; i < 100; i++)
                {
                    Statistics.GetInstance().PlayOneGame();
                }

                Statistics.GetInstance().PrintResult();
            }
            else if(readKey.KeyChar == 'n'
            || readKey.KeyChar == 'N')
            {
                break;
            }

        } while (true);

        bool isBoardSmall = true;

        Game game = new Game(isBoardSmall);
        Visualizer visualizer = new Visualizer(game.Data, isBoardSmall);

        ConsoleInteractor prompter = new ConsoleInteractor(visualizer);
        string oldRecommendedString = string.Empty;
        int termBetweenVisualizazions = 1;
        int termIndex = 0;

        while (true)
        {
            if (oldRecommendedString != game.Data.EventFlow.RecommendedString)
            {
                termIndex++;
                visualizer.UpdateLogging();
                visualizer.SetFixedMessage2(string.Format("Press 1~9 to adjust the progress speed : {0}", termBetweenVisualizazions));
                oldRecommendedString = game.Data.EventFlow.RecommendedString;

                if (termIndex % termBetweenVisualizazions == 0)
                {

                    visualizer.Visualize();
                    var readKey = Console.ReadKey();

                    if (char.IsDigit(readKey.KeyChar))
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
