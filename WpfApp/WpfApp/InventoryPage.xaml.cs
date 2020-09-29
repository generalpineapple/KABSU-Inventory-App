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
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Window
    {
        SearchResult searchResult;
        private string notes;
        private string oldCode;
        private string oldOwner;
        private string oldCity;
        private string oldState;
        private AdditionalInfo info;
        private static int ID_INDEX = 321;
        private static int ROW_SPACING = 32;
        private static int MORPH_ID = 326;
        private List<Record> recordList;
        private Morph morph;
        private bool isMorph;
        private bool isOldMorph;
        private bool populating;
        private bool newRecord;
        private bool isOldRecord;
        private NoteWindow noteWindow;
        private AdditionalInfoWindow infoWindow;


        public InventoryPage()
        {
            newRecord = true;
            isOldRecord = false;
            searchResult = new SearchResult();
            InitializeComponent();
            notes = "";
        }

        public InventoryPage(List<string> results)
        {
            InitializeComponent();
            uxInventoryPage.ItemsSource = results;
            DescriptionColumn.Width = 200;
        }

        public InventoryPage(SearchResult search)
        {
            newRecord = false;
            searchResult = search;
            oldCode = searchResult.Code;
            oldOwner = searchResult.Owner;
            oldCity = searchResult.Town;
            oldState = searchResult.State;
            InitializeComponent();
            notes = "";
            isMorph = false;
            isOldMorph = false;
            populating = false;
        }
    }
}
