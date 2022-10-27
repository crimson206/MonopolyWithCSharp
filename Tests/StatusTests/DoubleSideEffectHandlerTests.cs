using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.StatusTests
{
    [TestClass]
    public class DoubleSideEffectHandlerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();

            /// check default
            Assert.AreEqual(doubleSideEffectHandler.DoubleCounts[0],0);

            /// count one
            doubleSideEffectHandler.CountDouble(0);
            Assert.AreEqual(doubleSideEffectHandler.DoubleCounts[0],1);

            /// reset count
            doubleSideEffectHandler.ResetDoubleCount(playerNumber:0);
            Assert.AreEqual(doubleSideEffectHandler.DoubleCounts[0],0);

            /// check default
            Assert.AreEqual(doubleSideEffectHandler.ExtraTurns[0],false);

            /// check true set
            doubleSideEffectHandler.SetExtraTurn(playerNumber:0, true);
            Assert.AreEqual(doubleSideEffectHandler.ExtraTurns[0],true);
        
            /// check false set
            doubleSideEffectHandler.SetExtraTurn(playerNumber:0, false);
            Assert.AreEqual(doubleSideEffectHandler.ExtraTurns[0],false);
        }
    }
}