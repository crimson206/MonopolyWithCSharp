
namespace Tests.TileManagersTests
{
    [TestClass]
    public class TileManagerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            TileManager tileManager = new TileManager(isBoardSmall:false);
            List<RealEstate> realEstates = tileManager.RealEstates;
            
            foreach (var item in tileManager.Properties)
            {
                tileManager.PropertyManager.ChangeOwner(item, 0);
            }
            tileManager.Analyser.Test();
            List<int> a = (from aaaa in tileManager.Properties where aaaa.OwnerPlayerNumber == 0 select aaaa.Price).ToList();

            /// tileManager has 40 tiles
            Assert.AreEqual(tileManager.Tiles.Count(), 40);

            /// can't change tiles list out of tile manager
            List<Tile> invader = tileManager.Tiles;
            invader[0] = new Jail("Invader", 10);
            Assert.AreNotEqual(tileManager.Tiles[0], invader[0]);

            /// tileManager has 22 realEstates
            int numRealEstates = tileManager.Tiles.Where(tile => tile is RealEstate).ToList().Count();
            Assert.AreEqual(numRealEstates, 22);

            /// get sample realEstate
            RealEstate realEstate = (RealEstate) tileManager.Properties.Where(property => property is RealEstate).ToList()[0];

            /// get property manager
            PropertyManager propertyManager = tileManager.PropertyManager;
            propertyManager.ChangeOwner(realEstate, 1);

            Assert.AreEqual(realEstate.OwnerPlayerNumber, 1);

            int playerNum = 0;
            foreach (var property in tileManager.Properties)
            {
                propertyManager.ChangeOwner(property, playerNum);
                playerNum = (playerNum + 1) % 4;
            }

            /// get analyser
            Analyser analyser = tileManager.Analyser;
            List<int> totalPrices = analyser.TotalPricesOfProerties;
            List<int> totalRents = analyser.TotalRentsOfProerties;
            List<double> costEfficienceis = analyser.ConvertRealEstateToBuildingHouseCoseEfficiency(tileManager.RealEstates);



            bool isAbleToMonopoly = analyser.IsAbleToMonopoly(0, realEstate);
            Assert.AreEqual(isAbleToMonopoly, false);

            /// reset owners
            foreach (var property in tileManager.Properties)
            {
                propertyManager.ChangeOwner(property, null);
            }

            bool isAbleToMonopoly2 = analyser.IsAbleToMonopoly(0, realEstate);
            Assert.AreEqual(isAbleToMonopoly2, true);

            propertyManager.ChangeOwner(realEstates[0], 0);
            bool isAbleToMonopoly3 = analyser.IsAbleToMonopoly(0, realEstate);
            Assert.AreEqual(isAbleToMonopoly3, true);

            foreach (var property in tileManager.Properties)
            {
                propertyManager.ChangeOwner(property, 0);
            }

            List<double> costEfficienceis2 = analyser.ConvertRealEstateToBuildingHouseCoseEfficiency(tileManager.RealEstates);

        }
    }
}
