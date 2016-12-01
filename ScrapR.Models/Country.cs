using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models
{
    public class Country
    {
        public string name { get; set; }
        public string code { get; set; }

        private static Dictionary<string, Country> countries { get; set; }

        public static Dictionary<string, Country> GetCountries()
        {
            if (countries != null) return countries;
            countries = new Dictionary<string, Country>();
            string[] countryLines = Resources.countries.Split('\n');
            countries = countryLines.ToList().Select((line) =>
            {
                string[] items = line.Split(',');
                Country country = new Country();
                country.code = items[2];
                country.name = items[0];
                return country;
            }).ToDictionary((country) => country.name, (country) => country);
            return countries;
        }
    }
}
