using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models;
using ScrapR.Models.Wkn;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ScrapR.Tests
{
    [TestClass]
    public class WknTests
    {
        private void testTrip(string tripType)
        {
            Models.Wkn.Scrapper scrapper = new Models.Wkn.Scrapper();
            Query query = Query.GetSampleQuery(tripType);
            Roots roots = scrapper.GetFlightsData(query);
            Assert.IsNotNull(roots);
            Assert.AreNotEqual(roots.Count, 0);
            CollectionAssert.AllItemsAreNotNull(roots);
        }

        [TestMethod]
        [Description("This simulates and tests a one-way trip via Wkn")]
        public void wknOneWayTest()
        {
            testTrip(Query.TripType.OneWay);
        }

        [TestMethod]
        [Description("This simulates and tests a return trip via Wkn")]
        public void wknReturnTripTest()
        {
            testTrip(Query.TripType.Return);
        }

        [TestMethod]
        [Description("This simulates and tests a multiple destination trip via Wkn")]
        public void wknMultiTripTest()
        {
            testTrip(Query.TripType.Multi);
        }
    }
}
