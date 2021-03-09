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
using System.Data;
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
using MySql.Data.MySqlClient;

namespace WpfApp
{
    /// <summary>
    /// class for Record Window form
    /// </summary>
    public partial class RecordWindow : Window
    {
        SearchResult searchResult;
        private string notes;
        private string oldCode;
        private string oldOwner;
        private string oldTown;
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
        
        /// <summary>
        /// constructor
        /// </summary>
        public RecordWindow()
        {
            newRecord = true;
            isOldRecord = false;
            searchResult = new SearchResult();
            InitializeComponent();
            notes = "";
            Closing += RecordWindow_Closing;
        }

        /// <summary>
        /// constructor for a record
        /// </summary>
        /// <param name="search"></param>
        public RecordWindow(SearchResult search)
        {
            newRecord = false;
            searchResult = search;
            oldCode = searchResult.Code;
            oldOwner = searchResult.Owner;
            oldTown = searchResult.Town;
            oldState = searchResult.State;
            InitializeComponent();
            uxCode.Text = searchResult.Code;
            uxBreed.Text = searchResult.Breed;
            uxAnimalName.Text = searchResult.AnimalName;
            uxRegNum.Text = searchResult.RegNum;
            uxOwner.Text = searchResult.Owner;
            uxCanNum.Text = searchResult.CanNum;
            notes = "";
            isMorph = false;
            isOldMorph = false;
            populating = false;
            Closing += RecordWindow_Closing;
            recordList = RetrieveRecords(searchResult.Code);
            morph = RetrieveMorph(searchResult.Code);
        }

        public AdditionalInfoWindow AdditionalInfoWindow
        {
            get => default;
            set
            {
            }
        }

        internal Record Record
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// event handler for closing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordWindow_Closing(object sender, CancelEventArgs e)
        {
            CollectAdditionalInfo();

            this.IsEnabled = false;

            StoreParent();
            List<string> list = new List<string>();
            List<string> morphList = new List<string>();
            int textCount = 0;
            int recordCount = 0;
            foreach(TextBox tb in FindVisualChildren<TextBox>(this))
            {
                list.Add(tb.Text);
                if (tb.Text != "" && (tb.Parent != uxBottomGrid && tb.Parent != uxMorphGrid))
                {
                    textCount++;
                    recordCount++;
                }
                if (tb.Text != "" && (tb.Parent != uxBottomGrid && tb.Parent != uxTopGrid1 && tb.Parent != uxTopGrid2))
                    isMorph = true;
            }
            recordList = new List<Record>();
            for (int i = 0; textCount > 0; i++)
            {
                if (list[i] != "" || list[i + ROW_SPACING] != "" || list[i + (ROW_SPACING * 2)] != "" || list[i + (ROW_SPACING * 3)] != "" || list[i + (ROW_SPACING * 4)] != "")
                {
                    recordList.Add(new Record(list[i], list[i + ROW_SPACING], list[i + (ROW_SPACING * 2)], list[i + (ROW_SPACING * 3)], list[i + (ROW_SPACING * 4)], list[ID_INDEX]));
                    if (list[i] != "")
                        textCount--;
                    if (list[i + ROW_SPACING] != "")
                        textCount--;
                    if (list[i + (ROW_SPACING * 2)] != "")
                        textCount--;
                    if (list[i + (ROW_SPACING * 3)] != "")
                        textCount--;
                    if (list[i + (ROW_SPACING * 4)] != "")
                        textCount--;
                }
            }
            if (isMorph)
            {
                morph = new Morph(notes, list[MORPH_ID], list[MORPH_ID + 1], list[MORPH_ID + 2], list[MORPH_ID + 3], list[MORPH_ID + 4], Convert.ToInt32(list[MORPH_ID + 5]), list[ID_INDEX]);
            }
            StoreRecords();
            StoreMorph();
        }

        /// <summary>
        /// method for grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// method to store records in database
        /// </summary>
        private void StoreRecords()
        {
            if (newRecord == true)
            {
                string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        /*using (var command = new MySqlCommand("kabsu.DeleteData", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@AnimalID", uxCode.Text);
                            connection.Open();
                            int k = command.ExecuteNonQuery();
                            connection.Close();
                        }*/
                        foreach (Record r in recordList)
                        {
                            string[] dateAndCollCode = r.Date.Split('#');
                            if(r.Rec.Equals(""))
                            {
                                r.Rec = "0";
                            }
                            if(r.Ship.Equals(""))
                            {
                                r.Ship = "0";
                            }
                               r.Balance = (Convert.ToInt32(r.Rec) - Convert.ToInt32(r.Ship) + Convert.ToInt32(uxMorphUnits.Text)).ToString();

                            using (var command = new MySqlCommand("kabsu.StoreData", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@ToFrom", r.ToFrom);
                                command.Parameters.AddWithValue("@RealDate", Convert.ToDateTime(r.Date));
                                command.Parameters.AddWithValue("@Date", r.Date);
                                command.Parameters.AddWithValue("@Received", Convert.ToInt32(r.Rec));
                                command.Parameters.AddWithValue("@Shipped", Convert.ToInt32(r.Ship));
                                command.Parameters.AddWithValue("@Balance", Convert.ToInt32(r.Balance));
                                command.Parameters.AddWithValue("@AnimalID", r.AnimalId);
                                command.Parameters.AddWithValue("@Can", uxCanNum.Text);
                                command.Parameters.AddWithValue("@CollDate", uxMorphCode.Text);

                                connection.Open();
                                int k = command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to database01.");
                }
            }
            else
            {
                string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        foreach (Record r in recordList)
                        {
                            string[] dateAndCollCode = r.Date.Split('#');
                            if (r.Rec.Equals(""))
                            {
                                r.Rec = "0";
                            }
                            if (r.Ship.Equals(""))
                            {
                                r.Ship = "0";
                            }
                            r.Balance = (Convert.ToInt32(r.Rec) - Convert.ToInt32(r.Ship) + Convert.ToInt32(uxMorphUnits.Text)).ToString();
                            using (var command = new MySqlCommand("kabsu.UpdateData", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@ToFrom", r.ToFrom);
                                command.Parameters.AddWithValue("@RealDate", Convert.ToDateTime(r.Date));
                                command.Parameters.AddWithValue("@Date", r.Date);
                                command.Parameters.AddWithValue("@Received", Convert.ToInt32(r.Rec));
                                command.Parameters.AddWithValue("@Shipped", Convert.ToInt32(r.Ship));
                                command.Parameters.AddWithValue("@Balance", Convert.ToInt32(r.Balance));
                                command.Parameters.AddWithValue("@AnimalID", r.AnimalId);
                                command.Parameters.AddWithValue("@Can", uxCanNum.Text);
                                command.Parameters.AddWithValue("@CollDate", uxMorphCode.Text);

                                connection.Open();
                                int k = command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to database01pt2.");
                }

            } 
        }

        /// <summary>
        /// method used to store additional info window in database
        /// </summary>
        private void StoreMorph()// creat an blank item
        {
            if (isMorph == true && isOldMorph == false)
            {
                string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        using (var command = new MySqlCommand("kabsu.StoreMorph", connection))
                        {

                            string[] dateAndCollCode = uxMorphDate.Text.Split('#');

                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@Notes", morph.Notes);
                            command.Parameters.AddWithValue("@Date", uxMorphDate.Text);
                            //command.Parameters.AddWithValue("@RealDate", Convert.ToDateTime(uxMorphDate.Text));
                            if (morph.Vigor == "")
                            {
                                command.Parameters.AddWithValue("@Vigor", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Vigor", Convert.ToInt32(morph.Vigor));
                            }

                            if (morph.Mot == "")
                            {
                                command.Parameters.AddWithValue("@Mot", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Mot", Convert.ToInt32(morph.Mot));
                            }

                            if (morph.Morphology == "")
                            {
                                command.Parameters.AddWithValue("@Morph", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Morph", Convert.ToInt32(morph.Morphology));
                            }

                            if (morph.Code == "")
                            {
                                if(dateAndCollCode.Length == 2)
                                    command.Parameters.AddWithValue("@CollCode", Convert.ToInt32(dateAndCollCode[1]));
                                else
                                    command.Parameters.AddWithValue("@CollCode", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CollCode", Convert.ToInt32(morph.Code));
                            }

                            if (morph.Units == 0)
                            {
                                command.Parameters.AddWithValue("@Units", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Units", Convert.ToInt32(uxMorphUnits.Text));
                            }

                            command.Parameters.AddWithValue("@AnimalID", morph.Id);

                            connection.Open();
                            int k = command.ExecuteNonQuery();
                            connection.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to database02.");
                }
            }
        }

        /// <summary>
        /// method to store records in database
        /// </summary>
        private void StoreParent()//input value to blank item
        {
            if (newRecord == true)
            {
                string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        using (var command = new MySqlCommand("kabsu.StoreParent", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            if (!uxCanNum.Text.Equals("") || !uxCode.Text.Equals("") || !uxMorphDate.Text.Equals("") || 
                                !info.Town.Equals("") || !info.State.Equals("") || !info.Country.Equals("") || !uxOwner.Text.Equals("") || !uxAnimalName.Text.Equals("") ||
                                !uxBreed.Text.Equals("") || !info.Species.Equals("") || !uxRegNum.Text.Equals(""))
                            { 
                                
                                command.Parameters.AddWithValue("@LastModified", DateTime.Now);
                                command.Parameters.AddWithValue("@CanNum", uxCanNum.Text);
                                command.Parameters.AddWithValue("@AnimalID", uxCode.Text);
                                command.Parameters.AddWithValue("@CollDate", uxMorphDate.Text);
                                command.Parameters.AddWithValue("@NumUnits", uxMorphUnits.Text);
                                command.Parameters.AddWithValue("@Town", info.Town);
                                command.Parameters.AddWithValue("@State", info.State);
                                command.Parameters.AddWithValue("@Country", info.Country);
                                command.Parameters.AddWithValue("@Owner", uxOwner.Text);
                                command.Parameters.AddWithValue("@AnimalName", uxAnimalName.Text);
                                command.Parameters.AddWithValue("@Breed", uxBreed.Text);
                                command.Parameters.AddWithValue("@Species", info.Species);
                                command.Parameters.AddWithValue("@RegNum", uxRegNum.Text);

                                connection.Open();
                                int k = command.ExecuteNonQuery();
                                connection.Close();
                            }
                            else
                            {
                                MessageBox.Show("Can num, Code num, Breed, Name of Animal, Reg num, Owner must all be filled out.");
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to database03.");
                }
            }
            else
            {
                string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        using (var command = new MySqlCommand("kabsu.UpdateParent", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            //command.Parameters.AddWithValue("@SValid", info.Valid.ToString().ToUpper());
                            command.Parameters.AddWithValue("@SCanNum", uxCanNum.Text);
                            command.Parameters.AddWithValue("@OldAnimalID", oldCode);
                            command.Parameters.AddWithValue("@AAnimalID", uxCode.Text);
                            command.Parameters.AddWithValue("@SCollDate", uxMorphDate.Text);
                            command.Parameters.AddWithValue("@SNumUnits", uxMorphUnits.Text);
                            command.Parameters.AddWithValue("@PTown", info.Town);
                            command.Parameters.AddWithValue("@OldTown", oldTown);
                            command.Parameters.AddWithValue("@PState", info.State);
                            command.Parameters.AddWithValue("@OldState", oldState);
                            command.Parameters.AddWithValue("@PCountry", info.Country);
                            command.Parameters.AddWithValue("@POwner", uxOwner.Text);
                            command.Parameters.AddWithValue("@OldOwner", oldOwner);
                            command.Parameters.AddWithValue("@AAnimalName", uxAnimalName.Text);
                            command.Parameters.AddWithValue("@ABreed", uxBreed.Text);
                            command.Parameters.AddWithValue("@ASpecies", info.Species);
                            command.Parameters.AddWithValue("@ARegNum", uxRegNum.Text);

                            connection.Open();
                            int k = command.ExecuteNonQuery();//useful part(upload to database)
                            connection.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to database04.");
                }
            }
        }

        /// <summary>
        /// method to retrieve records from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<Record> RetrieveRecords(string id)
        {
            string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var command = new MySqlCommand("kabsu.RetrieveRecords", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AnimalID", id);

                        connection.Open();
                        var reader = command.ExecuteReader();
                        

                        recordList = new List<Record>();
                        Record record;
                        while (reader.Read())
                        {
                            record = new Record(
                               reader.GetString(reader.GetOrdinal("ToFrom")),
                               reader.GetString(reader.GetOrdinal("Date")),
                               reader.GetInt32(reader.GetOrdinal("NumReceived")).ToString(),
                               reader.GetInt32(reader.GetOrdinal("NumShipped")).ToString(),
                               reader.GetInt32(reader.GetOrdinal("Balance")).ToString(), id);
                            recordList.Add(record);
                        }
                        connection.Close();
                        return recordList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to database05.");
                return new List<Record>();
            }
        }

        /// <summary>
        /// method to retrieve morph records from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Morph RetrieveMorph(string id)
        {
            string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var command = new MySqlCommand("kabsu.RetrieveMorph", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AnimalID", id);
                        connection.Open();

                        var reader = command.ExecuteReader();
                        Morph morph = new Morph();
                        while (reader.Read())
                        {
                            morph = new Morph(
                               reader.GetString(reader.GetOrdinal("Notes")),
                               reader.GetString(reader.GetOrdinal("Date")),
                               reader.GetInt32(reader.GetOrdinal("Vigor")).ToString(),
                               reader.GetInt32(reader.GetOrdinal("Mot")).ToString(),
                               reader.GetInt32(reader.GetOrdinal("Morph")).ToString(),
                               reader.GetInt32(reader.GetOrdinal("CollCode")).ToString(),
                               reader.GetInt32(reader.GetOrdinal("Units")), id);
                            if (morph.Notes != null)
                                notes = morph.Notes;
                            isMorph = true;
                            isOldMorph = true;
                        }
                        return morph;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to database06.");
                return new Morph();
            }
        }

        /// <summary>
        /// event handler for opening window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordWindow_Load(object sender, RoutedEventArgs e)
        {
            int textCount = 0;

            IEnumerable<TextBox> textBoxEnum = (IEnumerable<TextBox>)FindVisualChildren<TextBox>(this);
            List<TextBox> textBoxes = textBoxEnum.ToList<TextBox>();

            if (recordList != null)
            {
                foreach (Record r in recordList)
                {
                    textBoxes[textCount].Text = r.ToFrom;
                    textBoxes[textCount + ROW_SPACING].Text = r.Date;
                   textBoxes[textCount + (ROW_SPACING * 2)].Text = r.Rec.ToString();
                    textBoxes[textCount + (ROW_SPACING * 3)].Text = r.Ship.ToString();
                    textBoxes[textCount + (ROW_SPACING * 4)].Text = r.Balance.ToString();

                    textCount++;

                    if (textCount == 32)
                        textCount += 128;
                }
            }
            if (morph != null)
            {
                textBoxes[MORPH_ID].Text = morph.Date;
                textBoxes[MORPH_ID + 1].Text = morph.Vigor;
                textBoxes[MORPH_ID + 2].Text = morph.Mot;
                textBoxes[MORPH_ID + 3].Text = morph.Morphology;
                textBoxes[MORPH_ID + 4].Text = morph.Code;
                textBoxes[MORPH_ID + 5].Text = morph.Units.ToString();
            }
            if (searchResult.Units != null)
            {
                uxMorphUnits.Text = searchResult.Units.ToString();
            }
            if (searchResult.CollDate != null)
            {
                uxMorphDate.Text = searchResult.CollDate;
            }
            isOldMorph = true;
        }

        /// <summary>
        /// method for when morph is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MorphChanged(object sender, TextChangedEventArgs e)
        {
            isOldMorph = false;
        }

        /// <summary>
        /// event handler for opening notes button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UxNotesButton_Click(object sender, RoutedEventArgs e)
        {
            noteWindow = new NoteWindow(notes);
            noteWindow.Check += value => notes = value;
            noteWindow.ShowDialog();
            isOldMorph = false;
        }

        /// <summary>
        /// method to collect additional info that loads page
        /// </summary>
        private void CollectAdditionalInfo()
        {
            if (newRecord == true)
                info = new AdditionalInfo();
            else
                info = new AdditionalInfo(searchResult.Species, searchResult.Town, searchResult.State, searchResult.Country);
            infoWindow = new AdditionalInfoWindow(info);
            infoWindow.Check += value => info = value;
            infoWindow.ShowDialog();
        }
    }

}
