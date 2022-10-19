namespace Tests
{
    [TestClass]
    public class DoubleSideEffectHandlerTests
    {
        [TestMethod]
        public void Are_Defualt_Sizes_Of_Datas_Four()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            int sizeOfDoubleCounts = doubleSideEffectHandler.DoubleCounts.Count();
            int sizeOfExtraTurns = doubleSideEffectHandler.ExtraTurns.Count();

            int expectedSize = 4;
            Assert.AreEqual(sizeOfDoubleCounts, expectedSize);
            Assert.AreEqual(sizeOfExtraTurns, expectedSize);
        }
        [TestMethod]
        public void Are_Default_Values_Of_DoubleCounts_Zero()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            List<int> expectedDoubleCounts = new List<int> { 0, 0, 0, 0 };
            
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(doubleSideEffectHandler.DoubleCounts[i], expectedDoubleCounts[i]);
            }
        }
        [TestMethod]
        public void Are_Default_Values_Of_ExtraTurns_False()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            List<bool> expectedExtraTurns = new List<bool> { false, false, false, false };

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(doubleSideEffectHandler.ExtraTurns[i], expectedExtraTurns[i]);
            }
        }
        [TestMethod]
        public void Count_Double_To_Add_One_To_The_Value_With_Indices()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            int[] indices = new int[2] { 0, 2 };
            int[] expectedDoubleCounts = new int[4] {1, 2, 3, 4};

            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    doubleSideEffectHandler.CountDouble(indices[j]);
                    Assert.AreEqual(doubleSideEffectHandler.DoubleCounts[indices[j]], expectedDoubleCounts[i]);
                }
            }
        }
        [TestMethod]
        public void Reset_DoubleCounts_With_Indices()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            int[] indices = new int[2] { 1, 3 };

            for (int i = 0; i < 2; i++)
            {
                doubleSideEffectHandler.CountDouble(indices[i]);
                int doubleCountBeforeReset = doubleSideEffectHandler.DoubleCounts[indices[i]];

                doubleSideEffectHandler.ResetDoubleCount(indices[i]);

                int expectedDoubleCount = 0;
                Assert.AreNotEqual(doubleCountBeforeReset, expectedDoubleCount);
                Assert.AreEqual(doubleSideEffectHandler.DoubleCounts[indices[i]], expectedDoubleCount);
            }

        }
        [TestMethod]
        public void Set_ExtraTurn_With_Indices_To_Be_True_Or_False()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            int[] indices = new int[] { 0, 1, 2, 3, 2, 0 };
            bool[] boolsToSet = new bool[] { true, false, false, true, false, true};
            bool[] expectedBools = boolsToSet;

            for (int i = 0; i < 6; i++)
            {
                doubleSideEffectHandler.SetExtraTurn(indices[i], boolsToSet[i]);
                Assert.AreEqual(doubleSideEffectHandler.ExtraTurns[indices[i]], expectedBools[i]);
            }

        }
    }
}