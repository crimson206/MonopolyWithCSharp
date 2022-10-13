public class Prompter : IPrompter
{
    private Visualizer visualizer;
    private ConsoleKeyInfo readKey;

    public Prompter(Visualizer visualizer)
    {
        this.visualizer = visualizer;
    }
    public bool PromptBool()
    {
        visualizer.UpdatePromptMessage("Press Y or N");
        visualizer.Visualize();

        do
        {
            readKey = Console.ReadKey();

            if ( this.readKey.Key == ConsoleKey.Y )
            {
                visualizer.UpdatePromptMessage("Waiting a signal");
                return true;
            }
            else if ( this.readKey.Key == ConsoleKey.N )
            {
                visualizer.UpdatePromptMessage("Waiting a signal");
                return false;
            }
        } while (true);
    }

}
