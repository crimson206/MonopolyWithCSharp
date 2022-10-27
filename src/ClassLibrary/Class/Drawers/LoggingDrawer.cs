public class LoggingDrawer
{
    private List<string> loggingMsgs = new List<string>();
    private string promptMessage = "Press any key to continue";
    private int numLines;

    public LoggingDrawer(int numLines)
    {
        this.numLines = numLines;
        for (int i = 0; i < numLines; i++)
        {
            this.loggingMsgs.Add(string.Empty);
        }
    }

    public void DrawLogging(int cursorLeft, int cursorTop)
    {
        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;
        Console.Write(this.promptMessage);
        for (int i = 0; i < this.numLines; i++)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + 2 + i;
            Console.Write(this.loggingMsgs[i]);
        }
    }

    public void UpdateLogging(string msg)
    {
        if (msg == string.Empty)
        {
            return;
        }
        else
        {
            this.loggingMsgs.RemoveAt(this.numLines - 1);
            this.loggingMsgs.Insert(0, msg);
        }
    }

    public void UpdatePromptMessage(string promptMessage)
    {
        this.promptMessage = promptMessage;
    }
}
