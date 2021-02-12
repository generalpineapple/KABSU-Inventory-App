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

        //Make test for error pop up when user doesn't enter can code.
        [Test]
        private void EmptyRecordError()
        {

        }
    }
}