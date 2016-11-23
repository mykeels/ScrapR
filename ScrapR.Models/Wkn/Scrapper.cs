using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace ScrapR.Models.Wkn
{
    public class Scrapper: ScrapR.Models.Scrapper
    {
        public async Task<string> GetFlightsJsonAsync(string url, CancellationToken token)
        {
            string scriptData = Resources.wkn_getFlightsData.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\\\"", "\"");
            return await this.GetScriptDataAsync(url, token, scriptData, "getFlightsData");
        }

        public async Task<Roots> GetFlightsDataAsync(string url, CancellationToken token)
        {
            string data = await GetFlightsJsonAsync(url, token);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Roots>(data);
        }

        public Roots GetFlightsData(string url = null)
        {
            var startDate = DateTime.Now;
            if (String.IsNullOrEmpty(url)) {
                url = Query.GetSampleQuery().ToString();
            }

            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);

            Console.WriteLine("Run Wakanow");

            Models.Wkn.Scrapper scrapper = new Models.Wkn.Scrapper();

            string newSearchLocation = scrapper.RunTask<string>(scrapper.GetLocationChangeAsync(url, cts.Token));

            Console.WriteLine("Running Tests for: \n" + Query.GetSampleQuery().ToJson(true));

            Console.WriteLine("Location: " + newSearchLocation);

            Models.Wkn.Roots flightData = scrapper.RunTask(scrapper.GetFlightsDataAsync(
                    newSearchLocation,
                    cts.Token));

            if (flightData != null)
            {
                Console.WriteLine(flightData.ToJson(true));
                Console.WriteLine(flightData.Count + "Flights Found");
            }
            var endDate = DateTime.Now;

            Console.WriteLine("Time Taken: " + endDate.Subtract(startDate).TotalSeconds + " seconds");
            Console.WriteLine("========================================================= End of Tests for Wkn " +
                "====================================================================");

            return flightData;
        }

        public Roots GetFlightsData(Query query)
        {
            return GetFlightsData(query.ToString());
        }
    }
}
