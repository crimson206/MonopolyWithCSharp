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

                int breakPoint1 = 0;
                int breakPoint2 = 0;

                if (i % 100 == 0)
                {
                    breakPoint1++;
                }

                if (game.DelegatorData.NextEventName == "HouseBuildEvent")
                {
                    breakPoint2++;
                }
            }
        }
    }
}