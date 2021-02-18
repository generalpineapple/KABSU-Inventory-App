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

        /// <summary>
        /// can #1706 has 28 canes in it
        /// </summary>
        [Test]
        public void CanCapacityTest()
        {
            searchWindow = new SearchWindow();
            SearchResults searchResults = new SearchResults();
            List<SearchResult> sr = new List<SearchResult>();
            SearchTerm st = new SearchTerm("1706", "*", "*", "*", "*", "*", "*");
            sr = searchResults.retrieveData(st);
            int numOfCanes = searchWindow.CalculateCanList(sr);
            Assert.AreEqual(28, numOfCanes);
        }

        /// <summary>
        /// can #1850 has 132 units in it
        /// </summary>
        [Test]
        public void SumUnitTest()
        {
            searchWindow = new SearchWindow();
            SearchResults searchResults = new SearchResults();
            List<SearchResult> sr = new List<SearchResult>();
            SearchTerm st = new SearchTerm("1850", "*", "*", "*", "*", "*", "*");
            sr = searchResults.retrieveData(st);
            int numOfUnits = searchWindow.UnitSum(sr);
            Assert.AreEqual(132, numOfUnits);
        }
    }
}
