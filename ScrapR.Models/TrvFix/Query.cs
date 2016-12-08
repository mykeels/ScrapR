using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvFix
{
    public class Query
    {
        public AirportDisplay arrivalAirport { get; set; }
        public Airport.Options airportOptions { get; set; }
        public string arrivalDate { get; set; }
        public string cabinClass { get; set; }
        public string currentTripType { get; set; }
        public TripData data { get; set; }
        public string defaultTripType { get; set; }
        public AirportDisplay departureAirport { get; set; }
        public string departureDate { get; set; }
        public bool flexibleDate { get; set; }
        public bool isMultiCity { get; set; }
        public bool isReturnTrip { get; set; }
        public bool isTrue { get; set; }
        public string minimumDate { get; set; }
        public MultiCity multiCity { get; set; }
        public string numberOfPassengersDescription { get; set; }
        public string preferredAirlineCode { get; set; }
        public string tripType { get; set; }
        public class Airport
        {
            public string city { get; set; }
            public string code { get; set; }
            public string country { get; set; }
            public string countryCode { get; set; }
            public string name { get; set; }

            public class Option
            {

            }
            public class Options : List<Option>
            {

            }
        }
        public class AirportDisplay
        {
            public Airport airport { get; set; }
            public Airport.Options airportOptions { get; set; }
            public string display { get; set; }
        }
        public class MultiCity
        {
            public List<Trip> items { get; set; }
            public class Trip
            {
                public AirportDisplay arrivalAirport { get; set; }
                public AirportDisplay departureAirport { get; set; }
                public string departureDate { get; set; }
            }
        }
        public class TripData
        {
            public string cabinClass { get; set; }
            public bool flexibleDate { get; set; }
            public List<OriginDestinationRequest> originDestinationRequests { get; set; }
            public PassengerInfo passengerTypes { get; set; }

            public string preferredAirlineCode { get; set; }
            public string ticketLocale { get; set; }
            public string ticketPolicy { get; set; }
            public string tripType { get; set; }
            public class OriginDestinationRequest
            {
                public string departureDateTime { get; set; }
                public string destination { get; set; }
                public string origin { get; set; }
                public int rph { get; set; }
            }
            public class PassengerInfo
            {
                public int numberOfAdult { get; set; }
                public int numberOfChildren { get; set; }
                public int numberOfInfant { get; set; }
            }
        }
        public class TripType
        {
            public const string oneWay = "OneWay";
            public const string returnTrip = "Return";
            public const string multi = "Circle";

            public static string GetTripType(int type)
            {
                if (type == 1) return oneWay;
                else if (type == 2) return returnTrip;
                else return multi;
            }
        }
        public class CabinClass
        {
            public const string Economy = "Y";
            public const string Premium = "S";
            public const string Business = "C";
            public const string First = "F";
        }

        public static Query GetSampleData(string tripType)
        {
            var query = Newtonsoft.Json.JsonConvert.DeserializeObject<Query>(tripType == TripType.oneWay ? Resources.trvFix_sampleOneWayData : (tripType == TripType.returnTrip ? Resources.trvFix_sampleReturnData : Resources.trvFix_sampleMultiData));
            var d1 = DateTime.Now.AddDays(2);
            query.departureDate = d1.ToString("yyyy-MM-dd");
            query.arrivalDate = d1.AddDays(2).ToString("yyyy-MM-dd");
            query.minimumDate = d1.ToString("yyyy-MM-dd");
            query.multiCity?.items?.ForEach((item) =>
            {
                item.departureDate = d1.ToString("yyyy-MM-dd");
                d1 = d1.AddDays(2);
            });
            return query;
        }

        public static string GetHomeUrl()
        {
            return "http://travelfix.com/";
        }
    }
}
