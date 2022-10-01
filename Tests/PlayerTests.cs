namespace Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Initialize_Player_With_Correct_Name()
        {
            //Arrange & Act//
            Player player = new Player(name:"Peter");

            //Assert//
            string expectedName = "Peter";
            Assert.AreEqual(player.name, expectedName);
        }
    }
}