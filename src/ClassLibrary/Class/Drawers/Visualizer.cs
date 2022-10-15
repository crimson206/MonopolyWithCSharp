
public class Visualizer
{
    private MapDrawer mapDrawer = new MapDrawer();
    private PlayerDrawer? playerDrawer;
    public LoggingDrawer loggingDrawer = new LoggingDrawer(5);
    private DisplayTiles displayTiles = new DisplayTiles();
    private DataCenter data;
    private int mapWidth;
    private int mapHeight;
    private int tileWidth;
    private int tileHeight;
    private List<int[]> tileEdgeInfo = new List<int[]>();
    private List<int[]> innerMapEdge = new List<int[]>();
    private List<int> playerPositions => this.data.Board.PlayerPositions;

    public Visualizer(DataCenter data)
    {
        this.data = data;
    }

    private string recommendedString => this.data.Delegator.RecommendedString;

    public void Setup(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.tileWidth = tileWidth;
        this.tileHeight = tileHeight;
        this.tileEdgeInfo = mapDrawer.CreateTileEdgeCollection(mapWidth, mapHeight,  tileWidth,  tileHeight);
        this.innerMapEdge = mapDrawer.CreateInnerSpaceIndicator(mapWidth, mapHeight,  tileWidth,  tileHeight);
        this.playerDrawer = new PlayerDrawer(tileEdgeInfo);
    }

    public void UpdatePromptMessage(string promptMessage)
    {
        this.loggingDrawer.UpdatePromptMessage(promptMessage);
    }
    public void UpdateLogging()
    {
        loggingDrawer.UpdateLogging(this.recommendedString);
    }

    int backUpCursorLeft = Console.CursorLeft;
    int backUpCursorTop = Console.CursorTop;
    int backupWindowHeight = Console.WindowHeight;
    int backUpBufferHeight = Console.BufferHeight;
    int backUpBufferWidth = Console.BufferWidth;

    public void Visualize()
    {
        Console.Clear();
        Console.WindowHeight = 150;

        mapDrawer.DrawMap(mapWidth, mapHeight,  tileWidth,  tileHeight);

        /// need

        playerDrawer!.DrawPlayers(this.playerPositions);



        /// need
        List<TileData> tileDatas = this.data.TileDataSet;
        List<RealEstateData> realEstates = (from tileData in tileDatas where tileData is RealEstateData select tileData as RealEstateData).ToList();
        List<RailRoadData> railRoads = (from tileData in tileDatas where tileData is RailRoadData select tileData as RailRoadData).ToList();

        /// need
        displayTiles.DisplayRealEstates2( innerMapEdge[0][0] + 5, innerMapEdge[0][1] + 1, realEstates, 2);
        displayTiles.DisplayRailRoad( innerMapEdge[0][0] + 65, innerMapEdge[0][1] + 1, railRoads, 2);

        /// need
        loggingDrawer.DrawLogging(innerMapEdge[0][0] + 5, innerMapEdge[0][1] + 27);

        Console.CursorLeft = backUpCursorLeft;
        Console.CursorTop = backUpCursorTop;
        Console.WindowHeight = backupWindowHeight;
        Console.BufferHeight = backUpBufferHeight;
        Console.BufferWidth = backUpBufferWidth;
    }

    private List<RealEstate> FilterRealEstates(List<Tile> tiles)
    {
        var query = from tile in tiles where tile is RealEstate select tile as RealEstate;
        List<RealEstate> realEstates = query.ToList();
        return realEstates;
    }

    private List<RailRoad> FilterRailRoads(List<Tile> tiles)
    {
        var query = from tile in tiles where tile is RailRoad select tile as RailRoad;
        List<RailRoad> railRoads = query.ToList();
        return railRoads;
    }
}
