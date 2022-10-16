using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ExperimentTests
    {

        public delegate void DelTest(int a, int b);
        public delegate void DelTest2();
        public DelTest del1;

        [TestMethod]
        public void TestMethod1()
        {
            var t = ("aa bb").ToList().Count(c => c == ' ');
            var r = 0;

            List<int> ints = new List<int> { 1, 2, 3, 4};

            var boolean = ( 1 != 2 || 2 != 2);

            Delegator delegator = new Delegator();


            var test = new BankHandler();
            var test2 = nameof(test.IncreaseBalance);
            del1 = test.IncreaseBalance;

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
                    ///delegator.RunEvent();
                    boolCollector.Add(boolCopier.MockedBool);
                    var ab = 0;
                }
                aa.Add(boolCollector);
                boolCollector = new List<bool>();
            }
            IndependentEvent independentEvent = game.GenerateEvent();

            var boolColr = new List<bool>();

        }


        [TestMethod]
        public void TestMethod2()
        {
            BankHandler bankHandler = new BankHandler();

            DelTest test = bankHandler.IncreaseBalance;

            List<DelTest> list = new List<DelTest>();

            list.Add(test);

        }
    }
}