using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models;
using System.Windows.Forms;
using mshtml;

namespace ScrapR
{
    public class Program
    {
        private enum TripType
        {
            OneWay = 1,
            Return = 2,
            Multi = 3
        }

        public static void Main(string[] args)
        {
            runScrapper(getUserOption());
            Console.Read();
        }

        private static int getUserOption()
        {
            //Console.WriteLine(DateTime.Now.ToString("ddd, dd MMM yyyy"));
            Console.WriteLine("Choose Trv System");
            Console.WriteLine("1\t Wkn");
            Console.WriteLine("2\t Trv Start");
            Console.WriteLine("3\t Trv Beta");
            Console.WriteLine("4\t Trv Fix");
            Console.WriteLine("5\t Trv Paddy");
            return Convert.ToInt32(Console.ReadLine());
        }

        private static TripType getTripType()
        {
            Console.WriteLine("Choose Trip Type");
            Console.WriteLine("1\t One Way");
            Console.WriteLine("2\t Return");
            Console.WriteLine("3\t Multi");
            return (TripType)Convert.ToInt32(Console.ReadLine());
        }

        private static void runScrapper(int option)
        {
            switch (option)
            {
                case 1:
                    runWkn();
                    break;
                case 2:
                    runTrvStart();
                    break;
                case 3:
                    runTrvBeta();
                    break;
                case 4:
                    runTrvFix();
                    break;
                case 5:
                    runTrvPaddy();
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }

        private static void runTrvPaddy()
        {
            var baseQuery = Models.TrvPaddy.Query.GetSampleData();
            Console.WriteLine("Base Query: " + baseQuery.ToJson(true));
            Console.WriteLine("Local or International Flight?");
            Console.WriteLine("1\tLocal");
            Console.WriteLine("2\tInternational");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    var localQuery = Models.TrvPaddy.Local.Query.GetQuery(baseQuery);
                    Promise<string>.Create(() =>
                    {
                        Clipboard.SetText(localQuery.ToJson());
                        return null;
                    });
                    Console.WriteLine("Local Query: " + localQuery.ToJson(true));
                    Console.WriteLine("Search Url: " + localQuery.ToString());
                    Models.TrvPaddy.Local.Scrapper scrapper = new Models.TrvPaddy.Local.Scrapper();
                    scrapper.GetFlightsData(localQuery);
                    break;
                case 2:
                    var internationalQuery = Models.TrvPaddy.International.Query.GetQuery(baseQuery);
                    Promise<string>.Create(() =>
                    {
                        Clipboard.SetText(internationalQuery.ToJson());
                        return null;
                    });
                    Console.WriteLine("International Query: " + internationalQuery.ToJson(true));
                    Console.WriteLine("Search Url: " + internationalQuery.ToString());
                    break;
                default:
                    break;
            }
        }

        private static void runTrvFix()
        {
            var tripType = getTripType();
            Models.TrvFix.Scrapper scrapper = new Models.TrvFix.Scrapper();
            scrapper.GetResults(Models.TrvFix.Query.GetSampleData(Models.TrvFix.Query.TripType.GetTripType((int)tripType)));
        }

        private static void runTrvBeta()
        {
            var tripType = getTripType();
            Models.TrvBeta.Scrapper scrapper = new Models.TrvBeta.Scrapper();
            scrapper.GetFlightData(Models.TrvBeta.Query.GetSample((Models.TrvBeta.Query.TripType)(int)tripType));
        }

        private static void runTrvStart()
        {
            var tripType = getTripType();
            Models.TrvStart.Scrapper scrapper = new Models.TrvStart.Scrapper();
            scrapper.GetItineraries(Models.TrvStart.Query.GetSampleQuery(Models.TrvStart.Query.TripType.GetTripType((int)tripType)));
        }

        private static void runWkn()
        {
            var tripType = getTripType();
            Models.Wkn.Scrapper scrapper = new Models.Wkn.Scrapper();
            scrapper.GetFlightsData(Models.Wkn.Query.GetSampleQuery(Models.Wkn.Query.TripType.GetTripType((int)tripType)));
        }
    }
}
