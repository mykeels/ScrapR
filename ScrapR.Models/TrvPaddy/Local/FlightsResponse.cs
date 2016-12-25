using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ScrapR.Models.TrvPaddy.Local
{
    public class FlightsResponse
    {
        public int departure_trips_count = 0;
        public int return_trips_count = 0;
        public string distance = "";
        public Dictionary<int, Trip> departure_trips = new Dictionary<int, Trip>();
        public Dictionary<int, Trip> return_trips = new Dictionary<int, Trip>();
        public Trip.FareData.Fare fareInfo = new Trip.FareData.Fare();

        public List<Trip> GetDepartureTrips()
        {
            return departure_trips.Values.ToList();
        }

        public List<Trip> GetReturnTrips()
        {
            return return_trips.Values.ToList();
        }

        public ParallelFlights GetParallelFlights()
        {
            ParallelFlights ret = new ParallelFlights();
            int i = 0;
            int x = 0;
            foreach (var deptTrip in this.GetDepartureTrips())
            {
                if (x + 1 > this.departure_trips_count) break;
                int y = 0;
                foreach (var retTrip in this.GetReturnTrips())
                {
                    if (y + 1 > this.return_trips_count) break;
                    ret.Add(new ParallelFlight()
                    {
                        flight_id = i,
                        departure_flight_number = deptTrip.flight_number,
                        departure_ticket_class = deptTrip.ticket_class,
                        return_flight_number = retTrip.flight_number,
                        return_ticket_class = retTrip.ticket_class
                    });
                    
                    i++;
                    y++;
                }
                x++;
            }
            return ret;
        }

        public List<Flight> GetFlights()
        {
            List<Flight> flights = new List<Flight>();
            var parallelFlights = this.GetParallelFlights();
            parallelFlights.ForEach((flight) =>
            {
                flights.Add(new Flight()
                {
                    departure_Trip = this.GetDepartureTrips().FirstOrDefault(trip => trip.flight_number.Equals(flight.departure_flight_number)),
                    returnTrip = this.GetReturnTrips().FirstOrDefault(trip => trip.flight_number.Equals(flight.return_flight_number))
                });
            });
            return flights;
        }

        public class ParallelFlight
        {
            public int flight_id;
            public string departure_flight_number;
            public string departure_ticket_class;
            public string return_flight_number;
            public string return_ticket_class;
        }

        public class ParallelFlights : List<ParallelFlight> { }
    }
}
