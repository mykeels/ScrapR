using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models
{
    public class Airport
    {
        public string cityCode { get; set; }
        public string cityName { get; set; }
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string airportCode { get; set; }
        public string airportName { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        private static List<Airport> _airports;
        private static List<Airport> airports
        {
            get
            {
                if (_airports != null) return _airports;
                _airports = new List<Airport>();
                string[] airportLines = Resources.airports.Split('\n');
                _airports = airportLines.ToList().Select((line) =>
                {
                    string[] items = line.Split(',');
                    Airport airport = new Airport();
                    airport.airportCode = items[4];
                    airport.airportName = items[0];
                    airport.cityName = items[1];
                    airport.cityCode = items[3];
                    airport.countryName = items[2];
                    airport.latitude = Convert.ToDouble(items[5]);
                    airport.longitude = Convert.ToDouble(items[6]);
                    if (Country.GetCountries().ContainsKey(airport.countryName)) airport.countryCode = Country.GetCountries()[airport.countryName]?.code;
                    return airport;
                }).ToList();
                _airports.Sort(new Comparison<Airport>((Airport a, Airport b) => {
                    return a.airportCode.CompareTo(b.airportCode);
                }));
                return _airports;
            }
        }

        public static List<Airport> GetAirports()
        {
            return airports;
        }
    }
}

