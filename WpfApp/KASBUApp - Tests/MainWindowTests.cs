﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WpfApp;

namespace KASBUApp___Tests
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class MainWindowTests
    {
        MainWindow mainWindow;

        [Test]
        public void RecordWindowOpensProperly()
        {
            mainWindow = new MainWindow();
        }
    }
}
