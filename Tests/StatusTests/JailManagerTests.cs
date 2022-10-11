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
            JailManager jailManager = new JailManager();
            Assert.AreEqual(jailManager.TurnsInJail[0], 0);
            
            jailManager.CountTurnInJail(0);
            Assert.AreEqual(jailManager.TurnsInJail[0], 1);

            jailManager.ResetTurnInJail(0);
            Assert.AreEqual(jailManager.TurnsInJail[0], 0);

            Assert.ThrowsException<Exception>(() => jailManager.RemoveAFreeJailCard(0));
            
            jailManager.AddFreeJailCard(0);
            Assert.AreEqual(jailManager.FreeJailCards[0], 1);

            jailManager.RemoveAFreeJailCard(0);
            Assert.AreEqual(jailManager.FreeJailCards[0], 0);
        }
    }
}