using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.StatusTests
{
    [TestClass]
    public class JailManagerTests
    {
        [TestMethod]
        public void JailManagerTestsAll()
        {
            JailHandler jailManager = new JailHandler();
            Assert.AreEqual(jailManager.TurnsInJailCounts[0], 0);
            
            jailManager.CountTurnInJail(0);
            Assert.AreEqual(jailManager.TurnsInJailCounts[0], 1);

            jailManager.ResetTurnInJail(0);
            Assert.AreEqual(jailManager.TurnsInJailCounts[0], 0);

            Assert.ThrowsException<Exception>(() => jailManager.RemoveAFreeJailCard(0));
            
            jailManager.AddFreeJailCard(0);
            Assert.AreEqual(jailManager.FreeJailCardCounts[0], 1);

            jailManager.RemoveAFreeJailCard(0);
            Assert.AreEqual(jailManager.FreeJailCardCounts[0], 0);
        }
    }
}