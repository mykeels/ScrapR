using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvPaddy.Local
{
    public class Trip
    {
        public class FareData
        {
            public Fare departureTrip { get; set; }
            public Fare returnTrip { get; set; }
            public class Fare
            {
                public Dictionary<int, Passenger> passengers { get; set; }
                public decimal total_price { get; set; }
                public decimal total_tax { get; set; }
            }
        }
        public string airline { get; set; }
        public string arrival_airport_location_code { get; set; }
        public string arrival_airport_location_name { get; set; }
        public string arrival_date { get; set; }
        public string arrival_nice_day { get; set; }
        public string arrival_time { get; set; }
        public string available_seats { get; set; }
        public string cabin_class { get; set; }
        public string departure_airport_location_code { get; set; }
        public string departure_airport_location_name { get; set; }
        public string departure_date { get; set; }
        public string departure_nice_date { get; set; }
        public string departure_time { get; set; }
        public string flight_duration { get; set; }
        public string flight_number { get; set; }
        public string miles { get; set; }
        public decimal price { get; set; }
        public string stops { get; set; }
        public string tax { get; set; }
        public string ticket_class { get; set; }
        public string ticket_class_nicename { get; set; }
        public string untaxes_price { get; set; }
    }
}
