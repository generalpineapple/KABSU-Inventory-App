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
    public class MainWindowTests
    {
        MainWindow mainWindow;
        RecordWindow recordWindow;

        [Test]
        public void MainWindowOpensProperly()
        {
            mainWindow = new MainWindow();
            Assert.True(mainWindow.IsVisible == true);
        }

        //Make test for pop up when user clicks on calendar
        [Test]
        private void CalendarPopUp()
        {
            mainWindow = new MainWindow();
        }
    }
}
