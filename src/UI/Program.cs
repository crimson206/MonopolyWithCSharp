internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        MapDrawer mapDrawer = new MapDrawer();
        Console.WindowHeight = 100;
        mapDrawer.Test(mapWidth:4, mapHeight:3, tileWidth:10, tileHeight:3);


        Console.ReadKey();
    }
}
