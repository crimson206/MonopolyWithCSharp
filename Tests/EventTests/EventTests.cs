using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void TestMethod1()
        {

            Game game = new Game();


            StartTurn independentEvent = game.GenerateEvent();

            for (int i = 0; i < 10; i++)
            {
                game.Run();


                var makeBreakPointHere = 0;
            }


        }



    }
}