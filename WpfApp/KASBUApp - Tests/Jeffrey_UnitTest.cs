using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using WpfApp;

namespace KASBUApp___Testing
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    class Jeffrey_UnitTest
    {
        public static MainWindow mainWindow;
        //public static RecordWindow recordWindow;
        //public static SearchWindow searchWindow;

        [SetUp]
        public void setUp()
        {
            //recordWindow = new RecordWindow();
            //searchWindow = new SearchWindow();
        }

        [TearDown]
        public void TearDown()
        {

        }

        /*
        [Test]
        public void Unit_Test_7()
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.UxModifyRecord_Click(null, null);
            Form form = null;
            foreach (Form f in Application.OpenForms)
            {
                if(f.Name == "searchWindow")
                {
                    form = f;
                    break;
                }
            }
            if (form == null)
            {
                Assert.Fail();
            }
            foreach (Control c in form.Controls)
            {
                if (c.Name == "uxSearch")
                {
                    (Button)(c).PerformClick();
                }
            }

        }
        */

    }
}
