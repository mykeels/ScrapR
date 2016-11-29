using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvFix
{
    public class Result
    {
        public string airline { get; set; }
        public string airlineCode { get; set; }
        public dynamic cabin { get; set; }
        public dynamic cacheIndex { get; set; }
        public string gds { get; set; }
        public bool hotelCombo { get; set; }
        public string salesCategory { get; set; }
        public dynamic ticketLocale { get; set; }
        public dynamic ticketPolicy { get; set; }
 
        public class ItineraryWSResponse
        {
            public string directionIndicator { get; set; }
            public class OriginDestination
            {
                public string arrivalDateTime { get; set; }
                public dynamic cabin { get; set; }
                public string departureDateTime { get; set; }
                public string destinationAirport { get; set; }
                public string destinationAirportCode { get; set; }
                public string duration { get; set; }

                public string marketingAirline { get; set; }
                public string marketingAirlingCode { get; set; }
                public bool multiAirline { get; set; }
                public int numberOfStops { get; set; }
                public dynamic operatingAirline { get; set; }
                public dynamic operatingAirlineCode { get; set; }
                public string originAirport { get; set; }
                public string originAirportCode { get; set; }
                public class FlightSegment
                {
                    public string airEquipType { get; set; }
                    public string airportCodeContext { get; set; }
                    public string arrivalAirport { get; set; }
                    public string arrivalAirportCode { get; set; }
                    public string arrivalDateTime { get; set; }
                    public dynamic arrivalTimeZone { get; set; }
                }
            }
        }
    }
}
