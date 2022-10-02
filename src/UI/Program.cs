internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        MapDrawer mapDrawer = new MapDrawer();
        Console.WindowHeight = 100;
        mapDrawer.Test(mapWidth:11, mapHeight:11, tileWidth:13, tileHeight:3);

        int[] position = mapDrawer.CreateTileEdgeCollection(mapWidth:11, mapHeight:11, tileWidth:13, tileHeight:3)[16];

        Console.SetCursorPosition(position[0]+1, position[1]+2);

        Console.Write("P1 P2 P3 P4");

        Console.ReadKey();
    }
}
