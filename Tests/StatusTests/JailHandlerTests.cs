using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.StatusTests
{
    [TestClass]
    public class JailHandlerTests
    {

        [TestMethod]
        public void Initilize_Basic_Constructor_An_Object_Wiht_TurnsInJailCounts_Whose_Size_Is_Four()
        {
            JailHandler jailHandler = new JailHandler();

            int sizeOfTurnsInJailCounts =  jailHandler.TurnsInJailCounts.Count();

            int expectedSize = 4;
            Assert.AreEqual(sizeOfTurnsInJailCounts, expectedSize);
        }
        [TestMethod]
        public void Initilize_Basic_Constructor_An_Object_Wiht_JailFreeCardCounts_Whose_Size_Is_Four()
        {
            JailHandler jailHandler = new JailHandler();

            int sizeOfJailFreeCardCounts =  jailHandler.JailFreeCardCounts.Count();

            int expectedSize = 4;
            Assert.AreEqual(sizeOfJailFreeCardCounts, expectedSize);
        }
        [TestMethod]
        public void Is_TurnsInJailCounts_Protected_From_Change_Of_Its_Copy()
        {
            JailHandler jailHandler = new JailHandler();
            int index = 0;
            int previousTurnInJail = jailHandler.TurnsInJailCounts[index];
            
            List<int> copyOfTurnsInJailCounts =  jailHandler.TurnsInJailCounts;
            copyOfTurnsInJailCounts[index] += 10;

            int expectedTurnInJailIfIsIsNotProtected = 10;
            Assert.AreNotEqual(previousTurnInJail, expectedTurnInJailIfIsIsNotProtected);
        }
        [TestMethod]
        public void Is_JailFreeCardCounts_Protected_From_Change_Of_Its_Copy()
        {
            JailHandler jailHandler = new JailHandler();
            int index = 0;
            int previousJailFreeCardCount = jailHandler.JailFreeCardCounts[index];
            
            List<int> copyOfJailFreeCardCounts =  jailHandler.JailFreeCardCounts;
            copyOfJailFreeCardCounts[index] = 10;

            int expectedValueIfIsIsNotProtected = 10;
            Assert.AreNotEqual(previousJailFreeCardCount, expectedValueIfIsIsNotProtected);
        }
        [TestMethod]
        public void Count_Turn_In_Jail_With_Index_To_Increase_The_Value_Of_TurnsInJailCounts_At_Index_By_One()
        {
            JailHandler jailHandler = new JailHandler();
            int index = 0;
            int previousTurnsInJail = jailHandler.TurnsInJailCounts[index];

            jailHandler.CountTurnInJail(index);

            int turnsInJailAfterCount = jailHandler.TurnsInJailCounts[index];
            int expectedIncrease = 1;
            Assert.AreEqual(turnsInJailAfterCount-previousTurnsInJail, expectedIncrease);
        }
        [TestMethod]
        public void Reset_TurnInJail_With_Index_To_Make_The_Value_Of_TurnsInJailCounts_At_Index_Zero()
        {
            JailHandler jailHandler = new JailHandler();
            int index = 0;
            jailHandler.CountTurnInJail(index);
            int previousTurnsInJailCount = jailHandler.TurnsInJailCounts[index];

            jailHandler.ResetTurnInJail(index);

            int TurnsInJailCountAfterReset = jailHandler.TurnsInJailCounts[index];
            int expectedTurnsInJailCount = 0;
            Assert.AreNotEqual(previousTurnsInJailCount, TurnsInJailCountAfterReset);
            Assert.AreEqual(TurnsInJailCountAfterReset, expectedTurnsInJailCount);
        }
        [TestMethod]
        public void Add_JailFreeCard_With_Index_To_Increase_The_Value_Of_JailFreeCardCounts_At_Index_By_One()
        {
            JailHandler jailHandler = new JailHandler();
            int index = 0;
            int previousJailFreeCardCount = jailHandler.JailFreeCardCounts[index];

            jailHandler.AddJailFreeCard(index);

            int jailFreeCardCountAfterAddition = jailHandler.JailFreeCardCounts[index];
            int expectedIncrease = 1;
            Assert.AreEqual(jailFreeCardCountAfterAddition-previousJailFreeCardCount, expectedIncrease);
        }
        [TestMethod]
        public void Remove_A_JailFreeCard_With_Index_To_Decrease_The_Value_Of_JailFreeCardCounts_At_Index_By_One()
        {
            JailHandler jailHandler = new JailHandler();
            int index = 0;
            jailHandler.AddJailFreeCard(index);
            int previousJailFreeCardCount = jailHandler.JailFreeCardCounts[index];

            jailHandler.RemoveAJailFreeCard(index);

            int jailFreeCardCountAfteerRemove = jailHandler.JailFreeCardCounts[index];
            int expectedRemovedJailFreeCard = 1;
            Assert.AreEqual(previousJailFreeCardCount - jailFreeCardCountAfteerRemove, expectedRemovedJailFreeCard);
        }
        [TestMethod]
        public void Can_Not_Remove_JailFreeCard_If_JailFreeCardCount_Is_Zero()
        {
            JailHandler jailHandler = new JailHandler();
            int index = 0;
            int jailFreeCardCount = jailHandler.JailFreeCardCounts[index];
            
            int isZero = 0;
            Assert.AreEqual(jailFreeCardCount, isZero);
            Assert.ThrowsException<Exception>(() => jailHandler.RemoveAJailFreeCard(index));
        }
    }
}