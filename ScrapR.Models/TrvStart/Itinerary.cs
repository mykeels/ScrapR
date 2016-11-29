using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvStart
{
    public class Itinerary
    {
        public string airlineCode { get; set; }
        public string airlineImage { get; set; }
        public string airlineName { get; set; }
        public string arrivalTime { get; set; }
        public string cabinClass { get; set; }
        public int dayCount { get; set; }
        public string defaultAirlineImage { get; set; }
        public string departureTime { get; set; }
        public string destination { get; set; }
        public string origin { get; set; }
        public decimal amount { get; set; }
        public string currencyCode { get; set; }
        public int decimalPlaces { get; set; }
        public int id { get; set; }
        public decimal ppsAmount { get; set; }
        public Fare fareBreakdown { get; set; }
        public List<TripInfo> odoList { get; set; }
        public class Fare
        {
            public Unit adults { get; set; }
            public Unit children { get; set; }
            public Unit infants { get; set; }
            public decimal taxAmount { get; set; }
            public class Unit
            {
                public decimal baseFare { get; set; }
                public int qty { get; set; }
            }
        }
        public class TripInfo
        {
            public long duration { get; set; }
            public List<Segment> segments { get; set; }
            public class Segment
            {
                public int ID { get; set; }
                public string airlineCode { get; set; }
                public string arrivalDateTime { get; set; }
                public string cabinClass { get; set; }
                public string departureDateTime { get; set; }
                public string destCode { get; set; }
                public long duration { get; set; }
                public string flightNumber { get; set; }
                public dynamic opAirlineCode { get; set; }
                public string origCode { get; set; }
                public dynamic technicalStops { get; set; }
            }

        }
    }
}
