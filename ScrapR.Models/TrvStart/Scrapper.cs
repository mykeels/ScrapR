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
                browser.InjectScript(Resources.jQuery);

                string searchData = browser.ExecuteScript<string>(scriptData, "setFlightData", (new object[] { query.ToJson() }));

                Console.WriteLine("\nSearchData after setFlightData: " + searchData);
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
            Console.WriteLine("\nTest Search Data: \n" + query.ToJson());

            Models.TrvStart.Scrapper scrapper = new Models.TrvStart.Scrapper();
            //var task = Task.Run(async () => { await scrapper.GetItinerariesAsync(query, cts.Token) });
            //task.Wait();

            var itineraries = scrapper.RunTask(scrapper.GetItinerariesAsync(query, cts.Token));
            Console.WriteLine(itineraries.Count + "Flight Itineraries Found");
            Console.WriteLine("\nResult Data:\t" + itineraries.ToJson(true));
            var endDate = DateTime.Now;
            Console.WriteLine("Time Taken: " + endDate.Subtract(startDate).TotalSeconds + " seconds");
            Console.WriteLine("========================================================= End of Tests for TrvStart " + 
                "====================================================================");
            return itineraries;
        }

        public static Scrapper Create()
        {
            return new Scrapper();
        }
    }
}
