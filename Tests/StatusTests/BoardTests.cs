namespace Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void Initialze_BoardHandler_With_PlayerPositions_Whose_Size_Is_Four()
        {
            BoardHandler board = new BoardHandler();
            int sizeOfPlayerPositions = board.PlayerPositions.Count();

            int expectedSize = 4;
            Assert.AreEqual(sizeOfPlayerPositions, expectedSize);
        }
        [TestMethod]
        public void Initialze_BoardHandler_With_PlayerPositions_Whose_Initial_Values_Are_0()
        {
            BoardHandler board = new BoardHandler();
            List<int> expectedPositions = new List<int> { 0, 0, 0, 0 };
            
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(board.PlayerPositions[i], expectedPositions[i]);
            }
        }
        [TestMethod]
        public void Initialze_BoardHandler_With_PlayerPassedGo_Whose_Size_Is_Four()
        {
            BoardHandler board = new BoardHandler();
            int sizeOfPlayerPassedGo = board.PlayerPassedGo.Count();

            int expectedSize = 4;
            Assert.AreEqual(sizeOfPlayerPassedGo, expectedSize);
        }
        [TestMethod]
        public void Initialze_BoardHandler_With_PlayerPassedGo_Whose_Initial_Values_Are_False()
        {
            BoardHandler board = new BoardHandler();
            List<bool> expectedPlayerPassedGo = new List<bool> { false, false, false, false };
            
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(board.PlayerPassedGo[i], expectedPlayerPassedGo[i]);
            }
        }
        [TestMethod]
        public void Move_Player_Around_Board_Whose_Size_Is_40()
        {
            BoardHandler board = new BoardHandler();
            int index = 0;
            List<int> amountsToMove = new List<int> { 10, 20, 13, 14 };
            List<int> expectedPositions = new List<int> { 10, 30, 3, 17 };
            
            for (int i = 0; i < 4; i++)
            {
                board.MovePlayerAroundBoard(index, amountsToMove[i]);
                Assert.AreEqual(board.PlayerPositions[index], expectedPositions[i]);
            }
        }
        [TestMethod]
        public void Can_Not_Player_Move_Backward_Or_By_Amount_Larger_Than_Size_Of_Board()
        {
            BoardHandler board = new BoardHandler();
            int index = 0;
            int negativeInteger = -1;
            int amountLargerThanBoardSize = 43;

            Assert.ThrowsException<Exception>(() => board.MovePlayerAroundBoard(index, negativeInteger));
            Assert.ThrowsException<Exception>(() => board.MovePlayerAroundBoard(index, amountLargerThanBoardSize));
        }
        [TestMethod]
        public void Is_PassedGo_True_If_Pass_GoPosition_By_MoveAroundBoard()
        {
            BoardHandler board = new BoardHandler();
            int index = 0;
            int step = 30;

            board.MovePlayerAroundBoard(index, step);
            board.MovePlayerAroundBoard(index, step);

            bool expectedValueOfPassedGo = true;
            Assert.AreEqual(board.PlayerPassedGo[index], expectedValueOfPassedGo);
        }
        [TestMethod]
        public void Become_PassedGo_False_Again_If_Move_One_Move_Without_Passing_GoPosition()
        {
            BoardHandler board = new BoardHandler();
            int index = 0;
            int step = 22;

            board.MovePlayerAroundBoard(index, step);
            board.MovePlayerAroundBoard(index, step);
            board.MovePlayerAroundBoard(index, step);

            bool expectedValueOfPassedGo = false;
            Assert.AreEqual(board.PlayerPassedGo[index], expectedValueOfPassedGo);
        }
        [TestMethod]
        public void Move_Player_Around_Board_Especially_Nearby_GoPosition_And_PlayerPassedGo_Is_True_Only_When_Player_Arrived_At_GoPosition()
        {
            BoardHandler board = new BoardHandler();
            int AroundGoPosition = board.GoPosition - 2;
            if (AroundGoPosition < 0)
            {
                AroundGoPosition += board.Size;
            }
            int index = 0;
            int tightStepToMakeNearbyGoPosition = 1;
            board.Teleport(0, AroundGoPosition);
            bool[] expectedPassedGos = { false, true, false, false };

            foreach (var expectedPassedGo in expectedPassedGos)
            {
                board.MovePlayerAroundBoard(index, tightStepToMakeNearbyGoPosition);
                bool currentPassedGo = board.PlayerPassedGo[index];
                Assert.AreEqual(currentPassedGo, expectedPassedGo);
            }
        }
        public void Teleport_Player_To_Designated_Position()
        {
            BoardHandler board = new BoardHandler();
            int index = 0;
            int [] designatedPositions = new int[3] { 10, 4, 30};
            int [] expectedPositions = new int[3] { 10, 4, 30};

            for (int i = 0; i < 3; i++)
            {
                board.Teleport(index, designatedPositions[i]);
                Assert.AreEqual(board.PlayerPositions[index], expectedPositions[i]);
            }
        }
        [TestMethod]
        public void Can_Not_Player_Teleport_To_Outside_Of_Board()
        {
            BoardHandler board = new BoardHandler();
            int index = 0;
            int outSide1 = - 4;
            int outSide2 = 44;

            Assert.ThrowsException<Exception>(() => board.Teleport(index, outSide1));
            Assert.ThrowsException<Exception>(() => board.Teleport(index, outSide2));
        }
        [TestMethod]
        public void Are_Data_Protected_When_Their_Copies_Are_Modified()
        {
            BoardHandler board = new BoardHandler();
            int index = 0;
            int previousPosition = board.PlayerPositions[index];
            bool previousPassedGo = board.PlayerPassedGo[index];

            board.PlayerPositions[index] += 10;
            board.PlayerPassedGo[index] = previousPassedGo is false;

            Assert.AreEqual(board.PlayerPositions[index], previousPosition);
            Assert.AreEqual(board.PlayerPassedGo[index], previousPassedGo);
        }
    }
}