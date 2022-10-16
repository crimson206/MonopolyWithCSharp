
public class GameMapDrawing
{
    
    public void DrawMapWithRealEstatesRailRoad()
    {
            Console.WindowHeight = 200;
            MapDrawer mapDrawer = new MapDrawer();
            mapDrawer.DrawMap(10, 10, 13, 4);
            MapTilesFactory mapTilesGenerator = new MapTilesFactory();
            DisplayTileInfo displayTiles = new DisplayTileInfo();


            TileManager tileManager = new TileManager();
            var tileDatas = tileManager.tileDataSet;

            var realEstates = (from tileData in tileDatas where tileData is RealEstateData select tileData as RealEstateData).ToList();
            var railRoadDatas = (from tileData in tileDatas where tileData is RailRoadData select tileData as RailRoadData).ToList();

            var innerMapPoints = mapDrawer.CreateInnerSpaceIndicator(10, 10, 14, 4);
            displayTiles.DisplayRealEstates( innerMapPoints[0][0] + 5, innerMapPoints[0][1] + 1, realEstates, 2);
            displayTiles.DisplayRailRoad( innerMapPoints[0][0] + 65, innerMapPoints[0][1] + 1, railRoadDatas, 2);

            var tileInfo = mapDrawer.CreateTileEdgeCollection(10, 10, 13, 4);
            var playerDrawer = new TileDrawer(innerMapPoints);


            Console.ReadLine();
    }

    public void AddPlayerAnimation()
    {
        List<int> playerPositions = new List<int> { 0 , 0, 0, 0};
    }

}
