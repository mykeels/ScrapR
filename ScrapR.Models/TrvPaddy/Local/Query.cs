using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvPaddy.Local
{
    public class Query
    {
        public List<Trip> trips = new List<Trip>();
        public int adults = 1;
        public int children = 0;
        public int infants = 0;
        public string tripClass = Trip.Class.All;
        public string tripType = Trip.Type.OneWay;

        public class Trip
        {
            public string airportOrigin = "";
            public string airportDestination = "";
            public string departureDate = "";
            public string departureTime = "";
            public string returnDate = "";
            public string returnTime = "";

            public string getAirportOriginCode()
            {
                return Regex.Match(this.airportOrigin, @"\((.+?)\)").Groups[1].Value;
            }

            public string getAirportDestinationCode()
            {
                return Regex.Match(this.airportDestination, @"\((.+?)\)").Groups[1].Value;
            }

            public DateTime getDepartureDate()
            {
                return DateTime.Parse(this.departureDate);
            }
            public DateTime getReturnDate()
            {
                return DateTime.Parse(this.returnDate);
            }

            public class Class
            {
                public const string All = "all";
                public const string Economy = "economy";
                public const string Business = "business";
            }

            public class Type
            {
                public const string OneWay = "One Way";
                public const string Return = "Roundtrip";
            }

            public class TimeOfDay
            {
                public const string AnyTime = null;
                public const string Morning = "morning";
                public const string EarlyMorning = "early_morning";
                public const string MidDay = "midday";
                public const string Afternoon = "afternoon";
                public const string Evening = "evening";
                public const string Night = "night";

                public static string GetTimeOfDay(DateTime date)
                {
                    if (date == new DateTime() || date == default(DateTime) || date.Hour == 0) return AnyTime;
                    if (date.Hour < 8) return EarlyMorning;
                    else if (date.Hour < 11) return Morning;
                    else if (date.Hour < 12) return MidDay;
                    else if (date.Hour < 16) return Afternoon;
                    else if (date.Hour < 20) return Evening;
                    else return Night;
                }
            }
        }

        public override string ToString()
        {
            return "https://domestic.travelpaddy.com/flights/listing/?type=" + tripType + "&destination_type=Domestic" +
                "&from=" + trips.FirstOrDefault().airportOrigin + "&to=" + trips.FirstOrDefault().airportDestination +
                "&departure_date=" + trips.FirstOrDefault().departureDate?.Replace("/", "%2F") +
                "&return_date=" + trips.FirstOrDefault().returnDate?.Replace("/", "%2F") +
                "&departure_time_of_day=&return_time_of_day=&cabin_class=" + tripClass + "&adults=" + adults + "&children=" + children + "&infants=" + infants;
        }

        public string ToSearchUrl()
        {
            return "https://domestic.travelpaddy.com/ajax/flight-model.ajax.php?" + this.GetFlightSearchMetaData();
        }

        public string GetFlightSearchMetaData()
        {
            return "action=get_flight_search_results&return_as_json=1&" +
                $"type={tripType}&from={trips.FirstOrDefault().airportOrigin}&" +
                $"to={trips.FirstOrDefault().airportDestination}&departure_date={trips.FirstOrDefault().departureDate?.Replace("/", "%2F")}&" +
                $"return_date={trips.FirstOrDefault().returnDate?.Replace("/", "%2F")}&departure_time_of_day=&" +
                $"return_time_of_day=&cabin_class={tripClass}&adults={adults}&children={children}&infants={infants}";
        }

        public string GetFlightFaresMetaData()
        {
            string ret = "action=get_parallel_fare_details&return_as_json=true&" +
                $"type={tripType?.ToLower()}&from_code={trips.FirstOrDefault().getAirportOriginCode()}&" +
                $"to_code={trips.FirstOrDefault().getAirportDestinationCode()}&departure_day={trips.FirstOrDefault().getDepartureDate().Day}&" +
                $"departure_month={trips.FirstOrDefault().getDepartureDate().ToString("MMM")}&departure_year={trips.FirstOrDefault().getDepartureDate().Year}&" +
                $"adults={adults}&children={children}&infants={infants}";
            if (!String.IsNullOrEmpty(trips.FirstOrDefault().returnDate))
            {
                ret += "&" + $"return_day={trips.FirstOrDefault().getReturnDate().Day}&return_month={trips.FirstOrDefault().getReturnDate().ToString("MMM")}&return_year={trips.FirstOrDefault().getReturnDate().Year}";
            }
            return ret;
        }

        public static Query GetSampleData()
        {
            return Query.GetQuery(TrvPaddy.Query.GetSampleData());
        }

        private static string convertTripType(string parentTripType)
        {
            if (parentTripType == TrvPaddy.Query.Trip.Type.OneWay)
            {
                return Trip.Type.OneWay;
            }
            else
            {
                return Trip.Type.Return;
            }
        }

        public static Query GetQuery(TrvPaddy.Query baseQuery)
        {
            Query ret = new Query();
            ret.tripType = convertTripType(baseQuery.tripType);
            ret.tripClass = baseQuery.tripClass.ToLower();
            if (baseQuery.visitors != null)
            {
                ret.adults = baseQuery.visitors.adults;
                ret.children = baseQuery.visitors.children;
                ret.infants = baseQuery.visitors.infants;
            }
            if (baseQuery.trips != null)
            {
                ret.trips = new List<Trip>();
                baseQuery.trips.ForEach((trip) =>
                {
                    Trip retTrip = new Trip();
                    retTrip.airportDestination = $"{trip.destination.cityName}, {trip.destination.countryCode} - {trip.destination.airportName} ({trip.destination.cityCode})";
                    retTrip.airportOrigin = $"{trip.origin.cityName}, {trip.origin.countryCode} - {trip.origin.airportName} ({trip.origin.cityCode})";
                    retTrip.departureDate = trip.departureDate.ToString("MM/dd/yyyy");
                    retTrip.returnDate = ((trip.returnDate == new DateTime()) || (trip.returnDate == default(DateTime))) ? "" : trip.returnDate.ToString("MM/dd/yyyy");
                    retTrip.departureTime = Trip.TimeOfDay.GetTimeOfDay(trip.departureDate);
                    retTrip.returnTime = Trip.TimeOfDay.GetTimeOfDay(trip.returnDate);
                    ret.trips.Add(retTrip);
                });
            }
            return ret;
        }

        public static string GetHomeUrl()
        {
            return "https://domestic.travelpaddy.com";
        }
    }
}
