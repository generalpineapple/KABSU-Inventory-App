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
    /// Class for the Additional Info Window form that pops up when a record has been finished filling out.
    /// </summary>
    public partial class AdditionalInfoWindow : Window
    {
        public event Action<AdditionalInfo> Check;

        private AdditionalInfo info; //private object

        /// <summary>
        /// constructor that takes in any info given.
        /// </summary>
        /// <param name="info">an additionalInfo object that may or may not contain the four fields Species, City, State, and Country</param>
        public AdditionalInfoWindow(AdditionalInfo info)
        {
            this.info = info;
            InitializeComponent();
        }

        public AdditionalInfo AdditionalInfo
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// event handler for window closing
        /// Will save all fields that are not null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (uxSpeciesText.Text != null)
                info.Species = uxSpeciesText.Text;
            if (uxTownText.Text != null)
                info.Town = uxTownText.Text;
            if (uxStateText.Text != null)
                info.State = uxStateText.Text;
            if (uxCountryText.Text != null)
                info.Country = uxCountryText.Text;
           /* if (uxValidBox.SelectedItem != null && uxValidBox.SelectedItem == uxValidTrue)
                info.Valid = true;
            else if (uxValidBox.SelectedItem != null)
                info.Valid = false; */
            if (Check != null)
                Check(info);
        }

        /// <summary>
        /// event handler for opening window
        /// Will load in any existing info from the additionInfo class if availiable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UxInfoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            uxSpeciesText.Text = info.Species;
            uxTownText.Text = info.Town;
            uxStateText.Text = info.State;
            uxCountryText.Text = info.Country;
            /*
            if (info.Valid)
                uxValidBox.SelectedItem = uxValidTrue;
            else
                uxValidBox.SelectedItem = uxValidFalse;
            */
        }
    }
}
