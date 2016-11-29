using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvBeta
{
    public class Query
    {
        public class Trip
        {
            public int arrivalAirportId { get; set; }
            public int departureAirportId { get; set; }
            public string departingDate { get; set; }
        }
        public string arrivalAirportCode { get; set; }
        public int arrivalAirportId { get; set; }
        public string departureAirportCode { get; set; }
        public int departureAirportId { get; set; }
        public List<Trip> multiTrip { get; set; }
        public int adult { get; set; }
        public int children { get; set; }
        public int infant { get; set; }
        public string departingDate { get; set; }
        public string returningDate { get; set; }
        public string flightClass { get; set; }
        public int tripType { get; set; } //one-way = 1, round-trip = 2, multi-trip = 3

        public class TripType
        {
            public const int OneWay = 1;
            public const int Return = 2;
            public const int Multi = 3;
        }

        public new string ToString()
        {
            return "https://www.travelbeta.com/flight/flightsearchresult?arrivalAirportCode=" + arrivalAirportCode + 
                "&departureAirportCode=" + departureAirportCode + "&" +
            "adult=" + adult + "&children=" + children + "&departingDate=" + departingDate + "&returningDate=" + returningDate + 
            "&flightClass=" + flightClass + "&tripType=" + tripType;
        }

        public string GetHomeUrl()
        {
            return "https://www.travelbeta.com";
        }

        public static Query GetSample()
        {
            return new Models.TrvBeta.Query()
            {
                adult = 1,
                departureAirportId = 35248, //lagos nigeria
                arrivalAirportId = 34277, //jfk, new york
                departingDate = DateTime.Now.AddDays(4).ToString("dd/MM/yyyy"),
                returningDate = DateTime.Now.AddDays(8).ToString("dd/MM/yyyy"),
                flightClass = "economy",
                tripType = TripType.Multi,
                multiTrip = (new Models.TrvBeta.Query.Trip[] {
                    new Models.TrvBeta.Query.Trip() {
                        arrivalAirportId = 34277, //jfk
                        departureAirportId = 35248, //lagos
                        departingDate = DateTime.Now.AddDays(4).ToString("dd/MM/yyyy")
                    },
                    new Models.TrvBeta.Query.Trip() {
                        arrivalAirportId = 34979, //los angeles
                        departureAirportId = 34277, //jfk
                        departingDate = DateTime.Now.AddDays(8).ToString("dd/MM/yyyy")
                    }
                }).ToList()
            };
        }
    }
}
