using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models;
using ScrapR.Models.TrvBeta;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScrapR.Tests
{
    [TestClass]
    public class TrvBetaTests
    {
        private void testTrip(Query.TripType tripType)
        {
            Models.TrvBeta.Scrapper scrapper = new Models.TrvBeta.Scrapper();
            Query query = Query.GetSample(tripType);
            Routes results = scrapper.GetFlightData(query);
            Assert.IsNotNull(results);
            Assert.AreNotEqual(results.Count, 0);
            CollectionAssert.AllItemsAreNotNull(results);
        }

        [TestMethod]
        [Description("Get Flight Results for TrvBeta One-Way Sample Search Data")]
        public void trvBetaOneWayTest()
        {
            testTrip(Query.TripType.OneWay);
        }

        [TestMethod]
        [Description("Get Flight Results for TrvBeta Return Trip Sample Search Data")]
        public void trvBetaReturnTest()
        {
            testTrip(Query.TripType.Return);
        }

        [TestMethod]
        [Description("Get Flight Results for TrvBeta Multiple Trip Sample Search Data")]
        public void trvBetaMultiTest()
        {
            testTrip(Query.TripType.Multi);
        }
    }
}
