namespace Tests
{
    [TestClass]
    public class RollDiceResultTests
    {
        [DataRow(new int[] {1, 2}, 3)]
        [DataRow(new int[] {4, 4}, 8)]
        [DataRow(new int[] {5, 1}, 6)]
        [DataTestMethod]
        public void Initialize_RollDiceResult_With_Total_Property_Which_Is_The_Sum_Of_Inputs(int[] diceValues, int expectedTotal)
        {
            //Arrange & Action
            RollDiceResult rollDiceResult = new RollDiceResult(diceValues);

            //Assert
            Assert.AreEqual(expectedTotal, rollDiceResult.Total);
        }
        [DataRow(new int[] {1, 2}, false)]
        [DataRow(new int[] {4, 4}, true)]
        [DataRow(new int[] {5, 1}, false)]
        [DataTestMethod]
        public void Initialize_RollDiceResult_With_IsDouble_Checking_If_Two_Inputs_Are_Same(int[] diceValues, bool expectedIsDouble)
        {
            //Arrange & Action
            RollDiceResult rollDiceResult = new RollDiceResult(diceValues);

            //Assert
            Assert.AreEqual(expectedIsDouble, rollDiceResult.IsDouble);
        }
    }
}
