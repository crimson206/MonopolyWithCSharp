
public class Visualizer
{
    private int backUpCursorLeft = Console.CursorLeft;
    private int backUpCursorTop = Console.CursorTop;
    private int backupWindowHeight = Console.WindowHeight;
    private int backUpBufferHeight = Console.BufferHeight;
    private int backUpBufferWidth = Console.BufferWidth;

    private LoggingDrawer loggingDrawer = new LoggingDrawer(4);
    private MapDrawer mapDrawer = new MapDrawer();
    private TileDrawer? tileDrawer;
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

    public void Setup(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.tileWidth = tileWidth;
        this.tileHeight = tileHeight;
        this.tileEdgeInfo = mapDrawer.CreateTileEdgeCollection(mapWidth, mapHeight, tileWidth, tileHeight);
        this.innerMapEdge = mapDrawer.CreateInnerSpaceIndicator(mapWidth, mapHeight, tileWidth, tileHeight);
        this.tileDrawer = new TileDrawer(tileEdgeInfo);
    }

    public void UpdatePromptMessage(string promptMessage)
    {
        this.loggingDrawer.UpdatePromptMessage(promptMessage);
    }

    public void UpdateLogging()
    {
        this.loggingDrawer.UpdateLogging(this.recommendedString);
    }

    public void Visualize()
    {
        Console.Clear();
        Console.WindowHeight = 150;

        this.mapDrawer.DrawMap(this.mapWidth, this.mapHeight, this.tileWidth, this.tileHeight);

        List<TileData> tileDatas = this.data.TileDatas;
        List<RealEstateData> realEstateDatas = (from tileData in tileDatas where tileData is RealEstateData select tileData as RealEstateData).ToList();
        List<RailRoadData> railRoadDatas = (from tileData in tileDatas where tileData is RailRoadData select tileData as RailRoadData).ToList();
        List<UtilityData> utilityDatas = (from tileData in tileDatas where tileData is UtilityData select tileData as UtilityData).ToList();        

        this.tileDrawer!.DrawPlayers(this.playerPositions);
        this.tileDrawer.DrawTiles(tileDatas);

        this.displayTiles.DisplayRealEstates(this.innerMapEdge[0][0] + 5, this.innerMapEdge[0][1] + 1, realEstateDatas, 2);
        this.displayTiles.DisplayRailRoad(this.innerMapEdge[0][0] + 65, this.innerMapEdge[0][1] + 1, railRoadDatas, 2);
        this.displayTiles.DisplayUtility(this.innerMapEdge[0][0] + 65, this.innerMapEdge[0][1] + 8, utilityDatas, 2);
        this.playerStatusDrawer.DrawArrangedLines(this.innerMapEdge[0][0] + 65, this.innerMapEdge[0][1] + 13, this.data);

        this.loggingDrawer.DrawLogging(this.innerMapEdge[0][0] + 5, this.innerMapEdge[0][1] + 27);

        Console.CursorLeft = this.backUpCursorLeft;
        Console.CursorTop = this.backUpCursorTop;
        Console.WindowHeight = this.backupWindowHeight;
        Console.BufferHeight = this.backUpBufferHeight;
        Console.BufferWidth = this.backUpBufferWidth;
    }

    public void VisualizeSmallMap()
    {
        Console.Clear();
        Console.WindowHeight = 150;

        this.mapDrawer.DrawMap(this.mapWidth, this.mapHeight, this.tileWidth, this.tileHeight);

        List<TileData> tileDatas = this.data.TileDatas;
        List<RealEstateData> realEstateDatas = (from tileData in tileDatas where tileData is RealEstateData select tileData as RealEstateData).ToList();
        List<RailRoadData> railRoadDatas = (from tileData in tileDatas where tileData is RailRoadData select tileData as RailRoadData).ToList();
        List<UtilityData> utilityDatas = (from tileData in tileDatas where tileData is UtilityData select tileData as UtilityData).ToList();

        this.tileDrawer!.DrawPlayers(this.playerPositions);
        this.tileDrawer.DrawTiles(tileDatas);

        this.displayTiles.DisplayRealEstates(this.innerMapEdge[0][0] + 3, this.innerMapEdge[0][1] + 1, realEstateDatas, 2);
        this.displayTiles.DisplayRailRoad(this.innerMapEdge[0][0] + 57, this.innerMapEdge[0][1] + 1, railRoadDatas, 2);
        this.displayTiles.DisplayUtility(this.innerMapEdge[0][0] + 57, this.innerMapEdge[0][1] + 5, utilityDatas, 2);
        this.playerStatusDrawer.DrawArrangedLines(this.innerMapEdge[0][0] + 57, this.innerMapEdge[0][1] + 9, this.data);

        this.loggingDrawer.DrawLogging(this.innerMapEdge[0][0] + 57, this.innerMapEdge[0][1] + 15);

        Console.CursorLeft = this.backUpCursorLeft;
        Console.CursorTop = this.backUpCursorTop;
        Console.WindowHeight = this.backupWindowHeight;
        Console.BufferHeight = this.backUpBufferHeight;
        Console.BufferWidth = this.backUpBufferWidth;
    }
}
