using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp;


namespace KASBUApp___Testing
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class UnitTest
    {

        public static MainWindow mainWindow;
                             
        [SetUp]
        public void Setup()
        {
            mainWindow = new MainWindow();
        }

        [Test]
        public void ViewTotalSampleTest()
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            ContentControl windowContent = mainWindow;
            Assert.Fail();
        }

        [Test]
        public void AddNewSampleRecordTest()
        {
            mainWindow = new MainWindow();
            Assert.Fail();
        }

        [Test]
        public void SearchForSampleRecord()
        {
            mainWindow = new MainWindow();
            Assert.Fail();
        }

    }
}