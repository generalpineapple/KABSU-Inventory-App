using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RecordWindow recordWindow;
        SearchWindow searchWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Upon clicking "Add New Record," opens a Record Window and closes the Main Window 
        /// </summary>
        /// <param name="sender">object containing sender information</param>
        /// <param name="e">EventArgs associated with button click</param>
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            recordWindow = new RecordWindow();
            recordWindow.Show();
        }

        /// <summary>
        /// Upon clicking "Modify Existing Record," opens a Search Window and closes the Main Window
        /// </summary>
        /// <param name="sender">object containing sender information</param>
        /// <param name="e">EventArgs associated with button click</param>
        public void UxModifyRecord_Click(object sender, RoutedEventArgs e)
        {
            searchWindow = new SearchWindow();
            searchWindow.Show();
        }

        /// <summary>
        /// Handle Date click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Primitives.CalendarDayButton button = sender as System.Windows.Controls.Primitives.CalendarDayButton;
            DateTime clickedDate = (DateTime)button.DataContext;
            if (!uxCalendar.BlackoutDates.Contains(clickedDate))
            {
                MessageBox.Show("Calander Record list");
            }
        }
    }
}
