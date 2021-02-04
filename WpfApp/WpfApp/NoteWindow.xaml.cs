/*
 Insert Copyright and Licensing Information
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
    /// class for the Note Window form
    /// </summary>
    public partial class NoteWindow : Window
    {
        public event Action<string> Check;
        private string notes;

        /// <summary>
        /// constructor
        /// </summary>
        public NoteWindow()
        {
            notes = "";
            InitializeComponent();
        }

        /// <summary>
        /// constructor when notes are filled out
        /// </summary>
        /// <param name="notes"></param>
        public NoteWindow(string notes)
        {
            this.notes = notes;
            InitializeComponent();
        }

        /// <summary>
        /// event handler when closing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoteWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Check != null)
                Check(uxNotesText.Text);
        }

        /// <summary>
        /// event handler when opening window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoteWindow_Loaded(object sender, RoutedEventArgs e)
        {
            uxNotesText.Text = notes;
        }
    }
}
