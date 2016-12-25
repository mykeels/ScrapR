using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace ScrapR.Models.TrvPaddy.Local
{
    public class Scrapper : ScrapR.Models.Scrapper
    {
        public FlightsResponse GetFlightsData(Query query)
        {
            var flights = new List<Flight>();
            var startDate = DateTime.Now;
            var response = Api.Get<FlightsResponse>(query.ToSearchUrl());
            return response;
        }

        public List<Flight> GetFlights(Query query)
        {
            return this.GetFlightsData(query).GetFlights();
        }

        public List<Flight> GetFlightsWithFareDetails(Query query)
        {
            int i = 0;
            var response = GetFlightsData(query);
            var fares = GetFlightsFareInfo(query, response);
            var parallelFlights = response.GetParallelFlights();
            var flights = response.GetFlights();

            flights.ForEach((flight) =>
            {
                flight.fareData = fares[i];
                i++;
            });

            return flights;
        }

        public List<Trip.FareData.Fare> GetFlightsFareInfo(Query query, FlightsResponse flights)
        {
            var faresResponses = Api.Post<Dictionary<string, Trip.FareData.Fare>>("https://domestic.travelpaddy.com/ajax/flight-model.ajax.php",
                query.GetFlightFaresMetaData() + "&encoded_flights_data=" + flights.GetParallelFlights().ToJson(), "application/x-www-form-urlencoded");
            return faresResponses.Values.ToList();
        }

        //public async Task<List<Flight>> GetFlightsDataAsync(Query query, CancellationToken token)
        //{
        //    return await this.ExecutePageAsync<List<Flight>>(query.ToString(), token, (browser) =>
        //    {
        //        //browser.InjectScript(Resources.JSON);
        //        //browser.InjectScript(Resources.trvPaddy_getFlightsData);
        //        string flightResults = browser.ExecuteScript<string>(null, "getFlightsData");
        //        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Flight>>(flightResults);
        //    });
        //}

        //public List<Flight> GetFlightsData(Query query = null)
        //{
        //    var startDate = DateTime.Now;
        //    if (query == null) query = Query.GetSampleData();

        //    var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);

        //    Console.WriteLine("Run TrvPaddy");
        //    Console.WriteLine("Test Data: \n" + query.ToJson(true));

        //    List<Flight> flightResults = RunTask<List<Flight>>(GetFlightsDataAsync(query, cts.Token));
        //    if (flightResults != null)
        //    {
        //        Console.WriteLine(flightResults.ToJson(true));
        //        Console.WriteLine("\n" + flightResults.Count + " Flights Found");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Flight Result is null. That's not good ... Something must have gone wrong");
        //    }


        //    var endDate = DateTime.Now;
        //    Console.WriteLine("Time Taken: " + endDate.Subtract(startDate).TotalSeconds + " seconds");
        //    Console.WriteLine("========================================================= End of Tests for TrvPaddy Local " +
        //        "====================================================================");
        //    return flightResults;
        //}

        public static Scrapper Create()
        {
            return new Scrapper();
        }


    }
}
