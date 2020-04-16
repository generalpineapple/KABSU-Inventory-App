using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp;

namespace KASBUApp___Testing
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class SmokeTest
    {

        public static MainWindow mainWindow;
        public static RecordWindow recordWindow;
        public static SearchWindow searchWindow;

        public static SearchWindowResults searchWindowResults;
        public static AdditionalInfoWindow additionalInfoWindow;

        [SetUp]
        public void Setup()
        {
            mainWindow = new MainWindow();
            recordWindow = new RecordWindow();
            searchWindow = new SearchWindow();
        }

        [Test]
        public void InitalizeMainWindowTest()
        {
            mainWindow = new MainWindow();
            Assert.NotNull(mainWindow);
            Assert.IsInstanceOf<WpfApp.MainWindow>(mainWindow, "This test failed due to a failed instance of mainWindow being an instance of MainWindow");
        }
        
        [Test]
        public void InitialRecordWindowTest()
        {
            recordWindow = new RecordWindow();
            Assert.NotNull(recordWindow);
            Assert.IsInstanceOf<WpfApp.RecordWindow>(recordWindow, "This test failed due to a failed instance of recordWindow being an instance of RecordWindow");
        }

        [Test]
        public void ShowRecordWindowTest()
        {
            recordWindow = new RecordWindow();
            recordWindow.Show();
            if (recordWindow.IsVisible == false ) {
                Assert.Fail();
            } else {
                recordWindow.Hide();
                Assert.Pass();
            }
        }

        [Test]
        public void InitialSearchWindowTest()
        {
            searchWindow = new SearchWindow();
            Assert.NotNull(searchWindow);
            Assert.IsInstanceOf<WpfApp.SearchWindow>(searchWindow, "This test failed due to a failed instance of searchWindow being an instance of SearchWindow");
        }

        [Test]
        public void ShowSearchWindowTest()
        {
            searchWindow = new SearchWindow();
            searchWindow.Show();
            if (searchWindow.IsVisible == false)
            {
                Assert.Fail();
            }
            else
            {
                searchWindow.Hide();
                Assert.Pass();
            }
        }

        [Test]
        public void InitialSearchResultsWindow()
        {
            searchWindow = new SearchWindow();
            searchWindowResults = new SearchWindowResults(searchWindow.CalculateResultList());
            Assert.IsNotNull(searchWindowResults);
            Assert.IsInstanceOf<WpfApp.SearchWindowResults>(searchWindowResults, "This test failed due to a failed instance of searchWindowResults being an instance of SearchWindowResults");
        }

        [Test]
        public void ShowSearchWindowResults()
        {
            searchWindow = new SearchWindow();
            searchWindowResults = new SearchWindowResults(searchWindow.CalculateResultList());
            searchWindowResults.Show();
            if (searchWindowResults.IsVisible == false)
            {
                Assert.Fail();
            }
            else
            {
                searchWindowResults.Hide();
                Assert.Pass();
            }
        }

        [Test]
        public void InitializeAdditionalInfoWindow()
        {
            AdditionalInfo additionalInfo = new AdditionalInfo();
            additionalInfoWindow = new AdditionalInfoWindow(additionalInfo);
            Assert.IsNotNull(additionalInfoWindow);
            Assert.IsInstanceOf<WpfApp.AdditionalInfoWindow>(additionalInfoWindow, "This test failed due to a failed instance of additionalInfoWindow being an instance of AdditionalInfoWindow");
        }

        [Test]
        public void ShowAdditionalInfoWindow()
        {
            AdditionalInfo additionalInfo = new AdditionalInfo();
            additionalInfoWindow = new AdditionalInfoWindow(additionalInfo);
            additionalInfoWindow.Show();
            if (additionalInfoWindow.IsVisible == false)
            {
                Assert.Fail();
            }
            else
            {
                additionalInfoWindow.Hide();
                Assert.Pass();
            }
        }

    }
}
