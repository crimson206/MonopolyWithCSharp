namespace Tests
{
    [TestClass]
    public class MovePlayerMethodTests
    {
        [TestMethod]
        public void MovePlayer_Move_Player_Around_Board()
        {
            var board = new Board(Size:40);
            int initialPosition = 5;
            int[] steps = new int[4] {9, 10, 12, 10};
            int position = initialPosition;
            var expectedPositions = new int[] {14, 24, 36, 6};

            for (int i = 0; i < 4; i++)
            {   
                position = board.MovePlayerToken(position, steps[i]);
                Assert.AreEqual(position, expectedPositions[i]);
            }
        }
    }
}