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
    public class SearchWindowTests
    {
        public static SearchWindow searchWindow;
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
        public void ShowSearchWindowResults()
        {
            searchWindow = new SearchWindow();
            SearchWindowResults searchWindowResults = new SearchWindowResults(searchWindow.CalculateResultList());
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
        public void CanCapacityTest()
        {
            searchWindow = new SearchWindow();
            searchWindow.Show();
            
        }

        [Test]
        public void SumUnitTest()
        {

        }
    }
}
