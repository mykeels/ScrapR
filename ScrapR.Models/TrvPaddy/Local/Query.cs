﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string tripType = TrvPaddy.Query.Trip.Type.OneWay;

        public class Trip
        {
            public string airportOrigin = "";
            public string airportDestination = "";
            public string departureDate = "";
            public string departureTime = "";
            public string returnDate = "";
            public string returnTime = "";

            public class Class
            {
                public const string All = "all";
                public const string Economy = "economy";
                public const string Business = "business";
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

        public static Query GetQuery(TrvPaddy.Query baseQuery)
        {
            Query ret = new Query();
            ret.tripType = baseQuery.tripType;
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
                    retTrip.departureDate = trip.departureDate.ToString("ddd, dd MMM yyyy");
                    retTrip.returnDate = ((trip.returnDate == new DateTime()) || (trip.returnDate == default(DateTime))) ? "" : trip.returnDate.ToString("ddd, dd MMM yyyy");
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