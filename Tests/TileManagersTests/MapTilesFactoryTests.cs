
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
            List<ITile> tiles = mapTilesFactory.CreateRandomMapTiles(22, 4, 2, 3, 3, true, 0, 10, 20, 30);

            GroupSetter groupSetter = new GroupSetter();
            groupSetter.SetGroups(tiles);

            /// Get separate properties
            var query = from tile in tiles where tile is IProperty select tile as IProperty;
            List<IProperty> properties = query.ToList();
            var query2 = from tile in tiles where tile is IRealEstate select tile as IRealEstate;
            List<IRealEstate> realEstates = query2.ToList();
            var query3 = from tile in tiles where tile is IRailRoad select tile as IRailRoad;
            List<IRailRoad> railRoads = query3.ToList();
            var query4 = from tile in tiles where tile is IUtility select tile as IUtility;
            List<IUtility> utilities = query4.ToList();

            /// check counts
            Assert.AreEqual(properties.Count(), 28);
            Assert.AreEqual(realEstates.Count(), 22);
            Assert.AreEqual(railRoads.Count(), 4);
            Assert.AreEqual(utilities.Count(), 2);



        }
    }
}