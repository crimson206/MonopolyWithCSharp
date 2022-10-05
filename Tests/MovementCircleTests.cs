using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MovementCircleTest
    {
        [TestMethod]
        public void Is_Lambda_Espression_Usable()
        {
            MovementCircle movementCircle = new MovementCircle(circleSize:10);
            
            int expectedCircleSize = 10;
            Assert.AreEqual(movementCircle.Size, expectedCircleSize);
        }


        [TestMethod]
        public void Initialize_Cirvular_Movement_Center_With_Size()
        {
            MovementCircle movementCircle = new MovementCircle(circleSize:10);
            
            int expectedCircleSize = 10;
            Assert.AreEqual(movementCircle.Size, expectedCircleSize);
        }

        [TestMethod]
        public void Register_ID_With_Position_Zero()
        {
            MovementCircle movementCircle = new MovementCircle(10);

            movementCircle.RegisterID(1);
            
            int expectedPosition = 0;
            Assert.AreEqual(movementCircle.Positions[1], expectedPosition);
        }

        [TestMethod]
        public void Is_Inner_Data_Protected_From_Changes_Of_Copy()
        {
            MovementCircle movementCircle = new MovementCircle(10);
            movementCircle.RegisterID(1);

            movementCircle.Positions[1] = 20;
            
            int expectedPosition = 0;
            Assert.AreEqual(movementCircle.Positions[1], expectedPosition);
        }

        [TestMethod]
        public void Move_In_Circle()
        {
            MovementCircle movementCircle = new MovementCircle(circleSize:10);
            movementCircle.RegisterID(1);
            int[] movementAmounts = new int[] {4, 9, 7};
            int[] expectedPositions = new int[] {4, 3, 0};

            int index = 0;
            foreach (var amount in movementAmounts)
            {   
                /// act ///
                movementCircle.MoveInCircle(1,amount);

                /// assert ///
                Assert.AreEqual(movementCircle.Positions[1], expectedPositions[index]);
                index++;
            }    
        }
        [DataTestMethod]
        [DataRow(15, true)]
        [DataRow(-15, true)]
        [DataRow(5, false)]
        [DataRow(-5, false)]
        public void Count_Passing_End_Point_While_Moving_In_Circle(int movementAmount, bool expectedBool)
        {
            MovementCircle movementCircle = new MovementCircle(circleSize:20);
            int playerID = 1;
            int moveToCenter = 10;
            movementCircle.RegisterID(playerID);
            movementCircle.MoveInCircle(playerID, moveToCenter);

            movementCircle.MoveInCircle(playerID, movementAmount);

            Assert.AreEqual(movementCircle.PassedEnd(playerID), expectedBool);
        }
        [TestMethod]
        public void Teleport_To_Point()
        {
            MovementCircle movementCircle = new MovementCircle(circleSize:10);
            movementCircle.RegisterID(1);

            movementCircle.Teleport(1, 4);

            int expectedPosition = 4;
            Assert.AreEqual(movementCircle.Positions[1],expectedPosition);
        }
        [TestMethod]
        public void Can_Not_Teleport_Out_of_Circle()
        {
            MovementCircle movementCircle = new MovementCircle(circleSize:10);
            movementCircle.RegisterID(1);

            Assert.ThrowsException<Exception>(() => movementCircle.Teleport(1,20));
        }
    }
}
