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

        [TestMethod]
        public void Intialize_Player_With_Start_Money_1500()
        {
            //Arrange & Act//
            Player player = new Player(name:"Peter", iD:0, new Bank());

            //Assert//
            int expectedPlayerMoney = 1500;
            Assert.AreEqual(player.Money, expectedPlayerMoney);
        }
    }
}