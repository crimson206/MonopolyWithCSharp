public class ConsoleInteractor : IPrompter
{
    private Visualizer visualizer;
    private ConsoleKeyInfo readKey;

    public ConsoleInteractor(Visualizer visualizer)
    {
        this.visualizer = visualizer;
    }

    public bool PromptBool()
    {
        this.visualizer.UpdatePromptMessage("Press Y or N");
        this.visualizer.Visualize();

        do
        {
            this.readKey = Console.ReadKey();

            if (this.readKey.Key == ConsoleKey.Y)
            {
                this.visualizer.UpdatePromptMessage("Press any key to continue");
                return true;
            }
            else if (this.readKey.Key == ConsoleKey.N)
            {
                this.visualizer.UpdatePromptMessage("Press any key to continue");
                return false;
            }
        }
        while (true);
    }
}
