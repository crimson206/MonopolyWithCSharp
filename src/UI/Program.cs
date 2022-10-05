using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        int OrigCol = Console.CursorLeft;
        int OrigRow = Console.CursorTop;
        Console.WindowHeight = 200;

        TimeEventer timeEventer = new TimeEventer(OrigCol, OrigRow);

        Thread t = new Thread(ThreadProc);
        t.Start();
        AutoResetEvent autoEvent = new AutoResetEvent(false);
        MapDrawer mapDrawer = new MapDrawer();
        Timer timer = new Timer(timeEventer.TimeEvent, autoEvent, 1000, 2000);
        
        autoEvent.WaitOne();

        timer.Dispose();

        void ThreadProc()
        {
            while(Console.ReadKey().Key != ConsoleKey.B)
            {
                while(Console.ReadKey().Key != ConsoleKey.A)
                {
                    Console.ReadKey();
                }
                Console.Write("IamFreeEEE");
            }
        }

    }
}
class TimeEventer
{
    public MapDrawer mapDrawer = new MapDrawer();
    public int origCol;
    public int origRow;

    public TimeEventer(int origCol, int origRow)
    {
        origCol = origCol;
        origRow = origRow;
    }

    int index = 0 ;
    public void TimeEvent(Object stateInfo)
    {
        AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
        Console.WindowHeight = 200;
        mapDrawer.OrigCol = origCol + (index%10)*4;
        mapDrawer.OrigRow = origRow + (index%10)*4;
        mapDrawer.DrawMap(5, 5, 10, 3);
        Console.WriteLine(index);
        index++;
        if (index == 100)
        {
            autoEvent.Set();
        }
    }

}