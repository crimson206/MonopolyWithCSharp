
namespace Tests
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        public void Initialize_Basic_Constructor_BankHandler_With_Balances_With_Initial_Value_1500()
        {
            BankHandler bank = new BankHandler();

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(bank.Balances[i], 1500);
            }
        }
        [TestMethod]
        public void Initialize_Basic_Constructor_BankHandler_With_Balances_Whose_Size_Is_Four()
        {
            BankHandler bank = new BankHandler();

            int sizeOfBalances = bank.Balances.Count();
            int expectedSize = 4;
            Assert.AreEqual(sizeOfBalances, expectedSize);
        }
        [TestMethod]
        public void Is_Balances_Not_Changed_By_Modificating_Its_Copy()
        {
            BankHandler bank = new BankHandler();
            List<int> copiedBlances = bank.Balances;
            int previousBalance = bank.Balances[0];

            copiedBlances[0] += 100;

            int balanceAfterAction = bank.Balances[0];
            Assert.AreEqual(previousBalance, balanceAfterAction);
        }
        [TestMethod]
        public void Increase_Balance_At_Index_By_Amount()
        {
            BankHandler bank = new BankHandler();
            int index = 2;
            int previousBalance = bank.Balances[index];
            int amount = 100;

            bank.IncreaseBalance(index, amount);

            int increasedAmount = bank.Balances[index] - previousBalance;
            int expectedIncreasedAmount = 100;
            Assert.AreEqual(increasedAmount, expectedIncreasedAmount);
        }
        [TestMethod]
        public void Decrease_Balance_At_Index_By_Amount()
        {
            BankHandler bank = new BankHandler();
            int index = 2;
            int previousBalance = bank.Balances[index];
            int amount = 100;

            bank.DecreaseBalance(index, amount);

            int decreasedAmount =  previousBalance - bank.Balances[index];
            int expectedDecreasedAmount = 100;
            Assert.AreEqual(decreasedAmount, expectedDecreasedAmount);
        }
        [TestMethod]
        public void Transfer_Money_From_Index_To_Another_By_Amount()
        {
            BankHandler bank = new BankHandler();
            int fromIndex = 0;
            int toIndex = 1;
            int previousBalanceOfFromIndex = bank.Balances[fromIndex];
            int previousBalanceOfToIndex = bank.Balances[toIndex];
            int amount = 200;

            bank.TransferBalanceFromTo(fromIndex, toIndex, amount);

            int decreasedAmountOfFromIndexBalance = previousBalanceOfFromIndex - bank.Balances[fromIndex];
            int increasedAmountOfToIndexBalance = bank.Balances[toIndex] - previousBalanceOfToIndex;
            int expectedDecreasedAmount = 200;
            int expectedIncreasedAmount = 200;
            Assert.AreEqual(decreasedAmountOfFromIndexBalance, expectedDecreasedAmount);
            Assert.AreEqual(increasedAmountOfToIndexBalance, expectedIncreasedAmount);
        }
        [TestMethod]
        public void Can_Not_Decrease_Balance_Wiht_Negative_Input()
        {
            BankHandler bank = new BankHandler();
            int index = 0;

            int negativeInteger = -100;

            Assert.ThrowsException<Exception>(() => bank.DecreaseBalance(index, negativeInteger));
        }
        [TestMethod]
        public void Can_Not_Increase_Balance_Wiht_Negative_Input()
        {
            BankHandler bank = new BankHandler();
            int index = 0;
            int negativeInteger = -100;

            Assert.ThrowsException<Exception>(() => bank.DecreaseBalance(index, negativeInteger));
        }
        [TestMethod]
        public void Can_Not_Transfer_Balance_Wiht_Negative_Input()
        {
            BankHandler bank = new BankHandler();
            int index1 = 0;
            int index2 = 1;
            int negativeInteger = -100;


            Assert.ThrowsException<Exception>(() => bank.TransferBalanceFromTo(index1, index2, negativeInteger));            
        }
        [TestMethod]
        public void Can_Not_Transfer_Money_To_Self()
        {
            BankHandler bank = new BankHandler();
            int index1 = 1;
            int someAmount = 100;

            Assert.ThrowsException<Exception>(() => bank.TransferBalanceFromTo(index1, index1, someAmount));            
        }
    }
}