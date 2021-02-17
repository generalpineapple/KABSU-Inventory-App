using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WpfApp;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KASBUApp___Tests
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class InventoryPageTests
    {
        InventoryPage inventoryPage;
        //Make test for error pop up when user doesn't enter can code.
        [TestMethod]
        public void EmptyRecordError()
        {
            string expected = "Enter cane code.";
            string actual;
            inventoryPage = new InventoryPage();
            inventoryPage.Close();
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Make sure when a user changes the units for a record, it changes in the database.
        /// </summary>
        [TestMethod]
        public void NumUnitsChangeInDatabase()
        {

        }

        /// <summary>
        /// Make sure our additional info window displays if we click the x.
        /// </summary>
        [TestMethod]
        public void AdditionalInfoWindowDisplaysWhenClosingWindow()
        {

        }
    }
}
