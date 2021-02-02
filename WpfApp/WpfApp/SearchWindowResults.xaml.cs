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
    /// class for the Search Window results form
    /// </summary>
    public partial class SearchWindowResults : Window
    {
        private RecordWindow recordWindow; // object for Record Window class

        private InventoryPage inventoryPage; //object for Inventory Page class

        /// <summary>
        /// constructor for class
        /// </summary>
        /// <param name="results"></param>
        public SearchWindowResults(List<SearchResult> results)
        {
            InitializeComponent();
            uxSearchResults.ItemsSource = results;
            ValidColumn.Width = 40;
            CanNumColumn.Width = 50;
            CodeColumn.Width = 110;
            CollDateColumn.Width = 90;
            UnitsColumn.Width = 40;
            AnimalNameColumn.Width = 225;
            BreedColumn.Width = 80;
            RegNumColumn.Width = 80;
            OwnerColumn.Width = 100;
            TownColumn.Width = 100;
            StateColumn.Width = 42;
        }

        /// <summary>
        /// event handler when a single row is double clicked on to trigger the record window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            SearchResult search= (SearchResult)row.Item;
            recordWindow = new RecordWindow(search);
            recordWindow.ShowDialog();
        }

        /// <summary>
        /// event handler when multiple rows are selected to trigger the inventory page report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxPullDataButton_Click(object sender, RoutedEventArgs e)
        {
            List<SearchResult> list = new List<SearchResult>();
            foreach (var r in uxSearchResults.SelectedItems)
            {
                SearchResult search = (SearchResult)r;
                list.Add(search);
            }
            inventoryPage = new InventoryPage(list);
            inventoryPage.ShowDialog();
            // DataGridRow row = sender as DataGridRow;
            //Int32 selectedRows = inventoryPage.uxTopGrid1.RowDefinitions.Count();
        }
    }

    /// <summary>
    /// class for the Search Window results form
    /// </summary>
    public partial class CopyOfSearchWindowResults : Window
    {
        private RecordWindow recordWindow; // object for Record Window class

        private InventoryPage inventoryPage; //object for Inventory Page class

        /// <summary>
        /// constructor for class
        /// </summary>
        /// <param name="results"></param>
        public CopyOfSearchWindowResults(List<SearchResult> results)
        {
            InitializeComponent();
            uxSearchResults.ItemsSource = results;
            ValidColumn.Width = 40;
            CanNumColumn.Width = 50;
            CodeColumn.Width = 110;
            CollDateColumn.Width = 90;
            UnitsColumn.Width = 40;
            AnimalNameColumn.Width = 225;
            BreedColumn.Width = 80;
            RegNumColumn.Width = 80;
            OwnerColumn.Width = 100;
            TownColumn.Width = 100;
            StateColumn.Width = 42;
        }

        /// <summary>
        /// event handler when a single row is double clicked on to trigger the record window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            SearchResult search = (SearchResult)row.Item;
            recordWindow = new RecordWindow(search);
            recordWindow.ShowDialog();
        }

        /// <summary>
        /// event handler when multiple rows are selected to trigger the inventory page report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxPullDataButton_Click(object sender, RoutedEventArgs e)
        {
            List<SearchResult> list = new List<SearchResult>();
            foreach (var r in uxSearchResults.SelectedItems)
            {
                SearchResult search = (SearchResult)r;
                list.Add(search);
            }
            inventoryPage = new InventoryPage(list);
            inventoryPage.ShowDialog();
            // DataGridRow row = sender as DataGridRow;
            //Int32 selectedRows = inventoryPage.uxTopGrid1.RowDefinitions.Count();
        }
    }
}
