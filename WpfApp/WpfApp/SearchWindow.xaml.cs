/*
 * Visual Studio 2019
 --------------------------------------------------------
-<<copyright file-"AdditionalInfo.cs"-company=KABSU>"
------Copyright-statement.-All-right-reserved
-</copyright>
 --------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// class for Search Window form
    /// </summary>
    public partial class SearchWindow : Window
    {
        private string owner = "*";
        private string breed = "*";
        private string animalName = "*";
        private string code = "*";
        private string canNum = "*";
        private string town = "*";
        private string state = "*";
        private int canCapacity = 300;
        private int numOfCans;
        private SearchResults searchResults;
        private SearchTerm searchTerm;
        SearchWindowResults windowResults;

        

        /// <summary>
        /// constructor
        /// </summary>
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void UxSearchTerm1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Upon clicking "Search," opens a search results window and closes this window
        private void UxSearch_Click(object sender, RoutedEventArgs e)
        {
            windowResults = new SearchWindowResults(CalculateResultList());
            windowResults.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// event handler when user clicks the Can Capacity button
        /// this will open up a windows display how full the cans entered are
        /// the cans are entered in teh search bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxCanCapacity_Click(object sender, RoutedEventArgs e)
        {
            List<SearchResult> results = CalculateCanList();
            int totalCanCapacity = numOfCans * canCapacity;
            int unitSum = 0;
            foreach (SearchResult sr in results)
            {
                unitSum += Convert.ToInt32(sr.Units);
            }
            double capacityPercent = Math.Round((double)unitSum / totalCanCapacity * 100, 2);
            MessageBox.Show("Number of Cans Entered: " + numOfCans + "\n" +
                             "Capacity of a can: " + canCapacity + "\n" +
                             "Sum of Units: " + unitSum + "\n" +
                             "Total Capacity: " + totalCanCapacity + "\n" +
                             "Percent used: " + capacityPercent + "\n");
        }

        /// <summary>
        /// event handler when the user clicks the Unit Sum button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UxUnitSum_Click(object sender, RoutedEventArgs e)
        {
            List<SearchResult> results = CalculateResultList();
            int unitSum = 0;
            foreach (SearchResult sr in results)
            {
                unitSum += Convert.ToInt32(sr.Units);
            }
            MessageBox.Show("Sum of Units: " + unitSum);
        }

        /// <summary>
        /// method to string together the term and contents
        /// </summary>
        /// <param name="term"></param>
        /// <param name="contents"></param>
        void SetTerm(string term, string contents)
        {
            switch (term)
            {
                case "Owner":
                    owner = "%" + contents + "%";
                    break;
                case "Breed":
                    breed = "%" + contents + "%";
                    break;
                case "Animal Name":
                    animalName = "%" + contents + "%";
                    break;
                case "Code":
                    code = "%" + contents + "%";
                    break;
                case "Can #":
                    canNum = "%" + contents + "%";
                    break;
                case "Town":
                    town = "%" + contents + "%";
                    break;
                case "State":
                    state = "%" + contents + "%";
                    break;
            }
        }

        /// <summary>
        /// method to create a new search term and retrieve its data
        /// </summary>
        /// <returns></returns>
        public List<SearchResult> CalculateResultList()
        {
            SetTerm(uxSearchTerm1.Text, uxSearchContents1.Text);
            SetTerm(uxSearchTerm2.Text, uxSearchContents2.Text);
            SetTerm(uxSearchTerm3.Text, uxSearchContents3.Text);
            SetTerm(uxSearchTerm4.Text, uxSearchContents4.Text);

            //searchTerm = new SearchTerm(canNum, code, animalName, breed, owner, town, state);
            searchTerm = new SearchTerm(canNum, code, animalName, breed, owner, town, state);
            searchResults = new SearchResults();
            List<SearchResult> results = searchResults.retrieveData(searchTerm);
            return results;
        }

        /// <summary>
        /// method to create a new search term and retrieve its data by a list of strings
        /// </summary>
        /// <returns></returns>
        public List<string> CalculateInventoryList()
        {
            SetTerm(uxSearchTerm1.Text, uxSearchContents1.Text);
            SetTerm(uxSearchTerm2.Text, uxSearchContents2.Text);
            SetTerm(uxSearchTerm3.Text, uxSearchContents3.Text);
            SetTerm(uxSearchTerm4.Text, uxSearchContents4.Text);

            searchTerm = new SearchTerm(canNum, code, animalName, breed, owner, town, state);
            searchTerm = new SearchTerm(canNum, code, animalName, breed, owner, town, state);
            searchResults = new SearchResults();
            List<SearchResult> results = searchResults.retrieveData(searchTerm);
            List<string> description = new List<string>();
            foreach(SearchResult s in results)
            {
                description.Add(s.ToString());
            }
            return description;
        }

        /// <summary>
        /// this finds the capacity of the cans in question
        /// if they are entered with ',' seperating them, then if will take every can # entered.
        /// if no can numbers were entered, it returns an empty list 
        /// </summary>
        /// <returns></returns>
        public List<SearchResult> CalculateCanList() 
        {
            SetTerm(uxSearchTerm1.Text, uxSearchContents1.Text);
            SetTerm(uxSearchTerm2.Text, uxSearchContents2.Text);
            SetTerm(uxSearchTerm3.Text, uxSearchContents3.Text);
            SetTerm(uxSearchTerm4.Text, uxSearchContents4.Text);

            searchResults = new SearchResults();
            List<SearchResult> results = new List<SearchResult>();

            if (canNum.Equals("*"))
            {
                searchTerm = new SearchTerm(canNum, code, animalName, breed, owner, town, state);
                results = searchResults.retrieveData(searchTerm);
                numOfCans = results.GroupBy(x => x.CanNum).Count();
                return results;

            }

            string[] canNumbers = canNum.Split(',');
            if(canNumbers.Length > 1)
            {
                numOfCans = canNumbers.Length;
                foreach(string can in canNumbers)
                {
                    searchTerm = new SearchTerm(can.Trim(), code, animalName, breed, owner, town, state);
                    results.AddRange(searchResults.retrieveData(searchTerm));
                }
                return results;
            }
            else
            {
                numOfCans = 1;
                searchTerm = new SearchTerm(canNum, code, animalName, breed, owner, town, state);                    
                results = searchResults.retrieveData(searchTerm);
                return results;
            }
        }
    }
}
