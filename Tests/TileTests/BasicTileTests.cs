using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.TileTests
{
    [TestClass]
    public class BasicTileTests
    {
        [TestMethod]
        public void Initialize_Basic_Constructor_An_Object_Of_Chance_With_Its_Name()
        {
            Chance chance = new Chance("Chance");

            string expectedName = "Chance";
            Assert.AreEqual(chance.Name, expectedName);
        }
        [TestMethod]
        public void Is_Name_Readable_After_Upcasting_To_EventTile()
        {
            Chance chance = new Chance("UpcastedChance");

            EventTile upcatedChance = (EventTile)chance;

            string expectedName = "UpcastedChance";
            Assert.AreEqual(chance.Name, expectedName);
        }
        [TestMethod]
        public void Is_Name_Readable_After_Upcasting_To_Tile()
        {
            Chance chance = new Chance("UpcastedChance");

            Tile upcatedChance = (Tile)chance;

            string expectedName = "UpcastedChance";
            Assert.AreEqual(chance.Name, expectedName);
        }
    }
}