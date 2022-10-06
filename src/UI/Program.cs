using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WindowHeight = 200;
        MapDrawer mapDrawer = new MapDrawer();

        mapDrawer.Test(10, 10, 13, 4);

    }
}
