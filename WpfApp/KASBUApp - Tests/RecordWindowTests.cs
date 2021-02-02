using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WpfApp;

namespace KASBUApp___Tests
{
    class RecordWindowTests
    {
        [Test]
        public void ShowRecordWindowTest()
        {
            RecordWindow recordWindow = new RecordWindow();
            recordWindow.Show();
            if (recordWindow.IsVisible == false)
            {
                Assert.Fail();
            }
            else
            {
                recordWindow.Hide();
                Assert.Pass();
            }
        }
    }
}
