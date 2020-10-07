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
    /// Interaction logic for RecordWindow.xaml
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
        private List<InventoryRecord> inventoryrecordList;
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
            Closing += InventoryPage_Closing;
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
            uxDescription.Text = searchResult.Code;
            uxDescription.Text += searchResult.Breed;
            uxDescription.Text += searchResult.AnimalName;
            uxDescription.Text += searchResult.RegNum;
            uxDescription.Text += searchResult.Owner;
            notes = "";
            isMorph = false;
            isOldMorph = false;
            populating = false;
            Closing += InventoryPage_Closing;
            recordList = RetrieveRecords(searchResult.Code);
            morph = RetrieveMorph(searchResult.Code);
        }

        private void InventoryPage_Closing(object sender, CancelEventArgs e)
        {
            CollectAdditionalInfo();

            this.IsEnabled = false;

            StoreParent();
            List<string> list = new List<string>();
            List<string> morphList = new List<string>();
            int textCount = 0;
            int recordCount = 0;
            foreach (TextBox tb in FindVisualChildren<TextBox>(this))
            {
                list.Add(tb.Text);
                if (tb.Text != "" && (tb.Parent != uxBottomGrid))
                {
                    textCount++;
                    recordCount++;
                }
                if (tb.Text != "" && (tb.Parent != uxBottomGrid && tb.Parent != uxTopGrid1))
                    isMorph = true;
            } 
            inventoryrecordList = new List<InventoryRecord>();
            for (int i = 0; textCount > 0; i++)
            {
                if (list[i] != "" || list[i + ROW_SPACING] != "" || list[i + (ROW_SPACING * 2)] != "" || list[i + (ROW_SPACING * 3)] != "" || list[i + (ROW_SPACING * 4)] != "")
                {
                    inventoryrecordList.Add(new InventoryRecord(list[i], list[i + ROW_SPACING], list[i + (ROW_SPACING * 2)], list[i + (ROW_SPACING * 3)], list[i + (ROW_SPACING * 4)], list[ID_INDEX]));
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
                morph = new Morph(notes, list[MORPH_ID], list[MORPH_ID + 1], list[MORPH_ID + 2], list[MORPH_ID + 3], list[MORPH_ID + 4], list[MORPH_ID + 5], list[ID_INDEX]);
            }
            StoreRecords();
            StoreMorph();
        }
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
        private void StoreRecords()
        {
            if (recordList.Count == 0)
            {
                string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        using (var command = new MySqlCommand("kabsu.DeleteData", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@ID", searchResult.Code);
                            connection.Open();
                            int k = command.ExecuteNonQuery();
                            connection.Close();
                        }
                        foreach (InventoryRecord r in inventoryrecordList)
                        {

                            using (var command = new MySqlCommand("kabsu.StoreData", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@Item", r.Item);
                                command.Parameters.AddWithValue("@Description", r.Description);
                                command.Parameters.AddWithValue("@Qty", Convert.ToInt32(r.Qty));
                                command.Parameters.AddWithValue("@Rate", Convert.ToInt32(r.Rate));
                                command.Parameters.AddWithValue("@Amount", Convert.ToInt32(r.Amount));
                                command.Parameters.AddWithValue("@AnimalID", r.AnimalId);

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
        }
        private void StoreMorph()
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
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@Notes", morph.Notes);
                           // command.Parameters.AddWithValue("@Date", uxMorphDate.Text);
                            if (morph.Vigor == "")
                            {
                                command.Parameters.AddWithValue("@Vigor", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Vigor", Convert.ToInt32(morph.Vigor));
                            }

                            if (morph.Vigor == "")
                            {
                                command.Parameters.AddWithValue("@Mot", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Mot", Convert.ToInt32(morph.Mot));
                            }

                            if (morph.Vigor == "")
                            {
                                command.Parameters.AddWithValue("@Morph", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Morph", Convert.ToInt32(morph.Morphology));
                            }

                            if (morph.Vigor == "")
                            {
                                command.Parameters.AddWithValue("@Code", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Code", Convert.ToInt32(morph.Code));
                            }

                            if (morph.Vigor == "")
                            {
                                command.Parameters.AddWithValue("@Units", 0);
                            }
                            /*else
                            {
                                command.Parameters.AddWithValue("@Units", Convert.ToInt32(uxMorphUnits.Text));
                            } */

                            command.Parameters.AddWithValue("@ID", morph.Id);

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

        private void StoreParent()
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

                            command.Parameters.AddWithValue("@Valid", info.Valid.ToString().ToUpper());
                           /* command.Parameters.AddWithValue("@CanNum", uxCanNum.Text);
                            command.Parameters.AddWithValue("@AnimalID", uxCode.Text);
                            command.Parameters.AddWithValue("@CollDate", uxMorphDate.Text);
                            command.Parameters.AddWithValue("@NumUnits", uxMorphUnits.Text); */
                            command.Parameters.AddWithValue("@City", info.City);
                            command.Parameters.AddWithValue("@State", info.State);
                            command.Parameters.AddWithValue("@Country", info.Country);
                            //command.Parameters.AddWithValue("@Owner", uxOwner.Text);
                            //command.Parameters.AddWithValue("@AnimalName", uxAnimalName.Text);
                           // command.Parameters.AddWithValue("@Breed", uxBreed.Text);
                            command.Parameters.AddWithValue("@Species", info.Species);
                            //command.Parameters.AddWithValue("@RegNum", uxRegNum.Text);

                            connection.Open();
                            int k = command.ExecuteNonQuery();
                            connection.Close();
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

                            command.Parameters.AddWithValue("@SValid", info.Valid.ToString().ToUpper());
                           // command.Parameters.AddWithValue("@SCanNum", uxCanNum.Text);
                            command.Parameters.AddWithValue("@OldAnimalID", oldCode);
                           // command.Parameters.AddWithValue("@AAnimalID", uxCode.Text);
                            //command.Parameters.AddWithValue("@SCollDate", uxMorphDate.Text);
                            //command.Parameters.AddWithValue("@SNumUnits", uxMorphUnits.Text);
                            command.Parameters.AddWithValue("@PCity", info.City);
                            command.Parameters.AddWithValue("@oldQty", oldCity);
                            command.Parameters.AddWithValue("@PState", info.State);
                            command.Parameters.AddWithValue("@oldRate", oldState);
                            command.Parameters.AddWithValue("@PCountry", info.Country);
                            //command.Parameters.AddWithValue("@POwner", uxOwner.Text);
                            command.Parameters.AddWithValue("@oldDescription", oldOwner);
                            //command.Parameters.AddWithValue("@AAnimalName", uxAnimalName.Text);
                            //command.Parameters.AddWithValue("@ABreed", uxBreed.Text);
                            command.Parameters.AddWithValue("@ASpecies", info.Species);
                            //command.Parameters.AddWithValue("@ARegNum", uxRegNum.Text);
                            
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
                               reader.GetInt32(reader.GetOrdinal("Code")).ToString(),
                               reader.GetInt32(reader.GetOrdinal("Units")).ToString(), id);
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

        private void InventoryPage_Load(object sender, RoutedEventArgs e)
        {
            int textCount = 0;

            IEnumerable<TextBox> textBoxEnum = (IEnumerable<TextBox>)FindVisualChildren<TextBox>(this);
            List<TextBox> textBoxes = textBoxEnum.ToList<TextBox>();

            if (recordList != null)
            {
                foreach (Record r in recordList)
                {
                   /* textBoxes[textCount].Text = r.ToFrom;
                    textBoxes[textCount + ROW_SPACING].Text = r.Date;
                    textBoxes[textCount + (ROW_SPACING * 2)].Text = r.Rec;
                    textBoxes[textCount + (ROW_SPACING * 3)].Text = r.Ship;
                    textBoxes[textCount + (ROW_SPACING * 4)].Text = r.Balance; */

                    uxDescription.Text = r.ToFrom;
                    uxDescription.Text = r.Date;
                    uxDescription.Text = r.Ship;
                    uxDescription.Text = r.Balance;

                    textBoxes[textCount + (ROW_SPACING * 1)].Text = uxDescription.ToString();
                    /*textBoxes[textCount].Text = r.Item;
                    textBoxes[textCount + ROW_SPACING].Text = r.Description;
                    textBoxes[textCount + (ROW_SPACING * 2)].Text = r.Qty;
                    textBoxes[textCount + (ROW_SPACING * 3)].Text = r.Rate;
                    textBoxes[textCount + (ROW_SPACING * 4)].Text = r.Amount; */

                    textCount++;

                    if (textCount == 32)
                        textCount += 128;
                }
            }
           /* if (morph != null)
            {
                textBoxes[MORPH_ID].Text = morph.Date;
                textBoxes[MORPH_ID + 1].Text = morph.Vigor;
                textBoxes[MORPH_ID + 2].Text = morph.Mot;
                textBoxes[MORPH_ID + 3].Text = morph.Morphology;
                textBoxes[MORPH_ID + 4].Text = morph.Code;
                textBoxes[MORPH_ID + 5].Text = morph.Units;
            } */
           /* if (searchResult.Units != null)
            {
                uxMorphUnits.Text = searchResult.Units;
            }
            if (searchResult.CollDate != null)
            {
                uxMorphDate.Text = searchResult.CollDate;
            }*/
            isOldMorph = true;
        }

        private void MorphChanged(object sender, TextChangedEventArgs e)
        {
            isOldMorph = false;
        }

        private void UxNotesButton_Click(object sender, RoutedEventArgs e)
        {
            noteWindow = new NoteWindow(notes);
            noteWindow.Check += value => notes = value;
            noteWindow.ShowDialog();
            isOldMorph = false;
        }
        private void CollectAdditionalInfo()
        {
            if (newRecord == true)
                info = new AdditionalInfo();
            else
                info = new AdditionalInfo(searchResult.Species, searchResult.Town, searchResult.State, searchResult.Country, Convert.ToBoolean(searchResult.INV.ToLower()));
            infoWindow = new AdditionalInfoWindow(info);
            infoWindow.Check += value => info = value;
            infoWindow.ShowDialog();
        }
    }

}
