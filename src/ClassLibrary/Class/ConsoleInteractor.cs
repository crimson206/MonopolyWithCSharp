public class ConsoleInteractor
{
    private Visualizer visualizer;
    private ConsoleKeyInfo readKey;

    public ConsoleInteractor(Visualizer visualizer)
    {
        this.visualizer = visualizer;
    }

    public bool PromptBool()
    {
        this.visualizer.SetFixedMessage1("Press Y or N");
        this.visualizer.VisualizeLargeMap();

        do
        {
            this.readKey = Console.ReadKey();

            if (this.readKey.Key == ConsoleKey.Y)
            {
                this.visualizer.SetFixedMessage1("Press any key to continue");
                return true;
            }
            else if (this.readKey.Key == ConsoleKey.N)
            {
                this.visualizer.SetFixedMessage1("Press any key to continue");
                return false;
            }
        }
        while (true);
    }
}
