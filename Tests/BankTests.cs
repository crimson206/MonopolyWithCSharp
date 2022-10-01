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
        public void Return_True_If_Decreased_Balance_By_DecreaseBalance_Is_Negative()
        {
            Bank bank = new Bank();
            int playerNumber = 0;
            int balanceBeforeDecrease = 100;
            int amountToBeDecreased = 200;
            bank.OpenAccount(playerNumber,balanceBeforeDecrease);

            bool returnedBool = bank.DecreaseBalance(playerNumber, amountToBeDecreased);

            bool expectedBool = true;
            Assert.AreEqual(returnedBool, expectedBool);
        }
        [TestMethod]
        public void Return_False_If_Decreased_Balance_By_DecreaseBalance_Is_Not_Negative()
        {
            /// arragne ///
            Bank bank = new Bank();
            int playerNumber = 0;
            int initialBalance = 100;
            bank.OpenAccount(playerNumber,initialBalance);
            /// balances after each decrease => { 50, 0 } ///
            int[] amountsToBeDecreased = new int[] {50, 50};

            
            List<bool> expectedBools = new List<bool> {false, false};
            int index = 0;
            foreach (var amount in amountsToBeDecreased)
            {
                /// act ///
                bool returnedBool = bank.DecreaseBalance(playerNumber, amount);

                /// assert ///
                Assert.AreEqual(returnedBool, expectedBools[index]);

                index++;
            }
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
        [TestMethod]
        public void Return_True_If_Decreased_Balance_By_TransferMoneyFromTo_Is_Negative()
        {
            Bank bank = new Bank();
            int senderPlayerNumber = 0;
            int receiverPlayerNumber = 1;
            int initialBalanceForBothPlayes = 100;
            int amountToBeTransfered = 200;
            bank.OpenAccount(senderPlayerNumber, initialBalanceForBothPlayes);
            bank.OpenAccount(receiverPlayerNumber, initialBalanceForBothPlayes);

            bool returnedBool = bank.TransferMoneyFromTo(senderPlayerNumber, receiverPlayerNumber, amountToBeTransfered);

            bool expectedBool = true;
            Assert.AreEqual(returnedBool, expectedBool);
        }
        [TestMethod]
        public void Return_False_If_Decreased_Balance_By_TransferMoneyFromTo_Is_Not_Negative()
        {
            Bank bank = new Bank();
            int senderPlayerNumber = 0;
            int receiverPlayerNumber = 1;
            int initialBalanceForBothPlayes = 100;
            bank.OpenAccount(senderPlayerNumber, initialBalanceForBothPlayes);
            bank.OpenAccount(receiverPlayerNumber, initialBalanceForBothPlayes);

            /// balances after each decrease => { 50, 0 } ///
            int[] amountsToTransfered = new int[] {50, 50};

            
            List<bool> expectedBools = new List<bool> {false, false};
            int index = 0;
            foreach (var amount in amountsToTransfered)
            {
                /// act ///
                bool returnedBool = bank.DecreaseBalance(senderPlayerNumber, amount);

                /// assert ///
                Assert.AreEqual(returnedBool, expectedBools[index]);

                index++;
            }
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
    }
}