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

        for (int i = 0; i < numLines; i++)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + i;
            Console.Write(loggingMsgs[i]);
        }
    }

    private List<string> loggingMsgs = new List<string>();

    public void UpdateLogging(string Msg)
    {
        loggingMsgs.RemoveAt(numLines - 1);
        loggingMsgs.Insert(0, Msg);
    }

}
