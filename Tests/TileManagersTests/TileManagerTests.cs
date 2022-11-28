
namespace Tests.TileManagersTests
{
    [TestClass]
    public class TileManagerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            TileManager tileManager = new TileManager(isBoardSmall:false);
            List<IRealEstate> realEstates = tileManager.RealEstates;
            
            foreach (var item in tileManager.Properties)
            {
                tileManager.PropertyManager.ChangeOwner(item, 0);
            }

            /// tileManager has 40 tiles
            Assert.AreEqual(tileManager.Tiles.Count(), 40);

            /// can't change tiles list out of tile manager
            List<ITile> invader = tileManager.Tiles;
            invader[0] = new Jail("Invader", 10);
            Assert.AreNotEqual(tileManager.Tiles[0], invader[0]);

            /// tileManager has 22 realEstates
            int numRealEstates = tileManager.Tiles.Where(tile => tile is RealEstate).ToList().Count();
            Assert.AreEqual(numRealEstates, 22);

            /// get sample realEstate
            IRealEstate realEstate = (IRealEstate) tileManager.Properties.Where(property => property is IRealEstate).ToList()[0];

            /// get property manager
            IPropertyManager propertyManager = tileManager.PropertyManager;
            propertyManager.ChangeOwner(realEstate, 1);

            Assert.AreEqual(realEstate.OwnerPlayerNumber, 1);

            int playerNum = 0;
            foreach (var property in tileManager.Properties)
            {
                propertyManager.ChangeOwner(property, playerNum);
                playerNum = (playerNum + 1) % 4;
            }


            /// reset owners
            foreach (var property in tileManager.Properties)
            {
                propertyManager.ChangeOwner(property, null);
            }

            propertyManager.ChangeOwner(realEstates[0], 0);

            foreach (var property in tileManager.Properties)
            {
                propertyManager.ChangeOwner(property, 0);
            }
        }
    }
}
