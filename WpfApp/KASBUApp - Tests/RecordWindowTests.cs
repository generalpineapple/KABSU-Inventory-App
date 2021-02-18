using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WpfApp;

namespace KASBUApp___Tests
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class RecordWindowTests
    {
        public static RecordWindow recordWindow;

        [Test]
        public void ShowRecordWindowTest()
        {
            recordWindow = new RecordWindow();
            recordWindow.Show();
            if (recordWindow.IsVisible == false)
            {
                Assert.Fail();
            }
            else
            {
                recordWindow.Hide();
                Assert.Pass();
            }
        }

        /// <summary>
        /// make sure no cane can go below zero
        /// </summary>
        [Test]
        public void MinUnitsForEachCane()
        {

        }


    }
}