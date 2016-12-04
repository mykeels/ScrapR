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
        public void testOneWay()
        {
            testTrip(Query.TripType.OneWay);
        }

        [TestMethod]
        public void testReturnTrip()
        {
            testTrip(Query.TripType.Return);
        }

        [TestMethod]
        public void testMultiTrip()
        {
            testTrip(Query.TripType.Multi);
        }
    }
}
