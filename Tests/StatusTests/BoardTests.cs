namespace Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void BoardTestsAll()
        {
            /// initiate
            BoardHandler board = new BoardHandler(40, 0);

            /// move
            board.MovePlayerAroundBoard(0, 20);
            board.MovePlayerAroundBoard(2, 30);
            Assert.AreEqual(board.PlayerPositions[0], 20);
            Assert.AreEqual(board.PlayerPositions[2], 30);
            Assert.AreEqual(board.PlayerPassedGo[0], false);

            /// pass go
            board.MovePlayerAroundBoard(0, 30);
            Assert.AreEqual(board.PlayerPositions[0], 10);
            Assert.AreEqual(board.PlayerPassedGo[0], true);

        }
    }
}