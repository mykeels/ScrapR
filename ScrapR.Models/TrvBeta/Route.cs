using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvBeta
{
    public class TrimHelper
    {
        public void TrimData()
        {
            var stringProperties = this.GetType().GetProperties()
                          .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(this, null);
                if (!String.IsNullOrEmpty(currentValue)) stringProperty.SetValue(this, currentValue.Trim(), null);
            }
        }
    }

    public class Routes : List<Route> 
    {
        public Routes TrimAll()
        {
            foreach (var route in this)
            {
                route.TrimData();
            }
            return this;
        }
    }

    public class Route: TrimHelper
    {
        public Airline airline { get; set; }
        public Trip[] trips { get; set; }
        public string name { get; set; }
        public Query query { get; set; }
        public string price { get; set; }
        public string detailsUrl { get; set; }
        public string totalDuration { get; set; }
        public string departureTime { get; set; }

        public Route GetTrimmedResult()
        {
            this.TrimData();
            if (this.airline != null) this.airline.TrimData();
            if (this.trips != null)
            {
                for (int i = 0; i < this.trips.Length; i++)
                {
                    this.trips[i].TrimData();
                    if (this.trips[i].details != null)
                    {
                        this.trips[i].details.TrimData();
                    }
                }
            }
            return this;
        }

        public class Airline : TrimHelper
        {
            public string imageUrl { get; set; }
            public string name { get; set; }
        }
        public class Trip : TrimHelper
        {
            public string name { get; set; }
            public string type { get; set; }
            public string departureTime { get; set; }
            public string arrivalTime { get; set; }
            public string duration { get; set; }
            public string departureAirport { get; set; }
            public string arrivalAirport { get; set; }
            public Details details { get; set; }
            public string layOver { get; set; }
            public string totalFlightTime { get; set; }
            public class Details : TrimHelper
            {
                public string airline { get; set; }
                public string serviceClass { get; set; }
                public string flightNumber { get; set; }
                public string airCraft { get; set; }
                public string departureTime { get; set; }
                public string arrivalTime { get; set; }
                public string totalTime { get; set; }
            }
        }
    }
}
