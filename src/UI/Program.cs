//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

internal class Program
{
    private static void Main(string[] args)
    {
        while(true)
        {
            Console.WriteLine("Do you want the statistics mode? Press Y, otherwise N");
            var readKey = Console.ReadKey();
            
            if(readKey.KeyChar == 'y'
            || readKey.KeyChar == 'Y')
            {
                Console.WriteLine("/nStatistics is working. Please wait a minute");
                
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

        }

        while(true)
        {
            Console.WriteLine("AutoMode = A, Manual = B");
            var readKey = Console.ReadKey();
            if(readKey.KeyChar == 'A'
            || readKey.KeyChar == 'a')
            {
                Console.WriteLine("Autometic Simulator is on. Wait a minute");
                Modes.RunAutoSimulator();
                break;
            }
            else if(readKey.KeyChar == 'B'
            || readKey.KeyChar == 'b')
            {
                Modes.RunManualSimulator();
                break;
            }
        }
    }
}

public static class Modes
{
    public static void RunAutoSimulator()
    {
        Game game = new Game(isBoardSmall:true);
        Visualizer visualizer = new Visualizer(game.Data, isBoardSmall:true);
        visualizer.SetFixedMessage2(string.Format("Auto-Simulator is on. The time interval is 10s"));
        System.Timers.Timer timer = new System.Timers.Timer(10000);

        timer.Elapsed += (sender, e) => Run5Times(game, visualizer);
        timer.Start();

        Console.ReadKey();
    }

    private static void Run5Times(Game game, Visualizer visualizer)
    {
        for (int i = 0; i < 5; i++)
        {
            game.SincRun();
            visualizer.UpdateLogging();
        }
        visualizer.Visualize();
    }

    public static void RunManualSimulator()
    {

        Game game = new Game(isBoardSmall:true);
        Visualizer visualizer = new Visualizer(game.Data, isBoardSmall:true);

        int termBetweenVisualizazions = 1;
        int termIndex = 0;

        while (true)
        {

            termIndex++;
            visualizer.UpdateLogging();
            visualizer.SetFixedMessage2(string.Format("Press 1~9 to adjust the progress speed : {0}", termBetweenVisualizazions));

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

            game.SincRun();
        }
    }
}