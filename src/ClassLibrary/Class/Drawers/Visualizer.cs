
public class Visualizer
{
    private int backUpCursorLeft = Console.CursorLeft;
    private int backUpCursorTop = Console.CursorTop;
    private int backupWindowHeight = Console.WindowHeight;
    private int backUpBufferHeight = Console.BufferHeight;
    private int backUpBufferWidth = Console.BufferWidth;

    private LoggingDrawer loggingDrawer;
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
    private bool isBoardSmall;

    public Visualizer(DataCenter data, bool isBoardSmall)
    {
        this.data = data;
        this.isBoardSmall = isBoardSmall;
        if (isBoardSmall)
        {
            this.loggingDrawer = new LoggingDrawer(7);
            this.Setup(10, 8, 13, 3);
        }
        else
        {
            this.loggingDrawer = new LoggingDrawer(9);
            this.Setup(11, 11, 13, 4);
        }
    }

    private List<bool> AreInGame => this.data.InGame.AreInGame;
    private List<int> PlayerPositions => this.data.Board.PlayerPositions;
    private string RecommendedString => this.data.EventFlow.RecommendedString;

    public void Visualize()
    {
        if (isBoardSmall)
        {
            this.VisualizeSmallMap();
        }
        else
        {
            this.VisualizeLargeMap();
        }
    }

    public void Setup(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.tileWidth = tileWidth;
        this.tileHeight = tileHeight;
        this.tileEdgeInfo = this.mapDrawer.CreateTileEdgeCollection(mapWidth, mapHeight, tileWidth, tileHeight);
        this.innerMapEdge = this.mapDrawer.CreateInnerSpaceIndicator(mapWidth, mapHeight, tileWidth, tileHeight);
        this.tileDrawer = new TileDrawer(tileEdgeInfo);
    }

    public void UpdatePromptMessage(string promptMessage)
    {
        this.loggingDrawer.UpdatePromptMessage(promptMessage);
    }

    public void UpdateLogging()
    {
        this.loggingDrawer.UpdateLogging(this.RecommendedString);
    }

    public void VisualizeLargeMap()
    {
        Console.Clear();
        Console.WindowHeight = 150;

        this.mapDrawer.DrawMap(this.mapWidth, this.mapHeight, this.tileWidth, this.tileHeight);

        List<ITileData> tileDatas = this.data.TileDatas;
        List<IRealEstateData> realEstateDatas = (from tileData in tileDatas where tileData is IRealEstateData select tileData as IRealEstateData).ToList();
        List<IRailRoadData> railRoadDatas = (from tileData in tileDatas where tileData is IRailRoadData select tileData as IRailRoadData).ToList();
        List<IUtilityData> utilityDatas = (from tileData in tileDatas where tileData is IUtilityData select tileData as IUtilityData).ToList();        

        this.tileDrawer!.DrawPlayers(this.PlayerPositions, this.AreInGame);
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

        List<ITileData> tileDatas = this.data.TileDatas;
        List<IRealEstateData> realEstateDatas = (from tileData in tileDatas where tileData is RealEstate select tileData as IRealEstateData).ToList();
        List<IRailRoadData> railRoadDatas = (from tileData in tileDatas where tileData is RailRoad select tileData as IRailRoadData).ToList();
        List<IUtilityData> utilityDatas = (from tileData in tileDatas where tileData is Utility select tileData as IUtilityData).ToList();

        this.tileDrawer!.DrawPlayers(this.PlayerPositions, this.AreInGame);
        this.tileDrawer.DrawTiles(tileDatas);

        this.displayTiles.DisplayRealEstates(this.innerMapEdge[0][0] + 3, this.innerMapEdge[0][1], realEstateDatas, 2);
        this.displayTiles.DisplayRailRoad(this.innerMapEdge[0][0] + 57, this.innerMapEdge[0][1], railRoadDatas, 2);
        this.displayTiles.DisplayUtility(this.innerMapEdge[0][0] + 57, this.innerMapEdge[0][1] + 4, utilityDatas, 2);
        this.playerStatusDrawer.DrawArrangedLines(this.innerMapEdge[0][0] + 57, this.innerMapEdge[0][1] + 8, this.data);

        this.loggingDrawer.DrawLogging(this.innerMapEdge[0][0] + 57, this.innerMapEdge[0][1] + 14);

        Console.CursorLeft = this.backUpCursorLeft;
        Console.CursorTop = this.backUpCursorTop;
        Console.WindowHeight = this.backupWindowHeight;
        Console.BufferHeight = this.backUpBufferHeight;
        Console.BufferWidth = this.backUpBufferWidth;
    }

    private List<IRealEstateData> FilterIRealEstateData(List<ITileData> tileDatas)
    {
        List<IRealEstateData> realEstateDatas = new List<IRealEstateData>();
        foreach (var tileData in tileDatas)
        {
            if (tileData is RealEstate)
            {
                IRealEstateData realEstateData = (IRealEstateData)tileData;
                realEstateDatas.Add(realEstateData);
            }
        }
        return realEstateDatas;
    }
}
