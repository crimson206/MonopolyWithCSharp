namespace Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Empty()
        {
            Game game = new Game(true);

            int index = 0;

            while (game.DelegatorData.NextActionName != "GameIsOver")
            {
                game.Run();
                index ++;
            }
            
            int breakPoint = 0;

        }
    }
}
