
namespace Tests.TileManagersTests
{
    [TestClass]
    public class MapTilesFactoryTests
    {
        [TestMethod]
        public void MapTilesFactoryTestsAll()
        {
            
            /// initiate
            MapTilesFactory mapTilesFactory = new MapTilesFactory();
            List<Tile> tiles = mapTilesFactory.CreateRandomMapTiles(22, 4, 2, 3, 3, true, 0, 10, 20, 30);

            GroupSetter groupSetter = new GroupSetter();
            groupSetter.SetGroups(tiles);

            /// Get separate properties
            var query = from tile in tiles where tile is Property select tile as Property;
            List<Property> properties = query.ToList();
            var query2 = from tile in tiles where tile is RealEstate select tile as RealEstate;
            List<RealEstate> realEstates = query2.ToList();
            var query3 = from tile in tiles where tile is RailRoad select tile as RailRoad;
            List<RailRoad> railRoads = query3.ToList();
            var query4 = from tile in tiles where tile is Utility select tile as Utility;
            List<Utility> utilities = query4.ToList();

            /// check counts
            Assert.AreEqual(properties.Count(), 28);
            Assert.AreEqual(realEstates.Count(), 22);
            Assert.AreEqual(railRoads.Count(), 4);
            Assert.AreEqual(utilities.Count(), 2);



        }
    }
}