
namespace Tests
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        public void BankTestsAll()
        {
            BankHandler bank = new BankHandler();

            /// Check initial value
            Assert.AreEqual(bank.Balances[0], 1500);

            /// increase Money
            bank.IncreaseBalance(0, 100);
            Assert.AreEqual(bank.Balances[0], 1600);

            /// decrease Money
            bank.DecreaseBalance(0, 100);
            Assert.AreEqual(bank.Balances[0], 1500);

            /// transfer Money
            bank.TransferBalanceFromTo(0, 1, 200);
            Assert.AreEqual(bank.Balances[0], 1300);
            Assert.AreEqual(bank.Balances[1], 1700);
        }
    }
}