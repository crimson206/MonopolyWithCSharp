namespace Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Initialize_Player_With_Correct_Name()
        {
            //Arrange & Act//
            Player player = new Player(name:"Peter", iD:0, new Bank());

            //Assert//
            string expectedName = "Peter";
            Assert.AreEqual(player.name, expectedName);
        }
    }
}