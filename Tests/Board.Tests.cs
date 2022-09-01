namespace Tests
{
    [TestClass]
    public class InitializeBoardTests
    {
        [TestMethod]
        public void IinitializeBoardWithPositionsOfPlayerTokens()
        {
            var board = new Board(4);

            int[,] tokenPositions = board.PlayerTokenPositions;
            int[,] expected = { {0, 0}, {0, 0}, {0, 0}, {0, 0} };
            CollectionAssert.AreEquivalent(tokenPositions, expected);
        }
    }

    [TestClass]
    public class MovePlayerMethodTests
    {
        [TestMethod]
        public void MovePlayerMovesToken()
        {
            var board = new Board(4);

            board.MoveToken(0, 5);
            int tokenPosition = board.PlayerTokenPositions[0,1];
            int expected = 5;
            Assert.AreEqual(tokenPosition, expected);
        }

        [TestMethod]
        public void MovePlayerChangesLineIfPlayerMovesMoreThan10Steps()
        {
            var board = new Board(4);
            int[] steps = new int[4] {9, 10, 12, 10};
            var expectedLine = new int[] {0, 1, 3, 0};
            var expectedPositionAtLine = new int[] {9, 9, 1, 1};


            for (int i = 0; i < 4; i++)
            {   
                board.MoveToken(0, steps[i]);
                Assert.AreEqual(board.PlayerTokenPositions[0, 0], expectedLine[i]);
                Assert.AreEqual(board.PlayerTokenPositions[0, 1], expectedPositionAtLine[i]);
            }
        }

    }
}