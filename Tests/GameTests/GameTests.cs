namespace Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void RunSmallMapGame500TurnToCatchAnyProblem()
        {
            Game game = new Game(isBoardSmall:true);

            for (int i = 0; i < 500; i++)
            {
                game.Run();

                if (i % 100 == 0)
                {
                    int breakPoint = 0;
                }
            }
        }
    }
}