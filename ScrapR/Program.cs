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
            runScrapper(getUserOption());
            Console.Read();
        }

        private static int getUserOption()
        {
            Console.WriteLine("1\t Wakanow");
            Console.WriteLine("2\t Trv Start");
            Console.WriteLine("3\t Trv Beta");
            Console.WriteLine("4\t Trv Fix");
            return Convert.ToInt32(Console.ReadLine());
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
                    //Console.WriteLine("This has not been implemented yet");
                    //Console.WriteLine(Models.TrvFix.Result.GetSampleData().ToJson(true));
                    runTrvFix();
                    break;
                default:
                    break;
            }
        }

        private static void runTrvFix()
        {
            Models.TrvFix.Scrapper scrapper = new Models.TrvFix.Scrapper();
            scrapper.GetResults();
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
