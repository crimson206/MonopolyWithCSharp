namespace Tests
{
    [TestClass]
    public class RailRoadTests
    {
        [TestMethod]
        public void RailRoadTestsALL()
        {
            /// initiate
            IRailRoad railRoad1 = new RailRoad("RailRoad1", 100, new List<int> {20, 40, 80, 160}, 50);
            IRailRoad railRoad2 = new RailRoad("RailRoad2", 100, new List<int> {20, 40, 80, 160}, 50);
            IRailRoad railRoad3 = new RailRoad("RailRoad3", 100, new List<int> {20, 40, 80, 160}, 50);
            IRailRoad railRoad4 = new RailRoad("RailRoad4", 100, new List<int> {20, 40, 80, 160}, 50);

            /// Make a group
            List<IProperty> railRoads = new List<IProperty> {railRoad1, railRoad2, railRoad3, railRoad4};
            railRoad1.SetGroup(railRoads);
            railRoad2.SetGroup(railRoads);
            railRoad3.SetGroup(railRoads);
            railRoad4.SetGroup(railRoads);

            /// check current rent
            Assert.AreEqual(railRoad1.CurrentRent, 20);

            /// check it is not tradable
            Assert.AreEqual(railRoad1.IsTradable, false);

            /// buy a railroad, rent is still the same
            railRoad1.SetOwnerPlayerNumber(1);
            Assert.AreEqual(railRoad1.CurrentRent, 20);
            Assert.AreEqual(railRoad2.CurrentRent, 20);
            Assert.AreEqual(railRoad1.IsTradable, true);

            /// same owner increases the rent
            railRoad2.SetOwnerPlayerNumber(1);
            railRoad3.SetOwnerPlayerNumber(2);
            Assert.AreEqual(railRoad1.CurrentRent, 40);
            Assert.AreEqual(railRoad2.CurrentRent, 40);
            Assert.AreEqual(railRoad3.CurrentRent, 20);

            /// check default and set ismortgaged
            Assert.AreEqual(railRoad1.IsMortgaged, false);
            railRoad1.SetIsMortgaged(true);
            Assert.AreEqual(railRoad1.IsMortgaged, true);
            Assert.AreEqual(railRoad1.IsTradable, false);
            
        }
    }
}