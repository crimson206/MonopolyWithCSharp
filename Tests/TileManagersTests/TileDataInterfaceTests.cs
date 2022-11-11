
namespace Tests.TileManagersTests
{
    [TestClass]
    public class TileDataInterfaceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TileManager tileManager = new TileManager(isBoardSmall:false);
            List<RealEstate> realEstates = tileManager.RealEstates;
            
            List<ITileData> tileDatas = tileManager.TileDatas;
            List<IRealEstateData> realEstateDatas = new List<IRealEstateData>();
            foreach (var tileData in tileDatas)
            {
                
                if (tileData is RealEstate)
                {
                    IRealEstateData realEstateData = (IRealEstateData)tileData;
                    realEstateDatas.Add(realEstateData);
                }
            }

        }
    }
}
