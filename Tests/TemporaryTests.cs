using Newtonsoft.Json.Linq;


namespace Tests.TestResults
{
    [TestClass]
    public class TemporaryTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            StreamReader read = new StreamReader(
                @"C:\Users\sgkm\VSCodeFolders\MonopolyWithCSharp\tiles.json");
            
            ///string jstr = read.ReadToEnd();
            ///string jstr3 = read.ReadToEnd();

            ///JObject json = JObject.Parse(jstr);

            TileHandler tileHandler = new TileHandler();



            List<Tile> tiles = tileHandler.CreateTiles(read);


            ///JToken jRealEstate = json.Values().ToList()[0];
            ///int test = jRealEstate["position"]!.Value<int>();
            ///var aa = json.Values()[0]["type"].ToString();

        }
    }
}