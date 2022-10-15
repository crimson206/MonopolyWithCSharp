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


            Delegator delegator = new Delegator();
            BoolCopier boolCopier  = new BoolCopier();
            
            Game game = new Game();

            boolCopier.isConditionBoolMocked = true;
            boolCopier.isDecisionBoolMocked = true;
            List<List<bool>> aa = new List<List<bool>>();
            for (int j = 0; j < 5; j++)
            {
                boolCopier.ResetBoolMockingCount();
                List<bool> boolCollector = new List<bool>();
                for (int i = 0; i < 5; i++)
                {
                    boolCopier.SetMockedBoolAtIndexFrom(j);
                    delegator.RunEvent();
                    boolCollector.Add(boolCopier.MockedBool);
                    var ab = 0;
                }
                aa.Add(boolCollector);
                boolCollector = new List<bool>();
            }

        }
    }
}