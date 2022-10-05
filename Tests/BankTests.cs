namespace Tests
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        public void Open_Account_With_Initial_Balance()
        {
            Bank bank = new Bank();
            int playerNumber = 0;

            bank.OpenAccount(playerNumber:playerNumber, initialBalance:100);

            int expectedBalance = 100;
            Assert.AreEqual(bank.Accounts[playerNumber], expectedBalance);
        }
        [TestMethod]
        public void Is_Inner_Accounts_Protected_From_Change_Of_Copied_Accouts()
        {
            Bank bank = new Bank();
            int playerNumber = 0;
            bank.OpenAccount(playerNumber:playerNumber,initialBalance:100);

            bank.Accounts[playerNumber] = 200;

            int expectedBalance = 100;
            Assert.AreEqual(bank.Accounts[playerNumber], expectedBalance);
        }
        [TestMethod]
        public void Increase_Balance_Of_Account_Of_PlayerNumber()
        {
            Bank bank = new Bank();
            int playerNumber = 0;
            int balanceBeforeIncrease = 100;
            int amountsToBeIncreased = 100;
            bank.OpenAccount(playerNumber,balanceBeforeIncrease);

            bank.IncreaseBalance(playerNumber, amountsToBeIncreased);

            int expectedBalance = balanceBeforeIncrease + amountsToBeIncreased;
            Assert.AreEqual(bank.Accounts[playerNumber], expectedBalance);
        }
        [TestMethod]
        public void Decrease_Balance_Of_Account_Of_PlayerNumber()
        {
            Bank bank = new Bank();
            int playerNumber = 0;
            int balanceBeforeDecrease = 100;
            int amountsToBeDecreased = 100;
            bank.OpenAccount(playerNumber,balanceBeforeDecrease);

            bank.IncreaseBalance(playerNumber, amountsToBeDecreased);

            int expectedBalance = balanceBeforeDecrease + amountsToBeDecreased;
            Assert.AreEqual(bank.Accounts[playerNumber], expectedBalance);
        }
        [TestMethod]
        public void Transfer_Money_From_An_Account_To_Another()
        {
            Bank bank = new Bank();
            int senderPlayerNumber = 0;
            int receiverPlayerNumber = 1;
            int initialBalanceForBothPlayes = 100;
            int amountToBeTransfered = 50;
            bank.OpenAccount(senderPlayerNumber, initialBalanceForBothPlayes);
            bank.OpenAccount(receiverPlayerNumber, initialBalanceForBothPlayes);

            bank.TransferMoneyFromTo(senderPlayerNumber, receiverPlayerNumber, amountToBeTransfered);

            int expectedBalanceOfSender = 50;
            Assert.AreEqual(bank.Accounts[senderPlayerNumber], expectedBalanceOfSender);
            int expectedBalanceOfReceiver = 150;
            Assert.AreEqual(bank.Accounts[receiverPlayerNumber], expectedBalanceOfReceiver);
        }
        [DataTestMethod]
        [DataRow(-100, true)]
        [DataRow(0, false)]
        [DataRow(100, false)]
        public void Return_True_If_Balance_Is_Negative_Otherwise_False(int initialBalance, bool expectedBool)
        {
            Bank bank = new Bank();
            int playerNumber = 0;

            bank.OpenAccount(playerNumber, initialBalance);

            bool checkedBool = bank.IsBalanceNegative(playerNumber);
            Assert.AreEqual(checkedBool, expectedBool);
        }
        [TestMethod]
        public void Throw_ArgumentException_If_Open_Accounts_With_Same_Player_Number()
        {
            Bank bank = new Bank();
            int playerNumber = 0;
            int initialBalance = 100;
            bank.OpenAccount(playerNumber, initialBalance);

            Assert.ThrowsException<ArgumentException>(() => bank.OpenAccount(playerNumber, initialBalance));
        }
        [TestMethod]
        public void Remove_Account_With_Player_Number()
        {
            Bank bank = new Bank();
            int playerNumber = 0;
            int initialBalance = 100;
            bank.OpenAccount(playerNumber, initialBalance);

            bank.RemoveAccount(playerNumber);

            bool isPlayerNumberStillInAccount = bank.Accounts.TryGetValue(playerNumber, out int value);
            bool expectedBool = false;
            Assert.AreEqual(isPlayerNumberStillInAccount, expectedBool);
        }
    }
}