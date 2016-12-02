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
        [TestMethod]
        public void testOneWay()
        {
            Models.Wkn.Scrapper scrapper = new Models.Wkn.Scrapper();
            Models.Wkn.Query query = Query.GetSampleQuery();
            query.trip = Query.TripType.OneWay;
            Roots roots = scrapper.GetFlightsData(query);
            Assert.IsNotNull(roots);
            Assert.AreNotEqual(roots.Count, 0);
            CollectionAssert.AllItemsAreNotNull(roots);
        }

        [TestMethod]
        public void testReturnTrip()
        {
            Models.Wkn.Scrapper scrapper = new Models.Wkn.Scrapper();
            Models.Wkn.Query query = Query.GetSampleQuery();
            query.trip = Query.TripType.Return;
            Roots roots = scrapper.GetFlightsData(query);
            Assert.IsNotNull(roots);
            Assert.AreNotEqual(roots.Count, 0);
            CollectionAssert.AllItemsAreNotNull(roots);
        }

        [TestMethod]
        public void testMultiTrip()
        {

        }
    }
}
