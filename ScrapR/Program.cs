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
        public static void Main(string[] args)
        {
            //Console.WriteLine(Models.TrvBeta.Query.GetSample().ToJson(true));
            //Console.Read();

            //runWkn();
            //runTrvBeta();
            runWkn();
            //Promise<object>.Create(() =>
            //{
            //    runTrvBeta();
            //    return null;
            //});
            //Promise<object>.Create(() =>
            //{
            //    runTrvStart();
            //    return null;
            //});
            //Promise<object>.Create(() =>
            //{
            //    runWkn();
            //    return null;
            //});

            Console.Read();
        }

        

        private static void runTrvBeta()
        {
            Models.TrvBeta.Scrapper scrapper = new Models.TrvBeta.Scrapper();
            scrapper.GetFlightData();
        }

        private static void runTrvStart()
        {
            Models.TrvStart.Scrapper scrapper = new Models.TrvStart.Scrapper();
            scrapper.GetItineraries();
        }

        private static void runWkn()
        {
            Models.Wkn.Scrapper scrapper = new Models.Wkn.Scrapper();
            scrapper.GetFlightsData();
        }
    }
}
