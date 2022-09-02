using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RollDiceTests
    {
        [TestMethod]
        public void RollDice_Return_Two_Random_Ints()
        {   
            Player player = new Player("Player");
            int i = 0;
            while (i < 1)
            {   
                int[] rollDiceResult = player.RollDice();
                if (rollDiceResult[0] == rollDiceResult[1])
                {
                    i++;
                }
            }
        }
        [TestMethod]
        public void SumRollDiceResult_Return_Sum_Of_RollDiceResult()
        {   
            Player player = new Player("Player");
            
            int[] rollDiceResult = {2, 5};

            int sumRollDiceResult = player.SumRollDiceResult(rollDiceResult);
            
            int expectedSum = 2+5;
            Assert.AreEqual(sumRollDiceResult, expectedSum);
        }
        [TestMethod]
        public void CheckRollDiceDouble_Return_True_For_Double()
        {   
            Player player = new Player("Player");
            int[] doubleResult = {2, 2};

            bool isDouble = player.CheckRollDiceDouble(doubleResult);
            
            Assert.IsTrue(isDouble);
        }
        [TestMethod]
        public void CheckRollDiceDouble_Return_False_For_Non_Double()
        {   
            Player player = new Player("Player");
            int[] nonDoubleResult = {2, 5};

            bool isDouble = player.CheckRollDiceDouble(nonDoubleResult);
            
            Assert.IsFalse(isDouble);
        }
    }
}