using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp;

namespace KASBUApp___Testing
{
    [TestFixture]
    public class SmokeTest
    {

        public static MainWindow mainWindow;
        public static RecordWindow recordWindow;
        public static SearchWindow searchWindow;

        [SetUp, STAThread]
        public void Setup()
        {
            mainWindow = new MainWindow();
            recordWindow = new RecordWindow();
            searchWindow = new SearchWindow();
        }

        [Test, STAThread]
        public void InitalizeMainWindowTest()
        {
            //Setup();
            Assert.NotNull(mainWindow);
            Assert.IsInstanceOf( System.Type.GetType("Window"), mainWindow);
        }
        
        [Test, STAThread]
        public void InitialRecordWindowTest()
        {
            //Setup();
            recordWindow = new RecordWindow();
            Assert.NotNull(recordWindow);
        }

        /*
        public void ShowRecordWindowTest()
        {
            recordWindow = new RecordWindow();
            recordWindow.Show();
  
        }
        */
    }
}
