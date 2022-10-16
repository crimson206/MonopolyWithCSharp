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

        }
    }
}