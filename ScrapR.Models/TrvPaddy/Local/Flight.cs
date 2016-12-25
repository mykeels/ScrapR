using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Models.TrvPaddy.Local
{
    public class Flight
    {
        public Trip departure_Trip { get; set; }
        public Trip.FareData.Fare fareData { get; set; }
        public Trip returnTrip { get; set; }
        
    }
}
