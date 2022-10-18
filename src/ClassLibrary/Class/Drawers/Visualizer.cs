
public class Visualizer
{
    private MapDrawer mapDrawer = new MapDrawer();
    private TileDrawer? tileDrawer;
    public LoggingDrawer loggingDrawer = new LoggingDrawer(8);
    private DisplayTileInfo displayTiles = new DisplayTileInfo();
    private PlayerStatusDrawer playerStatusDrawer = new PlayerStatusDrawer();
    private DataCenter data;
    private int mapWidth;
    private int mapHeight;
    private int tileWidth;
    private int tileHeight;
    private List<int[]> tileEdgeInfo = new List<int[]>();
    private List<int[]> innerMapEdge = new List<int[]>();
    private List<int> playerPositions => this.data.Board.PlayerPositions;
    private string recommendedString => this.data.EventFlow.RecommentedString;

    public Visualizer(DataCenter data)
    {
        this.data = data;
    }

///    private string recommendedString => this.data.Delegator.RecommendedString;

    public void Setup(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.tileWidth = tileWidth;
        this.tileHeight = tileHeight;
        this.tileEdgeInfo = mapDrawer.CreateTileEdgeCollection(mapWidth, mapHeight,  tileWidth,  tileHeight);
        this.innerMapEdge = mapDrawer.CreateInnerSpaceIndicator(mapWidth, mapHeight,  tileWidth,  tileHeight);
        this.tileDrawer = new TileDrawer(tileEdgeInfo);
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
        List<TileData> tileDatas = this.data.TileDatas;
        List<RealEstateData> realEstateDatas = (from tileData in tileDatas where tileData is RealEstateData select tileData as RealEstateData).ToList();
        List<RailRoadData> railRoadDatas = (from tileData in tileDatas where tileData is RailRoadData select tileData as RailRoadData).ToList();
        List<UtilityData> utilityDatas = (from tileData in tileDatas where tileData is UtilityData select tileData as UtilityData).ToList();        

        tileDrawer!.DrawPlayers(this.playerPositions);
        tileDrawer.DrawTiles(tileDatas);

        /// need
        displayTiles.DisplayRealEstates( innerMapEdge[0][0] + 5, innerMapEdge[0][1] + 1, realEstateDatas, 2);
        displayTiles.DisplayRailRoad( innerMapEdge[0][0] + 65, innerMapEdge[0][1] + 1, railRoadDatas, 2);
        displayTiles.DisplayUtility( innerMapEdge[0][0] + 65, innerMapEdge[0][1] + 8, utilityDatas, 2);
        playerStatusDrawer.DrawArrangedLines(innerMapEdge[0][0] + 65, innerMapEdge[0][1] + 13, data); 

        /// need


        loggingDrawer.DrawLogging(innerMapEdge[0][0] + 5, innerMapEdge[0][1] + 27);

        Console.CursorLeft = backUpCursorLeft;
        Console.CursorTop = backUpCursorTop;
        Console.WindowHeight = backupWindowHeight;
        Console.BufferHeight = backUpBufferHeight;
        Console.BufferWidth = backUpBufferWidth;
    }

}
