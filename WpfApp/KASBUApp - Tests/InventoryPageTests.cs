using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WpfApp;

namespace KASBUApp___Tests
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class InventoryPageTests
    {
        //Make test for error pop up when user doesn't enter can code.
        [Test]
        private void EmptyRecordError()
        {

        }


    }
}
