using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.TileTests
{
    [TestClass]
    public class TileWithNameTests
    {

        /// developer note
        /// Although they are different classes, they have exactly the same structure
        /// They are tested together here and at "TileWithNumberTests" folder
        /// I wrote tests for each class, but didn't seperate them to different files
        /// I am not sure of the standard rule. Please leave a comment for me if I have to make a file for each class in this case as well

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
        [TestMethod]
        public void Initialze_CommunityChest_With_Name()
        {
           CommunityChest communityChest = new CommunityChest("CommunityChest");


            string expectedName = "CommunityChest";
            Assert.AreEqual(communityChest.Name, expectedName);
        }
        [TestMethod]
        public void Initialze_FreeParking_With_Name()
        {
           FreeParking creeParking = new FreeParking("FreeParking");


            string expectedName = "FreeParking";
            Assert.AreEqual(creeParking.Name, expectedName);
        }
        [TestMethod]
        public void Initialze_GoToJail_With_Name()
        {
           GoToJail goToJail = new GoToJail("GoToJail");


            string expectedName = "GoToJail";
            Assert.AreEqual(goToJail.Name, expectedName);
        }
    }
}
