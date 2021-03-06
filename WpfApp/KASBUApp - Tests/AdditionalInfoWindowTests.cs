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
    public class AdditionalInfoWindowTests
    {
        AdditionalInfoWindow additionalInfoWindow;

        [Test]
        public void ShowAdditionalInfoWindow()
        {
            AdditionalInfo additionalInfo = new AdditionalInfo();
            additionalInfoWindow = new AdditionalInfoWindow(additionalInfo);
            additionalInfoWindow.Show();
            if (additionalInfoWindow.IsVisible == false)
            {
                Assert.Fail();
            }
            else
            {
                additionalInfoWindow.Hide();
                Assert.Pass();
            }
        }
    }
}
