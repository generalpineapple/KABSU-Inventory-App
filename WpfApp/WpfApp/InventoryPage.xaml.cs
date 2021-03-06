﻿/*
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
using System.Windows.Documents;
using System.IO;
using System.Windows.Xps.Packaging;

namespace WpfApp
{
    /// <summary>
    /// Class for Inventory Page window
    /// </summary>
    public partial class InventoryPage : Window
    {
        SearchResult searchResult; //object for SearchResult class
        private string notes;
        private string oldCode;
        private string oldOwner;
        private string oldTown;
        private string oldState;
        private AdditionalInfo info; //objecty for AdditionalInfo class
        private static int ID_INDEX = 321;
        private static int ROW_SPACING = 32;
        private static int MORPH_ID = 326;
        private List<InventoryRecord> inventoryrecordList; //list of Inventory Records
        private List<Record> recordList; //list of Records
        private Morph morph;
        private bool isMorph;
        private bool isOldMorph;
        private bool populating;
        private bool newRecord;
        private bool isOldRecord;
        private NoteWindow noteWindow; //object for Note Window class
        private AdditionalInfoWindow infoWindow; //object for Additional Info Window class
        List<SearchResult> searchList; //listt of Search Results

        /// <summary>
        /// constructor for class
        /// </summary>
        public InventoryPage()
        {
            newRecord = true;
            isOldRecord = false;
            searchResult = new SearchResult();
            InitializeComponent();
            notes = "";
            Closing += InventoryPage_Closing;
        }

        /// <summary>
        /// constructor when one row is selected from Search Results Window
        /// </summary>
        /// <param name="search"></param>
        public InventoryPage(SearchResult search)
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
            Closing += InventoryPage_Closing;
            recordList = RetrieveRecords(searchResult.Code);
            morph = RetrieveMorph(searchResult.Code);
        }

        /// <summary>
        /// Constructor when multiple rows are selected from Search Results Window
        /// </summary>
        /// <param name="search"></param>
        public InventoryPage(List<SearchResult> search)
        {
            newRecord = false;
            recordList = new List<Record>();
            searchList = new List<SearchResult>();
            foreach (SearchResult list in search)
            {
                searchResult = list;
                searchList.Add(searchResult);
                oldCode = list.Code;
                oldOwner = list.Owner;
                oldTown = list.Town;
                oldState = list.State;
                InitializeComponent();
                uxCode.Text = list.Code;
                uxBreed.Text = list.Breed;
                uxAnimalName.Text = list.AnimalName;
                uxRegNum.Text = list.RegNum;
                uxOwner.Text = list.Owner;
                uxCanNum.Text = list.CanNum;
                notes = "";
                isMorph = false;
                isOldMorph = false;
                populating = false;
                Closing += InventoryPage_Closing;
                recordList = RetrieveRecords(list.Code);
                morph = RetrieveMorph(list.Code);
            }
        }

        internal InventoryRecord InventoryRecord
        {
            get => default;
            set
            {
            }
        }

        public NoteWindow NoteWindow
        {
            get => default;
            set
            {
            }
        }

        public SearchResult SearchResult
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

        public AdditionalInfoWindow AdditionalInfoWindow
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
        public void InventoryPage_Closing(object sender, CancelEventArgs e)
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
                morph = new Morph(notes, list[MORPH_ID], list[MORPH_ID + 1], list[MORPH_ID + 2], list[MORPH_ID + 3], list[MORPH_ID + 4], Convert.ToInt32(list[MORPH_ID + 5]), list[ID_INDEX]);
            }
            StoreRecords();
            StoreMorph();
        }

        /// <summary>
        /// method for the grid in the window
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
        /// method to store all records into database.
        /// </summary>
        private void StoreRecords()
        {
            if (inventoryrecordList.Count == 0)
            {
                string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        /*using (var command = new MySqlCommand("kabsu.DeleteData", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@AnimalID", searchResult.Code);
                            connection.Open();
                            int k = command.ExecuteNonQuery();
                            connection.Close();
                        }*/
                        foreach (InventoryRecord r in inventoryrecordList)
                        {

                            using (var command = new MySqlCommand("kabsu.StoreInventoryData", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@Item", r.Item);
                                command.Parameters.AddWithValue("@Description", r.Description);
                                command.Parameters.AddWithValue("@Qty", Convert.ToInt32(r.Qty));
                                command.Parameters.AddWithValue("@WhatINeed", Convert.ToInt32(r.WhatINeed));
                                command.Parameters.AddWithValue("@Notes", Convert.ToInt32(r.Notes));
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

        /// <summary>
        /// method for the pop up window to save that information
        /// </summary>
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
                            command.Parameters.AddWithValue("@Date", uxMorphDate.Text);
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
                                command.Parameters.AddWithValue("@CollCode", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CollCode", Convert.ToInt32(morph.Code));
                            }

                            if (morph.Vigor == "")
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
        /// method storing the information into database as a new or updated record
        /// </summary>
        public void StoreParent()
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

                            if (!uxItemLeft1.Text.Equals("")) //need at least the cane code entered
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
                                MessageBox.Show("Enter cane code.");
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
                            if (uxWhatINeedLeft1.Text != "")
                            {
                                int qty = int.Parse(uxQtyLeft1.Text); //used temporary until I can get multiple selections for inventory page
                                int whatINeed = int.Parse(uxWhatINeedLeft1.Text); //used temporary until I can get multiple selections for inventory page
                                uxMorphUnits.Text = (qty - whatINeed).ToString(); //used temporary until I can get multiple selections for inventory page
                            }

                            //foreach(Grid.GetColumn tb in uxTopGrid1)
                            
                            command.CommandType = CommandType.StoredProcedure;

                            //command.Parameters.AddWithValue("@SValid", info.Valid.ToString().ToUpper());
                            command.Parameters.AddWithValue("@SCanNum", uxCanNum.Text);
                            command.Parameters.AddWithValue("@OldAnimalID", oldCode);
                            command.Parameters.AddWithValue("@AAnimalID", uxCode.Text);
                            command.Parameters.AddWithValue("@SCollDate", uxMorphDate.Text);
                            command.Parameters.AddWithValue("@SNumUnits", uxMorphUnits.Text);
                            //command.Parameters.AddWithValue("@SNumUnits", uxQtyLeft1.Text);
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


                        //recordList = new List<Record>();
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

        /*private List<Record> RetrieveRecords(List<SearchResult> searchList)
        {
            string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
            try
            {
                foreach (SearchResult sr in searchList)
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        using (var command = new MySqlCommand("kabsu.RetrieveRecords", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@AnimalID", id);

                            connection.Open();
                            var reader = command.ExecuteReader();


                            //recordList = new List<Record>();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to database05.");
                return new List<Record>();
            }
        }*/

        /// <summary>
        /// method to pull in a record's additional information from database
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
        /// event handler when loading the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InventoryPage_Load(object sender, RoutedEventArgs e)
        {
            int textCount = 0;

            IEnumerable<TextBox> textBoxEnum = (IEnumerable<TextBox>)FindVisualChildren<TextBox>(this);
            List<TextBox> textBoxes = textBoxEnum.ToList<TextBox>();

            if (recordList != null)
            {
                foreach (SearchResult sr in searchList)
                {
                    /* textBoxes[textCount].Text = r.ToFrom;
                     textBoxes[textCount + ROW_SPACING].Text = r.Date;
                     textBoxes[textCount + (ROW_SPACING * 2)].Text = r.Rec;
                     textBoxes[textCount + (ROW_SPACING * 3)].Text = r.Ship;
                     textBoxes[textCount + (ROW_SPACING * 4)].Text = r.Balance; */

                    //textBoxes[textCount].Text += "Cane code: " + uxCode.Text; //Can code for the item column..find a way to get the cane code for each record
                    textBoxes[textCount].Text += "Can Number: " + sr.CanNum;

                    //textBoxes[textCount + ROW_SPACING].Text += "Animal Name: " + r.AnimalId; //for right now, can only get id with record
                    textBoxes[textCount + ROW_SPACING].Text += "Animal Name: " + sr.AnimalName; //for right now, can only get id with record


                    textBoxes[textCount + ROW_SPACING].Text += "\nColl Date: " + sr.CollDate;
                    textBoxes[textCount + ROW_SPACING].Text += "\nOwner: " + sr.Owner;

                    textBoxes[textCount + ROW_SPACING * 2].Text = sr.Units.ToString(); //qty column

                    textCount++;

                    //if (textCount == 32)
                      //  textCount += 128;
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
                //uxMorphUnits.Text = textBoxes[textCount + ROW_SPACING * 2].Text;
            }
            if (searchResult.CollDate != null)
            {
                uxMorphDate.Text = searchResult.CollDate;
            }
            isOldMorph = true;
        }

        /// <summary>
        /// event handler to update the Morph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MorphChanged(object sender, TextChangedEventArgs e)
        {
            isOldMorph = false;
        }

        /// <summary>
        /// event handler when notes button is clicked
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

        private void UxPrintButton_Click(object sender, RoutedEventArgs e)
        {
            //PrintDialog dialog = new PrintDialog();
            //dialog.PrintVisual(this, "Window Printing");
            // Create the print dialog object and set options
            PrintDialog pDialog = new PrintDialog();
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;

            // Display the dialog. This returns true if the user presses the Print button.
            Nullable<Boolean> print = pDialog.ShowDialog();
            if (print == true)
            {
                XpsDocument xpsDocument = new XpsDocument("C:\\FixedDocumentSequence.xps", FileAccess.ReadWrite);
                FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
                pDialog.PrintDocument(fixedDocSeq.DocumentPaginator, "Test print job");
            }
        }

        /// <summary>
        /// method to collect additional information uploading the new window
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
