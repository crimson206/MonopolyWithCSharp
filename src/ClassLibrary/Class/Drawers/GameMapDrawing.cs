
public class GameMapDrawing
{
    
    public void DrawMapWithRealEstatesRailRoad()
    {
            Console.WindowHeight = 200;
            MapDrawer mapDrawer = new MapDrawer();
            mapDrawer.DrawMap(10, 10, 13, 4);
            MapTilesFactory mapTilesGenerator = new MapTilesFactory();
            DisplayTiles displayTiles = new DisplayTiles();
            var a = mapTilesGenerator.CreateRandomMapTiles(22,4,2,3,3, 1);
            var query = from tile in a where tile is RealEstate select tile as RealEstate;
            List<RealEstate> realEstates = query.ToList();
            var query2 = from tile in a where tile is RailRoad select tile as RailRoad;
            List<RailRoad> railRoads = query2.ToList();

            var innerMapPoints = mapDrawer.CreateInnerSpaceIndicator(10, 10, 14, 4);
            displayTiles.DisplayRealEstates2( innerMapPoints[0][0] + 5, innerMapPoints[0][1] + 1, realEstates, 2);
            displayTiles.DisplayRailRoad( innerMapPoints[0][0] + 65, innerMapPoints[0][1] + 1, railRoads, 2);

            var tileInfo = mapDrawer.CreateTileEdgeCollection(10, 10, 13, 4);
            var playerDrawer = new PlayerDrawer(innerMapPoints);


            Console.ReadLine();
    }

    public void AddPlayerAnimation()
    {
        List<int> playerPositions = new List<int> { 0 , 0, 0, 0};



    }

}
