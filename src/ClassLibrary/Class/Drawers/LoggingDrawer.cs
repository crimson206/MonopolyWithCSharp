public class LoggingDrawer
{
    private List<string> loggingMsgs = new List<string>();
    private string fixedMessage1 = string.Empty;
    private string fixedMessage2 = string.Empty;
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
        Console.Write(this.fixedMessage1);
        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop + 1;
        Console.Write(this.fixedMessage2);
        for (int i = 0; i < this.numLines; i++)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + 3 + i;
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

    public void SetFixedMessage1(string message)
    {
        this.fixedMessage1 = message;
    }
    public void SetFixedMessage2(string message)
    {
        this.fixedMessage2 = message;
    }
}
