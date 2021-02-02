using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WpfApp;

namespace KASBUApp___Tests
{
    class SearchWindowTests
    {
        [Test]
        public void ShowSearchWindowTest()
        {
            SearchWindow searchWindow = new SearchWindow();
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
            SearchWindow searchWindow = new SearchWindow();
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
            SearchWindow search = new SearchWindow();
            
        }

        [Test]
        public void SumUnitTest()
        {

        }
    }
}
