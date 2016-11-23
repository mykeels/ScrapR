using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvStart
{
    public class Query
    {
        public Locale locale { get; set; }
        public Travellers travellers { get; set; }
        public string tripType { get; set; }
        public List<Itinerary> itineraries { get; set; }
        public class TripType
        {
            public const string oneWay = "oneway";
            public const string returnTrip = "return";
            public const string multiTrip = "multiTrip";
        }
        public class Locale
        {
            public string country { get; set; }
            public string currentLocale { get; set; }
        }
        public class Travellers
        {
            public int adults { get; set; }
            public int children { get; set; }
            public int infants { get; set; }
        }
        public class Itinerary
        {
            public string departDate { get; set; }
            public string id { get; set; }
            public string returnDate { get; set; }
            public Location destination { get; set; }
            public Location origin { get; set; }
            public class Location
            {
                public string display { get; set; }
                public Detail value { get; set; }
                public class Detail
                {
                    public string airport { get; set; }
                    public string city { get; set; }
                    public string code { get; set; }
                    public string country { get; set; }
                    public string countryIata { get; set; }
                    public string iata { get; set; }
                    public string locationId { get; set; }
                    public string type { get; set; }
                }
            }
        }

        public string GetHomeUrl()
        {
            return "https://travelstart.com.ng";
        }

        public static Query GetSampleQuery()
        {
            Query query = new Query();
            query = Newtonsoft.Json.JsonConvert.DeserializeObject<Query>(Resources.trvStart_SampleData);
            query.tripType = TripType.oneWay;

            int dayAdd = 3;
            query.itineraries.ForEach((itinerary) =>
            {
                itinerary.departDate = DateTime.Now.AddDays(dayAdd).ToString("yyyy-MM-dd");
                itinerary.returnDate = DateTime.Now.AddDays(dayAdd + 2).ToString("yyyy-MM-dd");
                dayAdd += 3;
            });
            return query;
        }
    }
}
