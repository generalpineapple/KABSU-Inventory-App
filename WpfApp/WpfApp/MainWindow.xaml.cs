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
using MySql.Data.MySqlClient;
using System.Data;

namespace WpfApp
{
    /// <summary>
    /// Class for the Main Window form.
    /// </summary>
    public partial class MainWindow : Window
    {
        RecordWindow recordWindow; //object for Record Window
        SearchWindow searchWindow; //object for Search Window

        /// <summary>
        /// constructor
        /// </summary>
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
                //MessageBox.Show("Calander Record list");
                string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        using (var command = new MySqlCommand("kabsu.RecordDate", connection))
                        {
                            SearchResult searchResult;

                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@Date", clickedDate);

                            connection.Open();

                            var reader = command.ExecuteReader();

                            StringBuilder resultList = new StringBuilder();

                            while (reader.Read())
                            {
                                searchResult = new SearchResult(
                                   reader.GetString(reader.GetOrdinal("LastModified")),
                                   reader.GetString(reader.GetOrdinal("CanNum")),
                                   reader.GetString(reader.GetOrdinal("AnimalID")),
                                   reader.GetString(reader.GetOrdinal("CollDate")),
                                   reader.GetString(reader.GetOrdinal("NumUnits")),
                                   reader.GetString(reader.GetOrdinal("AnimalName")),
                                   reader.GetString(reader.GetOrdinal("Breed")),
                                   reader.GetString(reader.GetOrdinal("RegNum")),
                                   reader.GetString(reader.GetOrdinal("PersonName")),
                                   reader.GetString(reader.GetOrdinal("City")),
                                   reader.GetString(reader.GetOrdinal("State")),
                                   reader.GetString(reader.GetOrdinal("Country")),
                                   reader.GetString(reader.GetOrdinal("Species")));
                                resultList.Append(searchResult.ToString()).AppendLine();
                            }
                            MessageBox.Show(resultList.ToString());
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No plan today.");
                }
            }
        }
    }
}
