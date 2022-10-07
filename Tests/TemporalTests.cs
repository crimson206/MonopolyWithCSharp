using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TemporalTests
    {
        public delegate void DelConvertIntToStr(int num);
        
        public void Func1(int a)
        {

        }

        public void Func2(int a)
        {
        }

        [TestMethod]
        public void TestMethod1()
        {

            DelConvertIntToStr list = Func1;
            DelConvertIntToStr list2 = Func1;
            List<DelConvertIntToStr> list3 = new List<DelConvertIntToStr> {Func1, Func2};
            list += Func2;
            DelConvertIntToStr list4 = list + list2;
            Console.ReadLine();
        }
    }
}