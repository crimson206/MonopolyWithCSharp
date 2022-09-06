namespace Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void InitializePlayerWithName()
        {
            //Arrange & Act//
            Player player = new Player(name:"Peter");

            //Assert//
            Assert.IsInstanceOfType(player, typeof(Player));
        }
    }
}