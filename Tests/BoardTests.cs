namespace Tests
{
    [TestClass]
    public class BoardTests
    {
        [DataRow(10, 12, 22)]
        [DataRow(20, 9, 29)]
        [DataRow(30, 11, 1)]
        [DataTestMethod]
        public void Calculate_New_Position_Of_Movement_Considerting_Board_Size(int oldPosition, int amount, int expectedNewPosition)
        {
            //Arrange//
            List<CommonTileInfo> commonTileInfos = TileFactory.GenerateCommonTileInfos(40);
            Board board = new Board(commonTileInfos);

            //Act//
            int newPosition = board.CalculateMoveInBoard(oldPosition,amount);

            //Assert//
            Assert.AreEqual(newPosition, expectedNewPosition);
        }
    }
}
