namespace Tests
{
    [TestClass]
    public class DoubleSideEffectHandlerTests
    {
        [TestMethod]
        public void Initialize_Basic_Constructor_An_Object_With_DoubleCounts_Whose_Size_Is_Four()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            int sizeOfDoubleCounts = doubleSideEffectHandler.DoubleCounts.Count();

            int expectedSize = 4;
            Assert.AreEqual(sizeOfDoubleCounts, expectedSize);
        }
        [TestMethod]
        public void Initialize_Basic_Constructor_An_Object_With_ExtraTurns_Whose_Size_Is_Four()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            int sizeOfExtraTurns = doubleSideEffectHandler.ExtraTurns.Count();

            int expectedSize = 4;
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
        public void Is_ExtraTurns_Protected_From_Change_Of_Its_Copy()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            int index = 0;
            bool backupOfExtraTurn = doubleSideEffectHandler.ExtraTurns[index];

            List<bool> copiedExtraTurns = doubleSideEffectHandler.ExtraTurns;
            copiedExtraTurns[index] = true;
            
            bool expectedBoolIfItWasNotProtected = true;
            Assert.AreNotEqual(backupOfExtraTurn, expectedBoolIfItWasNotProtected);
        }
        [TestMethod]
        public void Is_DoubleCounts_Protected_From_Change_Of_Its_Copy()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
            int index = 0;
            int backupOfDoubleCount = doubleSideEffectHandler.DoubleCounts[index];

            List<int> copiedDoubleCounts = doubleSideEffectHandler.DoubleCounts;
            copiedDoubleCounts[index] = 10;

            int expectedIntIfItWasNotProtected = 10;
            Assert.AreNotEqual(backupOfDoubleCount, expectedIntIfItWasNotProtected);
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