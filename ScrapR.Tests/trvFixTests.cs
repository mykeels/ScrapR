using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models;
using ScrapR.Models.TrvFix;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScrapR.Tests
{
    [TestClass]
    public class TrvFixTests
    {
        private void testTrip(string tripType)
        {
            Models.TrvFix.Scrapper scrapper = new Models.TrvFix.Scrapper();
            Query query = Query.GetSampleData(tripType);
            List<Result> results = scrapper.GetResults(query);
            Assert.IsNotNull(results);
            Assert.AreNotEqual(results.Count, 0);
            CollectionAssert.AllItemsAreNotNull(results);
        }

        [TestMethod]
        [Description("Get Flight Results for One-Way Sample Search Data")]
        public void trvFixOneWayTest()
        {
            testTrip(Query.TripType.oneWay);
        }

        [TestMethod]
        [Description("Get Flight Results for Return Trip Sample Search Data")]
        public void trvFixReturnTest()
        {
            testTrip(Query.TripType.returnTrip);
        }

        [TestMethod]
        [Description("Get Flight Results for Multiple Trip Sample Search Data")]
        public void trvFixMultiTest()
        {
            testTrip(Query.TripType.multi);
        }
    }
}
