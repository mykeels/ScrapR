using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScrapR.Tests
{
    [TestClass]
    public class TrvPaddyTests
    {
        [TestMethod]
        public void testTrvPaddyPage()
        {
            Api.Execute("https://domestic.travelpaddy.com/flights/listing/?type=Roundtrip&destination_type=Domestic" + 
                "&from=Lagos, NG - Murtala Muhammed (LOS)&to=Abuja, NG - Nnamdi Azikiwe Intl (ABV)&departure_date=12%2F07%2F2016" + 
                "&return_date=12%2F10%2F2016&departure_time_of_day=&return_time_of_day=&cabin_class=economy&adults=1&children=0&infants=0").Success((browser) =>
                {
                    Console.WriteLine(browser.DocumentTitle);
                    browser.InjectScript(Resources.JSON);
                    browser.InjectScript(Resources.trvPaddy_getFlightsData);
                    string flightsData = browser.ExecuteScript<string>(null, "getFlightsData");
                    Console.WriteLine(flightsData);
                }).Error((ex) =>
                {
                    Console.WriteLine(ex.Message);
                }).Wait();
        }
    }
}
