namespace Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Initialize_Player_With_Correct_Name()
        {
            //Arrange & Act//
            Board board = new Board(new List<CommonTileInfo>());
            Player player = new Player(name:"Peter", board:board);

            //Assert//
            string expectedName = "Peter";
            Assert.AreEqual(player.Name, expectedName);
        }

        [TestMethod]
        public void InitialLize_Player_With_Zero_Position()
        {
            //Arrange && Act//
            Player player = new Player("Peter", new Board(new List<CommonTileInfo>()));

            //Assert//
            int expectedInitialPosition = 0;
            Assert.AreEqual(player.Position, expectedInitialPosition);
        }

        [TestMethod]
        public void Set_Last_Position_To_Be_Old_Position_When_Setting_Position()
        {
            //Arrange//
            Player player = new Player("Peter", new Board(new List<CommonTileInfo>()));

            //Act//
            player.Position = 10;
            player.Position = 30;

            //Assert//
            int expectedLastPosition = 10;
            int lastPosition = player.LastPosition;
            Assert.AreEqual(lastPosition, expectedLastPosition);
        }

        [TestMethod]
        public void Get_PassedGo_True_If_Position_Is_Smaller_Than_Last_Position()
        {
            //Arrange//
            Player player = new Player("Peter", new Board(new List<CommonTileInfo>()));

            //Act//
            player.Position = 10;
            player.LastPosition = 30;

            //Assert//
            bool expectedPassedGo = true;
            Assert.AreEqual(player.PassedGo, expectedPassedGo);
        }

        [TestMethod]
        public void Get_PassedGo_False_If_Position_Is_Larger_Than_Last_Position()
        {
            //Arrange//
            Player player = new Player("Peter", new Board(new List<CommonTileInfo>()));

            //Act//
            player.Position = 30;
            player.LastPosition = 10;

            //Assert//
            bool expectedPassedGo = false;
            Assert.AreEqual(player.PassedGo, expectedPassedGo);
        }
    }
}
