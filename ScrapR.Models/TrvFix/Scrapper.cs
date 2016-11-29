using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace ScrapR.Models.TrvFix
{
    public class Scrapper : ScrapR.Models.Scrapper
    {

        public async Task<List<Result>> GetResultsAsync(Query query, CancellationToken token)
        {
            return await this.ExecutePageAsync<List<Result>>(Query.GetHomeUrl(), token, (browser) =>
            {
                string scriptData = Resources.trvFix_setFlightsData;
                browser.InjectScript(Resources.JSON);
                browser.InjectScript(Resources.jQuery);

                string searchData = browser.ExecuteScript<string>(scriptData, "setFlightData", (new object[] { query.ToJson() }));

                Console.WriteLine("\nSearchData after setFlightData: " + searchData);
                Console.WriteLine("Waiting for Location Change ... Please wait");
                while (!browser.Url.ToString().Contains("flight/r"))
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                Console.WriteLine("Location after polling: " + browser.Url.ToString());

                browser.InjectScript(Resources.JSON);
                int batch = 0;
                string resultsJson = "";
                var results = new List<Result>();
                while (resultsJson != "[]" && resultsJson != null)
                {
                    resultsJson = browser.ExecuteScript<string>(scriptData, "getFlightResultsData", new object[] { batch });
                    results.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<Result>>(resultsJson));
                    batch++;
                }
                return results;
            });
        }

        public List<Result> GetResults(Query query = null)
        {
            var startDate = DateTime.Now;
            if (query == null) query = Query.GetSampleData(Query.TripType.multi);
            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);

            Console.WriteLine("Run TrvFix");
            Console.WriteLine("\nTest Search Data: \n" + query.ToJson());

            Scrapper scrapper = new Scrapper();

            var itineraries = scrapper.RunTask(scrapper.GetResultsAsync(query, cts.Token));
            Console.WriteLine("\nResult Data:\t" + itineraries.ToJson(true));
            var endDate = DateTime.Now;
            Console.WriteLine(itineraries.Count + " Flight Itineraries Found");
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
