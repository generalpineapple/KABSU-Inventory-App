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
    /// Interaction logic for SearchWindowResults.xaml
    /// </summary>
    public partial class SearchWindowResults : Window
    {
        private RecordWindow recordWindow;

        private InventoryPage inventoryPage;
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

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            SearchResult search= (SearchResult)row.Item;
            recordWindow = new RecordWindow(search);
            recordWindow.ShowDialog();
        }

        private void uxPullDataButton_Click(object sender, RoutedEventArgs e)
        {      
            foreach(DataGridRow r in uxSearchResults.SelectedItems)
            {
                // if (r.IsSelected)
                //{
                    DataGridRow row = r as DataGridRow;
                    SearchResult search = (SearchResult)row.Item;
                    inventoryPage = new InventoryPage(search);
                    inventoryPage.ShowDialog();
                //  break;
                //}

            }
            
            // DataGridRow row = sender as DataGridRow;
            //Int32 selectedRows = inventoryPage.uxTopGrid1.RowDefinitions.Count();

        }
    }
}
