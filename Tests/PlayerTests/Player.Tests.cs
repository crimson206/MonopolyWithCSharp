namespace Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void InitializePlayerWithName()
        {
            //Arrange & Act//
            Player player = new Player(playerName:"Peter");

            //Assert//
            Assert.IsInstanceOfType(player, typeof(Player));
        }
        [TestMethod]
        public void GithubTest()
        {
            //Assert//
            Assert.IsTrue(false);
        }
        
    }
}