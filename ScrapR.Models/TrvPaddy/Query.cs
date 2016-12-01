using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvPaddy
{
    public class Query
    {
        public List<Trip> trips { get; set; }
        public Visitors visitors { get; set; }
        public string tripClass { get; set; }
        public string tripType {
            get
            {
                if (trips == null || trips.Count == 0) return Trip.Type.OneWay;
                else if (trips.Count == 1)
                {
                    if (trips[0].returnDate == default(DateTime) || trips[0].returnDate == (new DateTime())) return Trip.Type.OneWay;
                    else return Trip.Type.Return;
                }
                else
                {
                    return Trip.Type.Multi;
                }
            }
        }
        public FlightType flightType = FlightType.None;
        public FlightType GetFlightType()
        {
            if (trips == null || trips.Count == 0) throw new NullReferenceException("Trips should not be null or empty");
            else
            {
                if (flightType != FlightType.None) return this.flightType;
                var airportFrom = Airport.GetAirports().Where((port) => port.airportCode.ToLower().Equals(trips[0].destination.airportCode.ToLower())).FirstOrDefault();
                var airportTo = Airport.GetAirports().Where((port) => port.airportCode.ToLower().Equals(trips[0].origin.airportCode.ToLower())).FirstOrDefault();
                if (airportFrom.countryName == airportTo.countryName)
                {
                    this.flightType = FlightType.Local;
                }
                else
                {
                    this.flightType = FlightType.International;
                }
                return this.flightType;
            }
        }

        public static Query GetSampleData()
        {
            Query ret = new Query();
            ret.visitors = new Visitors()
            {
                adults = 1,
                children = 0,
                infants = 0
            };
            ret.tripClass = Trip.Class.Economy;
            ret.trips = (new Trip[]
            {
                new Trip()
                {
                    departureDate = DateTime.Today.AddDays(3),
                    returnDate = DateTime.Today.AddDays(6),
                    destination = new Trip.Airport("DNAA"), //muritala lagos
                    origin = new Trip.Airport("DNMM") //nnamdi abuja
                },
                new Trip()
                {
                    departureDate = DateTime.Today.AddDays(9),
                    destination = new Trip.Airport("EGLL"), //nnamdi abuja
                    origin = new Trip.Airport("DNAA") //heathrow london
                }
            }).ToList();
            ret.GetFlightType();
            return ret;
        }


        public enum FlightType
        {
            None,
            Local,
            International
        }
        
        public class Trip
        {
            public class Class
            {
                public const string Economy = "Economy";
                public const string PremiumEconomy = "Premium Economy";
                public const string Business = "Business";
                public const string FirstClass = "First";
            }
            public class Type
            {
                public const string OneWay = "oneWay";
                public const string Return = "return";
                public const string Multi = "multi";
            }
            public Airport origin { get; set; }
            public Airport destination { get; set; }
            public DateTime departureDate { get; set; }
            public DateTime returnDate { get; set; }

            public class Airport
            {
                private string _airportCode;
                public string airportCode {
                    get
                    {
                        return _airportCode;
                    }
                    set
                    {
                        _airportCode = value;
                        var airportData = Models.Airport.GetAirports().Where((airport) => airport.airportCode.ToLower().Equals(value?.ToLower())).FirstOrDefault();
                        airportName = airportData.airportName;
                        if (String.IsNullOrEmpty(cityCode)) cityCode = airportData.cityCode;
                        if (String.IsNullOrEmpty(countryName)) countryName = airportData.countryName;
                        if (String.IsNullOrEmpty(cityName)) cityName = airportData.cityName;
                        if (String.IsNullOrEmpty(countryCode)) countryCode = airportData.countryCode;
                    }
                }
                public string airportName;
                public string cityCode { get; set; }
                public string countryCode { get; set; }
                public string cityName { get; set; }
                public string countryName { get; set; }

                public Airport() { }

                public Airport(Models.Airport airport)
                {
                    this.airportCode = airport.airportCode;
                }

                public Airport(string airportCode)
                {
                    this.airportCode = airportCode;
                }
            }
        }

        public class Visitors
        {
            public int adults { get; set; }
            public int children { get; set; }
            public int infants { get; set; }
        }
    }
}
