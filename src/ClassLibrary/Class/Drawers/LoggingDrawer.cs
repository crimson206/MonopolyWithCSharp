public class LoggingDrawer
{
    private int numLines;
    public LoggingDrawer(int numLines)
    {
        this.numLines = numLines;
        for (int i = 0; i < numLines; i++)
        {
            loggingMsgs.Add("");
        }
    }

    public void DrawLogging(int cursorLeft, int cursorTop)
    {
        Console.WindowHeight = 200;
        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;
        Console.Write(this.promptMessage);
        for (int i = 0; i < numLines; i++)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + 2 + i;
            Console.Write(loggingMsgs[i]);
        }
    }

    private List<string> loggingMsgs = new List<string>();
    private string promptMessage = String.Empty;

    public void UpdateLogging(string Msg)
    {
        if (Msg == String.Empty)
        {
            return;
        }
        else
        {
            loggingMsgs.RemoveAt(numLines - 1);
            loggingMsgs.Insert(0, Msg);
        }
    }

    public void UpdatePromptMessage(string promptMessage)
    {
        this.promptMessage = promptMessage;
    }

}
