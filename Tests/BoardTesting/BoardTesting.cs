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

            //Assert//
            int expectedSize = 0;
            Assert.AreEqual(expectedSize, emptyDictionary.Count());
        }
        [TestMethod]
        public void RegisterPlayer_Adds_Player_Position_Element_With_Position_Zero()
        {
            //Arrange//
            Board board = new Board();
            Player player = new Player("Player");

            //Act//
            board.RegisterPlayer(player);

            //Assert//
            int expectedPlayerPosition = 0;
            Assert.AreEqual(expectedPlayerPosition, board.PlayerPositions["Player"]);
        }
    }
}