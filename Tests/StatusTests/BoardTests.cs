namespace Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void BoardTestsAll()
        {

            /// initiate
            BoardHandler board = new BoardHandler();

            /// tileManager will make a board of 40 tiles 
            /// board has the dependency on tile manager. it must be fixed later
            TileManager tileManager = new TileManager(isBoardSmall:false);

            Assert.AreEqual(board.GoPosition, 0);
            Assert.AreEqual(board.Size, 40);

            /// move two different players
            board.MovePlayerAroundBoard(0, 20);
            board.MovePlayerAroundBoard(2, 30);
            Assert.AreEqual(board.PlayerPositions[0], 20);
            Assert.AreEqual(board.PlayerPositions[2], 30);

            /// check 
            Assert.AreEqual(board.PlayerPassedGo[0], false);

            /// pass go
            board.MovePlayerAroundBoard(0, 30);
            Assert.AreEqual(board.PlayerPositions[0], 10);
            Assert.AreEqual(board.PlayerPassedGo[0], true);

            /// new board size is 32
            TileManager smallMapTileManager = new TileManager(isBoardSmall:true);
            BoardHandler smallBoard = new BoardHandler();
            smallBoard.Size = smallMapTileManager.TileDatas.Count();

            /// give the board
            Assert.AreEqual(smallBoard.GoPosition, 0);
            Assert.AreEqual(smallBoard.Size, 32);

            /// move two different players
            smallBoard.MovePlayerAroundBoard(0, 30);
            Assert.AreEqual(smallBoard.PlayerPositions[0], 30);
            Assert.AreEqual(smallBoard.PlayerPassedGo[0], false);

            smallBoard.MovePlayerAroundBoard(0, 4);
            Assert.AreEqual(smallBoard.PlayerPositions[0], 2);
            Assert.AreEqual(smallBoard.PlayerPassedGo[0], true);
        }
    }
}