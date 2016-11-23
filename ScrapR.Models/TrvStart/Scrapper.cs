using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace ScrapR.Models.TrvStart
{
    public class Scrapper : ScrapR.Models.Scrapper
    {
        public async Task<List<Itinerary>> GetItinerariesAsync(Query query, CancellationToken token)
        {
            return await this.ExecutePageAsync<List<Itinerary>>(query.GetHomeUrl(), token, (browser) =>
            {
                string scriptData = Resources.trvStart_setFlightsData.ToString();

                browser.InjectScript(Resources.JSON);

                string location = browser.ExecuteScript<string>(scriptData, "setFlightData", (new object[] { query.ToJson() }));

                Console.WriteLine("Location after setFlightData: " + location);
                Console.WriteLine("Waiting for Location Change ... Please wait");
                while (!browser.Url.ToString().Contains("/search-results/"))
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
                Console.WriteLine("Location after polling: " + browser.Url.ToString());
                /*for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(50);
                    Application.DoEvents();
                }*/
                var itineraries = new List<Itinerary>();
                
                browser.InjectScript(Resources.JSON);
                var itinerariesJson = browser.ExecuteScript<string>(scriptData, "getAirlineScopeItineraries");
                if (!String.IsNullOrEmpty(itinerariesJson))
                {
                    itineraries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Itinerary>>(itinerariesJson);
                }

                return itineraries;
            });
        }

        public List<Itinerary> GetItineraries(Query query = null)
        {
            var startDate = DateTime.Now;
            if (query == null) query = Models.TrvStart.Query.GetSampleQuery();
            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);

            Console.WriteLine("Run TrvStart");
            Console.WriteLine("Test Data: \n" + query.ToJson(true));

            Models.TrvStart.Scrapper scrapper = new Models.TrvStart.Scrapper();

            var itineraries = scrapper.RunTask(scrapper.GetItinerariesAsync(query, cts.Token));
            Console.WriteLine(itineraries.Count + "Flights Found");
            Console.WriteLine(itineraries.ToJson(true));
            var endDate = DateTime.Now;
            Console.WriteLine("Time Taken: " + endDate.Subtract(startDate).TotalSeconds + " seconds");
            Console.WriteLine("========================================================= End of Tests for TrvStart " + 
                "====================================================================");
            return itineraries;
        }
    }
}
