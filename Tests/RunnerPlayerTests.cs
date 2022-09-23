namespace Tests
{
    [TestClass]
    public class RunnerPlayerTests
    {
        [TestMethod]
        public void Initialize_Player_With_Correct_Name()
        {
            //Arrange & Act//
            RunnerPlayer player = new RunnerPlayer(name:"Peter", id:0);
            string playerName = player.Name;

            //Assert//
            string expectedName = "Peter";
            Assert.AreEqual(player.Name, expectedName);
        }
        
        [TestMethod]
        public void Initialize_Player_With_Correct_ID()
        {
            //Arrange & Act//
            RunnerPlayer player = new RunnerPlayer(name:"Peter", id:0);

            //Assert//
            int expectedID = 0;
            Assert.AreEqual(player.ID, expectedID);
        }
    }
}