using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models;
using ScrapR.Models.TrvStart;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScrapR.Tests
{
    [TestClass]
    public class TrvStartTests
    {
        private void testTrip(string tripType)
        {
            Models.TrvStart.Scrapper scrapper = new Models.TrvStart.Scrapper();
            Query query = Query.GetSampleQuery(tripType);
            List<Itinerary> itineraries = scrapper.GetItineraries(query);
            Assert.IsNotNull(itineraries);
            Assert.AreNotEqual(itineraries.Count, 0);
            CollectionAssert.AllItemsAreNotNull(itineraries);
        }

        [TestMethod]
        [Description("Test GetItineraries for Sample One-Way Trip Search")]
        public void trvStartOneWayTest()
        {
            testTrip(Query.TripType.oneWay);
        }

        [TestMethod]
        [Description("Test GetItineraries for Sample Return Trip Search")]
        public void trvStartReturnTest()
        {
            testTrip(Query.TripType.returnTrip);
        }

        [TestMethod]
        [Description("Test GetItineraries for Sample Multi Trip Search")]
        public void trvStartMultiTest()
        {
            testTrip(Query.TripType.multiTrip);
        }
    }
}
