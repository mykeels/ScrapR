using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace ScrapR.Models.TrvBeta
{
    public class Scrapper : ScrapR.Models.Scrapper
    {
        public async Task<Dictionary<string, string>> GetFlightDataAsync(Query query, CancellationToken token)
        {
            return await this.ExecutePageAsync<Dictionary<string, string>>(query.GetHomeUrl(), token, (browser) =>
            {
                string scriptData = Resources.trvBeta_setFlightsData.ToString();
                string tripFn = "setOneTripData";
                string submitFn = "submitOneTripForm";
                if (query.tripType == (int)Query.TripType.OneWay) { }
                else if (query.tripType == (int)Query.TripType.Return)
                {
                    tripFn = "setRoundTripData";
                    submitFn = "submitRoundTripForm";
                }
                else
                {
                    tripFn = "setMultiTripData";
                    submitFn = "submitMultipleTripForm";
                }

                var args = new List<object>();
                args.Add(query.ToJson());

                browser.InjectScript(Resources.JSON);

                Dictionary<string, string> ret = new Dictionary<string, string>();
                ret.Add("finalQuery", browser.ExecuteScript<string>(scriptData, tripFn, args.ToArray()));
                ret.Add("urlLoc", browser.Url.ToString());

                string data = browser.ExecuteScript<string>(null, submitFn);
                Console.WriteLine(data);

                Console.WriteLine("Navigating ... Please wait");
                while (!browser.Url.ToString().Contains("/searchresult"))
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                Console.WriteLine(browser.Url.ToString() + " ... Please wait");
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(20);
                    Application.DoEvents();
                }
                ret.Add("newUrlLoc", browser.Url.ToString());

                Console.WriteLine("Now getting flight data ... Please wait");

                browser.InjectScript(Resources.JSON);
                string retData = browser.ExecuteScript<string>(scriptData, "getFlightsInfo", args.ToArray());

                ret.Add("flightsData", retData);
                
                return ret;
            });
        }

        public Routes GetFlightData(Query query = null)
        {
            var startDate = DateTime.Now;
            if (query == null) query = Models.TrvBeta.Query.GetSample();

            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);

            Console.WriteLine("Run TrvBeta");
            Console.WriteLine("Test Data: \n" + query.ToJson(true));

            Dictionary<string, string> result = RunTask<Dictionary<string, string>>(GetFlightDataAsync(query,
                cts.Token));

            var routes = new Routes();

            if (result != null)
            {
                if (!String.IsNullOrEmpty(result["flightsData"]))
                {
                    routes = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.TrvBeta.Routes>(result["flightsData"]).TrimAll();

                    Console.WriteLine(routes.ToJson(true));
                    Console.WriteLine("\n" + routes.Count + " Flights Found");
                }
                else
                {
                    Console.WriteLine("flightData is null ... Something must have gone wrong");
                }
            }
            else
            {
                Console.WriteLine("Value of [result] is null ... Something went wrong with the scrapper");
            }

            var endDate = DateTime.Now;
            Console.WriteLine("Time Taken: " + endDate.Subtract(startDate).TotalSeconds + " seconds");
            Console.WriteLine("========================================================= End of Tests for TrvBeta " +
                "====================================================================");

            return routes;
        }

        public static Scrapper Create()
        {
            return new Scrapper();
        }
    }
}
