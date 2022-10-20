using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.TileTests
{
    [TestClass]
    public class TileWithNumberTests
    {
        [TestMethod]
        public void Is_Tax_Of_TaxTile_Readable_After_Upcating_To_Tile()
        {
            TaxTile taxTile = new TaxTile(name:"TaxTile", tax:100);

            Tile upcastedTaxTile = (Tile)taxTile;

            int expectedTax = 100;
            Assert.AreEqual(taxTile.Tax, expectedTax);
        }
        [TestMethod]
        public void Is_Tax_Of_LuxuryTax_Readable_After_Upcating_To_TaxTile()
        {
            LuxuryTax luxuryTax = new LuxuryTax(name:"Luxury", tax:100);

            TaxTile upcastedLuxuryTax = (TaxTile)luxuryTax;

            int expectedTax = 100;
            Assert.AreEqual(upcastedLuxuryTax.Tax, expectedTax);
        }
        [TestMethod]
        public void Initialize_IncomeTax_With_PercentageOfTax()
        {
            IncomeTax incomeTax = new IncomeTax(name:"Income", tax:100, percentageOfTax:10);

            int expectedPercentageOfTax = 10;
            Assert.AreEqual(incomeTax.PercentageOfTax , expectedPercentageOfTax);
        }
        [TestMethod]
        public void Intialize_Go_With_Salary()
        {
            Go go = new Go(name:"Go", salary:100);

            int expectedSalary = 100;
            Assert.AreEqual(go.Salary, expectedSalary);
        }
        [TestMethod]
        public void Intialize_Jail_With_Salary()
        {
            Jail jail = new Jail(name:"Jail", jailFine:60);

            int expectedJailFine = 60;
            Assert.AreEqual(jail.JailFine, expectedJailFine);
        }
    }
}
