namespace Tests
{
    [TestClass]
    public class TryToEscapeJailTests
    {
        [TestMethod]
        public void TestMethod1()
        {

            Game game = new Game();

            game.delegator.NextEvent = game.events.tryToEscapeJail.Start;


        }
    }
}