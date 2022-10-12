namespace Tests
{
    [TestClass]
    public class RailRoadTests
    {
        [TestMethod]
        public void RailRoadTestsALL()
        {
            /// initiate
            RailRoad railRoad1 = new RailRoad("RailRoad1", 100, new List<int> {20, 40, 80, 160}, 50, 0);
            RailRoad railRoad2 = new RailRoad("RailRoad2", 100, new List<int> {20, 40, 80, 160}, 50, 0);
            RailRoad railRoad3 = new RailRoad("RailRoad3", 100, new List<int> {20, 40, 80, 160}, 50, 0);
            RailRoad railRoad4 = new RailRoad("RailRoad4", 100, new List<int> {20, 40, 80, 160}, 50, 0);

            /// Make a group
            List<Property> railRoads = new List<Property> {railRoad1, railRoad2, railRoad3, railRoad4};
            railRoad1.SetGroup(0, railRoads);
            railRoad2.SetGroup(0, railRoads);
            railRoad3.SetGroup(0, railRoads);
            railRoad4.SetGroup(0, railRoads);

            /// check current rent
            Assert.AreEqual(railRoad1.CurrentRent, 20);

            /// buy a railroad, rent is still the same
            railRoad1.SetOnwerPlayerNumber(0, 1);
            Assert.AreEqual(railRoad1.CurrentRent, 20);
            Assert.AreEqual(railRoad2.CurrentRent, 20);

            /// same owner increases the rent
            railRoad2.SetOnwerPlayerNumber(0, 1);
            railRoad3.SetOnwerPlayerNumber(0, 2);
            Assert.AreEqual(railRoad1.CurrentRent, 40);
            Assert.AreEqual(railRoad2.CurrentRent, 40);
            Assert.AreEqual(railRoad3.CurrentRent, 20);

            /// check default and set ismortgaged
            Assert.AreEqual(railRoad1.IsMortgaged, false);
            railRoad1.SetIsMortgaged(0, true);
            Assert.AreEqual(railRoad1.IsMortgaged, true);
            
        }
    }
}