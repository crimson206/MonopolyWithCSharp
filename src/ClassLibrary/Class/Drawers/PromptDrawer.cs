
public class PromptDrawer
{
    private string message = String.Empty;
    private bool? receivedBool;
    private ConsoleKeyInfo readKey;
    public bool? ReceivedBool { get => receivedBool;}
    public void PromptBool()
    {
        this.message = "Press Y or N key";
        readKey = Console.ReadKey();

        if ( this.readKey.Key == ConsoleKey.Y )
        {
            this.receivedBool = true;
        }
        else if ( this.readKey.Key == ConsoleKey.Y )
        {
            this.receivedBool = false;
        }
        else
        {
            this.receivedBool = null;
        }
    }

    public void ResetPrompt()
    {
        this.receivedBool = null;
    }

    public void DrawPrompt(int cursorLeft, int cursorTop)
    {
        Console.WindowHeight = 200;

        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;
    }
}
