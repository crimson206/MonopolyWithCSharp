
namespace BoardTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void Initialize_Board_With_Empty_Dictionary()
        {
            //Arrange & act//
            Board board = new Board();
            Dictionary<string, int> emptyDictionary = board.PlayerPositions;

            //assert//
            int expectedSize = 0;
            Assert.AreEqual(expectedSize, emptyDictionary.Count());
        }
    }
}